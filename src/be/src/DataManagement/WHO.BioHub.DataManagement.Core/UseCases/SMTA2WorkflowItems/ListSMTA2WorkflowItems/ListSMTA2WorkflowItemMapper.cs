using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItem;

public interface IListSMTA2WorkflowItemMapper
{
    List<SMTA2WorkflowItemDto> Map(List<SMTA2WorkflowItem> entities);
}

public class ListSMTA2WorkflowItemMapper : IListSMTA2WorkflowItemMapper
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
                LastSubmissionApproved = entity.LastSubmissionApproved,
                LaboratoryName = entity.Laboratory.Name,
                LaboratoryAbbreviation = entity.Laboratory.Abbreviation,
                UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
                Comment = entity.Comment,
                SMTA2DocumentId = entity.SMTA2WorkflowItemDocuments.Where(x => x.Type == DocumentFileType.SMTA2).FirstOrDefault()?.DocumentId,
                LaboratoryId = entity.LaboratoryId,
                HistoryDto = false,
                PreviousActionBy = GetPreviousOperationBy(entity),
                LaboratoryCountry = entity.Laboratory.Country.Iso3,
                IsPast = entity.IsPast,
            };
            list.Add(SMTA2WorkflowItemDto);
        }

        return list;
    }

    private string GetPreviousOperationBy(SMTA2WorkflowItem entity)
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