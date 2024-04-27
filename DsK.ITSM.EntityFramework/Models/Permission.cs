using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string PermissionName { get; set; } = null!;

    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}
