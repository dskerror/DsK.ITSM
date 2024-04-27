using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DsK.ITSM.EntityFramework.Models;

public partial class DsKITSMContext : DbContext
{
    public DsKITSMContext()
    {
    }

    public DsKITSMContext(DbContextOptions<DsKITSMContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Itsystem> Itsystems { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Office365EmailGroup> Office365EmailGroups { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestAssignedHistory> RequestAssignedHistories { get; set; }

    public virtual DbSet<RequestMessageHistory> RequestMessageHistories { get; set; }

    public virtual DbSet<RequestStatusHistory> RequestStatusHistories { get; set; }

    public virtual DbSet<Sop1> Sop1s { get; set; }

    public virtual DbSet<Sop1system> Sop1systems { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=dskitsm;Trusted_Connection=True;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Itsystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Systems");

            entity.ToTable("ITSystems");

            entity.Property(e => e.SystemName).HasMaxLength(255);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");

            entity.Property(e => e.DateTimeStamp).HasColumnType("datetime");
            entity.Property(e => e.Ipv4address)
                .HasMaxLength(16)
                .HasColumnName("IPV4Address");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasColumnName("URL");
            entity.Property(e => e.Username).HasMaxLength(50);
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
            entity.Property(e => e.PermissionName).HasMaxLength(50);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Priority)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.RequestDateTime).HasColumnType("datetime");
            entity.Property(e => e.RequestType).HasMaxLength(50);
            entity.Property(e => e.RequestedByDisplayName).HasMaxLength(100);
            entity.Property(e => e.RequestedByEmail).HasMaxLength(100);
            entity.Property(e => e.RequestedByUsername).HasMaxLength(100);
            entity.Property(e => e.Summary).HasMaxLength(100);
            entity.Property(e => e.System).HasMaxLength(255);
        });

        modelBuilder.Entity<RequestAssignedHistory>(entity =>
        {
            entity.ToTable("RequestAssignedHistory");

            entity.Property(e => e.AssignedDateTime).HasColumnType("datetime");
            entity.Property(e => e.AssignedTo).HasMaxLength(50);

            entity.HasOne(d => d.Request).WithMany(p => p.RequestAssignedHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestAssignedHistory_Requests");
        });

        modelBuilder.Entity<RequestMessageHistory>(entity =>
        {
            entity.ToTable("RequestMessageHistory");

            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.MessageDateTime).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Request).WithMany(p => p.RequestMessageHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestMessageHistory_Requests");
        });

        modelBuilder.Entity<RequestStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TransactionDateTime");

            entity.ToTable("RequestStatusHistory");

            entity.Property(e => e.ChangedByDisplayName).HasMaxLength(100);
            entity.Property(e => e.ChangedByUsername).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TransactionDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestStatusHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestStatusHistory_Requests");
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
            entity.Property(e => e.Supervisor).HasMaxLength(50);
            entity.Property(e => e.TitlePosition).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Sop1system>(entity =>
        {
            entity.ToTable("SOP1Systems");

            entity.Property(e => e.Sop1id).HasColumnName("SOP1Id");
            entity.Property(e => e.System).HasMaxLength(255);
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
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermissions_Permissions");

            entity.HasOne(d => d.User).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermissions_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
