using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ReadWorklistToBioHubItem;

public interface IReadWorklistToBioHubItemHandler
{
    Task<Either<ReadWorklistToBioHubItemQueryResponse, Errors>> Handle(ReadWorklistToBioHubItemQuery query, CancellationToken cancellationToken);
}

public class ReadWorklistToBioHubItemHandler : IReadWorklistToBioHubItemHandler
{
    private readonly ILogger<ReadWorklistToBioHubItemHandler> _logger;
    private readonly ReadWorklistToBioHubItemQueryValidator _validator;
    private readonly IWorklistToBioHubItemReadRepository _readRepository;
    private readonly IReadWorklistToBioHubItemMapper _mapper;
    private readonly IDocumentTemplateReadRepository _documentTemplateReadRepository;
    private readonly IDocumentReadRepository _documentReadRepository;

    public ReadWorklistToBioHubItemHandler(
        ILogger<ReadWorklistToBioHubItemHandler> logger,
        ReadWorklistToBioHubItemQueryValidator validator,
        IWorklistToBioHubItemReadRepository readRepository,
        IDocumentTemplateReadRepository documentTemplateReadRepository,
        IReadWorklistToBioHubItemMapper mapper,
        IDocumentReadRepository documentReadRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
        _documentTemplateReadRepository = documentTemplateReadRepository;
        _documentReadRepository = documentReadRepository;
    }

