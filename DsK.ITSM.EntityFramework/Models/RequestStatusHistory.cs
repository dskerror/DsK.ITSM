using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class RequestStatusHistory
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public DateTime TransactionDateTime { get; set; }

    public string Status { get; set; } = null!;

    public string ChangedByUsername { get; set; } = null!;

    public string ChangedByDisplayName { get; set; } = null!;

    public virtual Request Request { get; set; } = null!;
}
