using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ReadSMTA1WorkflowHistoryItem;

public interface IReadSMTA1WorkflowHistoryItemMapper
{
    SMTA1WorkflowItemDto Map(SMTA1WorkflowHistoryItem entity);
}

public class ReadSMTA1WorkflowHistoryItemMapper : IReadSMTA1WorkflowHistoryItemMapper
{
    public SMTA1WorkflowItemDto Map(SMTA1WorkflowHistoryItem entity)
    {        

        SMTA1WorkflowItemDto worklisttobiohubitemDto = new()
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
            SMTA1DocumentId = entity.SMTA1WorkflowHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.SMTA1).FirstOrDefault()?.Id,
            LaboratoryId = entity.LaboratoryId,
            HistoryDto = true,
            IsPast = entity.IsPast,
        };

        return worklisttobiohubitemDto;
    }
}