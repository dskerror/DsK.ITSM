using DsK.ITSM.Security.EntityFramework.Models;
using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.Shared;

public partial class CategoryDto
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<RequestDto> Requests { get; } = new List<RequestDto>();

    //For dropdowns
    public override bool Equals(object o)
    {
        var other = o as Category;
        return other?.CategoryName == CategoryName;
    }

    public override int GetHashCode() => CategoryName?.GetHashCode() ?? 0;
    public override string ToString() => CategoryName;
}
