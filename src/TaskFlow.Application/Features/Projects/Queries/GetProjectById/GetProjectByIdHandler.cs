using MediatR;
using TaskFlow.Application.Common.Exceptions;
using TaskFlow.Application.Features.Projects.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Projects.Queries.GetProjectById;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
{
    private readonly IUnitOfWork _uow;
    public GetProjectByIdHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken ct)
    {
        var project = await _uow.Projects.GetByIdWithTaskCountAsync(request.Id, ct)
                      ?? throw new NotFoundException(nameof(Project), request.Id);

        return new ProjectDto(
            project.Project.Id,
            project.Project.Name,
            project.Project.Description,
            project.Project.CreatedBy,
            project.Project.CreatedAt,
            project.Project.UpdatedAt,
            project.TaskCount
        );
    }
}