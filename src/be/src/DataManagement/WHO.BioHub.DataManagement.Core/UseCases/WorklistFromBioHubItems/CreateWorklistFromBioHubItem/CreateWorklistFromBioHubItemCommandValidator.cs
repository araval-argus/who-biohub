using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.CreateWorklistFromBioHubItem;

public class CreateWorklistFromBioHubItemCommandValidator : AbstractValidator<CreateWorklistFromBioHubItemCommand>
{
    public CreateWorklistFromBioHubItemCommandValidator()
    {
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

        RuleFor(cmd => cmd.LaboratoryId)
            .NotNull()
            .WithMessage("'Laboratory' is required")
            .NotEmpty()
            .WithMessage("'Laboratory' is required");
    }
}