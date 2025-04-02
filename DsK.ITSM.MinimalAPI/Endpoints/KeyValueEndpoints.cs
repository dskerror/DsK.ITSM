using DsK.ITSM.MinimalAPI.Models;
using DsK.ITSM.MinimalAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace DsK.ITSM.MinimalAPI.Endpoints;

public static class KeyValueEndpoints
{
    public static void MapKeyValueEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/kv", async (KeyValueService service) =>
            Results.Ok(await service.GetAllAsync()));

        app.MapGet("/kv/{key}", async (string key, KeyValueService service) =>
            await service.GetByKeyAsync(key) is { } kv
                ? Results.Ok(kv)
                : Results.NotFound());

        app.MapPost("/kv", async (KeyValueEntity kv, KeyValueService service) =>
            Results.Ok(await service.SetAsync(kv.Key, kv.Value)));

        app.MapDelete("/kv/{key}", async (string key, KeyValueService service) =>
            await service.DeleteAsync(key)
                ? Results.Ok("Deleted")
                : Results.NotFound());
    }
}