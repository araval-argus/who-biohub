using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ReadLaboratory;

public record struct ReadLaboratoryQueryResponse(LaboratoryPublicViewModel Laboratory) { }