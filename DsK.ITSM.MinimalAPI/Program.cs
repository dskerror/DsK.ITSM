//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
//dotnet add package Microsoft.EntityFrameworkCore
//dotnet add package Microsoft.EntityFrameworkCore.SqlServer
//dotnet add package Microsoft.EntityFrameworkCore.Design
//dotnet tool install --global dotnet-ef
//dotnet ef migrations add InitialCreate
//dotnet ef database update
using DsK.ITSM.MinimalAPI;
using DsK.ITSM.MinimalAPI.Data;
using DsK.ITSM.MinimalAPI.Data.Entities;
using DsK.ITSM.MinimalAPI.Endpoints;
using DsK.ITSM.MinimalAPI.Services;
using DsK.ITSM.MinimalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuration
        //var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not configured.");

        // Bind JWT settings
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
            ?? throw new InvalidOperationException("JWT settings are not configured.");

        builder.Services.AddJwtAuthentication(jwtSettings.Key);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<AuthService>();        
        builder.Services.AddScoped<IWorkflowService, WorkflowService>();

        builder.Services.AddAuthorization();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("PublicAPI", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error"); // Optional custom endpoint
            app.UseHsts(); // Enforce HTTPS in production
        }


        // ?? Run migrations and seed data
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // Ensure database and schema are up to date
            context.Database.Migrate(); // Only needed if you're using migrations
            await DbSeeder.SeedAsync(context); // Seed data
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database seeding failed: {ex.Message}");
            throw; // or exit, depending on your startup strategy
        }

        app.UseCors("PublicAPI");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapAuthEndpoints();
        app.MapWorkflowEndpoints();
        app.MapGet("/health", () => Results.Ok("Healthy"));

        app.Run();

    }
}