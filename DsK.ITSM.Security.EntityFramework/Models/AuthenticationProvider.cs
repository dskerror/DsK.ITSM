﻿using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.EntityFramework.Models;

public partial class AuthenticationProvider
{
    public int Id { get; set; }

    public string AuthenticationProviderName { get; set; } = null!;

    public string? AuthenticationProviderType { get; set; }

    public string? Domain { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<UserAuthenticationProvider> UserAuthenticationProviders { get; } = new List<UserAuthenticationProvider>();
}
