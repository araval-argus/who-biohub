using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.CreateShipment;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Shipments.CreateShipment;

public class CreateShipmentHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateShipmentCommandValidator validatorMock = Substitute.For<CreateShipmentCommandValidator>();
        ILogger<CreateShipmentHandler> loggerMock = Substitute.For<ILogger<CreateShipmentHandler>>();
        IShipmentWriteRepository repositoryMock = Substitute.For<IShipmentWriteRepository>();
        ICreateShipmentMapper mapperMock = Substitute.For<ICreateShipmentMapper>();
        CancellationToken cancellationToken = default;

        CreateShipmentHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateShipmentCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Shipment shipment = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(shipment);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(shipment, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<Shipment, Errors>>(() =>
                {
                    shipment.Id = assignedId;
                    return new(shipment);
                }));

        // Act
        Either<CreateShipmentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "Shipment should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the Shipment")
            .And.Be(assignedId, because: "Returned shipment Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateShipmentCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateShipmentCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Shipment>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateShipmentCommandValidator validatorMock = Substitute.For<CreateShipmentCommandValidator>();
        ILogger<CreateShipmentHandler> loggerMock = Substitute.For<ILogger<CreateShipmentHandler>>();
        IShipmentWriteRepository repositoryMock = Substitute.For<IShipmentWriteRepository>();
        ICreateShipmentMapper mapperMock = Substitute.For<ICreateShipmentMapper>();
        CancellationToken cancellationToken = default;

        CreateShipmentHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateShipmentCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateShipmentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateShipmentCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateShipmentCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<Shipment>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateShipmentCommandValidator validatorMock = Substitute.For<CreateShipmentCommandValidator>();
        ILogger<CreateShipmentHandler> loggerMock = Substitute.For<ILogger<CreateShipmentHandler>>();
        IShipmentWriteRepository repositoryMock = Substitute.For<IShipmentWriteRepository>();
        ICreateShipmentMapper mapperMock = Substitute.For<ICreateShipmentMapper>();
        CancellationToken cancellationToken = default;

        CreateShipmentHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateShipmentCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Shipment shipment = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(shipment);

        // TODO: change error type
        repositoryMock
            .Create(shipment, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<Shipment, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateShipmentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateShipmentCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateShipmentCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Shipment>(), cancellationToken).Received(1);
        });
    }
}