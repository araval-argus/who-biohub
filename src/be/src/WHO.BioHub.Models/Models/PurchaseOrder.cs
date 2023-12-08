namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

/// <summary>
/// A WHO Purchase Order with specific properties
/// </summary>
public class PurchaseOrder
{
    public Guid Id { get; set; }

    public string Number { get; set; }
    public DateTime Date { get; set; }

    public string AccountRegistrationNumber { get; set; }

    public DateTime RegistrationDate { get; set; }
    public double CourierEstimateUSD { get; set; }
    public double ApprovedPriceUSD { get; set; }
    public bool RequestCreated { get; set; }
    public DateTime FinalRegistrationApprovalDate { get; set; }

    public DateTime GSMReceiptDate { get; set; }

    public string AccountsPayableId { get; set; }
    public DateTime AccountsPayableDate { get; set; }
}