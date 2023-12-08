using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ReadWorklistFromBioHubHistoryItem;

public interface IReadWorklistFromBioHubHistoryItemHandler
{
    Task<Either<ReadWorklistFromBioHubHistoryItemQueryResponse, Errors>> Handle(ReadWorklistFromBioHubHistoryItemQuery query, CancellationToken cancellationToken);
}

public class ReadWorklistFromBioHubHistoryItemHandler : IReadWorklistFromBioHubHistoryItemHandler
{
    private readonly ILogger<ReadWorklistFromBioHubHistoryItemHandler> _logger;
    private readonly ReadWorklistFromBioHubHistoryItemQueryValidator _validator;
    private readonly IWorklistFromBioHubHistoryItemReadRepository _readRepository;

    public ReadWorklistFromBioHubHistoryItemHandler(
        ILogger<ReadWorklistFromBioHubHistoryItemHandler> logger,
        ReadWorklistFromBioHubHistoryItemQueryValidator validator,
        IWorklistFromBioHubHistoryItemReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadWorklistFromBioHubHistoryItemQueryResponse, Errors>> Handle(
        ReadWorklistFromBioHubHistoryItemQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            WorklistFromBioHubHistoryItem worklistfrombiohubhistoryitem = await _readRepository.Read(query.Id, cancellationToken);
            if (worklistfrombiohubhistoryitem == null)
                return new(new Errors(ErrorType.NotFound, $"WorklistFromBioHubHistoryItem with Id {query.Id} not found"));

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (worklistfrombiohubhistoryitem.RequestInitiationToLaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

                case RoleType.BioHubFacility:
                    if (worklistfrombiohubhistoryitem.RequestInitiationFromBioHubFacilityId != query.UserBioHubFacilityId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

            }
            return new(new ReadWorklistFromBioHubHistoryItemQueryResponse(worklistfrombiohubhistoryitem));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading WorklistFromBioHubHistoryItem with Id {id}", query.Id);
            throw;
        }
    }
}