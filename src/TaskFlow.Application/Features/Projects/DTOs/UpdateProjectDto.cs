namespace TaskFlow.Application.Features.Projects.DTOs;

public record UpdateProjectDto(
    string Name,
    string? Description
);