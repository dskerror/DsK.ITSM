using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class RequestStatusHistory
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public DateTime TransactionDateTime { get; set; }

    public int? UserId { get; set; }

    public int? StatusId { get; set; }

    public virtual Request Request { get; set; } = null!;

    public virtual Status? Status { get; set; }

    public virtual User? User { get; set; }
}
