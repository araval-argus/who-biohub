using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.CanStartSMTA2Request;

public record struct CanStartSMTARequestQuery(Guid? LaboratoryId, DocumentFileType Type) { }