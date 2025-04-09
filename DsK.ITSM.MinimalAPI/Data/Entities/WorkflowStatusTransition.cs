namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class WorkflowStatusTransition
{
    public Guid Id { get; set; }
    public Guid WorkflowId { get; set; }
    public Guid FromStatusId { get; set; }
    public Guid ToStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Workflow Workflow { get; set; } = null!;
}