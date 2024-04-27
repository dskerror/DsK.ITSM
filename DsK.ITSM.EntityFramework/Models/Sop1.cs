using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class Sop1
{
    public int Id { get; set; }

    public int? RequestId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string Company { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string TitlePosition { get; set; } = null!;

    public string Supervisor { get; set; } = null!;

    public string? CopyProfileLike { get; set; }

    public string? DeskLocation { get; set; }

    public DateOnly StartDate { get; set; }

    public string EmployeeType { get; set; } = null!;

    public string InternetAccess { get; set; } = null!;

    public string PhoneAccess { get; set; } = null!;

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PhoneExtension { get; set; }

    public string? EmailGroups { get; set; }
}
