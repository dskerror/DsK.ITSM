using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DsK.ITSM.EntityFramework.Models;

public partial class DskitsmContext : DbContext
{
    public DskitsmContext()
    {
    }

    public DskitsmContext(DbContextOptions<DskitsmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Itsystem> Itsystems { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestAssignedHistory> RequestAssignedHistories { get; set; }

    public virtual DbSet<RequestMessageHistory> RequestMessageHistories { get; set; }

    public virtual DbSet<RequestStatusHistory> RequestStatusHistories { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=dskitsm;Trusted_Connection=True;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC0768740976");

            entity.Property(e => e.CategoryName).HasMaxLength(20);
        });

        modelBuilder.Entity<Itsystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Systems");

            entity.ToTable("ITSystems");

            entity.Property(e => e.SystemName).HasMaxLength(255);
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Priority__3214EC071E8C4D17");

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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_ITSystems");

            entity.HasOne(d => d.Priority).WithMany(p => p.Requests)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Priority");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Users");
        });

        modelBuilder.Entity<RequestAssignedHistory>(entity =>
        {
            entity.ToTable("RequestAssignedHistory");

            entity.Property(e => e.AssignedDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestAssignedHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestAssignedHistory_Requests");

            entity.HasOne(d => d.User).WithMany(p => p.RequestAssignedHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RequestAssignedHistory_Users");
        });

        modelBuilder.Entity<RequestMessageHistory>(entity =>
        {
            entity.ToTable("RequestMessageHistory");

            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.MessageDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestMessageHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestMessageHistory_Requests");

            entity.HasOne(d => d.User).WithMany(p => p.RequestMessageHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RequestMessageHistory_Users");
        });

        modelBuilder.Entity<RequestStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TransactionDateTime");

            entity.ToTable("RequestStatusHistory");

            entity.Property(e => e.TransactionDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestStatusHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestStatusHistory_Requests");

            entity.HasOne(d => d.Status).WithMany(p => p.RequestStatusHistories)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_RequestStatusHistory_Status");

            entity.HasOne(d => d.User).WithMany(p => p.RequestStatusHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RequestStatusHistory_Users");
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
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
