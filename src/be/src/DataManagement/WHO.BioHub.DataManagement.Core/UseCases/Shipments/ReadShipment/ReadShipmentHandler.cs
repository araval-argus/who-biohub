using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.ReadShipment;

public interface IReadShipmentHandler
{
    Task<Either<ReadShipmentQueryResponse, Errors>> Handle(ReadShipmentQuery query, CancellationToken cancellationToken);
}

public class ReadShipmentHandler : IReadShipmentHandler
{
    private readonly ILogger<ReadShipmentHandler> _logger;
    private readonly ReadShipmentQueryValidator _validator;
    private readonly IShipmentReadRepository _readRepository;
    private readonly IReadShipmentMapper _mapper;

    public ReadShipmentHandler(
        ILogger<ReadShipmentHandler> logger,
        ReadShipmentQueryValidator validator,
        IShipmentReadRepository readRepository,
        IReadShipmentMapper mapper)
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
        Shipment shipment;
        ShipmentViewModel shipmentsViewModel;

        try
        {
            switch (query.RoleType)
            {
                case RoleType.WHO:               
                    shipment = await _readRepository.Read(query.Id, cancellationToken);
                    shipmentsViewModel = _mapper.Map(shipment, query.RoleType.GetValueOrDefault(), query.UserPermissions);
                    return new(new ReadShipmentQueryResponse(shipmentsViewModel));

                case RoleType.BioHubFacility:
                    shipment = await _readRepository.ReadForBioHubFacilityUser(query.Id, query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);
                    shipmentsViewModel = _mapper.MapForBioHubUser(shipment, query.BioHubFacilityId.GetValueOrDefault(), query.RoleType.GetValueOrDefault(), query.UserPermissions);
                    return new(new ReadShipmentQueryResponse(shipmentsViewModel));

                case RoleType.Laboratory:
                    shipment = await _readRepository.ReadForLaboratoryUser(query.Id, query.LaboratoryId.GetValueOrDefault(), cancellationToken);
                    shipmentsViewModel = _mapper.MapForLaboratoryUser(shipment, query.LaboratoryId.GetValueOrDefault(), query.RoleType.GetValueOrDefault(), query.UserPermissions);
                    return new(new ReadShipmentQueryResponse(shipmentsViewModel));

                default:
                    throw new InvalidOperationException();
            }


        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Shipment with Id {id}", query.Id);
            throw;
        }
    }
}