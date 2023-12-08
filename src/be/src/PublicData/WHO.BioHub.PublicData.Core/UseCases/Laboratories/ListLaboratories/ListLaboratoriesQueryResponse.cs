using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListLaboratories;

public record struct ListLaboratoriesQueryResponse(IEnumerable<LaboratoryPublicViewModel> Laboratories) { }