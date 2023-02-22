using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.Shared;

public partial class RequestDto
{
    public int Id { get; set; }

    public string Summary { get; set; } = null!;

    public string? Description { get; set; }

    public string System { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public DateTime RequestDateTime { get; set; }

    public string Category { get; set; } = null!;

    public string RequestType { get; set; } = null!;

    public int RequestedByUserId { get; set; }

    public virtual ICollection<RequestAssignedHistoryDto> RequestAssignedHistories { get; } = new List<RequestAssignedHistoryDto>();

    public virtual ICollection<RequestMessageHistoryDto> RequestMessageHistories { get; } = new List<RequestMessageHistoryDto>();

    public virtual ICollection<RequestStatusHistoryDto> RequestStatusHistories { get; } = new List<RequestStatusHistoryDto>();

    public virtual UserDto RequestedByUser { get; set; } = null!;
}
