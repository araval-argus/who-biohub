using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.UpdateDocument;

public class UpdateDocumentCommandValidator : AbstractValidator<UpdateDocumentCommand>
{
    public UpdateDocumentCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}