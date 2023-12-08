using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListBioHubFacilities;

public interface IListBioHubFacilitiesHandler
{
    Task<Either<ListBioHubFacilitiesQueryResponse, Errors>> Handle(ListBioHubFacilitiesQuery query, CancellationToken cancellationToken);
}

public class ListBioHubFacilitiesHandler : IListBioHubFacilitiesHandler
{
    private readonly ILogger<ListBioHubFacilitiesHandler> _logger;
    private readonly ListBioHubFacilitiesQueryValidator _validator;
    private readonly IBioHubFacilityReadRepository _readRepository;
    private readonly IListBioHubFacilityMapper _mapper;

    public ListBioHubFacilitiesHandler(
        ILogger<ListBioHubFacilitiesHandler> logger,
        ListBioHubFacilitiesQueryValidator validator,
        IBioHubFacilityReadRepository readRepository,
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
            IEnumerable<BioHubFacility> biohubfacilities;

            switch (query.RoleType)
            {
                case RoleType.WHO:
                case RoleType.BioHubFacility:
                    biohubfacilities = await _readRepository.List(cancellationToken);
                    break;
                case RoleType.Laboratory:
                    biohubfacilities = await _readRepository.ListForLaboratoryUser(cancellationToken);
                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }

            var biohubfacilityViewModels = _mapper.Map(biohubfacilities);
            return new(new ListBioHubFacilitiesQueryResponse(biohubfacilityViewModels));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all BioHubFacilities");
            throw;
        }
    }
}