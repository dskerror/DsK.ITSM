namespace DsK.ITSM.Shared.DTOs.Requests;

public class RequestCreateDto
{
    public string Summary { get; set; } = null!;

    public string? Description { get; set; }

    public string System { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string RequestType { get; set; } = null!;
}