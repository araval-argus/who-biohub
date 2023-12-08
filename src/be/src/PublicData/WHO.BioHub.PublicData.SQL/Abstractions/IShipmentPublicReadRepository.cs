using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IShipmentPublicReadRepository
{
    Task<Shipment> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Shipment>> List(CancellationToken cancellationToken);
    Task<List<ShipmentsTypePercentage>> OutgoingShipmentRate(CancellationToken cancellationToken);
    Task<List<ShipmentsTypePercentage>> IncomingShipmentRate(CancellationToken cancellationToken);
    Task<int> CountryNumber(CancellationToken cancellationToken);
    Task<int> MaterialNumber(CancellationToken cancellationToken);
    Task<int> NumberOfOutgoingShipments(CancellationToken cancellationToken);
    Task<int> NumberOfIncomingShipments(CancellationToken cancellationToken);
}
