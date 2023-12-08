using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ReadWorklistFromBioHubItem;

public interface IReadWorklistFromBioHubItemHandler
{
    Task<Either<ReadWorklistFromBioHubItemQueryResponse, Errors>> Handle(ReadWorklistFromBioHubItemQuery query, CancellationToken cancellationToken);
}

public class ReadWorklistFromBioHubItemHandler : IReadWorklistFromBioHubItemHandler
{
    private readonly ILogger<ReadWorklistFromBioHubItemHandler> _logger;
    private readonly ReadWorklistFromBioHubItemQueryValidator _validator;
    private readonly IWorklistFromBioHubItemReadRepository _readRepository;
    private readonly IReadWorklistFromBioHubItemMapper _mapper;
    private readonly IDocumentTemplateReadRepository _documentTemplateReadRepository;
    private readonly IDocumentReadRepository _documentReadRepository;

    public ReadWorklistFromBioHubItemHandler(
        ILogger<ReadWorklistFromBioHubItemHandler> logger,
        ReadWorklistFromBioHubItemQueryValidator validator,
        IWorklistFromBioHubItemReadRepository readRepository,
        IDocumentTemplateReadRepository documentTemplateReadRepository,
        IReadWorklistFromBioHubItemMapper mapper,
        IDocumentReadRepository documentReadRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
        _documentTemplateReadRepository = documentTemplateReadRepository;
        _documentReadRepository = documentReadRepository;
    }

    public async Task<Either<ReadWorklistFromBioHubItemQueryResponse, Errors>> Handle(
        ReadWorklistFromBioHubItemQuery query,
        CancellationToken cancellationToken)
    {
        WorklistFromBioHubItemDto worklistfrombiohubitemDto;
        List<Annex2OfSMTA2Condition> annex2OfSMTA2Conditions = null;

        List<BiosafetyChecklistOfSMTA2> biosafetyChecklistOfSMTA2 = null;

        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            WorklistFromBioHubItem worklistfrombiohubitem = await _readRepository.ReadByIdWithExtraInfo(query.Id, cancellationToken);
            if (worklistfrombiohubitem == null)
                return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (worklistfrombiohubitem.RequestInitiationToLaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

                case RoleType.BioHubFacility:
                    if (worklistfrombiohubitem.RequestInitiationFromBioHubFacilityId != query.UserBioHubFacilityId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

            }

            var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(worklistfrombiohubitem.Status, PermissionType.Read, worklistfrombiohubitem.IsPast);

            if (!query.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Page access forbidden"));
            }

            if (worklistfrombiohubitem.Status == WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 || worklistfrombiohubitem.Status == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval)
            {
                annex2OfSMTA2Conditions = (await _readRepository.GetAnnex2OfSMTA2ConditionList(cancellationToken)).ToList();
            }

            else if (worklistfrombiohubitem.Status == WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2 || worklistfrombiohubitem.Status == WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval)
            {
                biosafetyChecklistOfSMTA2 = (await _readRepository.GetBiosafetyChecklistOfSMTA2List(cancellationToken)).ToList();
            }

            var submitPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(worklistfrombiohubitem.Status, PermissionType.Update, worklistfrombiohubitem.IsPast);
            if (!query.UserPermissions.Contains(submitPermission))
            {
                worklistfrombiohubitemDto = _mapper.MapMinimal(worklistfrombiohubitem, query.UserPermissions, annex2OfSMTA2Conditions, biosafetyChecklistOfSMTA2);
                await SetShipmentDocumentsTemplate(worklistfrombiohubitemDto, worklistfrombiohubitem, cancellationToken);
            }

            else
            {
                worklistfrombiohubitemDto = _mapper.Map(worklistfrombiohubitem, query.UserPermissions, annex2OfSMTA2Conditions, biosafetyChecklistOfSMTA2);
                await SetSMTA2DocumentInfo(worklistfrombiohubitemDto, worklistfrombiohubitem, cancellationToken);
                await SetPreShipmentTemplates(worklistfrombiohubitemDto, worklistfrombiohubitem, cancellationToken);
                await SetShipmentDocumentsTemplate(worklistfrombiohubitemDto, worklistfrombiohubitem, cancellationToken);
            }

            return new(new ReadWorklistFromBioHubItemQueryResponse(worklistfrombiohubitemDto));
        }

        catch (Exception e)
        {
            _logger.LogError(e, "Error reading WorklistFromBioHubItem with Id {id}", query.Id);
            throw;
        }
    }

    private async Task SetShipmentDocumentsTemplate(WorklistFromBioHubItemDto worklistfrombiohubitemDto, WorklistFromBioHubItem worklistfrombiohubitem, CancellationToken cancellationToken)
    {
        if (worklistfrombiohubitem.Status >= WorklistFromBioHubStatus.WaitForPickUpCompleted)
        {
            var packagingListDocumentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.PackagingList, cancellationToken);
            if (packagingListDocumentTemplate != null)
            {
                worklistfrombiohubitemDto.PackagingListDocumentTemplateId = packagingListDocumentTemplate.Id;
                worklistfrombiohubitemDto.PackagingListDocumentTemplateName = packagingListDocumentTemplate.Name + "." + packagingListDocumentTemplate.Extension.ToLower();
            }
            var nonCommercialInvoiceCatADocumentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.NonCommercialInvoiceCatA, cancellationToken);
            if (nonCommercialInvoiceCatADocumentTemplate != null)
            {
                worklistfrombiohubitemDto.NonCommercialInvoiceCatADocumentTemplateId = nonCommercialInvoiceCatADocumentTemplate.Id;
                worklistfrombiohubitemDto.NonCommercialInvoiceCatADocumentTemplateName = nonCommercialInvoiceCatADocumentTemplate.Name + "." + nonCommercialInvoiceCatADocumentTemplate.Extension.ToLower();
            }

            var nonCommercialInvoiceCatBDocumentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.NonCommercialInvoiceCatB, cancellationToken);
            if (nonCommercialInvoiceCatBDocumentTemplate != null)
            {
                worklistfrombiohubitemDto.NonCommercialInvoiceCatBDocumentTemplateId = nonCommercialInvoiceCatBDocumentTemplate.Id;
                worklistfrombiohubitemDto.NonCommercialInvoiceCatBDocumentTemplateName = nonCommercialInvoiceCatBDocumentTemplate.Name + "." + nonCommercialInvoiceCatBDocumentTemplate.Extension.ToLower();
            }
        }
    }

    private async Task SetPreShipmentTemplates(WorklistFromBioHubItemDto worklistfrombiohubitemDto, WorklistFromBioHubItem worklistfrombiohubitem, CancellationToken cancellationToken)
    {
        if (worklistfrombiohubitem.Status == WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2)
        {
            var documentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.Annex2OfSMTA2, cancellationToken);
            if (documentTemplate != null)
            {
                worklistfrombiohubitemDto.Annex2OfSMTA2DocumentId = documentTemplate.Id;
                worklistfrombiohubitemDto.Annex2OfSMTA2DocumentName = documentTemplate.Name + "." + documentTemplate.Extension.ToLower();
                worklistfrombiohubitemDto.OriginalDocumentTemplateAnnex2OfSMTA2DocumentId = documentTemplate.Id;
            }
        }

        else if (worklistfrombiohubitem.Status == WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2)
        {
            var documentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.BiosafetyChecklist, cancellationToken);
            if (documentTemplate != null)
            {
                worklistfrombiohubitemDto.BiosafetyChecklistOfSMTA2DocumentId = documentTemplate.Id;
                worklistfrombiohubitemDto.BiosafetyChecklistOfSMTA2DocumentName = documentTemplate.Name + "." + documentTemplate.Extension.ToLower();
                worklistfrombiohubitemDto.OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId = documentTemplate.Id;
            }
        }

        else if (worklistfrombiohubitem.Status == WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2)
        {
            var documentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.BookingFormOfSMTA2, cancellationToken);
            if (documentTemplate != null)
            {
                worklistfrombiohubitemDto.BookingFormOfSMTA2DocumentId = documentTemplate.Id;
                worklistfrombiohubitemDto.BookingFormOfSMTA2DocumentName = documentTemplate.Name + "." + documentTemplate.Extension.ToLower();
                worklistfrombiohubitemDto.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId = documentTemplate.Id;
            }
        }
    }

    private async Task SetSMTA2DocumentInfo(WorklistFromBioHubItemDto worklistfrombiohubitemDto, WorklistFromBioHubItem worklistfrombiohubitem, CancellationToken cancellationToken)
    {
        if (worklistfrombiohubitem.IsPast != true && (worklistfrombiohubitem.Status == WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 || worklistfrombiohubitem.Status == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval))
        {
            var document = await _documentReadRepository.GetCurrentDocument(worklistfrombiohubitem.RequestInitiationToLaboratoryId.GetValueOrDefault(), DocumentFileType.SMTA2, cancellationToken);

            if (document != null)
            {
                worklistfrombiohubitemDto.CurrentDownloadSMTA2DocumentId = document.Id;
                worklistfrombiohubitemDto.SMTA2ApprovalStatus = document.Approved == true ? SMTAApprovalStatus.DocumentApprovalComplete : SMTAApprovalStatus.DocumentApprovalPending;
                worklistfrombiohubitemDto.CurrentDownloadSMTA2DocumentName = $"{document.Name}.{document.Extension.ToLower()}";

                if (document.Approved == true)
                {
                    worklistfrombiohubitemDto.SMTA2ApprovalDate = document.SMTA2WorkflowItemDocuments.Where(x => x.DocumentId == document.Id && x.Type == DocumentFileType.SMTA2).Select(x => x.SMTA2WorkflowItem).FirstOrDefault()?.OperationDate;
                }
            }

            else
            {
                var documentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.SMTA2, cancellationToken);
                if (documentTemplate != null)
                {
                    worklistfrombiohubitemDto.CurrentDownloadSMTA2DocumentId = documentTemplate.Id;
                    worklistfrombiohubitemDto.SMTA2ApprovalStatus = SMTAApprovalStatus.DocumentToBeSubmitted;
                    worklistfrombiohubitemDto.CurrentDownloadSMTA2DocumentName = $"{documentTemplate.Name}.{documentTemplate.Extension.ToLower()}";
                }
            }

        }
        else
        {
            var documentId = worklistfrombiohubitem.WorklistFromBioHubItemDocuments.Where(x => x.Type == DocumentFileType.SMTA2).FirstOrDefault()?.DocumentId;
            if (documentId != null)
            {
                var document = await _documentReadRepository.ReadWithSMTAInfo(documentId.GetValueOrDefault(), cancellationToken);

                worklistfrombiohubitemDto.CurrentDownloadSMTA2DocumentId = document.Id;
                worklistfrombiohubitemDto.SMTA2ApprovalStatus = SMTAApprovalStatus.DocumentApprovalComplete;
                worklistfrombiohubitemDto.CurrentDownloadSMTA2DocumentName = $"{document.Name}.{document.Extension.ToLower()}";

                if (document.Approved == true)
                {
                    worklistfrombiohubitemDto.SMTA2ApprovalDate = document.SMTA2WorkflowItemDocuments.Where(x => x.DocumentId == document.Id && x.Type == DocumentFileType.SMTA2).Select(x => x.SMTA2WorkflowItem).FirstOrDefault()?.OperationDate;
                }
            }

        }
    }
}