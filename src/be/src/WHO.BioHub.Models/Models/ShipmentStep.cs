namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class ShipmentStep
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

    public int Order { get; set; }

    public virtual ICollection<ShipmentTask> Tasks { get; set; }
}
