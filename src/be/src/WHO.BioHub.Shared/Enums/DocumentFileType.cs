using System.ComponentModel;

namespace WHO.BioHub.Shared.Enums
{
    public enum DocumentFileType
    {
        [Description("SMTA 1")]
        SMTA1 = 0,
        [Description("SMTA 2")]
        SMTA2 = 1,
        [Description("Annex 2 Of SMTA 1")]
        Annex2OfSMTA1 = 2,
        [Description("Annex 2 Of SMTA 2")]
        Annex2OfSMTA2 = 3,
        [Description("Booking Form Of SMTA 1")]
        BookingFormOfSMTA1 = 4,
        [Description("Packaging List")]
        PackagingList = 5,
        [Description("Non-Commercial Invoice (Category A - UN2814)")]
        NonCommercialInvoiceCatA = 6,
        [Description("Non-Commercial Invoice (Category B - UN3373)")]
        NonCommercialInvoiceCatB = 7,
        [Description("Dangerous Goods Declaration")]
        DangerousGoodsDeclaration = 8,
        [Description("Export Permit")]
        ExportPermit = 9,
        [Description("Import Permit")]
        ImportPermit = 10,
        [Description("Other")]
        Other = 11,
        [Description("Biosafety Checklist")]
        BiosafetyChecklist = 12,
        [Description("Booking Form Of SMTA 2")]
        BookingFormOfSMTA2 = 13,
        [Description("WHO Guidance")]
        WHOGuidance = 14,

    }
}
