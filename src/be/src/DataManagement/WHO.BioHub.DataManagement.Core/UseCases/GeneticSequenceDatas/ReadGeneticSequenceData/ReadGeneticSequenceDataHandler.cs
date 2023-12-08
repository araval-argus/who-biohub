using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.ReadGeneticSequenceData;

public interface IReadGeneticSequenceDataHandler
{
    Task<Either<ReadGeneticSequenceDataQueryResponse, Errors>> Handle(ReadGeneticSequenceDataQuery query, CancellationToken cancellationToken);
}

public class ReadGeneticSequenceDataHandler : IReadGeneticSequenceDataHandler
{
    private readonly ILogger<ReadGeneticSequenceDataHandler> _logger;
    private readonly ReadGeneticSequenceDataQueryValidator _validator;
    private readonly IGeneticSequenceDataReadRepository _readRepository;

    public ReadGeneticSequenceDataHandler(
        ILogger<ReadGeneticSequenceDataHandler> logger,
        ReadGeneticSequenceDataQueryValidator validator,
        IGeneticSequenceDataReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadGeneticSequenceDataQueryResponse, Errors>> Handle(
        ReadGeneticSequenceDataQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            GeneticSequenceData geneticsequencedata = await _readRepository.Read(query.Id, cancellationToken);
            if (geneticsequencedata == null)
                return new(new Errors(ErrorType.NotFound, $"GeneticSequenceData with Id {query.Id} not found"));

            return new(new ReadGeneticSequenceDataQueryResponse(geneticsequencedata));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading GeneticSequenceData with Id {id}", query.Id);
            throw;
        }
    }
}