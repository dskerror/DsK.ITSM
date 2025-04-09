namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class WorkflowAssignedToAutomatic
{
    public Guid Id { get; set; }
    public Guid WorkflowAssignedToId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public WorkflowAssignedTo WorkflowAssignedTo { get; set; } = null!;
}