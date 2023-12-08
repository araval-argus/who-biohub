namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.UpdateTransportMode;

public record struct UpdateTransportModeCommand(Guid Id,
    string Name,
    string Description,
    string HexColor,
    bool IsActive);