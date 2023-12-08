using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.CheckDocumentSigned;

public class CheckDocumentSignedQueryValidator : AbstractValidator<CheckDocumentSignedQuery>
{
    public CheckDocumentSignedQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}