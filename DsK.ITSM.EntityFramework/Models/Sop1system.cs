using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class Sop1system
{
    public int Id { get; set; }

    public int Sop1id { get; set; }

    public string System { get; set; } = null!;
}
