using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListBioHubFacilities;

public record struct ListBioHubFacilitiesQueryResponse(IEnumerable<BioHubFacilityViewModel> BioHubFacilities) { }