using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListDashboardWorklistFromBioHubItem;

public interface IListDashboardWorklistFromBioHubItemMapper
{
    List<WorklistFromBioHubItemDto> Map(List<WorklistFromBioHubItem> entities);
}

public class ListDashboardWorklistFromBioHubItemMapper : IListDashboardWorklistFromBioHubItemMapper
{
    public List<WorklistFromBioHubItemDto> Map(List<WorklistFromBioHubItem> entities)
    {
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
                LaboratoryAbbreviation = entity.RequestInitiationToLaboratory.Abbreviation,
                LaboratoryName = entity.RequestInitiationToLaboratory.Name,
                UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
                BioHubFacilityId = entity.RequestInitiationFromBioHubFacilityId,
                LaboratoryId = entity.RequestInitiationToLaboratoryId,
            };
            list.Add(worklisttobiohubitemDto);
        }

        return list;
    }
}