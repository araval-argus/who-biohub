using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.ListShipments;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Shipments.ListShipments;

public class ListShipmentsHandlerUnitTests
{
    [Fact]
    public async Task If_no_shipments_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListShipmentsQueryValidator validatorMock = Substitute.For<ListShipmentsQueryValidator>();
        ILogger<ListShipmentsHandler> loggerMock = Substitute.For<ILogger<ListShipmentsHandler>>();
        IShipmentReadRepository repositoryMock = Substitute.For<IShipmentReadRepository>();
        CancellationToken cancellationToken = default;

        IListShipmentsMapper mapperMock = Substitute.For<IListShipmentsMapper>();
        ListShipmentsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListShipmentsQuery cmd = new() { RoleType = RoleType.WHO }; 

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Shipment> shipments = Array.Empty<Shipment>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(shipments));

        // Act
        Either<ListShipmentsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Shipments.Should()
            .BeEquivalentTo(shipments, because: "Expected returned shipments to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListShipmentsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_shipment_exists_then_it_is_returned()
    {
        // Arrange
        ListShipmentsQueryValidator validatorMock = Substitute.For<ListShipmentsQueryValidator>();
        ILogger<ListShipmentsHandler> loggerMock = Substitute.For<ILogger<ListShipmentsHandler>>();
        IShipmentReadRepository repositoryMock = Substitute.For<IShipmentReadRepository>();

        CancellationToken cancellationToken = default;

        IListShipmentsMapper mapperMock = Substitute.For<IListShipmentsMapper>();
        ListShipmentsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);

        ListShipmentsQuery cmd = new() { RoleType = RoleType.WHO };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Shipment> shipments = new Shipment[1] { new() { Id = assignedId } };
        IEnumerable<ShipmentViewModel> shipmentViewModels = new ShipmentViewModel[1] { new() { Id = assignedId } };
        
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(shipments));

        mapperMock.Map(shipments).ReturnsForAnyArgs(shipmentViewModels);

        // Act
        Either<ListShipmentsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Shipments.Should()
            .BeEquivalentTo(shipmentViewModels, because: "Expected returned shipments to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListShipmentsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListShipmentsQueryValidator validatorMock = Substitute.For<ListShipmentsQueryValidator>();
        ILogger<ListShipmentsHandler> loggerMock = Substitute.For<ILogger<ListShipmentsHandler>>();
        IShipmentReadRepository repositoryMock = Substitute.For<IShipmentReadRepository>();
        CancellationToken cancellationToken = default;

        IListShipmentsMapper mapperMock = Substitute.For<IListShipmentsMapper>();
        ListShipmentsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListShipmentsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListShipmentsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListShipmentsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}