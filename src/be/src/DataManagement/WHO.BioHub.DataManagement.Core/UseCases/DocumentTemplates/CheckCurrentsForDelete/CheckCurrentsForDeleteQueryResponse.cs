using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CheckCurrentsForDelete;

public record struct CheckCurrentsForDeleteQueryResponse(bool FolderContainsCurrent);