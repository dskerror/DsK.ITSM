using System;
using System.Collections.Generic;

namespace DsK.ITSM.Dto;

public partial class RequestStatusHistoryDto
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public DateTime TransactionDateTime { get; set; }

    public string Status { get; set; } = null!;

    public int? ChangedByUsernameUserId { get; set; }
    public virtual UserDto? ChangedByUsernameUser { get; set; }

    public virtual RequestDto Request { get; set; } = null!;
}
