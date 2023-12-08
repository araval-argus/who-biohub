using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.DeleteIsolationHostType;

public interface IDeleteIsolationHostTypeHandler
{
    Task<Either<DeleteIsolationHostTypeCommandResponse, Errors>> Handle(DeleteIsolationHostTypeCommand command, CancellationToken cancellationToken);
}

public class DeleteIsolationHostTypeHandler : IDeleteIsolationHostTypeHandler
{
    private readonly ILogger<DeleteIsolationHostTypeHandler> _logger;
    private readonly DeleteIsolationHostTypeCommandValidator _validator;
    private readonly IIsolationHostTypeWriteRepository _writeRepository;

    public DeleteIsolationHostTypeHandler(
        ILogger<DeleteIsolationHostTypeHandler> logger,
        DeleteIsolationHostTypeCommandValidator validator,
        IIsolationHostTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteIsolationHostTypeCommandResponse, Errors>> Handle(
        DeleteIsolationHostTypeCommand command,
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

            return new(new DeleteIsolationHostTypeCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the IsolationHostType with {id}", command.Id);
            throw;
        }
    }
}