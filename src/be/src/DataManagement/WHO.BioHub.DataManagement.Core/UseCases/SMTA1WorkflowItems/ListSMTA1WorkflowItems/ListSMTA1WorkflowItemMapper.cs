using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItem;

public interface IListSMTA1WorkflowItemMapper
{
    List<SMTA1WorkflowItemDto> Map(List<SMTA1WorkflowItem> entities);
}

public class ListSMTA1WorkflowItemMapper : IListSMTA1WorkflowItemMapper
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
                LastSubmissionApproved = entity.LastSubmissionApproved,
                LaboratoryName = entity.Laboratory.Name,
                LaboratoryAbbreviation = entity.Laboratory.Abbreviation,
                UserName = $"{entity.LastOperationUser.FirstName} {entity.LastOperationUser.LastName}",
                Comment = entity.Comment,
                SMTA1DocumentId = entity.SMTA1WorkflowItemDocuments.Where(x => x.Type == DocumentFileType.SMTA1).FirstOrDefault()?.DocumentId,
                LaboratoryId = entity.LaboratoryId,
                HistoryDto = false,
                PreviousActionBy = GetPreviousOperationBy(entity),                
                LaboratoryCountry = entity.Laboratory.Country.Iso3,
                IsPast = entity.IsPast,
            };
            list.Add(SMTA1WorkflowItemDto);
        }

        return list;
    }

    private string GetPreviousOperationBy(SMTA1WorkflowItem entity)
    {
        if (entity.LastOperationUser.Role.RoleType == RoleType.WHO)
        {
            return entity.LastOperationUser.Role.Name;
        }

        if (entity.LastOperationUser.Laboratory != null)
        {
            return entity.LastOperationUser.Laboratory.Name;
        }

        return string.Empty;
    }    
}