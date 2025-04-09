namespace DsK.ITSM.MinimalAPI.Data.Dtos;
public class WorkflowTaskDto
{
    public Guid Id { get; set; }
    public Guid WorkflowId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? AssignedToUserId { get; set; }
    public Guid CurrentStatusId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}