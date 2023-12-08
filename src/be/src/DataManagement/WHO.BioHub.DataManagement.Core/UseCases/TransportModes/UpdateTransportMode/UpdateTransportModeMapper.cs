using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.UpdateTransportMode;

public interface IUpdateTransportModeMapper
{
    TransportMode Map(TransportMode transportmode, UpdateTransportModeCommand command);
}

public class UpdateTransportModeMapper : IUpdateTransportModeMapper
{
    public TransportMode Map(TransportMode transportmode, UpdateTransportModeCommand command)
    {
        transportmode.Id = command.Id;
        transportmode.Name = command.Name;
        transportmode.Description = command.Description;
        transportmode.HexColor = command.HexColor;
        transportmode.IsActive = command.IsActive;
        transportmode.DeletedOn = null;

        return transportmode;
    }
}