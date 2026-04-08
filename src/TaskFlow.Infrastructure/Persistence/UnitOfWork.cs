using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Persistence.Repositories;

namespace TaskFlow.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private bool _disposed = false;

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

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
            _disposed = true;
        }
    }
}