using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.ListShipments;

public interface IListShipmentsHandler
{
    Task<Either<ListShipmentsQueryResponse, Errors>> Handle(ListShipmentsQuery query, CancellationToken cancellationToken);
}

public class ListShipmentsHandler : IListShipmentsHandler
{
    private readonly ILogger<ListShipmentsHandler> _logger;
    private readonly ListShipmentsQueryValidator _validator;
    private readonly IShipmentReadRepository _readRepository;
    private readonly IListShipmentsMapper _mapper;

    public ListShipmentsHandler(
        ILogger<ListShipmentsHandler> logger,
        ListShipmentsQueryValidator validator,
        IShipmentReadRepository readRepository,
        IListShipmentsMapper mapper)
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
        IEnumerable<Shipment> shipments;
        IEnumerable<ShipmentViewModel> shipmentsViewModel;

        try
        {
            switch (query.RoleType)
            {
                case RoleType.WHO:                
                    shipments = await _readRepository.List(cancellationToken);
                    shipmentsViewModel = _mapper.Map(shipments);
                    return new(new ListShipmentsQueryResponse(shipmentsViewModel));

                case RoleType.BioHubFacility:
                    shipments = await _readRepository.ListForBioHubFacilityUser(query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);
                    shipmentsViewModel = _mapper.Map(shipments);
                    return new(new ListShipmentsQueryResponse(shipmentsViewModel));

                case RoleType.Laboratory:
                    shipments = await _readRepository.ListForLaboratoryUser(query.LaboratoryId.GetValueOrDefault(), cancellationToken);
                    shipmentsViewModel = _mapper.Map(shipments);
                    return new(new ListShipmentsQueryResponse(shipmentsViewModel));

                default:
                    throw new InvalidOperationException();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Shipments");
            throw;
        }
    }
}