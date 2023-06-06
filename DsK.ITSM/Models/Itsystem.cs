using System;
using System.Collections.Generic;

namespace DsK.ITSM.Models;

public partial class Itsystem
{
    public int Id { get; set; }

    public string SystemName { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; } = new List<Request>();

    public virtual ICollection<Sop1system> Sop1systems { get; } = new List<Sop1system>();
}
