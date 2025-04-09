using DsK.ITSM.MinimalAPI.Data;
using DsK.ITSM.MinimalAPI.Data.Dtos;
using DsK.ITSM.MinimalAPI.Data.Entities;
using DsK.ITSM.MinimalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DsK.ITSM.MinimalAPI.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly AppDbContext _context;

        public WorkflowService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WorkflowDto> CreateWorkflowAsync(CreateWorkflowDto dto)
        {
            var workflow = new Workflow
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = dto.CreatedByUserId
            };

            _context.Workflows.Add(workflow);
            await _context.SaveChangesAsync();

            return new WorkflowDto
            {
                Id = workflow.Id,
                Name = workflow.Name,
                CreatedAt = workflow.CreatedAt,
                CreatedByUserId = workflow.CreatedByUserId
            };
        }

        public async Task<IEnumerable<WorkflowDto>> GetAllWorkflowsAsync()
        {
            return await _context.Workflows
                .Select(w => new WorkflowDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    CreatedAt = w.CreatedAt,
                    CreatedByUserId = w.CreatedByUserId
                }).ToListAsync();
        }

        public async Task<WorkflowDto?> GetWorkflowByIdAsync(Guid id)
        {
            var w = await _context.Workflows.FindAsync(id);
            if (w == null) return null;

            return new WorkflowDto
            {
                Id = w.Id,
                Name = w.Name,
                CreatedAt = w.CreatedAt,
                CreatedByUserId = w.CreatedByUserId
            };
        }

        public async Task AddStatusAsync(Guid workflowId, string statusName, Guid createdByUserId)
        {
            var status = new WorkflowStatusDefinition
            {
                Id = Guid.NewGuid(),
                WorkflowId = workflowId,
                Name = statusName,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = createdByUserId
            };

            _context.WorkflowStatusDefinitions.Add(status);
            await _context.SaveChangesAsync();
        }

        public async Task AddStatusTransitionAsync(Guid workflowId, Guid fromStatusId, Guid toStatusId, Guid createdByUserId)
        {
            var transition = new WorkflowStatusTransition
            {
                Id = Guid.NewGuid(),
                WorkflowId = workflowId,
                FromStatusId = fromStatusId,
                ToStatusId = toStatusId,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = createdByUserId
            };

            _context.WorkflowStatusTransitions.Add(transition);
            await _context.SaveChangesAsync();
        }

        public async Task AddFieldDefinitionAsync(Guid workflowId, string fieldName, string dataType, Guid createdByUserId)
        {
            var field = new WorkflowAdditionalFieldDefinition
            {
                Id = Guid.NewGuid(),
                WorkflowId = workflowId,
                FieldName = fieldName,
                DataType = dataType,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = createdByUserId
            };

            _context.WorkflowAdditionalFieldDefinitions.Add(field);
            await _context.SaveChangesAsync();
        }

        public async Task AddAssignableUserAsync(Guid workflowId, Guid userId, Guid createdByUserId)
        {
            var assign = new WorkflowAssignedTo
            {
                Id = Guid.NewGuid(),
                WorkflowId = workflowId,
                AssignedToUserId = userId,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = createdByUserId
            };

            _context.WorkflowAssignedTos.Add(assign);
            await _context.SaveChangesAsync();
        }

        public async Task EnableRoundRobinAssignmentAsync(Guid workflowAssignedToId, Guid createdByUserId)
        {
            var auto = new WorkflowAssignedToAutomatic
            {
                Id = Guid.NewGuid(),
                WorkflowAssignedToId = workflowAssignedToId,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = createdByUserId
            };

            _context.WorkflowAssignedToAutomatics.Add(auto);
            await _context.SaveChangesAsync();
        }
    }
}