using MediatR;
using TaskFlow.Application.Common.Exceptions;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Projects.Commands.DeleteProject;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IUnitOfWork _uow;
    public DeleteProjectHandler(IUnitOfWork uow) => _uow = uow;

    public async Task Handle(DeleteProjectCommand request, CancellationToken ct)
    {
        var project = await _uow.Projects.GetByIdAsync(request.Id, ct)
                      ?? throw new NotFoundException(nameof(Project), request.Id);

        _uow.Projects.Delete(project);
        await _uow.SaveChangesAsync(ct);
    }
}