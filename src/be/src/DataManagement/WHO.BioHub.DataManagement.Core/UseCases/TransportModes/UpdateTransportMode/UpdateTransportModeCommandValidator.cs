using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.UpdateTransportMode;

public class UpdateTransportModeCommandValidator : AbstractValidator<UpdateTransportModeCommand>
{
    public UpdateTransportModeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}