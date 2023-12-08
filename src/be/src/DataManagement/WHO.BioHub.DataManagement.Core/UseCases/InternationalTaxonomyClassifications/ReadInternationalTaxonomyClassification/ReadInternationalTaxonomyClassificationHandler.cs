using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.ReadInternationalTaxonomyClassification;

public interface IReadInternationalTaxonomyClassificationHandler
{
    Task<Either<ReadInternationalTaxonomyClassificationQueryResponse, Errors>> Handle(ReadInternationalTaxonomyClassificationQuery query, CancellationToken cancellationToken);
}

public class ReadInternationalTaxonomyClassificationHandler : IReadInternationalTaxonomyClassificationHandler
{
    private readonly ILogger<ReadInternationalTaxonomyClassificationHandler> _logger;
    private readonly ReadInternationalTaxonomyClassificationQueryValidator _validator;
    private readonly IInternationalTaxonomyClassificationReadRepository _readRepository;

    public ReadInternationalTaxonomyClassificationHandler(
        ILogger<ReadInternationalTaxonomyClassificationHandler> logger,
        ReadInternationalTaxonomyClassificationQueryValidator validator,
        IInternationalTaxonomyClassificationReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadInternationalTaxonomyClassificationQueryResponse, Errors>> Handle(
        ReadInternationalTaxonomyClassificationQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            InternationalTaxonomyClassification internationaltaxonomyclassification = await _readRepository.Read(query.Id, cancellationToken);
            if (internationaltaxonomyclassification == null)
                return new(new Errors(ErrorType.NotFound, $"InternationalTaxonomyClassification with Id {query.Id} not found"));

            return new(new ReadInternationalTaxonomyClassificationQueryResponse(internationaltaxonomyclassification));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading InternationalTaxonomyClassification with Id {id}", query.Id);
            throw;
        }
    }
}