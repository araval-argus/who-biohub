using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListMapBioHubFacilities;

public interface IListMapBioHubFacilitiesHandler
{
    Task<Either<ListMapBioHubFacilitiesQueryResponse, Errors>> Handle(ListMapBioHubFacilitiesQuery query, CancellationToken cancellationToken);
}

public class ListMapBioHubFacilitiesHandler : IListMapBioHubFacilitiesHandler
{
    private readonly ILogger<ListMapBioHubFacilitiesHandler> _logger;
    private readonly ListMapBioHubFacilitiesQueryValidator _validator;
    private readonly IBioHubFacilityReadRepository _readRepository;
    private readonly IListMapBioHubFacilityMapper _mapper;

    public ListMapBioHubFacilitiesHandler(
        ILogger<ListMapBioHubFacilitiesHandler> logger,
        ListMapBioHubFacilitiesQueryValidator validator,
        IBioHubFacilityReadRepository readRepository,
        IListMapBioHubFacilityMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListMapBioHubFacilitiesQueryResponse, Errors>> Handle(
        ListMapBioHubFacilitiesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IEnumerable<BioHubFacilityMapViewModel> bioHubFacilitiesViewModel;

        try
        {
            IEnumerable<BioHubFacility> bioHubFacilities;
            switch (query.RoleType)
            {
                case RoleType.WHO:
                case RoleType.BioHubFacility:
                    bioHubFacilities = await _readRepository.ListMap(cancellationToken);
                    bioHubFacilitiesViewModel = await _mapper.Map(bioHubFacilities, query.RoleType, cancellationToken);
                    return new(new ListMapBioHubFacilitiesQueryResponse(bioHubFacilitiesViewModel));
                    break;
                case RoleType.Laboratory:
                    bioHubFacilities = await _readRepository.ListMapForLaboratoryUser(cancellationToken);
                    bioHubFacilitiesViewModel = await _mapper.Map(bioHubFacilities, query.RoleType, cancellationToken, query.LaboratoryId);
                    return new(new ListMapBioHubFacilitiesQueryResponse(bioHubFacilitiesViewModel));
                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all BioHubFacilities");
            throw;
        }
    }
}