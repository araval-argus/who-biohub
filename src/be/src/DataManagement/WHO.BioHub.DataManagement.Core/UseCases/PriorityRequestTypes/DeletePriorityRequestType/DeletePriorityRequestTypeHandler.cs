using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.DeletePriorityRequestType;

public interface IDeletePriorityRequestTypeHandler
{
    Task<Either<DeletePriorityRequestTypeCommandResponse, Errors>> Handle(DeletePriorityRequestTypeCommand command, CancellationToken cancellationToken);
}

public class DeletePriorityRequestTypeHandler : IDeletePriorityRequestTypeHandler
{
    private readonly ILogger<DeletePriorityRequestTypeHandler> _logger;
    private readonly DeletePriorityRequestTypeCommandValidator _validator;
    private readonly IPriorityRequestTypeWriteRepository _writeRepository;

    public DeletePriorityRequestTypeHandler(
        ILogger<DeletePriorityRequestTypeHandler> logger,
        DeletePriorityRequestTypeCommandValidator validator,
        IPriorityRequestTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeletePriorityRequestTypeCommandResponse, Errors>> Handle(
        DeletePriorityRequestTypeCommand command,
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

            return new(new DeletePriorityRequestTypeCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the PriorityRequestType with {id}", command.Id);
            throw;
        }
    }
}