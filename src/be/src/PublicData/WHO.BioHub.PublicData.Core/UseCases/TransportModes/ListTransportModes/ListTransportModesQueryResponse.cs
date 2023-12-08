using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.Core.UseCases.TransportModes.ListTransportModes;

public record struct ListTransportModesQueryResponse(IEnumerable<TransportMode> TransportModes) { }