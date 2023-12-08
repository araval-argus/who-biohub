using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.CreateSMTA1WorkflowItem;

public class CreateSMTA1WorkflowItemCommandValidator : AbstractValidator<CreateSMTA1WorkflowItemCommand>
{
    public CreateSMTA1WorkflowItemCommandValidator()
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