using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<(Project Project, int TaskCount)?> GetByIdWithTaskCountAsync(Guid id, CancellationToken ct = default)
    {
        var result = await _context.Projects
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new { Project = p, TaskCount = p.Tasks.Count })
            .FirstOrDefaultAsync(ct);

        return result is null ? null : (result.Project, result.TaskCount);
    }

    public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken ct = default)
        => await _context.Projects
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<IEnumerable<(Project Project, int TaskCount)>> GetAllWithTaskCountsAsync(CancellationToken ct = default)
    {
        var results = await _context.Projects
            .AsNoTracking()
            .Select(p => new { Project = p, TaskCount = p.Tasks.Count })
            .ToListAsync(ct);

        return results.Select(r => (r.Project, r.TaskCount));
    }

    public async Task AddAsync(Project project, CancellationToken ct = default)
        => await _context.Projects.AddAsync(project, ct);

    public void Update(Project project)
        => _context.Projects.Update(project);

    public void Delete(Project project)
        => _context.Projects.Remove(project);
}