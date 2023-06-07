namespace DsK.ITSM.Dto;
public class UserAuthenticationProviderDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AuthenticationProviderId { get; set; }

    public string Username { get; set; } = null!;

    public virtual AuthenticationProviderDto AuthenticationProvider { get; set; } = null!;

    public virtual UserDto User { get; set; } = null!;
}
