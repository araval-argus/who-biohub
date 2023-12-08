using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.DeleteSMTA1WorkflowItem;

public interface IDeleteSMTA1WorkflowItemHandler
{
    Task<Either<DeleteSMTA1WorkflowItemCommandResponse, Errors>> Handle(DeleteSMTA1WorkflowItemCommand command, CancellationToken cancellationToken);
}

public class DeleteSMTA1WorkflowItemHandler : IDeleteSMTA1WorkflowItemHandler
{
    private readonly ILogger<DeleteSMTA1WorkflowItemHandler> _logger;
    private readonly DeleteSMTA1WorkflowItemCommandValidator _validator;
    private readonly ISMTA1WorkflowItemWriteRepository _writeRepository;

    public DeleteSMTA1WorkflowItemHandler(
        ILogger<DeleteSMTA1WorkflowItemHandler> logger,
        DeleteSMTA1WorkflowItemCommandValidator validator,
        ISMTA1WorkflowItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteSMTA1WorkflowItemCommandResponse, Errors>> Handle(
        DeleteSMTA1WorkflowItemCommand command,
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

            return new(new DeleteSMTA1WorkflowItemCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the SMTA1WorkflowItem with {id}", command.Id);
            throw;
        }
    }
}