using DsK.ITSM.MinimalAPI.Data.Dtos;

namespace DsK.ITSM.MinimalAPI.Services.Interfaces;

public interface IWorkflowService
{
    Task<WorkflowDto> CreateWorkflowAsync(CreateWorkflowDto dto);
    Task<IEnumerable<WorkflowDto>> GetAllWorkflowsAsync();
    Task<WorkflowDto?> GetWorkflowByIdAsync(Guid id);
    Task AddStatusAsync(Guid workflowId, string statusName, Guid createdByUserId);
    Task AddStatusTransitionAsync(Guid workflowId, Guid fromStatusId, Guid toStatusId, Guid createdByUserId);
    Task AddFieldDefinitionAsync(Guid workflowId, string fieldName, string dataType, Guid createdByUserId);
    Task AddAssignableUserAsync(Guid workflowId, Guid userId, Guid createdByUserId);
    Task EnableRoundRobinAssignmentAsync(Guid workflowAssignedToId, Guid createdByUserId);
}
