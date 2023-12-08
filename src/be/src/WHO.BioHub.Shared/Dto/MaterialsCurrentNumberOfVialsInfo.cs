namespace WHO.BioHub.Shared.Dto
{
    public class MaterialsCurrentNumberOfVialsInfo
    {
        public string ReferenceNumber { get; set; }
        public int? PreviousNumberOfVials { get; set; }
        public int? NewNumberOfVials { get; set; }
        public int? WarningEmailCurrentNumberOfVialsThreshold { get; set; }
    }
}
