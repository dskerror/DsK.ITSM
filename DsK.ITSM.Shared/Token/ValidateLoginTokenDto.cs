namespace DsK.ITSM.Shared.Token;

public class ValidateLoginTokenDto
{
    public string LoginToken { get; set; } = string.Empty;
    public string TokenKey { get; set; } = string.Empty;
}