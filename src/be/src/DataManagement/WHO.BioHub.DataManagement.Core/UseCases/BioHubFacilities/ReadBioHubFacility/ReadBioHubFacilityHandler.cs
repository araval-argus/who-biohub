using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ReadBioHubFacility;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ReadBioHubFacility;

public interface IReadBioHubFacilityHandler
{
    Task<Either<ReadBioHubFacilityQueryResponse, Errors>> Handle(ReadBioHubFacilityQuery query, CancellationToken cancellationToken);
}

public class ReadBioHubFacilityHandler : IReadBioHubFacilityHandler
{
    private readonly ILogger<ReadBioHubFacilityHandler> _logger;
    private readonly ReadBioHubFacilityQueryValidator _validator;
    private readonly IBioHubFacilityReadRepository _readRepository;
    private readonly IReadBioHubFacilityMapper _mapper;

    public ReadBioHubFacilityHandler(
        ILogger<ReadBioHubFacilityHandler> logger,
        ReadBioHubFacilityQueryValidator validator,
        IBioHubFacilityReadRepository readRepository,
        IReadBioHubFacilityMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadBioHubFacilityQueryResponse, Errors>> Handle(
        ReadBioHubFacilityQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            BioHubFacility biohubfacility = await _readRepository.Read(query.Id, cancellationToken);
            if (biohubfacility == null)
                return new(new Errors(ErrorType.NotFound, $"BioHubFacility with Id {query.Id} not found"));

            var bioHubFacilityViewModel = _mapper.Map(biohubfacility);
            return new(new ReadBioHubFacilityQueryResponse(bioHubFacilityViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading BioHubFacility with Id {id}", query.Id);
            throw;
        }
    }
}