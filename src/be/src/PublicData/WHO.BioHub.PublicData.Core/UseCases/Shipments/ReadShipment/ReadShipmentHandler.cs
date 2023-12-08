using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Shipments.ReadShipment;

public interface IReadShipmentHandler
{
    Task<Either<ReadShipmentQueryResponse, Errors>> Handle(ReadShipmentQuery query, CancellationToken cancellationToken);
}

public class ReadShipmentHandler : IReadShipmentHandler
{
    private readonly ILogger<ReadShipmentHandler> _logger;
    private readonly ReadShipmentQueryValidator _validator;
    private readonly IShipmentPublicReadRepository _readRepository;
    private readonly IReadShipmentMapper _mapper;

    public ReadShipmentHandler(
        ILogger<ReadShipmentHandler> logger,
        ReadShipmentQueryValidator validator,
        IReadShipmentMapper mapper,
        IShipmentPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadShipmentQueryResponse, Errors>> Handle(
        ReadShipmentQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Shipment shipment = await _readRepository.Read(query.Id, cancellationToken);
            if (shipment == null)
                return new(new Errors(ErrorType.NotFound, $"Shipment with Id {query.Id} not found"));

            ShipmentPublicViewModel shipmentPublicViewModel = _mapper.Map(shipment);

            return new(new ReadShipmentQueryResponse(shipmentPublicViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Shipment with Id {id}", query.Id);
            throw;
        }
    }
}