using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<RequestAssignedHistory> RequestAssignedHistories { get; set; } = new List<RequestAssignedHistory>();

    public virtual ICollection<RequestMessageHistory> RequestMessageHistories { get; set; } = new List<RequestMessageHistory>();

    public virtual ICollection<RequestStatusHistory> RequestStatusHistories { get; set; } = new List<RequestStatusHistory>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
