using System;
using System.Collections.Generic;

namespace DsK.ITSM.Dto;

public partial class Sop1systemDto
{
    public int Id { get; set; }

    public int Sop1id { get; set; }

    public int? ItsystemId { get; set; }

    public virtual ItsystemDto? Itsystem { get; set; }
}
