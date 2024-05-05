namespace DsK.ITSM.Shared.DTOs;

public partial class RequestMessageHistoryDto
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime MessageDateTime { get; set; }

    public int? UserId { get; set; }

    public virtual RequestDto Request { get; set; } = null!;

    public virtual UserDto? User { get; set; }
}
