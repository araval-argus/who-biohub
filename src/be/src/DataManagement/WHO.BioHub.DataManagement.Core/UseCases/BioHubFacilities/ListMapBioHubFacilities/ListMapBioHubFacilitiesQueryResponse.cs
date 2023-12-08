using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListMapBioHubFacilities;

public record struct ListMapBioHubFacilitiesQueryResponse(IEnumerable<BioHubFacilityMapViewModel> BioHubFacilities) { }