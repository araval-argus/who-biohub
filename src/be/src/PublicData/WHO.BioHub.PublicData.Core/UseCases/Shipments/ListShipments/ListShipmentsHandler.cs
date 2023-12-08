using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;
using WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListShipments;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.Core.UseCases.Shipments.ListShipments;

public interface IListShipmentsHandler
{
    Task<Either<ListShipmentsQueryResponse, Errors>> Handle(ListShipmentsQuery query, CancellationToken cancellationToken);
}

public class ListShipmentsHandler : IListShipmentsHandler
{
    private readonly ILogger<ListShipmentsHandler> _logger;
    private readonly ListShipmentsQueryValidator _validator;
    private readonly IShipmentPublicReadRepository _readRepository;
    private readonly IListShipmentMapper _mapper;

    public ListShipmentsHandler(
        ILogger<ListShipmentsHandler> logger,
        ListShipmentsQueryValidator validator,
        IShipmentPublicReadRepository readRepository,
        IListShipmentMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListShipmentsQueryResponse, Errors>> Handle(
        ListShipmentsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<Shipment> shipments = await _readRepository.List(cancellationToken);

            IEnumerable<ShipmentPublicViewModel> shipmentPublicViewModels = _mapper.Map(shipments);
            return new(new ListShipmentsQueryResponse(shipmentPublicViewModels));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Shipments");
            throw;
        }
    }
}