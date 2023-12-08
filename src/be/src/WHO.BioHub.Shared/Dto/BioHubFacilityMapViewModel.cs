namespace WHO.BioHub.Shared.Dto
{
    public class BioHubFacilityMapViewModel : BioHubFacilityViewModel
    {
        public List<Coordinates> ToBioHubConnectedInstitutesLatLng { get; set; }
        public List<Coordinates> FromBioHubConnectedInstitutesLatLng { get; set; }
    }
}
