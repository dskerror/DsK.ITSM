namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class WorkflowTask
{
    public Guid Id { get; set; }
    public Guid WorkflowId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? AssignedToUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Guid CurrentStatusId { get; set; }

    public Workflow Workflow { get; set; } = null!;
    public User? AssignedToUser { get; set; }
    public ICollection<WorkflowTaskStatusHistory> StatusHistory { get; set; } = new List<WorkflowTaskStatusHistory>();
    public ICollection<WorkflowTaskAdditionalField> AdditionalFields { get; set; } = new List<WorkflowTaskAdditionalField>();
}