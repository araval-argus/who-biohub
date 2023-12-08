using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.DeleteShipment;

public interface IDeleteShipmentHandler
{
    Task<Either<DeleteShipmentCommandResponse, Errors>> Handle(DeleteShipmentCommand command, CancellationToken cancellationToken);
}

public class DeleteShipmentHandler : IDeleteShipmentHandler
{
    private readonly ILogger<DeleteShipmentHandler> _logger;
    private readonly DeleteShipmentCommandValidator _validator;
    private readonly IShipmentWriteRepository _writeRepository;

    public DeleteShipmentHandler(
        ILogger<DeleteShipmentHandler> logger,
        DeleteShipmentCommandValidator validator,
        IShipmentWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteShipmentCommandResponse, Errors>> Handle(
        DeleteShipmentCommand command,
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

            return new(new DeleteShipmentCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the Shipment with {id}", command.Id);
            throw;
        }
    }
}