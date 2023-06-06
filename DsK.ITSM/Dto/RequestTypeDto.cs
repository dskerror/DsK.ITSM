using System;
using System.Collections.Generic;

namespace DsK.ITSM.Dto;

public partial class RequestTypeDto
{
    public int Id { get; set; }

    public string RequestTypeName { get; set; } = null!;

    public virtual ICollection<RequestDto> Requests { get; } = new List<RequestDto>();
}
