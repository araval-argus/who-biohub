using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItem;

public interface IListWorklistFromBioHubItemMapper
{
    List<WorklistFromBioHubItemDto> Map(List<WorklistFromBioHubItem> entities);
}

public class ListWorklistFromBioHubItemMapper : IListWorklistFromBioHubItemMapper
{
    public List<WorklistFromBioHubItemDto> Map(List<WorklistFromBioHubItem> entities)
    {
        // TODO: Implement mapper
        List<WorklistFromBioHubItemDto> list = new List<WorklistFromBioHubItemDto>();

        foreach (WorklistFromBioHubItem entity in entities)
        {
            WorklistFromBioHubItemDto worklisttobiohubitemDto = new()
            {
                Id = entity.Id,
                CurrentStatus = entity.Status,
                CurrentStatusName = entity.LastSubmissionApproved != false ? entity.Status.StatusName() : entity.Status.WorklistItemRejectedTitle(),
                PreviousStatus = entity.PreviousStatus,
                WorklistItemTitle = entity.WorklistItemTitle,
                OperationDate = entity.OperationDate,
                LastSubmissionApproved = entity.LastSubmissionApproved,
                LaboratoryName = entity.RequestInitiationToLaboratory.Name,
                LaboratoryAbbreviation = entity.RequestInitiationToLaboratory.Abbreviation,
                BioHubFacilityName = entity.RequestInitiationFromBioHubFacility != null ? entity.RequestInitiationFromBioHubFacility.Name : string.Empty,
                UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
                LastOperationUserFirstName = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.FirstName : String.Empty,
                LastOperationUserLastName = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.LastName : String.Empty,
                LastOperationUserBusinessPhone = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.BusinessPhone : String.Empty,
                LastOperationUserMobilePhone = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.MobilePhone : String.Empty,
                LastOperationUserEmail = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.Email : String.Empty,
                LastOperationUserJobTitle = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.JobTitle : String.Empty,
                Comment = entity.Comment,
                SMTA2DocumentId = entity.WorklistFromBioHubItemDocuments.Where(x => x.Type == DocumentFileType.SMTA2).FirstOrDefault()?.DocumentId,
                BioHubFacilityId = entity.RequestInitiationFromBioHubFacilityId,
                LaboratoryId = entity.RequestInitiationToLaboratoryId,
                HistoryDto = false,
                PreviousActionBy = GetPreviousOperationBy(entity),
                LaboratoryCountryName = entity.RequestInitiationToLaboratory.Country.Name,
                IsPast = entity.IsPast,
            };
            list.Add(worklisttobiohubitemDto);
        }

        return list;
    }

    private string GetPreviousOperationBy(WorklistFromBioHubItem entity)
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