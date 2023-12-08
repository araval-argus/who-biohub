using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.ReadCultivabilityType;

public interface IReadCultivabilityTypeHandler
{
    Task<Either<ReadCultivabilityTypeQueryResponse, Errors>> Handle(ReadCultivabilityTypeQuery query, CancellationToken cancellationToken);
}

public class ReadCultivabilityTypeHandler : IReadCultivabilityTypeHandler
{
    private readonly ILogger<ReadCultivabilityTypeHandler> _logger;
    private readonly ReadCultivabilityTypeQueryValidator _validator;
    private readonly ICultivabilityTypeReadRepository _readRepository;

    public ReadCultivabilityTypeHandler(
        ILogger<ReadCultivabilityTypeHandler> logger,
        ReadCultivabilityTypeQueryValidator validator,
        ICultivabilityTypeReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadCultivabilityTypeQueryResponse, Errors>> Handle(
        ReadCultivabilityTypeQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            CultivabilityType cultivabilitytype = await _readRepository.Read(query.Id, cancellationToken);
            if (cultivabilitytype == null)
                return new(new Errors(ErrorType.NotFound, $"CultivabilityType with Id {query.Id} not found"));

            return new(new ReadCultivabilityTypeQueryResponse(cultivabilitytype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading CultivabilityType with Id {id}", query.Id);
            throw;
        }
    }
}