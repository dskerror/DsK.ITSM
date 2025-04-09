namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class WorkflowAdditionalFieldDefinition
{
    public Guid Id { get; set; }
    public Guid WorkflowId { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public string DataType { get; set; } = "string";
    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }

    public Workflow Workflow { get; set; } = null!;
}