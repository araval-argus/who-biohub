using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.ReadTransportMode;

public class ReadTransportModeQueryValidator : AbstractValidator<ReadTransportModeQuery>
{
    public ReadTransportModeQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}