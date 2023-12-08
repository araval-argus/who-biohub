namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.CreateTransportMode;

public record struct CreateTransportModeCommand(
    string Name,
    string Description,
    string HexColor,
    bool IsActive
);