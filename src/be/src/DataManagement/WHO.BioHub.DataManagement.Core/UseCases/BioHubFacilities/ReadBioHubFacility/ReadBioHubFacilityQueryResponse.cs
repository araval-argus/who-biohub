using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ReadBioHubFacility;

public record struct ReadBioHubFacilityQueryResponse(BioHubFacilityViewModel BioHubFacility) { }