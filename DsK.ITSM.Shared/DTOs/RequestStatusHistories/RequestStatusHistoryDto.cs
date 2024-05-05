namespace DsK.ITSM.Shared.DTOs;

public partial class RequestStatusHistoryDto
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public DateTime TransactionDateTime { get; set; }

    public int? UserId { get; set; }

    public int? StatusId { get; set; }

    public virtual RequestDto Request { get; set; } = null!;

    public virtual StatusDto? Status { get; set; }

    public virtual UserDto? User { get; set; }
}
