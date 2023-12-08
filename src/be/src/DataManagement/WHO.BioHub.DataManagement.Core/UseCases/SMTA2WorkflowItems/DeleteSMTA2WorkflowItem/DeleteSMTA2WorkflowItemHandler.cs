using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.DeleteSMTA2WorkflowItem;

public interface IDeleteSMTA2WorkflowItemHandler
{
    Task<Either<DeleteSMTA2WorkflowItemCommandResponse, Errors>> Handle(DeleteSMTA2WorkflowItemCommand command, CancellationToken cancellationToken);
}

public class DeleteSMTA2WorkflowItemHandler : IDeleteSMTA2WorkflowItemHandler
{
    private readonly ILogger<DeleteSMTA2WorkflowItemHandler> _logger;
    private readonly DeleteSMTA2WorkflowItemCommandValidator _validator;
    private readonly ISMTA2WorkflowItemWriteRepository _writeRepository;

    public DeleteSMTA2WorkflowItemHandler(
        ILogger<DeleteSMTA2WorkflowItemHandler> logger,
        DeleteSMTA2WorkflowItemCommandValidator validator,
        ISMTA2WorkflowItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteSMTA2WorkflowItemCommandResponse, Errors>> Handle(
        DeleteSMTA2WorkflowItemCommand command,
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

            return new(new DeleteSMTA2WorkflowItemCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the SMTA2WorkflowItem with {id}", command.Id);
            throw;
        }
    }
}