using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialEvents.ListMaterialEvents;

public record struct ListMaterialEventsQueryResponse(WorklistTimeline MaterialTimeline) { }