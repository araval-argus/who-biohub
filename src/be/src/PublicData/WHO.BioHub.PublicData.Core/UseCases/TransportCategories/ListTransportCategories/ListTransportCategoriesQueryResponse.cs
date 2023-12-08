using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.TransportCategories.ListTransportCategories;

public record struct ListTransportCategoriesQueryResponse(IEnumerable<TransportCategoryPublicDto> TransportCategories) { }