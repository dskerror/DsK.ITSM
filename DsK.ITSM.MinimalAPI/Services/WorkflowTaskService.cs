using DsK.ITSM.MinimalAPI.Data;
using DsK.ITSM.MinimalAPI.Data.Dtos;
using DsK.ITSM.MinimalAPI.Data.Entities;
using DsK.ITSM.MinimalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DsK.ITSM.MinimalAPI.Services;
public class WorkflowTaskService : IWorkflowTaskService
{
    private readonly AppDbContext _context;

    public WorkflowTaskService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<WorkflowTaskDto> CreateWorkflowTaskAsync(CreateTaskDto dto)
    {
        var task = new WorkflowTask
        {
            Id = Guid.NewGuid(),
            WorkflowId = dto.WorkflowId,
            Name = dto.Name,
            AssignedToUserId = dto.AssignedToUserId,
            CreatedAt = DateTime.UtcNow,
            CreatedByUserId = dto.CreatedByUserId,
            CurrentStatusId = dto.InitialStatusId
        };

        _context.WorkflowTasks.Add(task);

        var initialStatus = new WorkflowTaskStatusHistory
        {
            Id = Guid.NewGuid(),
            WorkflowTaskId = task.Id,
            StatusId = dto.InitialStatusId,
            CreatedAt = DateTime.UtcNow,
            ChangedByUserId = dto.CreatedByUserId
        };

        _context.WorkflowTaskStatusHistories.Add(initialStatus);

        await _context.SaveChangesAsync();

        return new WorkflowTaskDto
        {
            Id = task.Id,
            WorkflowId = task.WorkflowId,
            Name = task.Name,
            AssignedToUserId = task.AssignedToUserId,
            CurrentStatusId = task.CurrentStatusId,
            CreatedAt = task.CreatedAt,
            CreatedByUserId = task.CreatedByUserId
        };
    }

    public async Task<WorkflowTaskDto?> GetWorkflowTaskByIdAsync(Guid taskId)
    {
        var task = await _context.WorkflowTasks.FindAsync(taskId);
        if (task == null) return null;

        return new WorkflowTaskDto
        {
            Id = task.Id,
            WorkflowId = task.WorkflowId,
            Name = task.Name,
            AssignedToUserId = task.AssignedToUserId,
            CurrentStatusId = task.CurrentStatusId,
            CreatedAt = task.CreatedAt,
            CreatedByUserId = task.CreatedByUserId
        };
    }

    public async Task UpdateWorkflowTaskStatusAsync(Guid taskId, Guid newStatusId, Guid changedByUserId)
    {
        var task = await _context.WorkflowTasks.FindAsync(taskId);
        if (task == null)
            throw new InvalidOperationException("Task not found");

        task.CurrentStatusId = newStatusId;

        var history = new WorkflowTaskStatusHistory
        {
            Id = Guid.NewGuid(),
            WorkflowTaskId = task.Id,
            StatusId = newStatusId,
            CreatedAt = DateTime.UtcNow,
            ChangedByUserId = changedByUserId
        };

        _context.WorkflowTaskStatusHistories.Add(history);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateWorkflowTaskFieldAsync(Guid taskId, string fieldName, string value, Guid changedByUserId)
    {
        var existingField = await _context.WorkflowTaskAdditionalFields
            .FirstOrDefaultAsync(f => f.WorkflowTaskId == taskId && f.FieldName == fieldName);

        if (existingField != null)
        {
            existingField.Value = value;
        }
        else
        {
            var newField = new WorkflowTaskAdditionalField
            {
                Id = Guid.NewGuid(),
                WorkflowTaskId = taskId,
                FieldName = fieldName,
                Value = value,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = changedByUserId
            };

            _context.WorkflowTaskAdditionalFields.Add(newField);
        }

        await _context.SaveChangesAsync();
    }
}