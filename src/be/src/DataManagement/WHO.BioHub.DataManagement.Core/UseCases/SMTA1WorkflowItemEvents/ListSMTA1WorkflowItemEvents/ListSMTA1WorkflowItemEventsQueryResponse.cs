using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItemEvents.ListSMTA1WorkflowItemEvents;

public record struct ListSMTA1WorkflowItemEventsQueryResponse(IEnumerable<WorklistTimeline> WorklistTimelines) { }