using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SpecimenTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.ReadSpecimenType;

public interface IReadSpecimenTypeHandler
{
    Task<Either<ReadSpecimenTypeQueryResponse, Errors>> Handle(ReadSpecimenTypeQuery query, CancellationToken cancellationToken);
}

public class ReadSpecimenTypeHandler : IReadSpecimenTypeHandler
{
    private readonly ILogger<ReadSpecimenTypeHandler> _logger;
    private readonly ReadSpecimenTypeQueryValidator _validator;
    private readonly ISpecimenTypeReadRepository _readRepository;

    public ReadSpecimenTypeHandler(
        ILogger<ReadSpecimenTypeHandler> logger,
        ReadSpecimenTypeQueryValidator validator,
        ISpecimenTypeReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadSpecimenTypeQueryResponse, Errors>> Handle(
        ReadSpecimenTypeQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            SpecimenType specimenttype = await _readRepository.Read(query.Id, cancellationToken);
            if (specimenttype == null)
                return new(new Errors(ErrorType.NotFound, $"SpecimenType with Id {query.Id} not found"));

            return new(new ReadSpecimenTypeQueryResponse(specimenttype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading SpecimenType with Id {id}", query.Id);
            throw;
        }
    }
}