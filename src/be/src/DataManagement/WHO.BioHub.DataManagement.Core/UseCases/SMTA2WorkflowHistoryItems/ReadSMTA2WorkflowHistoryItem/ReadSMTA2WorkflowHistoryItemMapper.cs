using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ReadSMTA2WorkflowHistoryItem;

public interface IReadSMTA2WorkflowHistoryItemMapper
{
    SMTA2WorkflowItemDto Map(SMTA2WorkflowHistoryItem entity);
}

public class ReadSMTA2WorkflowHistoryItemMapper : IReadSMTA2WorkflowHistoryItemMapper
{
    public SMTA2WorkflowItemDto Map(SMTA2WorkflowHistoryItem entity)
    {        

        SMTA2WorkflowItemDto worklisttobiohubitemDto = new()
        {
            Id = entity.Id,
            CurrentStatus = entity.Status,
            CurrentStatusName = entity.LastSubmissionApproved != false ? entity.Status.StatusName() : entity.Status.WorklistItemRejectedTitle(), 
            PreviousStatus = entity.PreviousStatus,
            WorkflowItemTitle = entity.WorkflowItemTitle,
            OperationDate = entity.OperationDate,
            LastSubmissionApproved = entity.LastSubmissionApproved,
            LaboratoryName = entity.Laboratory.Name,
            UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
            Comment = entity.Comment,
            SMTA2DocumentId = entity.SMTA2WorkflowHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.SMTA2).FirstOrDefault()?.Id,
            LaboratoryId = entity.LaboratoryId,
            HistoryDto = true,
            IsPast = entity.IsPast,
        };

        return worklisttobiohubitemDto;
    }
}