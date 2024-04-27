using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class RequestAssignedHistory
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public string AssignedTo { get; set; } = null!;

    public DateTime AssignedDateTime { get; set; }

    public virtual Request Request { get; set; } = null!;
}
