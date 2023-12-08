using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.IsolationHostTypes.ListIsolationHostTypes;

public record struct ListIsolationHostTypesQueryResponse(IEnumerable<IsolationHostTypePublicDto> IsolationHostTypes) { }