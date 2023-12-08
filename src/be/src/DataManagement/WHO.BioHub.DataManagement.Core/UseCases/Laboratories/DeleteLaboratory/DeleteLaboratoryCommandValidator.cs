using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.DeleteLaboratory;

public class DeleteLaboratoryCommandValidator : AbstractValidator<DeleteLaboratoryCommand>
{
    public DeleteLaboratoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}