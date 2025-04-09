using DsK.ITSM.MinimalAPI.Data.Dtos;
using DsK.ITSM.MinimalAPI.Services.Interfaces;
public static class AuthEndpoints
{
    public static void MapWorkflowEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/workflows", async (
            CreateWorkflowDto dto,
            IWorkflowService service) =>
        {
            var result = await service.CreateWorkflowAsync(dto);
            return Results.Ok(result);
        });

        app.MapGet("/workflows", async (IWorkflowService service) =>
        {
            var result = await service.GetAllWorkflowsAsync();
            return Results.Ok(result);
        });

        app.MapGet("/workflows/{id:guid}", async (
            Guid id,
            IWorkflowService service) =>
        {
            var result = await service.GetWorkflowByIdAsync(id);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        });

        app.MapPost("/workflows/{id:guid}/status", async (
            Guid id,
            AddStatusDto dto,
            IWorkflowService service) =>
        {
            await service.AddStatusAsync(id, dto.Name, dto.CreatedByUserId);
            return Results.Ok();
        });

        app.MapPost("/workflows/{id:guid}/status-transition", async (
            Guid id,
            AddStatusTransitionDto dto,
            IWorkflowService service) =>
        {
            await service.AddStatusTransitionAsync(id, dto.FromStatusId, dto.ToStatusId, dto.CreatedByUserId);
            return Results.Ok();
        });

        app.MapPost("/workflows/{id:guid}/fields", async (
            Guid id,
            AddFieldDefinitionDto dto,
            IWorkflowService service) =>
        {
            await service.AddFieldDefinitionAsync(id, dto.FieldName, dto.DataType, dto.CreatedByUserId);
            return Results.Ok();
        });

        app.MapPost("/workflows/{id:guid}/assignable-users", async (
            Guid id,
            AssignUserDto dto,
            IWorkflowService service) =>
        {
            await service.AddAssignableUserAsync(id, dto.UserId, dto.CreatedByUserId);
            return Results.Ok();
        });
    }
}