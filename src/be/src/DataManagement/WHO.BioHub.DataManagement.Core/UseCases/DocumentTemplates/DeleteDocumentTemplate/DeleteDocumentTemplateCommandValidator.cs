using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.DeleteDocumentTemplate;

public class DeleteDocumentTemplateCommandValidator : AbstractValidator<DeleteDocumentTemplateCommand>
{
    public DeleteDocumentTemplateCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}