using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListMapLaboratories;

public record struct ListMapLaboratoriesQueryResponse(IEnumerable<LaboratoryPublicMapViewModel> Laboratories) { }