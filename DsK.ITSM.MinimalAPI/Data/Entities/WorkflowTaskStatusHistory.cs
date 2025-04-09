namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class WorkflowTaskStatusHistory
{
    public Guid Id { get; set; }
    public Guid WorkflowTaskId { get; set; }
    public Guid StatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid ChangedByUserId { get; set; }

    public WorkflowTask WorkflowTask { get; set; } = null!;
}