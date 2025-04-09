using Microsoft.EntityFrameworkCore;
using DsK.ITSM.MinimalAPI.Data.Entities;

namespace DsK.ITSM.MinimalAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Authentication
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    

    // Users
    public DbSet<User> Users { get; set; }

    // Workflows
    public DbSet<Workflow> Workflows { get; set; }
    public DbSet<WorkflowStatusDefinition> WorkflowStatusDefinitions { get; set; }
    public DbSet<WorkflowStatusTransition> WorkflowStatusTransitions { get; set; }
    public DbSet<WorkflowAdditionalFieldDefinition> WorkflowAdditionalFieldDefinitions { get; set; }

    // Workflow Assignments
    public DbSet<WorkflowAssignedTo> WorkflowAssignedTos { get; set; }
    public DbSet<WorkflowAssignedToAutomatic> WorkflowAssignedToAutomatics { get; set; }

    // Workflow Tasks
    public DbSet<WorkflowTask> WorkflowTasks { get; set; }
    public DbSet<WorkflowTaskStatusHistory> WorkflowTaskStatusHistories { get; set; }
    public DbSet<WorkflowTaskAdditionalField> WorkflowTaskAdditionalFields { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relationships and constraints
        modelBuilder.Entity<Workflow>()
            .HasOne(w => w.CreatedByUser)
            .WithMany(u => u.CreatedWorkflows)
            .HasForeignKey(w => w.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WorkflowStatusDefinition>()
            .HasOne(s => s.Workflow)              // 👈 explicitly reference the navigation property
            .WithMany(w => w.StatusDefinitions)  // 👈 this is fine as you have the collection in Workflow
            .HasForeignKey(s => s.WorkflowId);

        modelBuilder.Entity<WorkflowStatusTransition>()
            .HasOne(s => s.Workflow)
            .WithMany(w => w.StatusTransitions)
            .HasForeignKey(t => t.WorkflowId);

        modelBuilder.Entity<WorkflowAdditionalFieldDefinition>()
            .HasOne(f => f.Workflow)             // 👈 Match nav property
            .WithMany(w => w.AdditionalFieldDefinitions) // or .WithMany(w => w.AdditionalFieldDefinitions) if bidirectional
            .HasForeignKey(f => f.WorkflowId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevents cascade conflict

        modelBuilder.Entity<WorkflowAssignedTo>()
            .HasOne(a => a.Workflow)
            .WithMany(w => w.AssignedUsers)
            .HasForeignKey(a => a.WorkflowId);

        modelBuilder.Entity<WorkflowAssignedToAutomatic>()
            .HasOne(a => a.WorkflowAssignedTo)
            .WithMany()
            .HasForeignKey(a => a.WorkflowAssignedToId);

        modelBuilder.Entity<WorkflowTask>()
            .HasOne(s => s.Workflow)
            .WithMany(w => w.WorkflowTasks)
            .HasForeignKey(t => t.WorkflowId);

        modelBuilder.Entity<WorkflowTaskStatusHistory>()
            .HasOne(h => h.WorkflowTask)
            .WithMany(t => t.StatusHistory)
            .HasForeignKey(h => h.WorkflowTaskId);

        modelBuilder.Entity<WorkflowTaskAdditionalField>()
            .HasOne(f => f.WorkflowTask)
            .WithMany(t => t.AdditionalFields)
            .HasForeignKey(f => f.WorkflowTaskId)
            .OnDelete(DeleteBehavior.Restrict);

        // Optional: Set up indexes or unique constraints
        modelBuilder.Entity<WorkflowStatusTransition>()
            .HasIndex(t => new { t.WorkflowId, t.FromStatusId, t.ToStatusId })
            .IsUnique();

        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)  // navigation property on User
            .HasForeignKey(rt => rt.UserId); // foreign key in RefreshToken
    }
}