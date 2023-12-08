using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ListSMTADocumentTemplates;

public record struct ListSMTADocumentTemplatesQueryResponse(IEnumerable<DocumentTemplateItem> DocumentTemplates) { }