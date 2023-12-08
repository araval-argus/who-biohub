using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.IsolationTechniqueTypes.ReadIsolationTechniqueType;

public interface IReadIsolationTechniqueTypeHandler
{
    Task<Either<ReadIsolationTechniqueTypeQueryResponse, Errors>> Handle(ReadIsolationTechniqueTypeQuery query, CancellationToken cancellationToken);
}

public class ReadIsolationTechniqueTypeHandler : IReadIsolationTechniqueTypeHandler
{
    private readonly ILogger<ReadIsolationTechniqueTypeHandler> _logger;
    private readonly ReadIsolationTechniqueTypeQueryValidator _validator;
    private readonly IIsolationTechniqueTypePublicReadRepository _readRepository;

    public ReadIsolationTechniqueTypeHandler(
        ILogger<ReadIsolationTechniqueTypeHandler> logger,
        ReadIsolationTechniqueTypeQueryValidator validator,
        IIsolationTechniqueTypePublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadIsolationTechniqueTypeQueryResponse, Errors>> Handle(
        ReadIsolationTechniqueTypeQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IsolationTechniqueType isolationtechniquetype = await _readRepository.Read(query.Id, cancellationToken);
            if (isolationtechniquetype == null)
                return new(new Errors(ErrorType.NotFound, $"IsolationTechniqueType with Id {query.Id} not found"));

            return new(new ReadIsolationTechniqueTypeQueryResponse(isolationtechniquetype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading IsolationTechniqueType with Id {id}", query.Id);
            throw;
        }
    }
}