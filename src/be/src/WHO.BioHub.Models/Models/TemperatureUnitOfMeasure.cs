using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class TemperatureUnitOfMeasure : EntityBase
{
    public string Name { get; set; }

    public string Unit { get; set; }
    public virtual ICollection<Material> Materials { get; set; }
    public virtual ICollection<Shipment> Shipments { get; set; }
    public virtual ICollection<MaterialHistory> MaterialsHistory { get; set; }
    public virtual ICollection<MaterialLaboratoryAnalysisInformation> MaterialLaboratoryAnalysisInformation { get; set; }

    public virtual ICollection<MaterialLaboratoryAnalysisInformationHistory> MaterialLaboratoryAnalysisInformationHistory { get; set; }

}
