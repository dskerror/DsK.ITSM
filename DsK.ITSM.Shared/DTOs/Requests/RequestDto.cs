namespace DsK.ITSM.Shared.DTOs;

public partial class RequestDto
{
    public int Id { get; set; }

    public string Summary { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime RequestDateTime { get; set; }

    public int UserId { get; set; }

    public int ItsystemId { get; set; }

    public int PriorityId { get; set; }

    public int CategoryId { get; set; }

    public virtual CategoryDto Category { get; set; } = null!;

    public virtual ItsystemDto Itsystem { get; set; } = null!;

    public virtual PriorityDto Priority { get; set; } = null!;

    public virtual ICollection<RequestAssignedHistoryDto> RequestAssignedHistories { get; set; } = new List<RequestAssignedHistoryDto>();

    public virtual ICollection<RequestMessageHistoryDto> RequestMessageHistories { get; set; } = new List<RequestMessageHistoryDto>();

    public virtual ICollection<RequestStatusHistoryDto> RequestStatusHistories { get; set; } = new List<RequestStatusHistoryDto>();

    public virtual UserDto User { get; set; } = null!;
}
