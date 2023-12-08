using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.DeleteTransportMode;

public class DeleteTransportModeCommandValidator : AbstractValidator<DeleteTransportModeCommand>
{
    public DeleteTransportModeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}