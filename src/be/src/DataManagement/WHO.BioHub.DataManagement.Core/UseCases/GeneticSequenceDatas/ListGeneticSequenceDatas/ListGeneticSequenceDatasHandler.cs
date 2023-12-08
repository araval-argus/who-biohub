using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;

public interface IListGeneticSequenceDatasHandler
{
    Task<Either<ListGeneticSequenceDatasQueryResponse, Errors>> Handle(ListGeneticSequenceDatasQuery query, CancellationToken cancellationToken);
}

public class ListGeneticSequenceDatasHandler : IListGeneticSequenceDatasHandler
{
    private readonly ILogger<ListGeneticSequenceDatasHandler> _logger;
    private readonly ListGeneticSequenceDatasQueryValidator _validator;
    private readonly IGeneticSequenceDataReadRepository _readRepository;

    public ListGeneticSequenceDatasHandler(
        ILogger<ListGeneticSequenceDatasHandler> logger,
        ListGeneticSequenceDatasQueryValidator validator,
        IGeneticSequenceDataReadRepository readRepository)
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
            var GeneticSequenceDataDtos = new List<GeneticSequenceDataDto>();
            foreach (var geneticSequenceData in geneticsequencedatas)
            {
                GeneticSequenceDataDto GeneticSequenceDataDto = new()
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