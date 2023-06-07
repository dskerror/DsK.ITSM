using System.ComponentModel.DataAnnotations;

namespace DsK.ITSM.Dto;

public partial class AuthenticationProviderUpdateDto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Domain { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]    
    public string? Password { get; set; }
}
