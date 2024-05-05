using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class RequestMessageHistory
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime MessageDateTime { get; set; }

    public int? UserId { get; set; }

    public virtual Request Request { get; set; } = null!;

    public virtual User? User { get; set; }
}
