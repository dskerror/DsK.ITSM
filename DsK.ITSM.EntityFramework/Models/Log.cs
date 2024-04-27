using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class Log
{
    public int Id { get; set; }

    public DateTime? DateTimeStamp { get; set; }

    public string? Username { get; set; }

    public string? Ipv4address { get; set; }

    public string? Url { get; set; }
}
