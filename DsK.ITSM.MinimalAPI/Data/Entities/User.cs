namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<Workflow> CreatedWorkflows { get; set; } = new List<Workflow>();
}