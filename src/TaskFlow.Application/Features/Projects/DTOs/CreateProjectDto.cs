namespace TaskFlow.Application.Features.Projects.DTOs;

public record CreateProjectDto(
    string Name,
    string? Description
);