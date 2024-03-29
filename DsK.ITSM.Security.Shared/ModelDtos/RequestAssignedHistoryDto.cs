﻿using DsK.ITSM.Security.EntityFramework.Models;
using System;
using System.Collections.Generic;

namespace DsK.ITSM.Security.Shared;

public partial class RequestAssignedHistoryDto
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public DateTime AssignedDateTime { get; set; }

    public int? AssignedToUserId { get; set; }

    public virtual UserDto? AssignedToUser { get; set; }

    public virtual RequestDto Request { get; set; } = null!;
}
