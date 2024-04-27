using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class Request
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

    public virtual ICollection<RequestAssignedHistory> RequestAssignedHistories { get; set; } = new List<RequestAssignedHistory>();

    public virtual ICollection<RequestMessageHistory> RequestMessageHistories { get; set; } = new List<RequestMessageHistory>();

    public virtual ICollection<RequestStatusHistory> RequestStatusHistories { get; set; } = new List<RequestStatusHistory>();
}
