using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Materials;

public interface IMaterialReadRepository
{
    Task<Material> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Material>> List(CancellationToken cancellationToken);
    Task<IEnumerable<Material>> ListForLaboratoryUser(Guid userLaboratoryId, CancellationToken cancellationToken);
    Task<Material> ReadForLaboratoryUser(Guid id, Guid UserLaboratoryId, CancellationToken cancellationToken);
    Task<IEnumerable<Material>> ListForShipmentRequest(CancellationToken cancellationToken);
    Task<IEnumerable<Material>> ListForShipmentRequestFromBioHub(Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<bool> ReferenceNumberAlreadyPresent(Guid id, string referenceNumber, CancellationToken cancellationToken);
    Task<bool> ReferenceNumberAlreadyPresentForCreation(string referenceNumber, CancellationToken cancellationToken);
    Task<Material> ReadForBioHubFacilityUser(Guid id, Guid UserBioHubFacilityId, CancellationToken cancellationToken);
    Task<IEnumerable<Material>> ListForBioHubFacilityUser(Guid userBioHubFacilityId, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialCollectedSpecimenType>> ReadMaterialCollectedSpecimenTypes(Guid id, CancellationToken cancellationToken);
    Task<Material> ReadWithHistory(Guid id, CancellationToken cancellationToken);
    Task<Material> ReadForLaboratoryUserWithHistory(Guid id, Guid UserLaboratoryId, CancellationToken cancellationToken);
    Task<Material> ReadForBioHubFacilityUserWithHistory(Guid id, Guid UserBioHubFacilityId, CancellationToken cancellationToken);
    Task<MaterialHistory> ReadPastInformation(Guid id, DateTime date, CancellationToken cancellationToken);
}
