﻿namespace DsK.ITSM.Shared.DTOs;

public partial class UserDto
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;
}
