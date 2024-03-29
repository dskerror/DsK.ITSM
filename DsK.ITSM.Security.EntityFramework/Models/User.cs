﻿using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.EntityFramework.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool EmailConfirmed { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public virtual ICollection<RequestAssignedHistory> RequestAssignedHistories { get; } = new List<RequestAssignedHistory>();

    public virtual ICollection<RequestMessageHistory> RequestMessageHistories { get; } = new List<RequestMessageHistory>();

    public virtual ICollection<RequestStatusHistory> RequestStatusHistories { get; } = new List<RequestStatusHistory>();

    public virtual ICollection<Request> Requests { get; } = new List<Request>();

    public virtual ICollection<UserAuthenticationProvider> UserAuthenticationProviders { get; } = new List<UserAuthenticationProvider>();

    public virtual ICollection<UserPassword> UserPasswords { get; } = new List<UserPassword>();

    public virtual ICollection<UserPermission> UserPermissions { get; } = new List<UserPermission>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();

    public virtual ICollection<UserToken> UserTokens { get; } = new List<UserToken>();
}
