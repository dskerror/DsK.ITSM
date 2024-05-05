using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class Priority
{
    public int Id { get; set; }

    public string PriorityName { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
