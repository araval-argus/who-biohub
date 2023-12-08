using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.ListTransportModes;

public class ListTransportModesQueryValidator : AbstractValidator<ListTransportModesQuery>
{
    public ListTransportModesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}