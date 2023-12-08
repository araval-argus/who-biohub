using System.ComponentModel;

namespace WHO.BioHub.Shared.Enums
{
    public enum ResourceFileType
    {
        [Description("Standard Materials Transfer Agreement (SMTA) 1")]
        SMTA1 = 0,
        [Description("Standard Materials Transfer Agreement (SMTA) 2")]
        SMTA2 = 1,
        [Description("Shipment-related Documents")]
        Shipment = 2,
        [Description("Biosafety and Biosecurity Documents")]
        BiosafetyAndBiosecurity = 3,       

    }
}
