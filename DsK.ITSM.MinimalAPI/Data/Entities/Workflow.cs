namespace DsK.ITSM.MinimalAPI.Data.Entities;
public class Workflow
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User CreatedByUser { get; set; } = null!;
    public ICollection<WorkflowStatusDefinition> StatusDefinitions { get; set; } = new List<WorkflowStatusDefinition>();
    public ICollection<WorkflowStatusTransition> StatusTransitions { get; set; } = new List<WorkflowStatusTransition>();
    public ICollection<WorkflowTask> WorkflowTasks { get; set; } = new List<WorkflowTask>();
    public ICollection<WorkflowAdditionalFieldDefinition> AdditionalFieldDefinitions { get; set; } = new List<WorkflowAdditionalFieldDefinition>();
    public ICollection<WorkflowAssignedTo> AssignedUsers { get; set; } = new List<WorkflowAssignedTo>();
}