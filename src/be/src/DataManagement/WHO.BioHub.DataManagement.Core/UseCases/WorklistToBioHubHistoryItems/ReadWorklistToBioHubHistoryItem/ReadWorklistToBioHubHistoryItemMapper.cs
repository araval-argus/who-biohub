using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ReadWorklistToBioHubHistoryItem;

public interface IReadWorklistToBioHubHistoryItemMapper
{
    WorklistToBioHubItemDto Map(WorklistToBioHubHistoryItem entity);
}

public class ReadWorklistToBioHubHistoryItemMapper : IReadWorklistToBioHubHistoryItemMapper
{
    public WorklistToBioHubItemDto Map(WorklistToBioHubHistoryItem entity)
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
            BioHubFacilityName = entity.RequestInitiationToBioHubFacility.Name,
            UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
            Comment = entity.Comment,
            SMTA1DocumentId = entity.WorklistToBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.SMTA1).FirstOrDefault()?.Id,
            Annex2OfSMTA1DocumentId = entity.WorklistToBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA2).FirstOrDefault()?.Id,
            BioHubFacilityId = entity.RequestInitiationToBioHubFacilityId,
            LaboratoryId = entity.RequestInitiationFromLaboratoryId,
            HistoryDto = true,
            IsPast = entity.IsPast,
        };

        return worklisttobiohubitemDto;
    }
}