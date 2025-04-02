namespace DsK.ITSM.MinimalAPI.Models;

public class UserEntity
{
    public int Id { get; set; } // Database primary key
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public List<RefreshTokenEntity> RefreshTokens { get; set; } = new();
}