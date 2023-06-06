using System;
using System.Collections.Generic;

namespace DsK.ITSM.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; } = new List<Request>();
}
