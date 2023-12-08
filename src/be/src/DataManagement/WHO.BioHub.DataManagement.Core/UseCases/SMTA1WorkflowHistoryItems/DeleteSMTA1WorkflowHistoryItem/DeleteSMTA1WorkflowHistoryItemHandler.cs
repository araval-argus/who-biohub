using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.DeleteSMTA1WorkflowHistoryItem;

public interface IDeleteSMTA1WorkflowHistoryItemHandler
{
    Task<Either<DeleteSMTA1WorkflowHistoryItemCommandResponse, Errors>> Handle(DeleteSMTA1WorkflowHistoryItemCommand command, CancellationToken cancellationToken);
}

public class DeleteSMTA1WorkflowHistoryItemHandler : IDeleteSMTA1WorkflowHistoryItemHandler
{
    private readonly ILogger<DeleteSMTA1WorkflowHistoryItemHandler> _logger;
    private readonly DeleteSMTA1WorkflowHistoryItemCommandValidator _validator;
    private readonly ISMTA1WorkflowHistoryItemWriteRepository _writeRepository;

    public DeleteSMTA1WorkflowHistoryItemHandler(
        ILogger<DeleteSMTA1WorkflowHistoryItemHandler> logger,
        DeleteSMTA1WorkflowHistoryItemCommandValidator validator,
        ISMTA1WorkflowHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteSMTA1WorkflowHistoryItemCommandResponse, Errors>> Handle(
        DeleteSMTA1WorkflowHistoryItemCommand command,
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

            return new(new DeleteSMTA1WorkflowHistoryItemCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the SMTA1WorkflowHistoryItem with {id}", command.Id);
            throw;
        }
    }
}