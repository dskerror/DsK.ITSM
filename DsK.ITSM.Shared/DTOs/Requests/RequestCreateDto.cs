namespace DsK.ITSM.Shared.DTOs;

public partial class RequestCreateDto
{
    public int Id { get; set; }

    public string Summary { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime RequestDateTime { get; set; }    

    public int UserId { get; set; }

    public int ItsystemId { get; set; }

    public int PriorityId { get; set; }

    public int CategoryId { get; set; }
}
