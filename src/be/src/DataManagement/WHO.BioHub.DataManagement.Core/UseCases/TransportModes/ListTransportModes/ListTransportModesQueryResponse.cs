using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportModes.ListTransportModes;

public record struct ListTransportModesQueryResponse(IEnumerable<TransportModeDto> TransportModes) { }