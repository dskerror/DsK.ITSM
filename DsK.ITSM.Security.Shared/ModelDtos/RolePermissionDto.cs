﻿using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.Shared;

public partial class RolePermissionDto
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public virtual PermissionDto Role { get; set; } = null!;

    public virtual RoleDto RoleNavigation { get; set; } = null!;
}
