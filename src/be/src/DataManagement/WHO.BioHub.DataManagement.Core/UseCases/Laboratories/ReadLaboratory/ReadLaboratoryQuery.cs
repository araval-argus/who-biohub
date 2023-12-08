using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ReadLaboratory;

public record struct ReadLaboratoryQuery(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId);