namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.DeleteLaboratory;

public record struct DeleteLaboratoryCommand(Guid Id, Guid? OperationById) { }