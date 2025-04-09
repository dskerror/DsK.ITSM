namespace DsK.ITSM.MinimalAPI.Data.Dtos;
public class AddStatusDto
{
    public string Name { get; set; } = string.Empty;
    public Guid CreatedByUserId { get; set; }
}