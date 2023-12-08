using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItem;

public interface IListWorklistToBioHubItemMapper
{
    List<WorklistToBioHubItemDto> Map(List<WorklistToBioHubItem> entities);
}

public class ListWorklistToBioHubItemMapper : IListWorklistToBioHubItemMapper
{
    public List<WorklistToBioHubItemDto> Map(List<WorklistToBioHubItem> entities)
    {
        List<WorklistToBioHubItemDto> list = new List<WorklistToBioHubItemDto>();

        foreach (WorklistToBioHubItem entity in entities)
        {
            WorklistToBioHubItemDto worklisttobiohubitemDto = new()
            {
                Id = entity.Id,
                CurrentStatus = entity.Status,
                CurrentStatusName = entity.LastSubmissionApproved != false ? entity.Status.StatusName() : entity.Status.WorklistItemRejectedTitle(),
                PreviousStatus = entity.PreviousStatus,
                WorklistItemTitle = entity.WorklistItemTitle,
                OperationDate = entity.OperationDate,
                LastSubmissionApproved = entity.LastSubmissionApproved,
                LaboratoryName = entity.RequestInitiationFromLaboratory.Name,
                LaboratoryAbbreviation = entity.RequestInitiationFromLaboratory.Abbreviation,
                BioHubFacilityName = entity.RequestInitiationToBioHubFacility != null ? entity.RequestInitiationToBioHubFacility.Name : string.Empty,
                UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
                LastOperationUserFirstName = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.FirstName : String.Empty,
                LastOperationUserLastName = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.LastName : String.Empty,
                LastOperationUserBusinessPhone = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.BusinessPhone : String.Empty,
                LastOperationUserMobilePhone = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.MobilePhone : String.Empty,
                LastOperationUserEmail = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.Email : String.Empty,
                LastOperationUserJobTitle = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.JobTitle : String.Empty,
                Comment = entity.Comment,
                SMTA1DocumentId = entity.WorklistToBioHubItemDocuments.Where(x => x.Type == DocumentFileType.SMTA1).FirstOrDefault()?.DocumentId,
                BioHubFacilityId = entity.RequestInitiationToBioHubFacilityId,
                LaboratoryId = entity.RequestInitiationFromLaboratoryId,
                HistoryDto = false,
                PreviousActionBy = GetPreviousOperationBy(entity),
                LaboratoryCountryName = entity.RequestInitiationFromLaboratory.Country.Name,
                IsPast = entity.IsPast,
            };
            list.Add(worklisttobiohubitemDto);
        }

        return list;
    }

    private string GetPreviousOperationBy(WorklistToBioHubItem entity)
    {
        if (entity.LastOperationUser.Role.RoleType == RoleType.WHO)
        {
            return entity.LastOperationUser.Role.Name;
        }

        if (entity.LastOperationUser.Laboratory != null)
        {
            return entity.LastOperationUser.Laboratory.Name;
        }
        else
        {
            return entity.LastOperationUser.BioHubFacility.Name;
        }
    }
}