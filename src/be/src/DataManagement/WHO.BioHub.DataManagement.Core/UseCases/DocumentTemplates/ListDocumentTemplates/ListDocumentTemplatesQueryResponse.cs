using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ListDocumentTemplates;

public record struct ListDocumentTemplatesQueryResponse(IEnumerable<DocumentTemplateItem> DocumentTemplates) { }