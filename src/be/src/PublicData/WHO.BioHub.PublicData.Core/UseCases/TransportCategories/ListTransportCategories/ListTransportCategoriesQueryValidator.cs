using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.TransportCategories.ListTransportCategories;

public class ListTransportCategoriesQueryValidator : AbstractValidator<ListTransportCategoriesQuery>
{
    public ListTransportCategoriesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}