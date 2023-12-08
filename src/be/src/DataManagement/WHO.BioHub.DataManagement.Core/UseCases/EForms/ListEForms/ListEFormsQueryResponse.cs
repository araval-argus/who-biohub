using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.EForms.ListEForms;

public record struct ListEFormsQueryResponse(IEnumerable<EFormItemDto> EForms) { }