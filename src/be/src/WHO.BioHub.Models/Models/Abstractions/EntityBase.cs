namespace WHO.BioHub.Models.Models.Abstractions;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    // TODO: setter should be internal if moved this models in the DAL project
    public DateTime? DeletedOn { get; /* internal */ set; }
    public bool IsDeleted => DeletedOn.HasValue;

    // public User CreatedBy { get; set; }
    public DateTime CreationDate { get; set; }
}