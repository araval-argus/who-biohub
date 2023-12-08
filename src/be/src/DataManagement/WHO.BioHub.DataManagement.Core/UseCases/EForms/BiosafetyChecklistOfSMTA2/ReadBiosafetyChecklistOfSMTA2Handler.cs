using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Data.Core.UseCases.EForms.BiosafetyChecklistOfSMTA2;

public interface IReadBiosafetyChecklistOfSMTA2Handler
{
    Task<Either<ReadBiosafetyChecklistOfSMTA2QueryResponse, Errors>> Handle(ReadBiosafetyChecklistOfSMTA2Query query, CancellationToken cancellationToken);
}

public class ReadBiosafetyChecklistOfSMTA2Handler : IReadBiosafetyChecklistOfSMTA2Handler
{
    private readonly ILogger<ReadBiosafetyChecklistOfSMTA2Handler> _logger;
    private readonly ReadBiosafetyChecklistOfSMTA2QueryValidator _validator;
    private readonly IWorklistFromBioHubItemReadRepository _readRepository;
    private readonly IDocumentTemplateReadRepository _readDocumentTemplateRepository;
    private readonly ILaboratoryReadRepository _readLaboratoryRepository;
    private readonly IBioHubFacilityReadRepository _readBioHubFacilityRepository;

    public ReadBiosafetyChecklistOfSMTA2Handler(
        ILogger<ReadBiosafetyChecklistOfSMTA2Handler> logger,
        ReadBiosafetyChecklistOfSMTA2QueryValidator validator,
        IWorklistFromBioHubItemReadRepository readRepository,
        IDocumentTemplateReadRepository readDocumentTemplateRepository,
        ILaboratoryReadRepository readLaboratoryRepository,
        IBioHubFacilityReadRepository readBioHubFacilityRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _readDocumentTemplateRepository = readDocumentTemplateRepository;
        _readLaboratoryRepository = readLaboratoryRepository;
        _readBioHubFacilityRepository = readBioHubFacilityRepository;
    }

