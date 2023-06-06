using System;
using System.Collections.Generic;

namespace DsK.ITSM.Models;

public partial class RequestType
{
    public int Id { get; set; }

    public string RequestTypeName { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; } = new List<Request>();
}
