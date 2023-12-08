using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItemEvents.ListSMTA2WorkflowItemEvents;

public record struct ListSMTA2WorkflowItemEventsQueryResponse(IEnumerable<WorklistTimeline> WorklistTimelines) { }