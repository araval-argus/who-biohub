using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.ListTransportCategories;

public record struct ListTransportCategoriesQueryResponse(IEnumerable<TransportCategoryDto> TransportCategories) { }