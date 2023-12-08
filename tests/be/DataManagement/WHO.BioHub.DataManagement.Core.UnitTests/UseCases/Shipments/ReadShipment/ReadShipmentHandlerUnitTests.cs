using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.ReadShipment;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Shipments.ReadShipment;

public class ReadShipmentHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadShipmentQueryValidator validatorMock = Substitute.For<ReadShipmentQueryValidator>();
        ILogger<ReadShipmentHandler> loggerMock = Substitute.For<ILogger<ReadShipmentHandler>>();
        IShipmentReadRepository repositoryMock = Substitute.For<IShipmentReadRepository>();
        CancellationToken cancellationToken = default;

        IReadShipmentMapper mapperMock = Substitute.For<IReadShipmentMapper>();
        ReadShipmentHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);

        ReadShipmentQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>()};

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        Shipment shipment = new() { Id = id };
        ShipmentViewModel shipmentViewModel = new() { Id = id };

        mapperMock.Map(shipment, cmd.RoleType.GetValueOrDefault(), cmd.UserPermissions).ReturnsForAnyArgs(shipmentViewModel);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Read(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(shipment));

        // Act
        Either<ReadShipmentQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Shipment.Id.Should()
            .Be(id, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadShipmentQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadShipmentQueryValidator validatorMock = Substitute.For<ReadShipmentQueryValidator>();
        ILogger<ReadShipmentHandler> loggerMock = Substitute.For<ILogger<ReadShipmentHandler>>();
        IShipmentReadRepository repositoryMock = Substitute.For<IShipmentReadRepository>();
        CancellationToken cancellationToken = default;

        IReadShipmentMapper mapperMock = Substitute.For<IReadShipmentMapper>();
        ReadShipmentHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);

        ReadShipmentQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadShipmentQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadShipmentQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}