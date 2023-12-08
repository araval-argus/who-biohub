using System.ComponentModel;

namespace WHO.BioHub.Shared.Enums
{
    public enum EFormType
    {        
        [Description("Annex 2 Of SMTA 1")]
        Annex2OfSMTA1 = 2,
        [Description("Annex 2 Of SMTA 2")]
        Annex2OfSMTA2 = 3,
        [Description("Booking Form Of SMTA 1")]
        BookingFormOfSMTA1 = 4,        
        [Description("Biosafety Checklist")]
        BiosafetyChecklistOfSMTA2 = 12,
        [Description("Booking Form Of SMTA 2")]
        BookingFormOfSMTA2 = 13,       

    }
}
