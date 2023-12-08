using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ReadSMTA2WorkflowHistoryItem;

public interface IReadSMTA2WorkflowHistoryItemHandler
{
    Task<Either<ReadSMTA2WorkflowHistoryItemQueryResponse, Errors>> Handle(ReadSMTA2WorkflowHistoryItemQuery query, CancellationToken cancellationToken);
}

public class ReadSMTA2WorkflowHistoryItemHandler : IReadSMTA2WorkflowHistoryItemHandler
{
    private readonly ILogger<ReadSMTA2WorkflowHistoryItemHandler> _logger;
    private readonly ReadSMTA2WorkflowHistoryItemQueryValidator _validator;
    private readonly ISMTA2WorkflowHistoryItemReadRepository _readRepository;
    private readonly IReadSMTA2WorkflowHistoryItemMapper _mapper;

    public ReadSMTA2WorkflowHistoryItemHandler(
        ILogger<ReadSMTA2WorkflowHistoryItemHandler> logger,
        ReadSMTA2WorkflowHistoryItemQueryValidator validator,
        ISMTA2WorkflowHistoryItemReadRepository readRepository,
        IReadSMTA2WorkflowHistoryItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadSMTA2WorkflowHistoryItemQueryResponse, Errors>> Handle(
        ReadSMTA2WorkflowHistoryItemQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            SMTA2WorkflowHistoryItem SMTA2WorkflowHistoryItem = await _readRepository.Read(query.Id, cancellationToken);
            if (SMTA2WorkflowHistoryItem == null)
                return new(new Errors(ErrorType.NotFound, $"SMTA2WorkflowHistoryItem with Id {query.Id} not found"));

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (SMTA2WorkflowHistoryItem.LaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;


            }

            var requiredPermission = StatusPermissionMapper.SMTA2WorkflowStatusReadPermissionMapper[SMTA2WorkflowHistoryItem.Status];

            if (!query.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Page access forbidden"));
            }

            var SMTA2WorkflowHistoryItemDto = _mapper.Map(SMTA2WorkflowHistoryItem);

            return new(new ReadSMTA2WorkflowHistoryItemQueryResponse(SMTA2WorkflowHistoryItemDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading SMTA2WorkflowHistoryItem with Id {id}", query.Id);
            throw;
        }
    }
}