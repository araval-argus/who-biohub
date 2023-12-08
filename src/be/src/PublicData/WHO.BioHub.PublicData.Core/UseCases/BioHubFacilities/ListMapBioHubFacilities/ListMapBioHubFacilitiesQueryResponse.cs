using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.MapBioHubFacilities.ListMapBioHubFacilities;

public record struct ListMapBioHubFacilitiesQueryResponse(IEnumerable<BioHubFacilityPublicMapViewModel> BioHubFacilities) { }