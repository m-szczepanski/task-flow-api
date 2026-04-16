using MediatR;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    public CreateProjectHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken ct)
    {
        var project = Project.Create(request.Name, request.Description);
        await _uow.Projects.AddAsync(project, ct);
        await _uow.SaveChangesAsync(ct);
        return project.Id;
    }
}