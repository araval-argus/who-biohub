using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.DeleteTransportMode;

public interface IDeleteTransportModeHandler
{
    Task<Either<DeleteTransportModeCommandResponse, Errors>> Handle(DeleteTransportModeCommand command, CancellationToken cancellationToken);
}

public class DeleteTransportModeHandler : IDeleteTransportModeHandler
{
    private readonly ILogger<DeleteTransportModeHandler> _logger;
    private readonly DeleteTransportModeCommandValidator _validator;
    private readonly ITransportModeWriteRepository _writeRepository;

    public DeleteTransportModeHandler(
        ILogger<DeleteTransportModeHandler> logger,
        DeleteTransportModeCommandValidator validator,
        ITransportModeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteTransportModeCommandResponse, Errors>> Handle(
        DeleteTransportModeCommand command,
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

            return new(new DeleteTransportModeCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the TransportMode with {id}", command.Id);
            throw;
        }
    }
}