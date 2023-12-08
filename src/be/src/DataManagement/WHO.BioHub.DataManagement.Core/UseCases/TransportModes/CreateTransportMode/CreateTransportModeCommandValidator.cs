using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.CreateTransportMode;

public class CreateTransportModeCommandValidator : AbstractValidator<CreateTransportModeCommand>
{
    public CreateTransportModeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}