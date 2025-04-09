namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } // foreign key
    public string Token { get; set; } = "";
    public DateTime Expiry { get; set; }

    public User User { get; set; } = null!; // navigation property
}