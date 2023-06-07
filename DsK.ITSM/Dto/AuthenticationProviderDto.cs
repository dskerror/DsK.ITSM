namespace DsK.ITSM.Dto;

public partial class AuthenticationProviderDto
{
    public int Id { get; set; }

    public string AuthenticationProviderName { get; set; } = null!;

    public string? AuthenticationProviderType { get; set; }

    public string? Domain { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}
