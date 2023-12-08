using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ReadWorklistToBioHubHistoryItem;

public interface IReadWorklistToBioHubHistoryItemHandler
{
    Task<Either<ReadWorklistToBioHubHistoryItemQueryResponse, Errors>> Handle(ReadWorklistToBioHubHistoryItemQuery query, CancellationToken cancellationToken);
}

public class ReadWorklistToBioHubHistoryItemHandler : IReadWorklistToBioHubHistoryItemHandler
{
    private readonly ILogger<ReadWorklistToBioHubHistoryItemHandler> _logger;
    private readonly ReadWorklistToBioHubHistoryItemQueryValidator _validator;
    private readonly IWorklistToBioHubHistoryItemReadRepository _readRepository;
    private readonly IReadWorklistToBioHubHistoryItemMapper _mapper;

    public ReadWorklistToBioHubHistoryItemHandler(
        ILogger<ReadWorklistToBioHubHistoryItemHandler> logger,
        ReadWorklistToBioHubHistoryItemQueryValidator validator,
        IWorklistToBioHubHistoryItemReadRepository readRepository,
        IReadWorklistToBioHubHistoryItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadWorklistToBioHubHistoryItemQueryResponse, Errors>> Handle(
        ReadWorklistToBioHubHistoryItemQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            WorklistToBioHubHistoryItem worklisttobiohubhistoryitem = await _readRepository.Read(query.Id, cancellationToken);
            if (worklisttobiohubhistoryitem == null)
                return new(new Errors(ErrorType.NotFound, $"WorklistToBioHubHistoryItem with Id {query.Id} not found"));

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (worklisttobiohubhistoryitem.RequestInitiationFromLaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

                case RoleType.BioHubFacility:
                    if (worklisttobiohubhistoryitem.RequestInitiationToBioHubFacilityId != query.UserBioHubFacilityId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

            }

            var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(worklisttobiohubhistoryitem.Status, PermissionType.Read, worklisttobiohubhistoryitem.IsPast);

            if (!query.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Page access forbidden"));
            }

            var worklisttobiohubhistoryitemDto = _mapper.Map(worklisttobiohubhistoryitem);

            return new(new ReadWorklistToBioHubHistoryItemQueryResponse(worklisttobiohubhistoryitemDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading WorklistToBioHubHistoryItem with Id {id}", query.Id);
            throw;
        }
    }
}