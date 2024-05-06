namespace DsK.ITSM.Shared.DTOs;

public class RequestGridDto
{
    public int Id { get; set; }
    public string Summary { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime RequestDateTime { get; set; }
    public string Username { get; set; } = null!;
    public string SystemName { get; set; } = null!;
    public string PriorityName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string StatusName { get; set; } = null!;
}
