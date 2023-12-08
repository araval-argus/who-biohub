using FluentValidation.Results;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.DeleteMaterial;
using WHO.BioHub.Shared.Utils;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Materials.DeleteMaterial;

public class DeleteMaterialHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_deleted_then_a_valid_response_is_returned()
    {
        // Arrange
        DeleteMaterialCommandValidator validatorMock = Substitute.For<DeleteMaterialCommandValidator>();
        ILogger<DeleteMaterialHandler> loggerMock = Substitute.For<ILogger<DeleteMaterialHandler>>();
        IMaterialWriteRepository repositoryMock = Substitute.For<IMaterialWriteRepository>();
        CancellationToken cancellationToken = default;

        
        DeleteMaterialHandler handler = new(loggerMock, validatorMock, repositoryMock);

        DeleteMaterialCommand cmd = new();

        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        Material material = new() { Id = Guid.NewGuid() };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();

        Guid assignedId = Guid.NewGuid();

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(
                Task.FromResult(material));

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryMock
            .Update(material, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(
                Task.FromResult<Errors?>(null));

        repositoryMock.CreateMaterialHistoryItem(material, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));       

        // Act
        Either<DeleteMaterialCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<DeleteMaterialCommand>(), cancellationToken).Received(1);
            await repositoryMock.Update(material, cancellationToken, transactionMock).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        DeleteMaterialCommandValidator validatorMock = Substitute.For<DeleteMaterialCommandValidator>();
        ILogger<DeleteMaterialHandler> loggerMock = Substitute.For<ILogger<DeleteMaterialHandler>>();
        IMaterialWriteRepository repositoryMock = Substitute.For<IMaterialWriteRepository>();
        CancellationToken cancellationToken = default;

        DeleteMaterialHandler handler = new(loggerMock, validatorMock, repositoryMock);
        DeleteMaterialCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<DeleteMaterialCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsRight.Should()
            .BeTrue(because: "Errors are expected in this scenario");
        response.Right.Should()
            .NotBeNull(because: "If errors are returned, errors should NOT be null");
        response.Right.Messages.Should()
            .NotBeNullOrEmpty(because: "If errors are returned, at least one message must be defined");
        response.Right.ErrorType.Should()
            .Be(ErrorType.Validation, because: "Validation Errors are expected in this scenario");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<DeleteMaterialCommand>(), cancellationToken).Received(1);
            await repositoryMock.Delete(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        DeleteMaterialCommandValidator validatorMock = Substitute.For<DeleteMaterialCommandValidator>();
        ILogger<DeleteMaterialHandler> loggerMock = Substitute.For<ILogger<DeleteMaterialHandler>>();
        IMaterialWriteRepository repositoryMock = Substitute.For<IMaterialWriteRepository>();
        CancellationToken cancellationToken = default;

       
        DeleteMaterialHandler handler = new(loggerMock, validatorMock, repositoryMock);

        DeleteMaterialCommand cmd = new();
        Guid id = Guid.NewGuid();
        Material material = new() { Id = id };
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryMock
           .ReadForUpdate(cmd.Id, cancellationToken)
           .ReturnsForAnyArgs(
               Task.FromResult(material));

        repositoryMock
            .Update(material, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(
                Task.FromResult<Errors?>(null));

        repositoryMock.CreateMaterialHistoryItem(material, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

       

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        repositoryMock
            .Update(material, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<DeleteMaterialCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsRight.Should()
            .BeTrue(because: "Errors are expected in this scenario");
        response.Right.Should()
            .NotBeNull(because: "If errors are returned, errors should NOT be null");
        response.Right.Messages.Should()
            .NotBeNullOrEmpty(because: "If errors are returned, at least one message must be defined");
        response.Right.ErrorType.Should()
            .Be(ErrorType.Validation, because: "Validation Errors are expected in this scenario");
        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<DeleteMaterialCommand>(), cancellationToken).Received(1);
            await repositoryMock.Update(material, cancellationToken, transactionMock).Received(1);
        });
    }
}