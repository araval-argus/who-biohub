using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialEvents.ListMaterialEvents;

public class ListMaterialEventsQueryValidator : AbstractValidator<ListMaterialEventsQuery>
{
    public ListMaterialEventsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}