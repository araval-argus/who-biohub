using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.PublicData.Core.Dto
{
    public class LaboratoryPublicMapViewModel : LaboratoryPublicViewModel
    {
        public List<Coordinates> ToBioHubConnectedInstitutesLatLng { get; set; }
        public List<Coordinates> FromBioHubConnectedInstitutesLatLng { get; set; }
        public InstituteType InstituteType { get; set; }
    }
}
