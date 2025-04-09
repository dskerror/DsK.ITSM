using DsK.ITSM.MinimalAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace DsK.ITSM.MinimalAPI.Data;
public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return; // Prevent reseeding

        var now = DateTime.UtcNow;

        // Seed Users
        var adminguid = Guid.NewGuid();
        var users = new List<User>
        {
            new() { Id = adminguid, Username = "admin", Email = "admin@example.com", CreatedAt = now, CreatedByUserId = adminguid },
            new() { Id = Guid.NewGuid(), Username = "user2", Email = "user2@example.com", CreatedAt = now, CreatedByUserId = adminguid },
            new() { Id = Guid.NewGuid(), Username = "user3", Email = "user3@example.com", CreatedAt = now, CreatedByUserId = adminguid },
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();

        // Create Workflow
        var workflow = new Workflow
        {
            Id = Guid.NewGuid(),
            Name = "IT Service Management",
            CreatedAt = now,
            CreatedByUserId = users[0].Id
        };

        context.Workflows.Add(workflow);

        // Statuses
        var openStatus = new WorkflowStatusDefinition
        {
            Id = Guid.NewGuid(),
            WorkflowId = workflow.Id,
            Name = "Open",
            CreatedAt = now,
            CreatedByUserId = users[0].Id
        };
        var assignedStatus = new WorkflowStatusDefinition
        {
            Id = Guid.NewGuid(),
            WorkflowId = workflow.Id,
            Name = "Assigned",
            CreatedAt = now,
            CreatedByUserId = users[0].Id
        };
        var awaitingUserResponse = new WorkflowStatusDefinition
        {
            Id = Guid.NewGuid(),
            WorkflowId = workflow.Id,
            Name = "Awaiting User Response",
            CreatedAt = now,
            CreatedByUserId = users[0].Id
        };
        var closedStatus = new WorkflowStatusDefinition
        {
            Id = Guid.NewGuid(),
            WorkflowId = workflow.Id,
            Name = "Closed",
            CreatedAt = now,
            CreatedByUserId = users[0].Id
        };

        context.WorkflowStatusDefinitions.AddRange(openStatus, assignedStatus, awaitingUserResponse, closedStatus);

        // Status transitions
        var transitions = new[]
        {
            new WorkflowStatusTransition
            {
                Id = Guid.NewGuid(), WorkflowId = workflow.Id, FromStatusId = openStatus.Id, ToStatusId = assignedStatus.Id, CreatedAt = now, CreatedByUserId = users[0].Id
            },
            new WorkflowStatusTransition
            {
                Id = Guid.NewGuid(), WorkflowId = workflow.Id, FromStatusId = assignedStatus.Id, ToStatusId = awaitingUserResponse.Id, CreatedAt = now, CreatedByUserId = users[0].Id
            },
            new WorkflowStatusTransition
            {
                Id = Guid.NewGuid(), WorkflowId = workflow.Id, FromStatusId = awaitingUserResponse.Id, ToStatusId = closedStatus.Id, CreatedAt = now, CreatedByUserId = users[0].Id
            }
        };
        context.WorkflowStatusTransitions.AddRange(transitions);

        // Additional fields
        context.WorkflowAdditionalFieldDefinitions.AddRange(
            new WorkflowAdditionalFieldDefinition
            {
                Id = Guid.NewGuid(),
                WorkflowId = workflow.Id,
                FieldName = "Priority",
                DataType = "string",
                CreatedAt = now,
                CreatedByUserId = users[0].Id
            },
            new WorkflowAdditionalFieldDefinition
            {
                Id = Guid.NewGuid(),
                WorkflowId = workflow.Id,
                FieldName = "Due Date",
                DataType = "DateTime",
                CreatedAt = now,
                CreatedByUserId = users[0].Id
            },
            new WorkflowAdditionalFieldDefinition
            {
                Id = Guid.NewGuid(),
                WorkflowId = workflow.Id,
                FieldName = "Assigned To",
                DataType = "string",
                CreatedAt = now,
                CreatedByUserId = users[0].Id
            }
        );

        // Assigned users
        foreach (var u in users)
        {
            context.WorkflowAssignedTos.Add(new WorkflowAssignedTo
            {
                Id = Guid.NewGuid(),
                WorkflowId = workflow.Id,
                AssignedToUserId = u.Id,
                CreatedByUserId = users[0].Id,
                CreatedAt = now
            });
        }

        await context.SaveChangesAsync();

        // Create a Task
        var task = new WorkflowTask
        {
            Id = Guid.NewGuid(),
            WorkflowId = workflow.Id,
            Name = "Problems with computer",
            AssignedToUserId = users[0].Id,
            CreatedAt = now,
            CreatedByUserId = users[0].Id,
            CurrentStatusId = openStatus.Id
        };

        context.WorkflowTasks.Add(task);

        // Status history
        var history = new[]
        {
            new WorkflowTaskStatusHistory { Id = Guid.NewGuid(), WorkflowTaskId = task.Id, StatusId = openStatus.Id, CreatedAt = now, ChangedByUserId = users[0].Id },
            new WorkflowTaskStatusHistory { Id = Guid.NewGuid(), WorkflowTaskId = task.Id, StatusId = assignedStatus.Id, CreatedAt = now.AddMinutes(5), ChangedByUserId = users[0].Id },
            new WorkflowTaskStatusHistory { Id = Guid.NewGuid(), WorkflowTaskId = task.Id, StatusId = awaitingUserResponse.Id, CreatedAt = now.AddMinutes(10), ChangedByUserId = users[0].Id },
            new WorkflowTaskStatusHistory { Id = Guid.NewGuid(), WorkflowTaskId = task.Id, StatusId = closedStatus.Id, CreatedAt = now.AddMinutes(15), ChangedByUserId = users[0].Id },
        };
        context.WorkflowTaskStatusHistories.AddRange(history);

        // Additional fields for task
        context.WorkflowTaskAdditionalFields.AddRange(
            new WorkflowTaskAdditionalField
            {
                Id = Guid.NewGuid(),
                WorkflowTaskId = task.Id,
                FieldName = "Priority",
                Value = "High",
                CreatedAt = now,
                CreatedByUserId = users[0].Id
            },
            new WorkflowTaskAdditionalField
            {
                Id = Guid.NewGuid(),
                WorkflowTaskId = task.Id,
                FieldName = "Due Date",
                Value = "2023-10-01",
                CreatedAt = now,
                CreatedByUserId = users[0].Id
            },
            new WorkflowTaskAdditionalField
            {
                Id = Guid.NewGuid(),
                WorkflowTaskId = task.Id,
                FieldName = "Assigned To",
                Value = "user1",
                CreatedAt = now,
                CreatedByUserId = users[0].Id
            }
        );

        await context.SaveChangesAsync();
    }
}