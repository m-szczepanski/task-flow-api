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
        var projects = await _uow.Projects.GetAllAsync(ct);

        return projects.Select(p => new ProjectDto(
            p.Id,
            p.Name,
            p.Description,
            p.CreatedBy,
            p.CreatedAt,
            p.UpdatedAt,
            p.Tasks.Count
        ));
    }
}