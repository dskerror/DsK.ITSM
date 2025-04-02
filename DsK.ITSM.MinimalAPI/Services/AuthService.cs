using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DsK.ITSM.MinimalAPI.Data;
using DsK.ITSM.MinimalAPI.Models;

namespace DsK.ITSM.MinimalAPI.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<(bool Success, string Message)> RegisterAsync(string username, string password)
    {
        if (await _db.Users.AnyAsync(u => u.Username == username))
            return (false, "Username already exists.");

        var user = new UserEntity
        {
            Username = username,
            PasswordHash = HashPassword(password),
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return (true, "User registered successfully.");
    }

    public async Task<(string? AccessToken, string? RefreshToken)> LoginAsync(string username, string password)
    {
        var user = await _db.Users.Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null || !VerifyPassword(password, user.PasswordHash))
            return (null, null);

        var accessToken = GenerateJwt(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshTokens.Add(new RefreshTokenEntity
        {
            Token = refreshToken,
            Expiry = DateTime.UtcNow.AddDays(7),
            User = user
        });

        await _db.SaveChangesAsync();

        return (accessToken, refreshToken);
    }

    public async Task<string?> RefreshAsync(string username, string refreshToken)
    {
        var user = await _db.Users.Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
            return null;

        var storedToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);

        if (storedToken == null || storedToken.Expiry < DateTime.UtcNow)
            return null;

        return GenerateJwt(user);
    }

    public async Task<bool> LogoutAsync(string username, string refreshToken)
    {
        var user = await _db.Users.Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null) return false;

        var tokenToRemove = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);
        if (tokenToRemove == null) return false;

        user.RefreshTokens.Remove(tokenToRemove);
        await _db.SaveChangesAsync();

        return true;
    }


    private string GenerateJwt(UserEntity user)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations: 100000,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: 32);

        return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hash);
    }

    private static bool VerifyPassword(string input, string storedHash)
    {
        var parts = storedHash.Split('.');
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] expectedHash = Convert.FromBase64String(parts[1]);

        byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            iterations: 100000,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: 32);

        return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
