using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.CreateCourier;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Repositories.Users;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Couriers.CreateCourier;

public class CreateCourierHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        IUserReadRepository userRepositoryMock = Substitute.For<IUserReadRepository>();
        CreateCourierCommandValidator validatorMock = Substitute.For<CreateCourierCommandValidator>(userRepositoryMock);
        ILogger<CreateCourierHandler> loggerMock = Substitute.For<ILogger<CreateCourierHandler>>();
        ICourierWriteRepository repositoryMock = Substitute.For<ICourierWriteRepository>();
        ICreateCourierMapper mapperMock = Substitute.For<ICreateCourierMapper>();
        CancellationToken cancellationToken = default;

        CreateCourierHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateCourierCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Courier courier = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(courier);

        Guid assignedId = Guid.NewGuid();


        userRepositoryMock.EmailPresent(string.Empty, cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        repositoryMock
            .Create(courier, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<Courier, Errors>>(() =>
                {
                    courier.Id = assignedId;
                    return new(courier);
                }));

        // Act
        Either<CreateCourierCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");        
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the Courier")
            .And.Be(assignedId, because: "Returned courier Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateCourierCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateCourierCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Courier>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IUserReadRepository userRepositoryMock = Substitute.For<IUserReadRepository>();
        CreateCourierCommandValidator validatorMock = Substitute.For<CreateCourierCommandValidator>(userRepositoryMock);
        ILogger<CreateCourierHandler> loggerMock = Substitute.For<ILogger<CreateCourierHandler>>();
        ICourierWriteRepository repositoryMock = Substitute.For<ICourierWriteRepository>();
        ICreateCourierMapper mapperMock = Substitute.For<ICreateCourierMapper>();
        CancellationToken cancellationToken = default;

        CreateCourierHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateCourierCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateCourierCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateCourierCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateCourierCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<Courier>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IUserReadRepository userRepositoryMock = Substitute.For<IUserReadRepository>();
        CreateCourierCommandValidator validatorMock = Substitute.For<CreateCourierCommandValidator>(userRepositoryMock);
        ILogger<CreateCourierHandler> loggerMock = Substitute.For<ILogger<CreateCourierHandler>>();
        ICourierWriteRepository repositoryMock = Substitute.For<ICourierWriteRepository>();
        ICreateCourierMapper mapperMock = Substitute.For<ICreateCourierMapper>();
        CancellationToken cancellationToken = default;

        CreateCourierHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateCourierCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Courier courier = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(courier);

        // TODO: change error type
        repositoryMock
            .Create(courier, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<Courier, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        userRepositoryMock.EmailPresent(string.Empty, cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        // Act
        Either<CreateCourierCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateCourierCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateCourierCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Courier>(), cancellationToken).Received(1);
        });
    }
}