namespace DsK.ITSM.MinimalAPI.Data.Dtos;
public class AddStatusTransitionDto
{
    public Guid FromStatusId { get; set; }
    public Guid ToStatusId { get; set; }
    public Guid CreatedByUserId { get; set; }
}