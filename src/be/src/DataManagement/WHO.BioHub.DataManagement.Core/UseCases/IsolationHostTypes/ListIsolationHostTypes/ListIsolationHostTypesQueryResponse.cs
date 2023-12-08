using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.ListIsolationHostTypes;

public record struct ListIsolationHostTypesQueryResponse(IEnumerable<IsolationHostTypeDto> IsolationHostTypes) { }