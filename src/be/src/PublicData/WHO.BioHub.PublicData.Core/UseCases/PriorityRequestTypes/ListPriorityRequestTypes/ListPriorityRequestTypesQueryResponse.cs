using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.Core.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;

public record struct ListPriorityRequestTypesQueryResponse(IEnumerable<PriorityRequestType> PriorityRequestTypes) { }