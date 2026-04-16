using MediatR;
using TaskFlow.Application.Common.Exceptions;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IUnitOfWork _uow;
    public UpdateProjectHandler(IUnitOfWork uow) => _uow = uow;

    public async Task Handle(UpdateProjectCommand request, CancellationToken ct)
    {
        var project = await _uow.Projects.GetByIdAsync(request.Id, ct)
                      ?? throw new NotFoundException(nameof(Projects), request.Id);

        project.Update(request.Name, request.Description);
        _uow.Projects.Update(project);
        await _uow.SaveChangesAsync(ct);
    }
}