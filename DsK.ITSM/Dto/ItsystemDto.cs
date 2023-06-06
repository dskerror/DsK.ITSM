using System;
using System.Collections.Generic;

namespace DsK.ITSM.Dto;

public partial class ItsystemDto
{
    public int Id { get; set; }

    public string SystemName { get; set; } = null!;

    public virtual ICollection<RequestDto> Requests { get; } = new List<RequestDto>();

    public virtual ICollection<Sop1systemDto> Sop1systems { get; } = new List<Sop1systemDto>();

    //For dropdowns
    public override bool Equals(object o)
    {
        var other = o as ItsystemDto;
        return other?.SystemName == SystemName;
    }

    public override int GetHashCode() => SystemName?.GetHashCode() ?? 0;
    public override string ToString() => SystemName;
}
