namespace DsK.ITSM.MinimalAPI.Models;
public class RefreshTokenEntity
{
    public int Id { get; set; }
    public string Token { get; set; } = "";
    public DateTime Expiry { get; set; }

    public int UserId { get; set; }
    public UserEntity User { get; set; } = default!;
}