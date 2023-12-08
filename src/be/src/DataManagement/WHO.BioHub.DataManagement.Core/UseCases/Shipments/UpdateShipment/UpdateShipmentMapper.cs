using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.UpdateShipment;

public interface IUpdateShipmentMapper
{
    Shipment Map(Shipment shipment, UpdateShipmentCommand command);
}

public class UpdateShipmentMapper : IUpdateShipmentMapper
{
    public Shipment Map(Shipment shipment, UpdateShipmentCommand command)
    {       

        shipment.Id = command.Id;
        shipment.ReferenceNumber = command.ReferenceNumber;        

        shipment.DeletedOn = null;

        return shipment;
    }
}