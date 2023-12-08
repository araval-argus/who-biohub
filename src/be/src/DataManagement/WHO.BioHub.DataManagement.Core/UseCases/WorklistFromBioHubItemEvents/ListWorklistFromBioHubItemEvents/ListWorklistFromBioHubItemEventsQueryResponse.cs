using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItemEvents.ListWorklistFromBioHubItemEvents;

public record struct ListWorklistFromBioHubItemEventsQueryResponse(IEnumerable<WorklistTimeline> WorklistTimelines) { }