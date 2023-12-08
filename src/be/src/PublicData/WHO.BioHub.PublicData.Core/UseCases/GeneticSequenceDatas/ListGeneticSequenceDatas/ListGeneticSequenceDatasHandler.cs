using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;

public interface IListGeneticSequenceDatasHandler
{
    Task<Either<ListGeneticSequenceDatasQueryResponse, Errors>> Handle(ListGeneticSequenceDatasQuery query, CancellationToken cancellationToken);
}

public class ListGeneticSequenceDatasHandler : IListGeneticSequenceDatasHandler
{
    private readonly ILogger<ListGeneticSequenceDatasHandler> _logger;
    private readonly ListGeneticSequenceDatasQueryValidator _validator;
    private readonly IGeneticSequenceDataPublicReadRepository _readRepository;

    public ListGeneticSequenceDatasHandler(
        ILogger<ListGeneticSequenceDatasHandler> logger,
        ListGeneticSequenceDatasQueryValidator validator,
        IGeneticSequenceDataPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListGeneticSequenceDatasQueryResponse, Errors>> Handle(
        ListGeneticSequenceDatasQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<GeneticSequenceData> geneticsequencedatas = await _readRepository.List(cancellationToken);
            var GeneticSequenceDataDtos = new List<GeneticSequenceDataPublicDto>();
            foreach (var geneticSequenceData in geneticsequencedatas)
            {
                GeneticSequenceDataPublicDto GeneticSequenceDataDto = new()
                {
                    Id = geneticSequenceData.Id,
                    Code = geneticSequenceData.Code,
                    Name = geneticSequenceData.Name,
                    Description = geneticSequenceData.Description,
                    IsActive = geneticSequenceData.IsActive
                };

                GeneticSequenceDataDtos.Add(GeneticSequenceDataDto);
            }
            return new(new ListGeneticSequenceDatasQueryResponse(GeneticSequenceDataDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all GeneticSequenceDatas");
            throw;
        }
    }
}