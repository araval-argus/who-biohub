using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class BSLLevel : EntityBase
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<BioHubFacility> BioHubFacilities { get; set; }
    public ICollection<Laboratory> Laboratories { get; set; }
    public ICollection<BioHubFacilityHistory> BioHubFacilitiesHistory { get; set; }
    public ICollection<LaboratoryHistory> LaboratoriesHistory { get; set; }
}
