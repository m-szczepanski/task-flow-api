using FluentValidation;

namespace TaskFlow.Application.Features.Projects.Queries.GetProjectById;

public class GetProjectByIdValidator : AbstractValidator<GetProjectByIdQuery>
{
    public GetProjectByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Project ID cannot be empty.");
    }
}