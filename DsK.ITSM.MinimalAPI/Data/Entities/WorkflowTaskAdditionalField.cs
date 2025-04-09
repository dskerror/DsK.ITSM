namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class WorkflowTaskAdditionalField
{
    public Guid Id { get; set; }
    public Guid WorkflowTaskId { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public WorkflowTask WorkflowTask { get; set; } = null!;
}