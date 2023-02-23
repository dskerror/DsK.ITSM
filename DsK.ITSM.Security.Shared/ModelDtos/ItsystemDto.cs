using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.Shared;

public partial class ItsystemDto
{
    public int Id { get; set; }

    public string SystemName { get; set; } = null!;

    public virtual ICollection<RequestDto> Requests { get; } = new List<RequestDto>();

    public virtual ICollection<Sop1systemDto> Sop1systems { get; } = new List<Sop1systemDto>();
}
