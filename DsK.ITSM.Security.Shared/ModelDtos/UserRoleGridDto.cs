﻿using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.Shared;

public partial class UserRoleGridDto
{
    public int Id { get; set; }
    public string RoleName { get; set; }
    public string RoleDescription { get; set; }
    public bool Enable { get; set; }
}
