using System;
using System.Collections.Generic;

namespace DsK.ITSM.Dto;

public partial class RequestDto
{
    public int Id { get; set; }

    public string Summary { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime RequestDateTime { get; set; }

    public int RequestedByUserId { get; set; }

    public int? ItsystemId { get; set; }

    public int PriorityId { get; set; }

    public int CategoryId { get; set; }

    public int RequestTypeId { get; set; }

    public virtual CategoryDto Category { get; set; } = null!;

    public virtual ItsystemDto? Itsystem { get; set; }

    public virtual PriorityDto Priority { get; set; } = null!;

    public virtual ICollection<RequestAssignedHistoryDto> RequestAssignedHistories { get; } = new List<RequestAssignedHistoryDto>();

    public virtual ICollection<RequestMessageHistoryDto> RequestMessageHistories { get; } = new List<RequestMessageHistoryDto>();

    public virtual ICollection<RequestStatusHistoryDto> RequestStatusHistories { get; } = new List<RequestStatusHistoryDto>();

    public virtual RequestTypeDto RequestType { get; set; } = null!;

    public virtual UserDto RequestedByUser { get; set; } = null!;
}
