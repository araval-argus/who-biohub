using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ReadSMTA2WorkflowItem;

public interface IReadSMTA2WorkflowItemHandler
{
    Task<Either<ReadSMTA2WorkflowItemQueryResponse, Errors>> Handle(ReadSMTA2WorkflowItemQuery query, CancellationToken cancellationToken);
}

public class ReadSMTA2WorkflowItemHandler : IReadSMTA2WorkflowItemHandler
{
    private readonly ILogger<ReadSMTA2WorkflowItemHandler> _logger;
    private readonly ReadSMTA2WorkflowItemQueryValidator _validator;
    private readonly ISMTA2WorkflowItemReadRepository _readRepository;
    private readonly IReadSMTA2WorkflowItemMapper _mapper;
    private readonly IDocumentTemplateReadRepository _documentTemplateReadRepository;
    private readonly IDocumentReadRepository _documentReadRepository;

    public ReadSMTA2WorkflowItemHandler(
        ILogger<ReadSMTA2WorkflowItemHandler> logger,
        ReadSMTA2WorkflowItemQueryValidator validator,
        ISMTA2WorkflowItemReadRepository readRepository,
        IDocumentTemplateReadRepository documentTemplateReadRepository,
        IReadSMTA2WorkflowItemMapper mapper,
        IDocumentReadRepository documentReadRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
        _documentTemplateReadRepository = documentTemplateReadRepository;
        _documentReadRepository = documentReadRepository;
    }

    public async Task<Either<ReadSMTA2WorkflowItemQueryResponse, Errors>> Handle(
        ReadSMTA2WorkflowItemQuery query,
        CancellationToken cancellationToken)
    {

        SMTA2WorkflowItemDto SMTA2WorkflowItemDto;

        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));


        try
        {
            SMTA2WorkflowItem SMTA2WorkflowItem = await _readRepository.ReadByIdWithExtraInfo(query.Id, cancellationToken);
            if (SMTA2WorkflowItem == null)
                return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (SMTA2WorkflowItem.LaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

            }

            var requiredPermission = StatusPermissionMapper.GetSMTA2WorkflowStatusPermission(SMTA2WorkflowItem.Status, PermissionType.Read, SMTA2WorkflowItem.IsPast);

            if (!query.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Page access forbidden"));
            }

            var submitPermission = StatusPermissionMapper.GetSMTA2WorkflowStatusPermission(SMTA2WorkflowItem.Status, PermissionType.Update, SMTA2WorkflowItem.IsPast);
            if (!query.UserPermissions.Contains(submitPermission))
            {
                SMTA2WorkflowItemDto = _mapper.MapMinimal(SMTA2WorkflowItem, query.UserPermissions);
                return new(new ReadSMTA2WorkflowItemQueryResponse(SMTA2WorkflowItemDto));

            }

            SMTA2WorkflowItemDto = _mapper.Map(SMTA2WorkflowItem, query.UserPermissions);

            var laboratoryId = SMTA2WorkflowItem.LaboratoryId.GetValueOrDefault();

            if (SMTA2WorkflowItem.IsPast != true)
            {
                SMTA2WorkflowItemDto.CanSkipSMTA2Phase = await _documentReadRepository.IsDocumentSignedByLaboratoryId(laboratoryId, DocumentFileType.SMTA2, cancellationToken);
            }

            if (SMTA2WorkflowItem.Status == SMTA2WorkflowStatus.SubmitSMTA2)
            {
                var documentTemplate = await _documentTemplateReadRepository.GetCurrentDocumentTemplateByType(DocumentFileType.SMTA2, cancellationToken);
                if (documentTemplate != null)
                {
                    SMTA2WorkflowItemDto.SMTA2DocumentId = documentTemplate.Id;
                    SMTA2WorkflowItemDto.SMTA2DocumentName = documentTemplate.Name + "." + documentTemplate.Extension.ToLower();
                    SMTA2WorkflowItemDto.OriginalDocumentTemplateSMTA2DocumentId = SMTA2WorkflowItem.IsPast != true ? documentTemplate.Id : null;
                }
            }

            return new(new ReadSMTA2WorkflowItemQueryResponse(SMTA2WorkflowItemDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading SMTA2WorkflowItem with Id {id}", query.Id);
            throw;
        }
    }
}