﻿using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.Shared;

public partial class RolePermissionGridDto
{   
    public int Id { get; set; }
    public string PermissionName { get; set; }
    public string PermissionDescription { get; set; }    
    public bool Allow { get; set; }
}
