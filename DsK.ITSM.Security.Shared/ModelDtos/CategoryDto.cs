using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.Shared;

public partial class CategoryDto
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<RequestDto> Requests { get; } = new List<RequestDto>();
}
