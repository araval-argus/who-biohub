using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.CreateShipment;

public interface ICreateShipmentMapper
{
    Shipment Map(CreateShipmentCommand command);
}

public class CreateShipmentMapper : ICreateShipmentMapper
{
    public Shipment Map(CreateShipmentCommand command)
    {
        // TODO: Implement mapper

        Shipment shipment = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return shipment;
    }
}