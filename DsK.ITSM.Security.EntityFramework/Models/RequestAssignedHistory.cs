using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.EntityFramework.Models;

public partial class RequestAssignedHistory
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public DateTime AssignedDateTime { get; set; }

    public int? AssignedToUserId { get; set; }

    public virtual Request Request { get; set; } = null!;
}
