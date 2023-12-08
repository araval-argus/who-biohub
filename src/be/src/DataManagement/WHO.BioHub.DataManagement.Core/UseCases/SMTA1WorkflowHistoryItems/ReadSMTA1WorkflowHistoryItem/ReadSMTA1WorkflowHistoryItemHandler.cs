using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ReadSMTA1WorkflowHistoryItem;

public interface IReadSMTA1WorkflowHistoryItemHandler
{
    Task<Either<ReadSMTA1WorkflowHistoryItemQueryResponse, Errors>> Handle(ReadSMTA1WorkflowHistoryItemQuery query, CancellationToken cancellationToken);
}

public class ReadSMTA1WorkflowHistoryItemHandler : IReadSMTA1WorkflowHistoryItemHandler
{
    private readonly ILogger<ReadSMTA1WorkflowHistoryItemHandler> _logger;
    private readonly ReadSMTA1WorkflowHistoryItemQueryValidator _validator;
    private readonly ISMTA1WorkflowHistoryItemReadRepository _readRepository;
    private readonly IReadSMTA1WorkflowHistoryItemMapper _mapper;

    public ReadSMTA1WorkflowHistoryItemHandler(
        ILogger<ReadSMTA1WorkflowHistoryItemHandler> logger,
        ReadSMTA1WorkflowHistoryItemQueryValidator validator,
        ISMTA1WorkflowHistoryItemReadRepository readRepository,
        IReadSMTA1WorkflowHistoryItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadSMTA1WorkflowHistoryItemQueryResponse, Errors>> Handle(
        ReadSMTA1WorkflowHistoryItemQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            SMTA1WorkflowHistoryItem SMTA1WorkflowHistoryItem = await _readRepository.Read(query.Id, cancellationToken);
            if (SMTA1WorkflowHistoryItem == null)
                return new(new Errors(ErrorType.NotFound, $"SMTA1WorkflowHistoryItem with Id {query.Id} not found"));

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (SMTA1WorkflowHistoryItem.LaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

            }

            var requiredPermission = StatusPermissionMapper.SMTA1WorkflowStatusReadPermissionMapper[SMTA1WorkflowHistoryItem.Status];

            if (!query.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Page access forbidden"));
            }

            var SMTA1WorkflowHistoryItemDto = _mapper.Map(SMTA1WorkflowHistoryItem);

            return new(new ReadSMTA1WorkflowHistoryItemQueryResponse(SMTA1WorkflowHistoryItemDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading SMTA1WorkflowHistoryItem with Id {id}", query.Id);
            throw;
        }
    }
}