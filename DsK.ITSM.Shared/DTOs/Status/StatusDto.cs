namespace DsK.ITSM.Shared.DTOs;

public partial class StatusDto
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<RequestStatusHistoryDto> RequestStatusHistories { get; set; } = new List<RequestStatusHistoryDto>();
}
