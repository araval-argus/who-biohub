using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.UpdateShipment;

public interface IUpdateShipmentHandler
{
    Task<Either<UpdateShipmentCommandResponse, Errors>> Handle(UpdateShipmentCommand command, CancellationToken cancellationToken);
}

public class UpdateShipmentHandler : IUpdateShipmentHandler
{
    private readonly ILogger<UpdateShipmentHandler> _logger;
    private readonly UpdateShipmentCommandValidator _validator;
    private readonly IUpdateShipmentMapper _mapper;
    private readonly IShipmentWriteRepository _writeRepository;

    public UpdateShipmentHandler(
        ILogger<UpdateShipmentHandler> logger,
        UpdateShipmentCommandValidator validator,
        IUpdateShipmentMapper mapper,
        IShipmentWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateShipmentCommandResponse, Errors>> Handle(
        UpdateShipmentCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Shipment shipment = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (shipment == null)
            {
                return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
            }

            shipment = _mapper.Map(shipment, command);

            Errors? errors = await _writeRepository.Update(shipment, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateShipmentCommandResponse(shipment.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Shipment");
            throw;
        }
    }
}