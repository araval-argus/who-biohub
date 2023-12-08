namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class ShipmentTask
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

    public int Order { get; set; }

    public Guid? ParentId { get; set; }

    public virtual ICollection<Document> Documents { get; set; }
}
