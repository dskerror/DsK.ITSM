namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class WorkflowAssignedTo
{
    public Guid Id { get; set; }
    public Guid WorkflowId { get; set; }
    public Guid AssignedToUserId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Workflow Workflow { get; set; } = null!;
}