using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;
using WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ListMapBioHubFacilities;

namespace WHO.BioHub.PublicData.Core.UseCases.MapBioHubFacilities.ListMapBioHubFacilities;

public interface IListMapBioHubFacilitiesHandler
{
    Task<Either<ListMapBioHubFacilitiesQueryResponse, Errors>> Handle(ListMapBioHubFacilitiesQuery query, CancellationToken cancellationToken);
}

public class ListMapBioHubFacilitiesHandler : IListMapBioHubFacilitiesHandler
{
    private readonly ILogger<ListMapBioHubFacilitiesHandler> _logger;
    private readonly ListMapBioHubFacilitiesQueryValidator _validator;
    private readonly IBioHubFacilityPublicReadRepository _readRepository;
    private readonly IListMapBioHubFacilityMapper _mapper;

    public ListMapBioHubFacilitiesHandler(
        ILogger<ListMapBioHubFacilitiesHandler> logger,
        ListMapBioHubFacilitiesQueryValidator validator,
        IBioHubFacilityPublicReadRepository readRepository,
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

        try
        {
            IEnumerable<BioHubFacility> biohubfacilities = await _readRepository.ListMap(cancellationToken);
            IEnumerable<BioHubFacilityPublicMapViewModel> bioHubFacilityPublicViewModels = await _mapper.Map(biohubfacilities, cancellationToken);
            return new(new ListMapBioHubFacilitiesQueryResponse(bioHubFacilityPublicViewModels));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all MapBioHubFacilities");
            throw;
        }
    }
}