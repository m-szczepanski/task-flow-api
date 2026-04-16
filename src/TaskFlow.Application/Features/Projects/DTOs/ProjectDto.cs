namespace TaskFlow.Application.Features.Projects.DTOs;

public record ProjectDto(
    Guid Id,
    string Name,
    string? Description,
    string CreatedBy,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int TaskCount
);