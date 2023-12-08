using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;

public record struct ListGeneticSequenceDatasQueryResponse(IEnumerable<GeneticSequenceDataDto> GeneticSequenceDatas) { }