using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.CreateWorklistToBioHubItem;

public class CreateWorklistToBioHubItemCommandValidator : AbstractValidator<CreateWorklistToBioHubItemCommand>
{
    public CreateWorklistToBioHubItemCommandValidator()
    {

        RuleFor(cmd => cmd.LaboratoryId)
            .NotNull()
            .WithMessage("'Laboratory' is required")
            .NotEmpty()
            .WithMessage("'Laboratory' is required");

        When(cmd => cmd.IsPast == true, () =>
        {
            RuleFor(cmd => cmd.AssignedOperationDate)
                .NotNull()
                .WithMessage("'Operation Date' is required")
                .NotEmpty()
                .WithMessage("'Operation Date' is required")
                .Must((e) => e.GetValueOrDefault().Date < DateTime.UtcNow.Date)
                .WithMessage("'Operation Date' most be before than today");
        });
    }
}