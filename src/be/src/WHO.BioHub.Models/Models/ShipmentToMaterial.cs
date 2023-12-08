namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class ShipmentToMaterial
{
    public Guid Id { get; set; }

    public Shipment Shipment { get; set; }

    public Material Material { get; set; }
}
