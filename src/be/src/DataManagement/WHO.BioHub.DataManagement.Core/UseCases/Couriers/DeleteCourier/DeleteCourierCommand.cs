namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.DeleteCourier;

public record struct DeleteCourierCommand(Guid Id, Guid? OperationById) { }