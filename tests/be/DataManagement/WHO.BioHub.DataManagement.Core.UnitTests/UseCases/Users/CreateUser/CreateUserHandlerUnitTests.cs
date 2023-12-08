using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUser;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Users.CreateUser;

public class CreateUserHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        IUserReadRepository readRepositoryMock = Substitute.For<IUserReadRepository>();
        CreateUserCommandValidator validatorMock = Substitute.For<CreateUserCommandValidator>(readRepositoryMock);
        ILogger<CreateUserHandler> loggerMock = Substitute.For<ILogger<CreateUserHandler>>();
        IUserWriteRepository repositoryMock = Substitute.For<IUserWriteRepository>();
        ICreateUserMapper mapperMock = Substitute.For<ICreateUserMapper>();
        CancellationToken cancellationToken = default;

        CreateUserHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateUserCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        User user = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(user);

        readRepositoryMock.EmailPresent(cmd.Email, cancellationToken).ReturnsForAnyArgs(false);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(user, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<User, Errors>>(() =>
                {
                    user.Id = assignedId;
                    return new(user);
                }));

        // Act
        Either<CreateUserCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");      
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the User")
            .And.Be(assignedId, because: "Returned user Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateUserCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateUserCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<User>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IUserReadRepository readRepositoryMock = Substitute.For<IUserReadRepository>();
        CreateUserCommandValidator validatorMock = Substitute.For<CreateUserCommandValidator>(readRepositoryMock);
        ILogger<CreateUserHandler> loggerMock = Substitute.For<ILogger<CreateUserHandler>>();
        IUserWriteRepository repositoryMock = Substitute.For<IUserWriteRepository>();
        ICreateUserMapper mapperMock = Substitute.For<ICreateUserMapper>();
        CancellationToken cancellationToken = default;

        CreateUserHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateUserCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateUserCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateUserCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateUserCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<User>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IUserReadRepository readRepositoryMock = Substitute.For<IUserReadRepository>();
        CreateUserCommandValidator validatorMock = Substitute.For<CreateUserCommandValidator>(readRepositoryMock);
        ILogger<CreateUserHandler> loggerMock = Substitute.For<ILogger<CreateUserHandler>>();
        IUserWriteRepository repositoryMock = Substitute.For<IUserWriteRepository>();
        ICreateUserMapper mapperMock = Substitute.For<ICreateUserMapper>();
        CancellationToken cancellationToken = default;

        CreateUserHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateUserCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        User user = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(user);

        readRepositoryMock.EmailPresent(cmd.Email, cancellationToken).ReturnsForAnyArgs(false);

        // TODO: change error type
        repositoryMock
            .Create(user, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<User, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateUserCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateUserCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateUserCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<User>(), cancellationToken).Received(1);
        });
    }
}