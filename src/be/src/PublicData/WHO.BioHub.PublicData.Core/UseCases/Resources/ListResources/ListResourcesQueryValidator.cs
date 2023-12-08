using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Resources.ListResources;

public class ListResourcesQueryValidator : AbstractValidator<ListResourcesQuery>
{
    public ListResourcesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}