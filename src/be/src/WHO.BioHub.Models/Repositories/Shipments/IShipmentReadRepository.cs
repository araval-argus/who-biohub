using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.Models.Repositories.Shipments;

public interface IShipmentReadRepository
{
    Task<Shipment> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Shipment>> List(CancellationToken cancellationToken);
    Task<IEnumerable<Shipment>> ListForLaboratoryUser(Guid userLaboratoryId, CancellationToken cancellationToken);
    Task<Shipment> ReadForLaboratoryUser(Guid id, Guid userLaboratoryId, CancellationToken cancellationToken);
    Task<KpiData> OutgoingShipmentsKPIData(CancellationToken cancellationToken);
    Task<KpiData> IncomingShipmentsKPIData(CancellationToken cancellationToken);
    Task<Shipment> ReadForBioHubFacilityUser(Guid id, Guid userBioHubFacilityId, CancellationToken cancellationToken);
    Task<IEnumerable<Shipment>> ListForBioHubFacilityUser(Guid userBioHubFacilityId, CancellationToken cancellationToken);
}
