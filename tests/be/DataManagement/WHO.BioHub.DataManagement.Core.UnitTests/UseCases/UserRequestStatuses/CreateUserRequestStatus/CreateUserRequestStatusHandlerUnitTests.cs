using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.CreateUserRequestStatus;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.UserRequestStatuses.CreateUserRequestStatus;

public class CreateUserRequestStatusHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateUserRequestStatusCommandValidator validatorMock = Substitute.For<CreateUserRequestStatusCommandValidator>();
        ILogger<CreateUserRequestStatusHandler> loggerMock = Substitute.For<ILogger<CreateUserRequestStatusHandler>>();
        IUserRequestStatusWriteRepository repositoryMock = Substitute.For<IUserRequestStatusWriteRepository>();
        ICreateUserRequestStatusMapper mapperMock = Substitute.For<ICreateUserRequestStatusMapper>();
        CancellationToken cancellationToken = default;

        CreateUserRequestStatusHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateUserRequestStatusCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        UserRequestStatus userRequestStatus = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(userRequestStatus);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(userRequestStatus, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<UserRequestStatus, Errors>>(() =>
                {
                    userRequestStatus.Id = assignedId;
                    return new(userRequestStatus);
                }));

        // Act
        Either<CreateUserRequestStatusCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.UserRequestStatus.Should()
            .NotBeNull(because: "UserRequestStatus should NOT be null");
        response.Left.UserRequestStatus.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the UserRequestStatus")
            .And.Be(assignedId, because: "Returned userRequestStatus Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateUserRequestStatusCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateUserRequestStatusCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<UserRequestStatus>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateUserRequestStatusCommandValidator validatorMock = Substitute.For<CreateUserRequestStatusCommandValidator>();
        ILogger<CreateUserRequestStatusHandler> loggerMock = Substitute.For<ILogger<CreateUserRequestStatusHandler>>();
        IUserRequestStatusWriteRepository repositoryMock = Substitute.For<IUserRequestStatusWriteRepository>();
        ICreateUserRequestStatusMapper mapperMock = Substitute.For<ICreateUserRequestStatusMapper>();
        CancellationToken cancellationToken = default;

        CreateUserRequestStatusHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateUserRequestStatusCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateUserRequestStatusCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateUserRequestStatusCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateUserRequestStatusCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<UserRequestStatus>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateUserRequestStatusCommandValidator validatorMock = Substitute.For<CreateUserRequestStatusCommandValidator>();
        ILogger<CreateUserRequestStatusHandler> loggerMock = Substitute.For<ILogger<CreateUserRequestStatusHandler>>();
        IUserRequestStatusWriteRepository repositoryMock = Substitute.For<IUserRequestStatusWriteRepository>();
        ICreateUserRequestStatusMapper mapperMock = Substitute.For<ICreateUserRequestStatusMapper>();
        CancellationToken cancellationToken = default;

        CreateUserRequestStatusHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateUserRequestStatusCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        UserRequestStatus userRequestStatus = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(userRequestStatus);

        // TODO: change error type
        repositoryMock
            .Create(userRequestStatus, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<UserRequestStatus, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateUserRequestStatusCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateUserRequestStatusCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateUserRequestStatusCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<UserRequestStatus>(), cancellationToken).Received(1);
        });
    }
}