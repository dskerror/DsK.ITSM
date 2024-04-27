using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class Office365EmailGroup
{
    public int Id { get; set; }

    public string? GroupName { get; set; }

    public string? GroupEmail { get; set; }

    public string? Description { get; set; }

    public string? GroupType { get; set; }
}
