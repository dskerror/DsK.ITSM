using DsK.ITSM.MinimalAPI.Models;
using DsK.ITSM.MinimalAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace DsK.ITSM.MinimalAPI.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (AuthRequest request, AuthService authService) =>
        {
            var result = await authService.RegisterAsync(request.Username, request.Password);
            return result.Success ? Results.Ok(result.Message) : Results.BadRequest(result.Message);
        });

        app.MapPost("/login", async (AuthRequest request, AuthService authService) =>
        {
            var (accessToken, refreshToken) = await authService.LoginAsync(request.Username, request.Password);
            return accessToken is null
                ? Results.Unauthorized()
                : Results.Ok(new { accessToken, refreshToken });
        });

        app.MapPost("/refresh-token", async (RefreshRequest request, AuthService authService) =>
        {
            var newToken = await authService.RefreshAsync(request.Username, request.RefreshToken);
            return newToken is null ? Results.Unauthorized() : Results.Ok(new { accessToken = newToken });
        });

        app.MapPost("/logout", async (RefreshRequest request, AuthService authService) =>
        {
            await authService.LogoutAsync(request.Username, request.RefreshToken);
            return Results.Ok("Logged out.");
        });

        app.MapGet("/auth-test", [Authorize] () =>
        {
            return Results.Ok("You are authenticated.");
        });
    }
}