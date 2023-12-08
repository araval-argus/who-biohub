using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ListBioHubFacilities;

public record struct ListBioHubFacilitiesQueryResponse(IEnumerable<BioHubFacilityPublicViewModel> BioHubFacilities) { }