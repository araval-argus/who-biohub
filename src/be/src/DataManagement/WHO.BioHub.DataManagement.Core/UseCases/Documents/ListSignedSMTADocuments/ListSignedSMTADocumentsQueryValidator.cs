using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ListSignedSMTADocuments;

public class ListSignedSMTADocumentsQueryValidator : AbstractValidator<ListSignedSMTADocumentsQuery>
{
    public ListSignedSMTADocumentsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}