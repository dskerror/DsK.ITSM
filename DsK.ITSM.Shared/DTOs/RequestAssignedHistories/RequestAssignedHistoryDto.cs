namespace DsK.ITSM.Shared.DTOs;

public partial class RequestAssignedHistoryDto
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public DateTime AssignedDateTime { get; set; }

    public int? UserId { get; set; }

    public virtual RequestDto Request { get; set; } = null!;

    public virtual UserDto? User { get; set; }
}
