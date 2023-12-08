using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.UpdateWorklistToBioHubHistoryItem;

public interface IUpdateWorklistToBioHubHistoryItemHandler
{
    Task<Either<UpdateWorklistToBioHubHistoryItemCommandResponse, Errors>> Handle(UpdateWorklistToBioHubHistoryItemCommand command, CancellationToken cancellationToken);
}

public class UpdateWorklistToBioHubHistoryItemHandler : IUpdateWorklistToBioHubHistoryItemHandler
{
    private readonly ILogger<UpdateWorklistToBioHubHistoryItemHandler> _logger;
    private readonly UpdateWorklistToBioHubHistoryItemCommandValidator _validator;
    private readonly IUpdateWorklistToBioHubHistoryItemMapper _mapper;
    private readonly IWorklistToBioHubHistoryItemWriteRepository _writeRepository;

    public UpdateWorklistToBioHubHistoryItemHandler(
        ILogger<UpdateWorklistToBioHubHistoryItemHandler> logger,
        UpdateWorklistToBioHubHistoryItemCommandValidator validator,
        IUpdateWorklistToBioHubHistoryItemMapper mapper,
        IWorklistToBioHubHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateWorklistToBioHubHistoryItemCommandResponse, Errors>> Handle(
        UpdateWorklistToBioHubHistoryItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            WorklistToBioHubHistoryItem worklisttobiohubhistoryitem = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            worklisttobiohubhistoryitem = _mapper.Map(worklisttobiohubhistoryitem, command);

            Errors? errors = await _writeRepository.Update(worklisttobiohubhistoryitem, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateWorklistToBioHubHistoryItemCommandResponse(worklisttobiohubhistoryitem));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new WorklistToBioHubHistoryItem");
            throw;
        }
    }
}