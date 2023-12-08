using FluentValidation.Results;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.DeleteCourier;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Couriers.DeleteCourier;

public class DeleteCourierHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_deleted_then_a_valid_response_is_returned()
    {
        // Arrange
        DeleteCourierCommandValidator validatorMock = Substitute.For<DeleteCourierCommandValidator>();
        ILogger<DeleteCourierHandler> loggerMock = Substitute.For<ILogger<DeleteCourierHandler>>();
        ICourierWriteRepository repositoryMock = Substitute.For<ICourierWriteRepository>();
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();
        CancellationToken cancellationToken = default;

        DeleteCourierHandler handler = new(loggerMock, validatorMock, repositoryMock);
        DeleteCourierCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();

        Courier courier = new() { Id = Guid.NewGuid() };

        Guid assignedId = Guid.NewGuid();

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(
                Task.FromResult(courier));

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryMock
            .Update(courier, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(
                Task.FromResult<Errors?>(null));

        repositoryMock.CreateCourierHistoryItem(courier, cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<DeleteCourierCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<DeleteCourierCommand>(), cancellationToken).Received(1);
            await repositoryMock.Delete(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        DeleteCourierCommandValidator validatorMock = Substitute.For<DeleteCourierCommandValidator>();
        ILogger<DeleteCourierHandler> loggerMock = Substitute.For<ILogger<DeleteCourierHandler>>();
        ICourierWriteRepository repositoryMock = Substitute.For<ICourierWriteRepository>();
        CancellationToken cancellationToken = default;

        DeleteCourierHandler handler = new(loggerMock, validatorMock, repositoryMock);
        DeleteCourierCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<DeleteCourierCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<DeleteCourierCommand>(), cancellationToken).Received(1);
            await repositoryMock.Delete(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        DeleteCourierCommandValidator validatorMock = Substitute.For<DeleteCourierCommandValidator>();
        ILogger<DeleteCourierHandler> loggerMock = Substitute.For<ILogger<DeleteCourierHandler>>();
        ICourierWriteRepository repositoryMock = Substitute.For<ICourierWriteRepository>();
        CancellationToken cancellationToken = default;
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        DeleteCourierHandler handler = new(loggerMock, validatorMock, repositoryMock);

        DeleteCourierCommand cmd = new();
        Guid id = Guid.NewGuid();

        Courier courier = new() { Id = id };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        // TODO: change error type
        repositoryMock
            .Update(courier, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        repositoryMock
           .ReadForUpdate(cmd.Id, cancellationToken)
           .ReturnsForAnyArgs(
               Task.FromResult(courier));

        repositoryMock.CreateCourierHistoryItem(courier, cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        // Act
        Either<DeleteCourierCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<DeleteCourierCommand>(), cancellationToken).Received(1);
            await repositoryMock.Delete(id, cancellationToken).Received(1);
        });
    }
}