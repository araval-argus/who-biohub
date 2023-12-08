using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.DeleteWorklistToBioHubHistoryItem;

public interface IDeleteWorklistToBioHubHistoryItemHandler
{
    Task<Either<DeleteWorklistToBioHubHistoryItemCommandResponse, Errors>> Handle(DeleteWorklistToBioHubHistoryItemCommand command, CancellationToken cancellationToken);
}

public class DeleteWorklistToBioHubHistoryItemHandler : IDeleteWorklistToBioHubHistoryItemHandler
{
    private readonly ILogger<DeleteWorklistToBioHubHistoryItemHandler> _logger;
    private readonly DeleteWorklistToBioHubHistoryItemCommandValidator _validator;
    private readonly IWorklistToBioHubHistoryItemWriteRepository _writeRepository;

    public DeleteWorklistToBioHubHistoryItemHandler(
        ILogger<DeleteWorklistToBioHubHistoryItemHandler> logger,
        DeleteWorklistToBioHubHistoryItemCommandValidator validator,
        IWorklistToBioHubHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteWorklistToBioHubHistoryItemCommandResponse, Errors>> Handle(
        DeleteWorklistToBioHubHistoryItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Errors? errors = await _writeRepository.Delete(command.Id, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new DeleteWorklistToBioHubHistoryItemCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the WorklistToBioHubHistoryItem with {id}", command.Id);
            throw;
        }
    }
}