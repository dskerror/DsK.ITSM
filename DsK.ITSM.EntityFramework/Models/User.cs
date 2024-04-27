using System;
using System.Collections.Generic;

namespace DsK.ITSM.EntityFramework.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}
