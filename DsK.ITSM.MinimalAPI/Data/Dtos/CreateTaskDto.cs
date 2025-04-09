namespace DsK.ITSM.MinimalAPI.Data.Dtos;
public class CreateTaskDto
{
    public Guid WorkflowId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? AssignedToUserId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Guid InitialStatusId { get; set; }
}