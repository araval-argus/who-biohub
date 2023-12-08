using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UpdateDocumentTemplate;

public class UpdateDocumentTemplateCommandValidator : AbstractValidator<UpdateDocumentTemplateCommand>
{
    public UpdateDocumentTemplateCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithMessage("Name is mandatory");
    }
}