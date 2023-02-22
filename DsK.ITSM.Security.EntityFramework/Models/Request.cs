using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.EntityFramework.Models;

public partial class Request
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

    public virtual ICollection<RequestAssignedHistory> RequestAssignedHistories { get; } = new List<RequestAssignedHistory>();

    public virtual ICollection<RequestMessageHistory> RequestMessageHistories { get; } = new List<RequestMessageHistory>();

    public virtual ICollection<RequestStatusHistory> RequestStatusHistories { get; } = new List<RequestStatusHistory>();

    public virtual User RequestedByUser { get; set; } = null!;
}
