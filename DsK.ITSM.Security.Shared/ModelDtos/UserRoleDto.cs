﻿using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.Shared;

public partial class UserRoleDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual RoleDto Role { get; set; } = null!;

    public virtual UserDto User { get; set; } = null!;
}
