using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.UpdateCourier;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Repositories.Users;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Couriers.UpdateCourier;

public class UpdateCourierHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        IUserReadRepository userRepositoryMock = Substitute.For<IUserReadRepository>();
        UpdateCourierCommandValidator validatorMock = Substitute.For<UpdateCourierCommandValidator>(userRepositoryMock);
        ILogger<UpdateCourierHandler> loggerMock = Substitute.For<ILogger<UpdateCourierHandler>>();
        ICourierWriteRepository repositoryMock = Substitute.For<ICourierWriteRepository>();
        IUpdateCourierMapper mapperMock = Substitute.For<IUpdateCourierMapper>();
        CancellationToken cancellationToken = default;
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateCourierHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateCourierCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Courier courier = new() { Id = Guid.NewGuid() };
        Courier courierMapped = new() { Id = courier.Id };

        mapperMock.Map(courier, cmd).ReturnsForAnyArgs(courierMapped);

        repositoryMock
            .Update(courier, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryMock.CreateCourierHistoryItem(courier, cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryMock
          .ReadForUpdate(cmd.Id, cancellationToken)
          .ReturnsForAnyArgs(
              Task.FromResult(courier));

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        userRepositoryMock.EmailPresent(string.Empty, cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));
        // Act
        Either<UpdateCourierCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
       
        response.Left.Id.Should()
            .Be(courierMapped.Id, because: "Returned courier must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateCourierCommand>(), cancellationToken).Received(1);
            mapperMock.Map(courier, Arg.Any<UpdateCourierCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Courier>(), cancellationToken, transactionMock).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IUserReadRepository userRepositoryMock = Substitute.For<IUserReadRepository>();
        UpdateCourierCommandValidator validatorMock = Substitute.For<UpdateCourierCommandValidator>(userRepositoryMock);
        ILogger<UpdateCourierHandler> loggerMock = Substitute.For<ILogger<UpdateCourierHandler>>();
        ICourierWriteRepository repositoryMock = Substitute.For<ICourierWriteRepository>();
        IUpdateCourierMapper mapperMock = Substitute.For<IUpdateCourierMapper>();
        CancellationToken cancellationToken = default;

        UpdateCourierHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateCourierCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateCourierCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateCourierCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Courier>(), Arg.Any<UpdateCourierCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<Courier>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IUserReadRepository userRepositoryMock = Substitute.For<IUserReadRepository>();
        UpdateCourierCommandValidator validatorMock = Substitute.For<UpdateCourierCommandValidator>(userRepositoryMock);
        ILogger<UpdateCourierHandler> loggerMock = Substitute.For<ILogger<UpdateCourierHandler>>();
        ICourierWriteRepository repositoryMock = Substitute.For<ICourierWriteRepository>();
        IUpdateCourierMapper mapperMock = Substitute.For<IUpdateCourierMapper>();
        CancellationToken cancellationToken = default;

        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateCourierHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateCourierCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Courier courier = new() { Id = Guid.NewGuid() };
        Courier courierMapped = new() { Id = courier.Id };

        mapperMock.Map(courier, cmd).ReturnsForAnyArgs(courierMapped);

        repositoryMock
          .ReadForUpdate(cmd.Id, cancellationToken)
          .ReturnsForAnyArgs(
              Task.FromResult(courier));

        repositoryMock.CreateCourierHistoryItem(courier, cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        userRepositoryMock.EmailPresent(string.Empty, cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        // TODO: change error type
        repositoryMock
            .Update(courier, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateCourierCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateCourierCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Courier>(), Arg.Any<UpdateCourierCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Courier>(), cancellationToken, transactionMock).Received(1);
        });
    }
}