using System;
using System.Collections.Generic;

namespace DsK.ITSM.Models;

public partial class RolePermission
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public virtual Permission Role { get; set; } = null!;

    public virtual Role RoleNavigation { get; set; } = null!;
}
