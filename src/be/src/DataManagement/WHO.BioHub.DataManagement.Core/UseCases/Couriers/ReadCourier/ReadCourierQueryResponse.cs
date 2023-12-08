using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.ReadCourier;

public record struct ReadCourierQueryResponse(CourierViewModel Courier) { }