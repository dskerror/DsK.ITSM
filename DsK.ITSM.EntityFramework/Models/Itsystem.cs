﻿using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class Itsystem
{
    public int Id { get; set; }

    public string? SystemName { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
