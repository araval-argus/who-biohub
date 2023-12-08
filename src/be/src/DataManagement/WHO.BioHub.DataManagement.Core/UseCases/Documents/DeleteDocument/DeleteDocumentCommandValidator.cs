using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.DeleteDocument;

public class DeleteDocumentCommandValidator : AbstractValidator<DeleteDocumentCommand>
{
    public DeleteDocumentCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}