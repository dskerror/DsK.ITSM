using System.ComponentModel.DataAnnotations;

namespace DsK.ITSM.Dto;

public partial class RequestCreateDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Summary { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string? Description { get; set; }

    [Required]
    [StringLength(256, MinimumLength = 3)]
    public string RequestedBy { get; set; } = null!;
    public int RequestedByUserId { get; set; }

    public int? ItsystemId { get; set; }

    public int PriorityId { get; set; }

    public int CategoryId { get; set; }

    public int RequestTypeId { get; set; }
}
