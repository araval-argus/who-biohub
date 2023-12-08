using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;

public record struct ListPriorityRequestTypesQueryResponse(IEnumerable<PriorityRequestType> PriorityRequestTypes) { }