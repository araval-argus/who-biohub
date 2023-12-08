using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.ListResources;

public class ListResourcesQueryValidator : AbstractValidator<ListResourcesQuery>
{
    public ListResourcesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}