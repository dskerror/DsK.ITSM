﻿using System.ComponentModel.DataAnnotations;

namespace DsK.ITSM.Security.Shared
{
	public class UserLoginDto
	{
        [Required]
        [StringLength(256, MinimumLength = 3)]
        public string? Username { get; set; }
		[Required]
		public string? Password { get; set; }
		public int AuthenticationProviderId { get; set; } = 1;
	}
}
