using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.ListCouriers;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Couriers.ListCouriers;

public class ListCouriersHandlerUnitTests
{
    [Fact]
    public async Task If_no_couriers_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListCouriersQueryValidator validatorMock = Substitute.For<ListCouriersQueryValidator>();
        ILogger<ListCouriersHandler> loggerMock = Substitute.For<ILogger<ListCouriersHandler>>();
        ICourierReadRepository repositoryMock = Substitute.For<ICourierReadRepository>();
        CancellationToken cancellationToken = default;

        IListCouriersMapper mapperMock = Substitute.For<IListCouriersMapper>();
        ListCouriersHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListCouriersQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Courier> couriers = Array.Empty<Courier>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(couriers));

        // Act
        Either<ListCouriersQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Couriers.Should()
            .BeEquivalentTo(couriers, because: "Expected returned couriers to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListCouriersQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_courier_exists_then_it_is_returned()
    {
        // Arrange
        ListCouriersQueryValidator validatorMock = Substitute.For<ListCouriersQueryValidator>();
        ILogger<ListCouriersHandler> loggerMock = Substitute.For<ILogger<ListCouriersHandler>>();
        ICourierReadRepository repositoryMock = Substitute.For<ICourierReadRepository>();
        CancellationToken cancellationToken = default;

        IListCouriersMapper mapperMock = Substitute.For<IListCouriersMapper>();
        ListCouriersHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListCouriersQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Courier> couriers = new Courier[1] { new() { Id = assignedId } };
        IEnumerable<CourierViewModel> courierDtos = new CourierViewModel[1] { new() { Id = assignedId } };


        mapperMock.Map(couriers).ReturnsForAnyArgs(courierDtos);

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(couriers));

        // Act
        Either<ListCouriersQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Couriers.Should()
            .BeEquivalentTo(courierDtos, because: "Expected returned couriers to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListCouriersQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListCouriersQueryValidator validatorMock = Substitute.For<ListCouriersQueryValidator>();
        ILogger<ListCouriersHandler> loggerMock = Substitute.For<ILogger<ListCouriersHandler>>();
        ICourierReadRepository repositoryMock = Substitute.For<ICourierReadRepository>();
        CancellationToken cancellationToken = default;

        IListCouriersMapper mapperMock = Substitute.For<IListCouriersMapper>();
        ListCouriersHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListCouriersQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListCouriersQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListCouriersQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}