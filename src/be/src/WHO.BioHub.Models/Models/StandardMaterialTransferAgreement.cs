namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class StandardMaterialTransferAgreement
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Laboratory SignedBy { get; set; }

    public DateTime SignedData { get; set; }
}
