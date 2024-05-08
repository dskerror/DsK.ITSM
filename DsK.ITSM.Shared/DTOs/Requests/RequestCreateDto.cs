namespace DsK.ITSM.Shared.DTOs;

public partial class RequestCreateDto
{
    public string Summary { get; set; } = null!;

    public string? Description { get; set; }

    public int ItsystemId { get; set; }

    public int PriorityId { get; set; }

    public int CategoryId { get; set; }
}
