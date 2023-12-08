using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItemEvents.ListWorklistToBioHubItemEvents;

public record struct ListWorklistToBioHubItemEventsQueryResponse(IEnumerable<WorklistTimeline> WorklistTimelines) { }