    public async Task<Either<ReadWorklistToBioHubItemQueryResponse, Errors>> Handle(
        ReadWorklistToBioHubItemQuery query,
        CancellationToken cancellationToken)
    {

        WorklistToBioHubItemDto worklisttobiohubitemDto;

        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));


        try
        {
            WorklistToBioHubItem worklisttobiohubitem = await _readRepository.ReadByIdWithExtraInfo(query.Id, cancellationToken);
            if (worklisttobiohubitem == null)
                return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (worklisttobiohubitem.RequestInitiationFromLaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

                case RoleType.BioHubFacility:
                    if (worklisttobiohubitem.RequestInitiationToBioHubFacilityId != query.UserBioHubFacilityId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

            }

            var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(worklisttobiohubitem.Status, PermissionType.Read, worklisttobiohubitem.IsPast);

            if (!query.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Page access forbidden"));
            }

            var submitPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(worklisttobiohubitem.Status, PermissionType.Update, worklisttobiohubitem.IsPast);

            if (!query.UserPermissions.Contains(submitPermission))
            {
                worklisttobiohubitemDto = _mapper.MapMinimal(worklisttobiohubitem, query.UserPermissions);
                await SetShipmentDocumentsTemplate(worklisttobiohubitemDto, worklisttobiohubitem, cancellationToken);
            }

            else
            {
                worklisttobiohubitemDto = _mapper.Map(worklisttobiohubitem, query.UserPermissions);
                await SetSMTA1DocumentInfo(worklisttobiohubitemDto, worklisttobiohubitem, cancellationToken);
                await SetPreShipmentTemplates(worklisttobiohubitemDto, worklisttobiohubitem, cancellationToken);
                await SetShipmentDocumentsTemplate(worklisttobiohubitemDto, worklisttobiohubitem, cancellationToken);
            }

            return new(new ReadWorklistToBioHubItemQueryResponse(worklisttobiohubitemDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading WorklistToBioHubItem with Id {id}", query.Id);
            throw;
        }
    }

    private async Task SetPreShipmentTemplates(WorklistToBioHubItemDto worklisttobiohubitemDto, WorklistToBioHubItem worklisttobiohubitem, CancellationToken cancellationToken)
    {
        if (worklisttobiohubitem.Status == WorklistToBioHubStatus.SubmitAnnex2OfSMTA1)
        {
            var documentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.Annex2OfSMTA1, cancellationToken);
            if (documentTemplate != null)
            {
                worklisttobiohubitemDto.Annex2OfSMTA1DocumentId = documentTemplate.Id;
                worklisttobiohubitemDto.Annex2OfSMTA1DocumentName = documentTemplate.Name + "." + documentTemplate.Extension.ToLower();
                worklisttobiohubitemDto.OriginalDocumentTemplateAnnex2OfSMTA1DocumentId = documentTemplate.Id;
            }
        }

        else if (worklisttobiohubitem.Status == WorklistToBioHubStatus.SubmitBookingFormOfSMTA1)
        {
            var documentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.BookingFormOfSMTA1, cancellationToken);
            if (documentTemplate != null)
            {
                worklisttobiohubitemDto.BookingFormOfSMTA1DocumentId = documentTemplate.Id;
                worklisttobiohubitemDto.BookingFormOfSMTA1DocumentName = documentTemplate.Name + "." + documentTemplate.Extension.ToLower();
                worklisttobiohubitemDto.OriginalDocumentTemplateBookingFormOfSMTA1DocumentId = documentTemplate.Id;
            }
        }
    }

    private async Task SetShipmentDocumentsTemplate(WorklistToBioHubItemDto worklisttobiohubitemDto, WorklistToBioHubItem worklisttobiohubitem, CancellationToken cancellationToken)
    {
        if (worklisttobiohubitem.Status >= WorklistToBioHubStatus.WaitForPickUpCompleted)
        {
            var packagingListDocumentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.PackagingList, cancellationToken);
            if (packagingListDocumentTemplate != null)
            {
                worklisttobiohubitemDto.PackagingListDocumentTemplateId = packagingListDocumentTemplate.Id;
                worklisttobiohubitemDto.PackagingListDocumentTemplateName = packagingListDocumentTemplate.Name + "." + packagingListDocumentTemplate.Extension.ToLower();
            }
            var nonCommercialInvoiceCatADocumentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.NonCommercialInvoiceCatA, cancellationToken);
            if (nonCommercialInvoiceCatADocumentTemplate != null)
            {
                worklisttobiohubitemDto.NonCommercialInvoiceCatADocumentTemplateId = nonCommercialInvoiceCatADocumentTemplate.Id;
                worklisttobiohubitemDto.NonCommercialInvoiceCatADocumentTemplateName = nonCommercialInvoiceCatADocumentTemplate.Name + "." + nonCommercialInvoiceCatADocumentTemplate.Extension.ToLower();
            }
            var nonCommercialInvoiceCatBDocumentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.NonCommercialInvoiceCatB, cancellationToken);
            if (nonCommercialInvoiceCatBDocumentTemplate != null)
            {
                worklisttobiohubitemDto.NonCommercialInvoiceCatBDocumentTemplateId = nonCommercialInvoiceCatBDocumentTemplate.Id;
                worklisttobiohubitemDto.NonCommercialInvoiceCatBDocumentTemplateName = nonCommercialInvoiceCatBDocumentTemplate.Name + "." + nonCommercialInvoiceCatBDocumentTemplate.Extension.ToLower();
            }
        }
    }

    private async Task SetSMTA1DocumentInfo(WorklistToBioHubItemDto worklisttobiohubitemDto, WorklistToBioHubItem worklisttobiohubitem, CancellationToken cancellationToken)
    {
        if (worklisttobiohubitem.IsPast != true && (worklisttobiohubitem.Status == WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 || worklisttobiohubitem.Status == WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval))
        {
            var document = await _documentReadRepository.GetCurrentDocument(worklisttobiohubitem.RequestInitiationFromLaboratoryId.GetValueOrDefault(), DocumentFileType.SMTA1, cancellationToken);

            if (document != null)
            {
                worklisttobiohubitemDto.CurrentDownloadSMTA1DocumentId = document.Id;
                worklisttobiohubitemDto.SMTA1ApprovalStatus = document.Approved == true ? SMTAApprovalStatus.DocumentApprovalComplete : SMTAApprovalStatus.DocumentApprovalPending;
                worklisttobiohubitemDto.CurrentDownloadSMTA1DocumentName = $"{document.Name}.{document.Extension.ToLower()}";

                if (document.Approved == true)
                {
                    worklisttobiohubitemDto.SMTA1ApprovalDate = document.SMTA1WorkflowItemDocuments.Where(x => x.DocumentId == document.Id && x.Type == DocumentFileType.SMTA1).Select(x => x.SMTA1WorkflowItem).FirstOrDefault()?.OperationDate;
                }
            }

            else
            {
                var documentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.SMTA1, cancellationToken);
                if (documentTemplate != null)
                {
                    worklisttobiohubitemDto.CurrentDownloadSMTA1DocumentId = documentTemplate.Id;
                    worklisttobiohubitemDto.SMTA1ApprovalStatus = SMTAApprovalStatus.DocumentToBeSubmitted;
                    worklisttobiohubitemDto.CurrentDownloadSMTA1DocumentName = $"{documentTemplate.Name}.{documentTemplate.Extension.ToLower()}";
                }
            }
        }

        else
        {
            var documentId = worklisttobiohubitem.WorklistToBioHubItemDocuments.Where(x => x.Type == DocumentFileType.SMTA1).FirstOrDefault()?.DocumentId;
            if (documentId != null)
            {
                var document = await _documentReadRepository.ReadWithSMTAInfo(documentId.GetValueOrDefault(), cancellationToken);

                worklisttobiohubitemDto.CurrentDownloadSMTA1DocumentId = document.Id;
                worklisttobiohubitemDto.SMTA1ApprovalStatus = SMTAApprovalStatus.DocumentApprovalComplete;
                worklisttobiohubitemDto.CurrentDownloadSMTA1DocumentName = $"{document.Name}.{document.Extension.ToLower()}";

                if (document.Approved == true)
                {
                    worklisttobiohubitemDto.SMTA1ApprovalDate = document.SMTA1WorkflowItemDocuments.Where(x => x.DocumentId == document.Id && x.Type == DocumentFileType.SMTA1).Select(x => x.SMTA1WorkflowItem).FirstOrDefault()?.OperationDate;
                }
            }

        }
    }
}