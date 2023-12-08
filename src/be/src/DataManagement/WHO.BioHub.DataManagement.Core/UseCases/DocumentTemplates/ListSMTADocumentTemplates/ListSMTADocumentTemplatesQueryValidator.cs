using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ListSMTADocumentTemplates;

public class ListSMTADocumentTemplatesQueryValidator : AbstractValidator<ListSMTADocumentTemplatesQuery>
{
    public ListSMTADocumentTemplatesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}