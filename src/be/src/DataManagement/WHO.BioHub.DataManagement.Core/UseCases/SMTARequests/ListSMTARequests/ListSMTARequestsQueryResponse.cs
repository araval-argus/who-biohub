using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTARequests.ListSMTARequests;

public record struct ListSMTARequestsQueryResponse(IEnumerable<SMTARequestViewModel> SMTARequests) { }