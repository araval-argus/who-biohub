using FluentValidation.Results;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.DeleteShipment;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Shipments.DeleteShipment;

public class DeleteShipmentHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_deleted_then_a_valid_response_is_returned()
    {
        // Arrange
        DeleteShipmentCommandValidator validatorMock = Substitute.For<DeleteShipmentCommandValidator>();
        ILogger<DeleteShipmentHandler> loggerMock = Substitute.For<ILogger<DeleteShipmentHandler>>();
        IShipmentWriteRepository repositoryMock = Substitute.For<IShipmentWriteRepository>();
        CancellationToken cancellationToken = default;

        DeleteShipmentHandler handler = new(loggerMock, validatorMock, repositoryMock);
        DeleteShipmentCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Delete(id, cancellationToken)
            .ReturnsForAnyArgs(
                Task.FromResult<Errors?>(null));

        // Act
        Either<DeleteShipmentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<DeleteShipmentCommand>(), cancellationToken).Received(1);
            await repositoryMock.Delete(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        DeleteShipmentCommandValidator validatorMock = Substitute.For<DeleteShipmentCommandValidator>();
        ILogger<DeleteShipmentHandler> loggerMock = Substitute.For<ILogger<DeleteShipmentHandler>>();
        IShipmentWriteRepository repositoryMock = Substitute.For<IShipmentWriteRepository>();
        CancellationToken cancellationToken = default;

        DeleteShipmentHandler handler = new(loggerMock, validatorMock, repositoryMock);
        DeleteShipmentCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<DeleteShipmentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<DeleteShipmentCommand>(), cancellationToken).Received(1);
            await repositoryMock.Delete(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        DeleteShipmentCommandValidator validatorMock = Substitute.For<DeleteShipmentCommandValidator>();
        ILogger<DeleteShipmentHandler> loggerMock = Substitute.For<ILogger<DeleteShipmentHandler>>();
        IShipmentWriteRepository repositoryMock = Substitute.For<IShipmentWriteRepository>();
        CancellationToken cancellationToken = default;

        DeleteShipmentHandler handler = new(loggerMock, validatorMock, repositoryMock);

        DeleteShipmentCommand cmd = new();
        Guid id = Guid.NewGuid();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        // TODO: change error type
        repositoryMock
            .Delete(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<DeleteShipmentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<DeleteShipmentCommand>(), cancellationToken).Received(1);
            await repositoryMock.Delete(id, cancellationToken).Received(1);
        });
    }
}