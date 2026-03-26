using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using DomainTaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Domain.Interfaces;

public interface ITaskRepository
{
    Task<ProjectTask?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(
        Guid projectId,
        DomainTaskStatus? status = null,
        Priority? priority = null,
        CancellationToken ct = default);
    Task AddAsync(ProjectTask task, CancellationToken ct = default);
    void Update(ProjectTask task);
    void Delete(ProjectTask task);
}