using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.CreateUserRequest;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Repositories.Users;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.UserRequests.CreateUserRequest;

public class CreateUserRequestHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        IUserReadRepository userReadRepositoryMock = Substitute.For<IUserReadRepository>();
        CreateUserRequestCommandValidator validatorMock = Substitute.For<CreateUserRequestCommandValidator>(userReadRepositoryMock);
        ILogger<CreateUserRequestHandler> loggerMock = Substitute.For<ILogger<CreateUserRequestHandler>>();
        IUserRequestWriteRepository repositoryMock = Substitute.For<IUserRequestWriteRepository>();
        ICreateUserRequestMapper mapperMock = Substitute.For<ICreateUserRequestMapper>();
        CancellationToken cancellationToken = default;

        CreateUserRequestHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateUserRequestCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        UserRequest userRequest = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(userRequest);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(userRequest, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<UserRequest, Errors>>(() =>
                {
                    userRequest.Id = assignedId;
                    return new(userRequest);
                }));

        // Act
        Either<CreateUserRequestCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.UserRequest.Should()
            .NotBeNull(because: "UserRequest should NOT be null");
        response.Left.UserRequest.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the UserRequest")
            .And.Be(assignedId, because: "Returned userRequest Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateUserRequestCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateUserRequestCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<UserRequest>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IUserReadRepository userReadRepositoryMock = Substitute.For<IUserReadRepository>();
        CreateUserRequestCommandValidator validatorMock = Substitute.For<CreateUserRequestCommandValidator>(userReadRepositoryMock);
        ILogger<CreateUserRequestHandler> loggerMock = Substitute.For<ILogger<CreateUserRequestHandler>>();
        IUserRequestWriteRepository repositoryMock = Substitute.For<IUserRequestWriteRepository>();
        ICreateUserRequestMapper mapperMock = Substitute.For<ICreateUserRequestMapper>();
        CancellationToken cancellationToken = default;

        CreateUserRequestHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateUserRequestCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateUserRequestCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateUserRequestCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateUserRequestCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<UserRequest>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IUserReadRepository userReadRepositoryMock = Substitute.For<IUserReadRepository>();
        CreateUserRequestCommandValidator validatorMock = Substitute.For<CreateUserRequestCommandValidator>(userReadRepositoryMock);
        ILogger<CreateUserRequestHandler> loggerMock = Substitute.For<ILogger<CreateUserRequestHandler>>();
        IUserRequestWriteRepository repositoryMock = Substitute.For<IUserRequestWriteRepository>();
        ICreateUserRequestMapper mapperMock = Substitute.For<ICreateUserRequestMapper>();
        CancellationToken cancellationToken = default;

        CreateUserRequestHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateUserRequestCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        UserRequest userRequest = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(userRequest);

        // TODO: change error type
        repositoryMock
            .Create(userRequest, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<UserRequest, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateUserRequestCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateUserRequestCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateUserRequestCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<UserRequest>(), cancellationToken).Received(1);
        });
    }
}