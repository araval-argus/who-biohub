using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class Country : EntityBase
{
    public Country()
    {
        Laboratories = new HashSet<Laboratory>();
    }

    public string Uncode { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Iso2 { get; set; }
    public string Iso3 { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int? GmtHour { get; set; }
    public int? GmtMin { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Laboratory> Laboratories { get; set; }
    public virtual ICollection<BioHubFacility> BioHubFacilities { get; set; }
    public virtual ICollection<Material> Materials { get; set; }
    public virtual ICollection<UserRequest> UserRequests { get; set; }
    public virtual ICollection<Courier> Couriers { get; set; }
    public virtual ICollection<MaterialHistory> MaterialsHistory { get; set; }
    public virtual ICollection<LaboratoryHistory> LaboratoriesHistory { get; set; }
    public virtual ICollection<BioHubFacilityHistory> BioHubFacilitiesHistory { get; set; }
    public virtual ICollection<CourierHistory> CouriersHistory { get; set; }
}