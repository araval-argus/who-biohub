using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ReadBioHubFacility;

public record struct ReadBioHubFacilityQueryResponse(BioHubFacilityPublicViewModel BioHubFacility) { }