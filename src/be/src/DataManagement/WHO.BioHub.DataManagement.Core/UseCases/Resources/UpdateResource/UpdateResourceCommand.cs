namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.UpdateResource;

public record struct UpdateResourceCommand(Guid Id, string Name) { }