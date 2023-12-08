using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ListDocumentTemplates;

public class ListDocumentTemplatesQueryValidator : AbstractValidator<ListDocumentTemplatesQuery>
{
    public ListDocumentTemplatesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}