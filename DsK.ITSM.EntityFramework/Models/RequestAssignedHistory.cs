using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class RequestAssignedHistory
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public DateTime AssignedDateTime { get; set; }

    public int? UserId { get; set; }

    public virtual Request Request { get; set; } = null!;

    public virtual User? User { get; set; }
}
