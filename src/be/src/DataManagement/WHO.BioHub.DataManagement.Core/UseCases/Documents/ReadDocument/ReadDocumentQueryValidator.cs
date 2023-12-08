using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ReadDocument;

public class ReadDocumentQueryValidator : AbstractValidator<ReadDocumentQuery>
{
    public ReadDocumentQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}