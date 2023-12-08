using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.DeleteWorklistFromBioHubHistoryItem;

public interface IDeleteWorklistFromBioHubHistoryItemHandler
{
    Task<Either<DeleteWorklistFromBioHubHistoryItemCommandResponse, Errors>> Handle(DeleteWorklistFromBioHubHistoryItemCommand command, CancellationToken cancellationToken);
}

public class DeleteWorklistFromBioHubHistoryItemHandler : IDeleteWorklistFromBioHubHistoryItemHandler
{
    private readonly ILogger<DeleteWorklistFromBioHubHistoryItemHandler> _logger;
    private readonly DeleteWorklistFromBioHubHistoryItemCommandValidator _validator;
    private readonly IWorklistFromBioHubHistoryItemWriteRepository _writeRepository;

    public DeleteWorklistFromBioHubHistoryItemHandler(
        ILogger<DeleteWorklistFromBioHubHistoryItemHandler> logger,
        DeleteWorklistFromBioHubHistoryItemCommandValidator validator,
        IWorklistFromBioHubHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteWorklistFromBioHubHistoryItemCommandResponse, Errors>> Handle(
        DeleteWorklistFromBioHubHistoryItemCommand command,
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

            return new(new DeleteWorklistFromBioHubHistoryItemCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the WorklistFromBioHubHistoryItem with {id}", command.Id);
            throw;
        }
    }
}