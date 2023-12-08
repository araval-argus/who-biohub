using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListDashboardWorklistToBioHubItem;

public interface IListDashboardWorklistToBioHubItemMapper
{
    List<WorklistToBioHubItemDto> Map(List<WorklistToBioHubItem> entities);
}

public class ListDashboardWorklistToBioHubItemMapper : IListDashboardWorklistToBioHubItemMapper
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
                LaboratoryAbbreviation = entity.RequestInitiationFromLaboratory.Abbreviation,
                LaboratoryName = entity.RequestInitiationFromLaboratory.Name,
                UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
                BioHubFacilityId = entity.RequestInitiationToBioHubFacilityId,
                LaboratoryId = entity.RequestInitiationFromLaboratoryId,
            };
            list.Add(worklisttobiohubitemDto);
        }

        return list;
    }
}