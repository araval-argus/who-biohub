using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Resources.ListResources;

public record struct ListResourcesQueryResponse(IEnumerable<ResourcePublicItem> Resources) { }