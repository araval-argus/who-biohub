using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListLaboratories;

public record struct ListLaboratoriesQueryResponse(IEnumerable<LaboratoryViewModel> Laboratories) { }