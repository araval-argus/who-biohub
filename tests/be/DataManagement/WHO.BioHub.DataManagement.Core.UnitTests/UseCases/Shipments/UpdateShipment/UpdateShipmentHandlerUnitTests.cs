using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.UpdateShipment;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Shipments.UpdateShipment;

public class UpdateShipmentHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        IWorklistItemUsedReferenceNumberReadRepository worklistItemUsedReferenceNumberReadRepositoryMock = Substitute.For<IWorklistItemUsedReferenceNumberReadRepository>();
        IShipmentReadRepository shipmentReadRepositoryMock = Substitute.For<IShipmentReadRepository>();
        UpdateShipmentCommandValidator validatorMock = Substitute.For<UpdateShipmentCommandValidator>(worklistItemUsedReferenceNumberReadRepositoryMock, shipmentReadRepositoryMock);
        ILogger<UpdateShipmentHandler> loggerMock = Substitute.For<ILogger<UpdateShipmentHandler>>();
        IShipmentWriteRepository repositoryMock = Substitute.For<IShipmentWriteRepository>();
        IUpdateShipmentMapper mapperMock = Substitute.For<IUpdateShipmentMapper>();
        CancellationToken cancellationToken = default;

        UpdateShipmentHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateShipmentCommand cmd = new() { ReferenceNumber = "test" };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Shipment shipment = new() { Id = Guid.NewGuid() };
        Shipment shipmentMapped = new() { Id = shipment.Id };

        mapperMock.Map(shipment, cmd).ReturnsForAnyArgs(shipmentMapped);

        repositoryMock
            .Update(shipment, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(shipment);

        shipmentReadRepositoryMock.Read(cmd.Id, cancellationToken).ReturnsForAnyArgs(shipment);

        worklistItemUsedReferenceNumberReadRepositoryMock.ReferenceNumberAlreadyPresent(shipment.ReferenceNumber, cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        // Act
        Either<UpdateShipmentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Shipment.Should()
            .NotBeEmpty(because: "Shipment should NOT be null");
        response.Left.Shipment.Should()
            .Be(shipmentMapped.Id, because: "Returned shipment must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateShipmentCommand>(), cancellationToken).Received(1);
            mapperMock.Map(shipment, Arg.Any<UpdateShipmentCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Shipment>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IWorklistItemUsedReferenceNumberReadRepository worklistItemUsedReferenceNumberReadRepositoryMock = Substitute.For<IWorklistItemUsedReferenceNumberReadRepository>();
        IShipmentReadRepository shipmentReadRepositoryMock = Substitute.For<IShipmentReadRepository>();
        UpdateShipmentCommandValidator validatorMock = Substitute.For<UpdateShipmentCommandValidator>(worklistItemUsedReferenceNumberReadRepositoryMock, shipmentReadRepositoryMock);
        ILogger<UpdateShipmentHandler> loggerMock = Substitute.For<ILogger<UpdateShipmentHandler>>();
        IShipmentWriteRepository repositoryMock = Substitute.For<IShipmentWriteRepository>();
        IUpdateShipmentMapper mapperMock = Substitute.For<IUpdateShipmentMapper>();
        CancellationToken cancellationToken = default;

        UpdateShipmentHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateShipmentCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateShipmentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateShipmentCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Shipment>(), Arg.Any<UpdateShipmentCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<Shipment>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IWorklistItemUsedReferenceNumberReadRepository worklistItemUsedReferenceNumberReadRepositoryMock = Substitute.For<IWorklistItemUsedReferenceNumberReadRepository>();
        IShipmentReadRepository shipmentReadRepositoryMock = Substitute.For<IShipmentReadRepository>();
        UpdateShipmentCommandValidator validatorMock = Substitute.For<UpdateShipmentCommandValidator>(worklistItemUsedReferenceNumberReadRepositoryMock, shipmentReadRepositoryMock);
        ILogger<UpdateShipmentHandler> loggerMock = Substitute.For<ILogger<UpdateShipmentHandler>>();
        IShipmentWriteRepository repositoryMock = Substitute.For<IShipmentWriteRepository>();
        IUpdateShipmentMapper mapperMock = Substitute.For<IUpdateShipmentMapper>();
        CancellationToken cancellationToken = default;

        UpdateShipmentHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateShipmentCommand cmd = new() { ReferenceNumber = "test" };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Shipment shipment = new();
        Shipment shipmentMapped = new();
        mapperMock.Map(shipment, cmd).ReturnsForAnyArgs(shipmentMapped);

        // TODO: change error type
        repositoryMock
            .Update(shipment, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(shipment);

        shipmentReadRepositoryMock.Read(cmd.Id, cancellationToken).ReturnsForAnyArgs(shipment);

        worklistItemUsedReferenceNumberReadRepositoryMock.ReferenceNumberAlreadyPresent(shipment.ReferenceNumber, cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        // Act
        Either<UpdateShipmentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateShipmentCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Shipment>(), Arg.Any<UpdateShipmentCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Shipment>(), cancellationToken).Received(1);
        });
    }
}