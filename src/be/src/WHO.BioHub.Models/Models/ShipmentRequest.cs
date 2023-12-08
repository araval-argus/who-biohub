namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

/// <summary>
/// Pre-Shipment | Shipment | Post-Shipment
/// </summary>
public class ShipmentRequest
{
    public Guid Id { get; set; }

    public ShipmentDirection Direction { get; set; }

    public virtual Laboratory From { get; set; }

    public virtual Laboratory To { get; set; }

    public virtual Shipment Shipment { get; set; }

    public virtual ICollection<ShipmentStep> Steps { get; set; }
}
