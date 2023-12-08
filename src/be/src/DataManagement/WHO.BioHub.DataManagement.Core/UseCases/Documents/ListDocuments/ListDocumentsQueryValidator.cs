using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocuments;

public class ListDocumentsQueryValidator : AbstractValidator<ListDocumentsQuery>
{
    public ListDocumentsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}