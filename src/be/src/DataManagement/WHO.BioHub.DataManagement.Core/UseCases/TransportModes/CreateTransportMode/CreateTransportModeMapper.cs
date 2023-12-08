using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.CreateTransportMode;

public interface ICreateTransportModeMapper
{
    TransportMode Map(CreateTransportModeCommand command);
}

public class CreateTransportModeMapper : ICreateTransportModeMapper
{
    public TransportMode Map(CreateTransportModeCommand command)
    {

        TransportMode transportmode = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            HexColor = command.HexColor,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return transportmode;
    }
}