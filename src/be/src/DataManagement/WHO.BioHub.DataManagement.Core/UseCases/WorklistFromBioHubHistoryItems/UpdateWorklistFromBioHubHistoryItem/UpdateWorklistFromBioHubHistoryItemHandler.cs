using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.UpdateWorklistFromBioHubHistoryItem;

public interface IUpdateWorklistFromBioHubHistoryItemHandler
{
    Task<Either<UpdateWorklistFromBioHubHistoryItemCommandResponse, Errors>> Handle(UpdateWorklistFromBioHubHistoryItemCommand command, CancellationToken cancellationToken);
}

public class UpdateWorklistFromBioHubHistoryItemHandler : IUpdateWorklistFromBioHubHistoryItemHandler
{
    private readonly ILogger<UpdateWorklistFromBioHubHistoryItemHandler> _logger;
    private readonly UpdateWorklistFromBioHubHistoryItemCommandValidator _validator;
    private readonly IUpdateWorklistFromBioHubHistoryItemMapper _mapper;
    private readonly IWorklistFromBioHubHistoryItemWriteRepository _writeRepository;

    public UpdateWorklistFromBioHubHistoryItemHandler(
        ILogger<UpdateWorklistFromBioHubHistoryItemHandler> logger,
        UpdateWorklistFromBioHubHistoryItemCommandValidator validator,
        IUpdateWorklistFromBioHubHistoryItemMapper mapper,
        IWorklistFromBioHubHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateWorklistFromBioHubHistoryItemCommandResponse, Errors>> Handle(
        UpdateWorklistFromBioHubHistoryItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            WorklistFromBioHubHistoryItem worklistfrombiohubhistoryitem = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            worklistfrombiohubhistoryitem = _mapper.Map(worklistfrombiohubhistoryitem, command);

            Errors? errors = await _writeRepository.Update(worklistfrombiohubhistoryitem, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateWorklistFromBioHubHistoryItemCommandResponse(worklistfrombiohubhistoryitem));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new WorklistFromBioHubHistoryItem");
            throw;
        }
    }
}