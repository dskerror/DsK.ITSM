using DsK.ITSM.MinimalAPI.Data.Dtos;

namespace DsK.ITSM.MinimalAPI.Services.Interfaces;
public interface IWorkflowTaskService
{
    Task<WorkflowTaskDto> CreateWorkflowTaskAsync(CreateTaskDto dto);
    Task<WorkflowTaskDto?> GetWorkflowTaskByIdAsync(Guid taskId);
    Task UpdateWorkflowTaskStatusAsync(Guid taskId, Guid newStatusId, Guid changedByUserId);
    Task UpdateWorkflowTaskFieldAsync(Guid taskId, string fieldName, string value, Guid changedByUserId);
}