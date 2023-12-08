namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UpdateDocumentTemplate;

public record struct UpdateDocumentTemplateCommand(Guid Id, string Name, bool? Current);