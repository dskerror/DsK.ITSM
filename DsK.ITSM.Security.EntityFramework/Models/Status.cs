using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.EntityFramework.Models;

public partial class Status
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;
}
