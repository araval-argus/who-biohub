using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.ReadTransportCategory;

public class ReadTransportCategoryQueryValidator : AbstractValidator<ReadTransportCategoryQuery>
{
    public ReadTransportCategoryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}