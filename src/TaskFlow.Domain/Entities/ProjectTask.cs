using TaskFlow.Domain.Common;
using TaskFlow.Domain.Enums;
using DomainTaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Domain.Entities;

public class ProjectTask : BaseEntity
{
    private ProjectTask() { }
    
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public DomainTaskStatus Status { get; private set; }
    public Priority Priority { get; private set; }
    public DateTime? DueDate { get; private set; }
    
    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;
    
    public static ProjectTask Create(
        string title,
        string? description,
        Priority priority,
        DateTime? dueDate,
        Guid projectId)
    {
        return new ProjectTask
        {
            Title = title,
            Description = description,
            Status = DomainTaskStatus.ToDo,
            Priority = priority,
            DueDate = dueDate,
            ProjectId = projectId
        };
    }
    
    public void Update(string title, string? description, Priority priority, DateTime? dueDate)
    {
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        SetUpdated();
    }

    public void ChangeStatus(DomainTaskStatus newStatus)
    {
        if (Status == DomainTaskStatus.Cancelled)
            throw new InvalidOperationException(
                "Cannot change status of a cancelled task.");

        if (Status == DomainTaskStatus.Done && newStatus == DomainTaskStatus.ToDo)
            throw new InvalidOperationException(
                "Cannot move a completed task back to Todo.");

        Status = newStatus;
        SetUpdated();
    }
}