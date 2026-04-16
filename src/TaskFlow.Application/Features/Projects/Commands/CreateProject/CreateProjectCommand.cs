using MediatR;

namespace TaskFlow.Application.Features.Projects.Commands.CreateProject;

public record CreateProjectCommand(
    string Name,
    string? Description
) : IRequest<Guid>;