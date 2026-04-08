using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Persistence.Repositories;

namespace TaskFlow.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IProjectRepository Projects { get; }
    public ITaskRepository Tasks { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Projects = new ProjectRepository(context);
        Tasks = new TaskRepository(context);
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await _context.SaveChangesAsync(ct);
}