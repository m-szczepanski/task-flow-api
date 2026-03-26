namespace TaskFlow.Domain.Interfaces;

public interface IUnitOfWork
{
    IProjectRepository Projects { get; }
    ITaskRepository Tasks { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}