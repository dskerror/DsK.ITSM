namespace DsK.ITSM.MinimalAPI.Data.Dtos;
public class WorkflowDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}