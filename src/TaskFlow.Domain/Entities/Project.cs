using TaskFlow.Domain.Common;

namespace TaskFlow.Domain.Entities;

public class Project : BaseEntity
{
    private Project() { }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public string CreatedBy { get; private set; } = null!;
    
    private readonly List<ProjectTask> _tasks = new();
    public IReadOnlyCollection<ProjectTask> Tasks => _tasks.AsReadOnly();
    
    public static Project Create(string name, string? description, string createdBy = "system")
    {
        return new Project
        {
            Name = name,
            Description = description,
            CreatedBy = createdBy
        };
    }
    
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
        SetUpdated();
    }
}