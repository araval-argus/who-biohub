using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.PublicData.Core.Dto
{
    public class BioHubFacilityPublicMapViewModel : BioHubFacilityPublicViewModel
    {
        public List<Coordinates> ToBioHubConnectedInstitutesLatLng { get; set; }
        public List<Coordinates> FromBioHubConnectedInstitutesLatLng { get; set; }
    }
}