    public async Task<Either<ReadBiosafetyChecklistOfSMTA2QueryResponse, Errors>> Handle(
        ReadBiosafetyChecklistOfSMTA2Query query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {

            WorklistFromBioHubItem worklistFromBioHubItem;

            switch (query.RoleType)
            {
                case RoleType.WHO:
                    worklistFromBioHubItem = await _readRepository.ReadBiosafetyChecklistOfSMTA2Info(query.WorklistId, cancellationToken);
                    break;
                case RoleType.BioHubFacility:
                    worklistFromBioHubItem = await _readRepository.ReadBiosafetyChecklistOfSMTA2InfoForBioHubFacility(query.WorklistId, query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);

                    break;
                case RoleType.Laboratory:
                    worklistFromBioHubItem = await _readRepository.ReadBiosafetyChecklistOfSMTA2InfoForLaboratory(query.WorklistId, query.LaboratoryId.GetValueOrDefault(), cancellationToken);

                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }


            if (worklistFromBioHubItem == null)
                return new(new Errors(ErrorType.NotFound, $"Annex 2 Of SMTA 2 data with Id {query.WorklistId} not found"));
            
            DocumentTemplate biosafetyChecklistOfSmta2Document = null;
            
            if (worklistFromBioHubItem.OriginalBiosafetyChecklistDocumentTemplateId != null)
            {
                biosafetyChecklistOfSmta2Document = await _readDocumentTemplateRepository.Read(worklistFromBioHubItem.OriginalBiosafetyChecklistDocumentTemplateId.GetValueOrDefault(), cancellationToken);
            }

            BiosafetyChecklistOfSMTA2DataViewModel biosafetyChecklistOfSMTA2DataViewModel = new BiosafetyChecklistOfSMTA2DataViewModel();

            var smta2Document = worklistFromBioHubItem.WorklistFromBioHubItemDocuments.FirstOrDefault(x => x.Type == DocumentFileType.SMTA2)?.Document;

            DateTime? approvalDate = worklistFromBioHubItem.BiosafetyChecklistApprovalDate;

            var laboratory = worklistFromBioHubItem.RequestInitiationToLaboratory;
            var bioHubFacility = worklistFromBioHubItem.RequestInitiationFromBioHubFacility;
            bool isPast = worklistFromBioHubItem.IsPast == true;

            biosafetyChecklistOfSMTA2DataViewModel.ShipmentRequestNumber = worklistFromBioHubItem.ReferenceNumber;
            biosafetyChecklistOfSMTA2DataViewModel.BiosafetyChecklistOfSMTA2SignatureText = worklistFromBioHubItem.BiosafetyChecklistOfSMTA2SignatureText;
            biosafetyChecklistOfSMTA2DataViewModel.LaboratoryName = worklistFromBioHubItem.RequestInitiationToLaboratory != null ? worklistFromBioHubItem.RequestInitiationToLaboratory.Name : string.Empty;
            biosafetyChecklistOfSMTA2DataViewModel.LaboratoryAddress = worklistFromBioHubItem.RequestInitiationToLaboratory != null ? worklistFromBioHubItem.RequestInitiationToLaboratory.Address : string.Empty;
            biosafetyChecklistOfSMTA2DataViewModel.LaboratoryCountry = worklistFromBioHubItem.RequestInitiationToLaboratory != null ? worklistFromBioHubItem.RequestInitiationToLaboratory.Country.Name : string.Empty;
            biosafetyChecklistOfSMTA2DataViewModel.BioHubFacilityName = worklistFromBioHubItem.RequestInitiationFromBioHubFacility != null ? worklistFromBioHubItem.RequestInitiationFromBioHubFacility.Name : string.Empty;
            biosafetyChecklistOfSMTA2DataViewModel.BioHubFacilityAddress = worklistFromBioHubItem.RequestInitiationFromBioHubFacility != null ? worklistFromBioHubItem.RequestInitiationFromBioHubFacility.Address : string.Empty;
            biosafetyChecklistOfSMTA2DataViewModel.BioHubFacilityCountry = worklistFromBioHubItem.RequestInitiationFromBioHubFacility != null ? worklistFromBioHubItem.RequestInitiationFromBioHubFacility.Country.Name : string.Empty;
            biosafetyChecklistOfSMTA2DataViewModel.ApprovalDate = approvalDate;
            biosafetyChecklistOfSMTA2DataViewModel.BiosafetyChecklistApprovalComment = worklistFromBioHubItem.BiosafetyChecklistApprovalComment;
      
            biosafetyChecklistOfSMTA2DataViewModel.SMTA2DocumentId = smta2Document?.Id;
            biosafetyChecklistOfSMTA2DataViewModel.OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId = biosafetyChecklistOfSmta2Document?.Id;

            biosafetyChecklistOfSMTA2DataViewModel.SMTA2DocumentName = $"{smta2Document?.Name}.{smta2Document?.Extension.ToLower()}";
            biosafetyChecklistOfSMTA2DataViewModel.OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentName = $"{biosafetyChecklistOfSmta2Document?.Name}.{biosafetyChecklistOfSmta2Document?.Extension.ToLower()}";


            await SetLaboratoryInformation(biosafetyChecklistOfSMTA2DataViewModel, laboratory, approvalDate.GetValueOrDefault(), isPast, cancellationToken);
            await SetBioHubFacilityInformation(biosafetyChecklistOfSMTA2DataViewModel, bioHubFacility, approvalDate.GetValueOrDefault(), isPast, cancellationToken);
            
            biosafetyChecklistOfSMTA2DataViewModel.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s = GetBiosafetyChecklists(worklistFromBioHubItem);
            biosafetyChecklistOfSMTA2DataViewModel.BiosafetyChecklistThreadComments = GetBiosafetyChecklistComments(worklistFromBioHubItem);


            return new(new ReadBiosafetyChecklistOfSMTA2QueryResponse(biosafetyChecklistOfSMTA2DataViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Annex 2 Of SMTA 2 data");
            throw;
        }
    }


    private async Task SetLaboratoryInformation(BiosafetyChecklistOfSMTA2DataViewModel biosafetyChecklistOfSMTA2DataViewModel, Laboratory? laboratory, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (laboratory != null)
        {
            if (laboratory.LastOperationDate != null && laboratory.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastLaboratoryInformation(biosafetyChecklistOfSMTA2DataViewModel, laboratory, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentLaboratoryInformation(biosafetyChecklistOfSMTA2DataViewModel, laboratory);
            }
        }
    }

    private void SetCurrentLaboratoryInformation(BiosafetyChecklistOfSMTA2DataViewModel biosafetyChecklistOfSMTA2DataViewModel, Laboratory? laboratory)
    {
        biosafetyChecklistOfSMTA2DataViewModel.LaboratoryName = laboratory != null ? laboratory.Name : string.Empty;
        biosafetyChecklistOfSMTA2DataViewModel.LaboratoryAddress = laboratory != null ? laboratory.Address : string.Empty;
        biosafetyChecklistOfSMTA2DataViewModel.LaboratoryCountry = laboratory != null ? laboratory.Country.Name : string.Empty;
    }

    private async Task SetPastLaboratoryInformation(BiosafetyChecklistOfSMTA2DataViewModel biosafetyChecklistOfSMTA2DataViewModel, Laboratory? laboratory, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var laboratoryHistory = await _readLaboratoryRepository.ReadPastInformation(laboratory.Id, approvalDate, cancellationToken);
        if (laboratoryHistory != null)
        {
            biosafetyChecklistOfSMTA2DataViewModel.LaboratoryName = laboratoryHistory != null ? laboratoryHistory.Name : string.Empty;
            biosafetyChecklistOfSMTA2DataViewModel.LaboratoryAddress = laboratoryHistory != null ? laboratoryHistory.Address : string.Empty;
            biosafetyChecklistOfSMTA2DataViewModel.LaboratoryCountry = laboratoryHistory != null ? laboratoryHistory.Country.Name : string.Empty;
        }
        else
        {
            SetCurrentLaboratoryInformation(biosafetyChecklistOfSMTA2DataViewModel, laboratory);
        }
    }



    private async Task SetBioHubFacilityInformation(BiosafetyChecklistOfSMTA2DataViewModel biosafetyChecklistOfSMTA2DataViewModel, BioHubFacility? bioHubFacility, DateTime approvalDate, bool isPast, CancellationToken cancellationToken)
    {
        if (bioHubFacility != null)
        {
            if (bioHubFacility.LastOperationDate != null && bioHubFacility.LastOperationDate > approvalDate && !isPast)
            {
                await SetPastBioHubFacilityInformation(biosafetyChecklistOfSMTA2DataViewModel, bioHubFacility, approvalDate, cancellationToken);
            }
            else
            {
                SetCurrentBioHubFacilityInformation(biosafetyChecklistOfSMTA2DataViewModel, bioHubFacility);
            }
        }
    }

    private void SetCurrentBioHubFacilityInformation(BiosafetyChecklistOfSMTA2DataViewModel biosafetyChecklistOfSMTA2DataViewModel, BioHubFacility? bioHubFacility)
    {
        biosafetyChecklistOfSMTA2DataViewModel.BioHubFacilityName = bioHubFacility != null ? bioHubFacility.Name : string.Empty;
        biosafetyChecklistOfSMTA2DataViewModel.BioHubFacilityAddress = bioHubFacility != null ? bioHubFacility.Address : string.Empty;
        biosafetyChecklistOfSMTA2DataViewModel.BioHubFacilityCountry = bioHubFacility != null ? bioHubFacility.Country.Name : string.Empty;
    }

    private async Task SetPastBioHubFacilityInformation(BiosafetyChecklistOfSMTA2DataViewModel biosafetyChecklistOfSMTA2DataViewModel, BioHubFacility? bioHubFacility, DateTime approvalDate, CancellationToken cancellationToken)
    {
        var bioHubFacilityHistory = await _readBioHubFacilityRepository.ReadPastInformation(bioHubFacility.Id, approvalDate, cancellationToken);
        if (bioHubFacilityHistory != null)
        {
            biosafetyChecklistOfSMTA2DataViewModel.BioHubFacilityName = bioHubFacilityHistory != null ? bioHubFacilityHistory.Name : string.Empty;
            biosafetyChecklistOfSMTA2DataViewModel.BioHubFacilityAddress = bioHubFacilityHistory != null ? bioHubFacilityHistory.Address : string.Empty;
            biosafetyChecklistOfSMTA2DataViewModel.BioHubFacilityCountry = bioHubFacilityHistory != null ? bioHubFacilityHistory.Country.Name : string.Empty;
        }
        else
        {
            SetCurrentBioHubFacilityInformation(biosafetyChecklistOfSMTA2DataViewModel, bioHubFacility);
        }
    }

    private IEnumerable<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto> GetBiosafetyChecklists(WorklistFromBioHubItem entity)
    {
        List<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto> conditions = new List<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto>();

        
        foreach (var condition in entity.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s)
        {
            
            WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto biosafetyChecklistOfSMTA2ConditionDto = new WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto();
            biosafetyChecklistOfSMTA2ConditionDto.Id = condition != null ? condition.BiosafetyChecklistOfSMTA2.Id : Guid.NewGuid();
            biosafetyChecklistOfSMTA2ConditionDto.BiosafetyChecklistId = condition.BiosafetyChecklistOfSMTA2.Id;
            biosafetyChecklistOfSMTA2ConditionDto.Flag = condition != null ? condition.Flag : (condition.BiosafetyChecklistOfSMTA2.FlagPresetValue != null ? condition.BiosafetyChecklistOfSMTA2.FlagPresetValue : (condition.BiosafetyChecklistOfSMTA2.IsParentCondition == true ? condition.Flag : false));
            biosafetyChecklistOfSMTA2ConditionDto.Order = condition.BiosafetyChecklistOfSMTA2.Order;
            biosafetyChecklistOfSMTA2ConditionDto.Condition = condition.BiosafetyChecklistOfSMTA2.Condition;
            biosafetyChecklistOfSMTA2ConditionDto.Mandatory = condition.BiosafetyChecklistOfSMTA2.Mandatory;
            biosafetyChecklistOfSMTA2ConditionDto.Selectable = condition.BiosafetyChecklistOfSMTA2.Selectable;
            biosafetyChecklistOfSMTA2ConditionDto.ParentConditionId = condition.BiosafetyChecklistOfSMTA2.ParentConditionId;
            biosafetyChecklistOfSMTA2ConditionDto.ShowOnParentValue = condition.BiosafetyChecklistOfSMTA2.ShowOnParentValue;
            biosafetyChecklistOfSMTA2ConditionDto.IsParentCondition = condition.BiosafetyChecklistOfSMTA2.IsParentCondition;
            

            conditions = conditions.OrderBy(x => x.Order).ToList();

            conditions.Add(biosafetyChecklistOfSMTA2ConditionDto);

        }

        foreach (var condition in conditions)
        {
            if (condition.IsParentCondition == true || condition.ParentConditionId == null)
            {
                condition.IsVisible = true;
            }
            else
            {
                var parent = conditions.Where(x => x.BiosafetyChecklistId == condition.ParentConditionId).FirstOrDefault();
                if (parent == null)
                {
                    condition.IsVisible = false;
                }
                else
                {
                    condition.IsVisible = parent.Flag == condition.ShowOnParentValue;
                }
            }
        }

        conditions = conditions.OrderBy(x => x.Order).ToList();

        return conditions;
    }

    private IEnumerable<BiosafetyChecklistThreadCommentDto> GetBiosafetyChecklistComments(WorklistFromBioHubItem entity)
    {
        List<BiosafetyChecklistThreadCommentDto> comments = new List<BiosafetyChecklistThreadCommentDto>();

        if (entity.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments != null && entity.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments.Any())
        {
            foreach (var comment in entity.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments)
            {
                var newComment = new BiosafetyChecklistThreadCommentDto();
                newComment.Date = comment.Date;
                newComment.Text = comment.Text;
                newComment.PostedBy = comment.PostedBy != null ? comment.PostedBy.FirstName + " " + comment.PostedBy.LastName : string.Empty;
                comments.Add(newComment);
            }
        }
        comments = comments.OrderBy(x => x.Date).ToList();
        return comments;
    }


}