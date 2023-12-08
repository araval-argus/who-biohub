using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.ListCouriers;

public record struct ListCouriersQueryResponse(IEnumerable<CourierViewModel> Couriers) { }