using Microsoft.EntityFrameworkCore;
using DsK.ITSM.MinimalAPI.Models;

namespace DsK.ITSM.MinimalAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();
    public DbSet<KeyValueEntity> KeyValues => Set<KeyValueEntity>();

}