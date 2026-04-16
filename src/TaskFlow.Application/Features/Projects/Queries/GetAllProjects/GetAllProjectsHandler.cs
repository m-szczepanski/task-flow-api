using MediatR;
using TaskFlow.Application.Features.Projects.DTOs;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Projects.Queries.GetAllProjects;

public class GetAllProjectsHandler
    : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllProjectsHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<IEnumerable<ProjectDto>> Handle(
        GetAllProjectsQuery request,
        CancellationToken ct)
    {
        var projects = await _uow.Projects.GetAllWithTaskCountsAsync(ct);

        return projects.Select(p => new ProjectDto(
            p.Project.Id,
            p.Project.Name,
            p.Project.Description,
            p.Project.CreatedBy,
            p.Project.CreatedAt,
            p.Project.UpdatedAt,
            p.TaskCount
        ));
    }
}