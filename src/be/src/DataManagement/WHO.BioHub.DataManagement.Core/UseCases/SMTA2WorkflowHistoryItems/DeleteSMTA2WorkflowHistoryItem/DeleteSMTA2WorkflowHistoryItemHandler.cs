using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.DeleteSMTA2WorkflowHistoryItem;

public interface IDeleteSMTA2WorkflowHistoryItemHandler
{
    Task<Either<DeleteSMTA2WorkflowHistoryItemCommandResponse, Errors>> Handle(DeleteSMTA2WorkflowHistoryItemCommand command, CancellationToken cancellationToken);
}

public class DeleteSMTA2WorkflowHistoryItemHandler : IDeleteSMTA2WorkflowHistoryItemHandler
{
    private readonly ILogger<DeleteSMTA2WorkflowHistoryItemHandler> _logger;
    private readonly DeleteSMTA2WorkflowHistoryItemCommandValidator _validator;
    private readonly ISMTA2WorkflowHistoryItemWriteRepository _writeRepository;

    public DeleteSMTA2WorkflowHistoryItemHandler(
        ILogger<DeleteSMTA2WorkflowHistoryItemHandler> logger,
        DeleteSMTA2WorkflowHistoryItemCommandValidator validator,
        ISMTA2WorkflowHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteSMTA2WorkflowHistoryItemCommandResponse, Errors>> Handle(
        DeleteSMTA2WorkflowHistoryItemCommand command,
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

            return new(new DeleteSMTA2WorkflowHistoryItemCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the SMTA2WorkflowHistoryItem with {id}", command.Id);
            throw;
        }
    }
}