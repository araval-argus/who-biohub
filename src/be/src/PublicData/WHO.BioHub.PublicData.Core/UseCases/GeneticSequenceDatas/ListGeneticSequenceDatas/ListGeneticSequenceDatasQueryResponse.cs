using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;

public record struct ListGeneticSequenceDatasQueryResponse(IEnumerable<GeneticSequenceDataPublicDto> GeneticSequenceDatas) { }