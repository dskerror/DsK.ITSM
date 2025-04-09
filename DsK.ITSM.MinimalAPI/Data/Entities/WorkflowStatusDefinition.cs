namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class WorkflowStatusDefinition
{
    public Guid Id { get; set; }
    public Guid WorkflowId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Workflow Workflow { get; set; } = null!;
}