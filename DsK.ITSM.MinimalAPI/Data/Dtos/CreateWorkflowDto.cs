namespace DsK.ITSM.MinimalAPI.Data.Dtos;
public class CreateWorkflowDto
{
    public string Name { get; set; } = string.Empty;
    public Guid CreatedByUserId { get; set; }
}