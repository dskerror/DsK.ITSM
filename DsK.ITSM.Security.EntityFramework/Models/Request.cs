using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.EntityFramework.Models;

public partial class Request
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

    public virtual Category Category { get; set; } = null!;

    public virtual Itsystem? Itsystem { get; set; }

    public virtual Priority Priority { get; set; } = null!;

    public virtual ICollection<RequestAssignedHistory> RequestAssignedHistories { get; } = new List<RequestAssignedHistory>();

    public virtual ICollection<RequestMessageHistory> RequestMessageHistories { get; } = new List<RequestMessageHistory>();

    public virtual ICollection<RequestStatusHistory> RequestStatusHistories { get; } = new List<RequestStatusHistory>();

    public virtual RequestType RequestType { get; set; } = null!;

    public virtual User RequestedByUser { get; set; } = null!;
}
