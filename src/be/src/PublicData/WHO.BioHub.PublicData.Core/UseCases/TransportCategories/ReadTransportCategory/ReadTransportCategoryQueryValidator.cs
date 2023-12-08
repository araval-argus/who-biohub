using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.TransportCategories.ReadTransportCategory;

public class ReadTransportCategoryQueryValidator : AbstractValidator<ReadTransportCategoryQuery>
{
    public ReadTransportCategoryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}