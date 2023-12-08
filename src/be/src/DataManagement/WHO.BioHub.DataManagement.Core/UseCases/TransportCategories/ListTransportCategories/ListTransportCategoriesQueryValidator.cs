using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.ListTransportCategories;

public class ListTransportCategoriesQueryValidator : AbstractValidator<ListTransportCategoriesQuery>
{
    public ListTransportCategoriesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}