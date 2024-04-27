namespace DsK.ITSM.Shared.DTOs.Requests;

public class RequestDto
{
    public int Id { get; set; }

    public string Summary { get; set; } = null!;

    public string? Description { get; set; }

    public string System { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string RequestedByUsername { get; set; } = null!;

    public string RequestedByEmail { get; set; } = null!;

    public string RequestedByDisplayName { get; set; } = null!;

    public DateTime RequestDateTime { get; set; }

    public string Category { get; set; } = null!;

    public string RequestType { get; set; } = null!;
}