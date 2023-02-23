using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.Shared;

public partial class PriorityDto
{
    public int Id { get; set; }

    public string PriorityName { get; set; } = null!;

    public virtual ICollection<RequestDto> Requests { get; } = new List<RequestDto>();
}
