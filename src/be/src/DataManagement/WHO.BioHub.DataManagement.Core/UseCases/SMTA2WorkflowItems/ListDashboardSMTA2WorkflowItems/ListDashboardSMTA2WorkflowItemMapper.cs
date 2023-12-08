using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListDashboardSMTA2WorkflowItem;

public interface IListDashboardSMTA2WorkflowItemMapper
{
    List<SMTA2WorkflowItemDto> Map(List<SMTA2WorkflowItem> entities);
}

public class ListDashboardSMTA2WorkflowItemMapper : IListDashboardSMTA2WorkflowItemMapper
{
    public List<SMTA2WorkflowItemDto> Map(List<SMTA2WorkflowItem> entities)
    {        
        List<SMTA2WorkflowItemDto> list = new List<SMTA2WorkflowItemDto>();

        foreach (SMTA2WorkflowItem entity in entities)
        {
            SMTA2WorkflowItemDto SMTA2WorkflowItemDto = new()
            {
                Id = entity.Id,
                CurrentStatus = entity.Status,
                CurrentStatusName = entity.LastSubmissionApproved != false ? entity.Status.StatusName() : entity.Status.WorklistItemRejectedTitle(), 
                PreviousStatus = entity.PreviousStatus,
                WorkflowItemTitle = entity.WorkflowItemTitle,
                OperationDate = entity.OperationDate,
                LaboratoryAbbreviation = entity.Laboratory.Abbreviation,
                LaboratoryName = entity.Laboratory.Name,
                UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
                LaboratoryId = entity.LaboratoryId,
            };
            list.Add(SMTA2WorkflowItemDto);
        }

        return list;
    }
}