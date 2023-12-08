using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListDashboardSMTA1WorkflowItem;

public interface IListDashboardSMTA1WorkflowItemMapper
{
    List<SMTA1WorkflowItemDto> Map(List<SMTA1WorkflowItem> entities);
}

public class ListDashboardSMTA1WorkflowItemMapper : IListDashboardSMTA1WorkflowItemMapper
{
    public List<SMTA1WorkflowItemDto> Map(List<SMTA1WorkflowItem> entities)
    {        
        List<SMTA1WorkflowItemDto> list = new List<SMTA1WorkflowItemDto>();

        foreach (SMTA1WorkflowItem entity in entities)
        {
            SMTA1WorkflowItemDto SMTA1WorkflowItemDto = new()
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
            list.Add(SMTA1WorkflowItemDto);
        }

        return list;
    }
}