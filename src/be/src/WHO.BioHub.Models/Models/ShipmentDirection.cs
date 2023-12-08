namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class ShipmentDirection
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// Whit this field I can fill the ShipmentRequest Laboratory (From | To)
    /// If <code>true</code> From = BioHubFacility and To = The "Lab"
    /// If <code>false</code> From = The "Lab" and To = BioHubFacility
    /// </summary>
    public bool SendToBioHubFacility { get; set; }

    /// <summary>
    /// To fill in case in the next future we need to manage more than
    /// one BioHub Facility (in this way if <code>SendToBioHubFacility</code> 
    /// is <code>true</code> we register also the specific laboratory)
    /// </summary>
    public Laboratory BioHubFacility { get; set; }
}
