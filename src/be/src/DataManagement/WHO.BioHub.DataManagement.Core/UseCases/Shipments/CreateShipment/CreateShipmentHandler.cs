using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.CreateShipment;

public interface ICreateShipmentHandler
{
    Task<Either<CreateShipmentCommandResponse, Errors>> Handle(CreateShipmentCommand command, CancellationToken cancellationToken);
}

public class CreateShipmentHandler : ICreateShipmentHandler
{
    private readonly ILogger<CreateShipmentHandler> _logger;
    private readonly CreateShipmentCommandValidator _validator;
    private readonly ICreateShipmentMapper _mapper;
    private readonly IShipmentWriteRepository _writeRepository;

    public CreateShipmentHandler(
        ILogger<CreateShipmentHandler> logger,
        CreateShipmentCommandValidator validator,
        ICreateShipmentMapper mapper,
        IShipmentWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateShipmentCommandResponse, Errors>> Handle(
        CreateShipmentCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        Shipment shipment = _mapper.Map(command);

        try
        {
            Either<Shipment, Errors> response = await _writeRepository.Create(shipment, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            Shipment createdShipment =
                response.Left ?? throw new Exception("This is a bug: shipment value must be defined");
            return new(new CreateShipmentCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Shipment");
            throw;
        }
    }
}