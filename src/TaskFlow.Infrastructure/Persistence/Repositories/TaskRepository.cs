using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Interfaces;
using DomainTaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Infrastructure.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<ProjectTask?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(
        Guid projectId,
        DomainTaskStatus? status = null,
        Priority? priority = null,
        CancellationToken ct = default)
    {
        var query = _context.Tasks
            .AsNoTracking()
            .Where(t => t.ProjectId == projectId);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (priority.HasValue)
            query = query.Where(t => t.Priority == priority.Value);

        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync(ct);
    }

    public async Task AddAsync(ProjectTask task, CancellationToken ct = default)
        => await _context.Tasks.AddAsync(task, ct);

    public void Update(ProjectTask task) => _context.Tasks.Update(task);

    public void Delete(ProjectTask task) => _context.Tasks.Remove(task);
}