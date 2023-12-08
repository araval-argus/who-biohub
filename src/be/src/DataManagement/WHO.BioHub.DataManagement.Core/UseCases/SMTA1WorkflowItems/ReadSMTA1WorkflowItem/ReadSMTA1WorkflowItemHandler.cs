using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ReadSMTA1WorkflowItem;

public interface IReadSMTA1WorkflowItemHandler
{
    Task<Either<ReadSMTA1WorkflowItemQueryResponse, Errors>> Handle(ReadSMTA1WorkflowItemQuery query, CancellationToken cancellationToken);
}

public class ReadSMTA1WorkflowItemHandler : IReadSMTA1WorkflowItemHandler
{
    private readonly ILogger<ReadSMTA1WorkflowItemHandler> _logger;
    private readonly ReadSMTA1WorkflowItemQueryValidator _validator;
    private readonly ISMTA1WorkflowItemReadRepository _readRepository;
    private readonly IReadSMTA1WorkflowItemMapper _mapper;
    private readonly IDocumentTemplateReadRepository _documentTemplateReadRepository;
    private readonly IDocumentReadRepository _documentReadRepository;

    public ReadSMTA1WorkflowItemHandler(
        ILogger<ReadSMTA1WorkflowItemHandler> logger,
        ReadSMTA1WorkflowItemQueryValidator validator,
        ISMTA1WorkflowItemReadRepository readRepository,
        IDocumentTemplateReadRepository documentTemplateReadRepository,
        IReadSMTA1WorkflowItemMapper mapper,
        IDocumentReadRepository documentReadRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
        _documentTemplateReadRepository = documentTemplateReadRepository;
        _documentReadRepository = documentReadRepository;
    }

    public async Task<Either<ReadSMTA1WorkflowItemQueryResponse, Errors>> Handle(
        ReadSMTA1WorkflowItemQuery query,
        CancellationToken cancellationToken)
    {

        SMTA1WorkflowItemDto SMTA1WorkflowItemDto;

        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));


        try
        {
            SMTA1WorkflowItem SMTA1WorkflowItem = await _readRepository.ReadByIdWithExtraInfo(query.Id, cancellationToken);
            if (SMTA1WorkflowItem == null)
                return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (SMTA1WorkflowItem.LaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;


            }

            var requiredPermission = StatusPermissionMapper.GetSMTA1WorkflowStatusPermission(SMTA1WorkflowItem.Status, PermissionType.Read, SMTA1WorkflowItem.IsPast);

            if (!query.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Page access forbidden"));
            }

            var submitPermission = StatusPermissionMapper.SMTA1WorkflowStatusUpdatePermissionMapper[SMTA1WorkflowItem.Status];
            if (!query.UserPermissions.Contains(submitPermission))
            {
                SMTA1WorkflowItemDto = _mapper.MapMinimal(SMTA1WorkflowItem, query.UserPermissions);
                return new(new ReadSMTA1WorkflowItemQueryResponse(SMTA1WorkflowItemDto));

            }

            SMTA1WorkflowItemDto = _mapper.Map(SMTA1WorkflowItem, query.UserPermissions);
            var laboratoryId = SMTA1WorkflowItem.LaboratoryId.GetValueOrDefault();

            if (SMTA1WorkflowItem.IsPast != true)
            {
                SMTA1WorkflowItemDto.CanSkipSMTA1Phase = await _documentReadRepository.IsDocumentSignedByLaboratoryId(laboratoryId, DocumentFileType.SMTA1, cancellationToken);
            }

            if (SMTA1WorkflowItem.Status == SMTA1WorkflowStatus.SubmitSMTA1)
            {
                var documentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.SMTA1, cancellationToken);
                if (documentTemplate != null)
                {
                    SMTA1WorkflowItemDto.SMTA1DocumentId = documentTemplate.Id;
                    SMTA1WorkflowItemDto.SMTA1DocumentName = documentTemplate.Name + "." + documentTemplate.Extension.ToLower();
                    SMTA1WorkflowItemDto.OriginalDocumentTemplateSMTA1DocumentId = SMTA1WorkflowItem.IsPast != true ? documentTemplate.Id : null;
                }
            }

            return new(new ReadSMTA1WorkflowItemQueryResponse(SMTA1WorkflowItemDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading SMTA1WorkflowItem with Id {id}", query.Id);
            throw;
        }
    }
}