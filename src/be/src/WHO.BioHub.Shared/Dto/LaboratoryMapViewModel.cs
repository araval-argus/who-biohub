using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class LaboratoryMapViewModel : LaboratoryViewModel
    {
        public List<Coordinates> ToBioHubConnectedInstitutesLatLng { get; set; }
        public List<Coordinates> FromBioHubConnectedInstitutesLatLng { get; set; }
        public InstituteType InstituteType { get; set; }
    }
}
