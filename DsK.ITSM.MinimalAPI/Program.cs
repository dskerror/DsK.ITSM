//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
//dotnet add package Microsoft.EntityFrameworkCore
//dotnet add package Microsoft.EntityFrameworkCore.SqlServer
//dotnet add package Microsoft.EntityFrameworkCore.Design
//dotnet tool install --global dotnet-ef
//dotnet ef migrations add InitialCreate
//dotnet ef database update
using DsK.ITSM.MinimalAPI;
using DsK.ITSM.MinimalAPI.Data;
using DsK.ITSM.MinimalAPI.Endpoints;
using DsK.ITSM.MinimalAPI.Services;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuration
        var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not configured.");

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<KeyValueService>();

        builder.Services.AddJwtAuthentication(jwtKey);

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapAuthEndpoints();
        app.MapKeyValueEndpoints();

        app.Run();

    }
}