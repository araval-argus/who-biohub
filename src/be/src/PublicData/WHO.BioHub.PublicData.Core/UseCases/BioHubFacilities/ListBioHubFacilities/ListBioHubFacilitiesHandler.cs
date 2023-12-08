using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ListBioHubFacilities;

public interface IListBioHubFacilitiesHandler
{
    Task<Either<ListBioHubFacilitiesQueryResponse, Errors>> Handle(ListBioHubFacilitiesQuery query, CancellationToken cancellationToken);
}

public class ListBioHubFacilitiesHandler : IListBioHubFacilitiesHandler
{
    private readonly ILogger<ListBioHubFacilitiesHandler> _logger;
    private readonly ListBioHubFacilitiesQueryValidator _validator;
    private readonly IBioHubFacilityPublicReadRepository _readRepository;
    private readonly IListBioHubFacilityMapper _mapper;

    public ListBioHubFacilitiesHandler(
        ILogger<ListBioHubFacilitiesHandler> logger,
        ListBioHubFacilitiesQueryValidator validator,
        IBioHubFacilityPublicReadRepository readRepository,
        IListBioHubFacilityMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListBioHubFacilitiesQueryResponse, Errors>> Handle(
        ListBioHubFacilitiesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<BioHubFacility> biohubfacilities = await _readRepository.List(cancellationToken);
            IEnumerable<BioHubFacilityPublicViewModel> bioHubFacilityPublicViewModels = _mapper.Map(biohubfacilities);
            return new(new ListBioHubFacilitiesQueryResponse(bioHubFacilityPublicViewModels));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all BioHubFacilities");
            throw;
        }
    }
}