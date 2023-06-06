using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DsK.ITSM.Models;

public partial class DsKitsmContext : DbContext
{
    public DsKitsmContext()
    {
    }

    public DsKitsmContext(DbContextOptions<DsKitsmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthenticationProvider> AuthenticationProviders { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Itsystem> Itsystems { get; set; }

    public virtual DbSet<Office365EmailGroup> Office365EmailGroups { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestAssignedHistory> RequestAssignedHistories { get; set; }

    public virtual DbSet<RequestMessageHistory> RequestMessageHistories { get; set; }

    public virtual DbSet<RequestStatusHistory> RequestStatusHistories { get; set; }

    public virtual DbSet<RequestType> RequestTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Sop1> Sop1s { get; set; }

    public virtual DbSet<Sop1system> Sop1systems { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAuthenticationProvider> UserAuthenticationProviders { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    public virtual DbSet<UserPassword> UserPasswords { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=DsKITSM;Trusted_Connection=True;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthenticationProvider>(entity =>
        {
            entity.HasIndex(e => e.AuthenticationProviderName, "IX_AuthenticationProviders");

            entity.Property(e => e.AuthenticationProviderName).HasMaxLength(50);
            entity.Property(e => e.AuthenticationProviderType).HasMaxLength(50);
            entity.Property(e => e.Domain).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07D45114DC");

            entity.Property(e => e.CategoryName).HasMaxLength(20);
        });

        modelBuilder.Entity<Itsystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Systems");

            entity.ToTable("ITSystems");

            entity.Property(e => e.SystemName).HasMaxLength(255);
        });

        modelBuilder.Entity<Office365EmailGroup>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.GroupEmail).HasMaxLength(255);
            entity.Property(e => e.GroupName).HasMaxLength(255);
            entity.Property(e => e.GroupType).HasMaxLength(255);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasIndex(e => e.PermissionName, "IX_Permissions").IsUnique();

            entity.Property(e => e.PermissionDescription).HasMaxLength(250);
            entity.Property(e => e.PermissionName).HasMaxLength(50);
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Priority__3214EC073C9C5F2E");

            entity.ToTable("Priority");

            entity.Property(e => e.PriorityName)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ItsystemId).HasColumnName("ITSystemId");
            entity.Property(e => e.RequestDateTime).HasColumnType("datetime");
            entity.Property(e => e.Summary).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Categories");

            entity.HasOne(d => d.Itsystem).WithMany(p => p.Requests)
                .HasForeignKey(d => d.ItsystemId)
                .HasConstraintName("FK_Requests_ITSystems");

            entity.HasOne(d => d.Priority).WithMany(p => p.Requests)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Priority");

            entity.HasOne(d => d.RequestType).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequestTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_RequestType");

            entity.HasOne(d => d.RequestedByUser).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequestedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Users");
        });

        modelBuilder.Entity<RequestAssignedHistory>(entity =>
        {
            entity.ToTable("RequestAssignedHistory");

            entity.Property(e => e.AssignedDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.RequestAssignedHistories)
                .HasForeignKey(d => d.AssignedToUserId)
                .HasConstraintName("FK_RequestAssignedHistory_Requests");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestAssignedHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestAssignedHistory_Requests1");
        });

        modelBuilder.Entity<RequestMessageHistory>(entity =>
        {
            entity.ToTable("RequestMessageHistory");

            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.MessageDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestMessageHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestMessageHistory_Requests1");

            entity.HasOne(d => d.User).WithMany(p => p.RequestMessageHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RequestMessageHistory_Requests");
        });

        modelBuilder.Entity<RequestStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TransactionDateTime");

            entity.ToTable("RequestStatusHistory");

            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TransactionDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.ChangedByUsernameUser).WithMany(p => p.RequestStatusHistories)
                .HasForeignKey(d => d.ChangedByUsernameUserId)
                .HasConstraintName("FK_RequestStatusHistory_Requests");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestStatusHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestStatusHistory_Requests1");
        });

        modelBuilder.Entity<RequestType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RequestT__3214EC07A8537660");

            entity.ToTable("RequestType");

            entity.Property(e => e.RequestTypeName).HasMaxLength(20);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.RoleName, "IX_Roles").IsUnique();

            entity.Property(e => e.RoleDescription).HasMaxLength(250);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId }).HasName("PK_RolePermissions_1");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Permissions");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Roles");
        });

        modelBuilder.Entity<Sop1>(entity =>
        {
            entity.ToTable("SOP1");

            entity.Property(e => e.Company).HasMaxLength(100);
            entity.Property(e => e.CopyProfileLike).HasMaxLength(50);
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.DeskLocation).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EmailGroups).HasMaxLength(200);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.EmployeeType).HasMaxLength(20);
            entity.Property(e => e.InternetAccess).HasMaxLength(20);
            entity.Property(e => e.PhoneAccess).HasMaxLength(20);
            entity.Property(e => e.PhoneExtension).HasMaxLength(10);
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.Supervisor).HasMaxLength(50);
            entity.Property(e => e.TitlePosition).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Sop1system>(entity =>
        {
            entity.ToTable("SOP1Systems");

            entity.Property(e => e.ItsystemId).HasColumnName("ITSystemId");
            entity.Property(e => e.Sop1id).HasColumnName("SOP1Id");

            entity.HasOne(d => d.Itsystem).WithMany(p => p.Sop1systems)
                .HasForeignKey(d => d.ItsystemId)
                .HasConstraintName("FK_SOP1Systems_ITSystems");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();

            entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEnd).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(256);
        });

        modelBuilder.Entity<UserAuthenticationProvider>(entity =>
        {
            entity.HasIndex(e => e.AuthenticationProviderId, "IX_UserAuthenticationProviders_AuthenticationProviderId");

            entity.HasIndex(e => e.UserId, "IX_UserAuthenticationProviders_UserId");

            entity.Property(e => e.Username).HasMaxLength(256);

            entity.HasOne(d => d.AuthenticationProvider).WithMany(p => p.UserAuthenticationProviders)
                .HasForeignKey(d => d.AuthenticationProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAuthenticationProviders_AuthenticationProviders");

            entity.HasOne(d => d.User).WithMany(p => p.UserAuthenticationProviders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAuthenticationProviders_Users");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_UserLogs_UserId");

            entity.Property(e => e.Ip).HasColumnName("IP");
            entity.Property(e => e.LogDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserPassword>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_UserPasswords_UserId");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserPasswords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPasswords_Users");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasIndex(e => new { e.PermissionId, e.UserId }, "IX_UserPermissions").IsUnique();

            entity.HasIndex(e => e.UserId, "IX_UserPermissions_UserId");

            entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermissions_Permissions");

            entity.HasOne(d => d.User).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermissions_Users");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasIndex(e => new { e.UserId, e.RoleId }, "IX_UserRoles").IsUnique();

            entity.HasIndex(e => e.RoleId, "IX_UserRoles_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserAuthenticationProviderTokens");

            entity.HasIndex(e => e.UserId, "IX_UserTokens_UserId");

            entity.Property(e => e.TokenCreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.TokenRefreshedDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTokens_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
