using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.CreateDocument;

public class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}