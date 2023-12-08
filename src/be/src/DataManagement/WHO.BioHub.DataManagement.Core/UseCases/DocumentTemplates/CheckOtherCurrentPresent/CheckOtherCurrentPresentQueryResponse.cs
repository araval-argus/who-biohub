using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CheckOtherCurrentPresent;

public record struct CheckOtherCurrentPresentQueryResponse(bool IsOtherCurrentPresent);