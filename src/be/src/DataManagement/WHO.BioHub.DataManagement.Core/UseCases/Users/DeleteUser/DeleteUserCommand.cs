namespace WHO.BioHub.DataManagement.Core.UseCases.Users.DeleteUser;

public record struct DeleteUserCommand(Guid Id, Guid? OperationById) { }