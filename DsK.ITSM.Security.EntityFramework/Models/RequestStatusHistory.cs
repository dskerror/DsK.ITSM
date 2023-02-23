using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.EntityFramework.Models;

public partial class RequestStatusHistory
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public DateTime TransactionDateTime { get; set; }

    public string Status { get; set; } = null!;

    public int? ChangedByUsernameUserId { get; set; }

    public virtual User? ChangedByUsernameUser { get; set; }

    public virtual Request Request { get; set; } = null!;
}
