using MediatR;

namespace TaskFlow.Application.Features.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid Id) : IRequest;