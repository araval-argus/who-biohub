using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.WorkflowEngine
{
    public interface IWorkflowEngineUtility
    {
        string BaseUrl();
        string FormatEmailBodyBookingFormInformation(string body, BookingFormEmailInfoDto emailCustomBookingFormInfo);
        string FormatEmailBodyGeneralInformation(string body, WorkflowEmailInfoDto emailCustomInfo, RoleType roleType, string entityUrl);
        string FormatEmailBodyWarningCurrentNumberOfVialsInformation(string body, IEnumerable<MaterialsCurrentNumberOfVialsInfo> materialsCurrentNumberOfVialsInfo);
    }
}