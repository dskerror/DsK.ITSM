﻿using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.EntityFramework.Models;

public partial class Sop1system
{
    public int Id { get; set; }

    public int Sop1id { get; set; }

    public int? ItsystemId { get; set; }

    public virtual Itsystem? Itsystem { get; set; }
}
