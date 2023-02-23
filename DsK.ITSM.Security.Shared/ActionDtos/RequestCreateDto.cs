using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DsK.ITSM.Security.Shared;

public partial class RequestCreateDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Summary { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string? Description { get; set; }

    public string System { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string RequestType { get; set; } = null!;

    public int RequestedByUserId { get; set; }
}
