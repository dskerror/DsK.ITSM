using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class Request
{
    public int Id { get; set; }

    public string Summary { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime RequestDateTime { get; set; }

    public int UserId { get; set; }

    public int ItsystemId { get; set; }

    public int PriorityId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Itsystem Itsystem { get; set; } = null!;

    public virtual Priority Priority { get; set; } = null!;

    public virtual ICollection<RequestAssignedHistory> RequestAssignedHistories { get; set; } = new List<RequestAssignedHistory>();

    public virtual ICollection<RequestMessageHistory> RequestMessageHistories { get; set; } = new List<RequestMessageHistory>();

    public virtual ICollection<RequestStatusHistory> RequestStatusHistories { get; set; } = new List<RequestStatusHistory>();

    public virtual User User { get; set; } = null!;
}
