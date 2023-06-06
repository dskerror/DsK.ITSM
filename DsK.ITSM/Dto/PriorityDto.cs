using System;
using System.Collections.Generic;

namespace DsK.ITSM.Dto;

public partial class PriorityDto
{
    public int Id { get; set; }

    public string PriorityName { get; set; } = null!;

    public virtual ICollection<RequestDto> Requests { get; } = new List<RequestDto>();

    //For dropdowns
    public override bool Equals(object o)
    {
        var other = o as PriorityDto;
        return other?.PriorityName == PriorityName;
    }

    public override int GetHashCode() => PriorityName?.GetHashCode() ?? 0;
    public override string ToString() => PriorityName;
}
