using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.DataManagement.Core.UseCases.Users.UpdateUser;
using WHO.BioHub.Shared.Utils;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Users.UpdateUser;

public class UpdateUserHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        
        UpdateUserCommandValidator validatorMock = Substitute.For<UpdateUserCommandValidator>();
        ILogger<UpdateUserHandler> loggerMock = Substitute.For<ILogger<UpdateUserHandler>>();
        IUserWriteRepository repositoryMock = Substitute.For<IUserWriteRepository>();
        IUpdateUserMapper mapperMock = Substitute.For<IUpdateUserMapper>();
        CancellationToken cancellationToken = default;
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateUserHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);      

        UpdateUserCommand cmd = new() { };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        User user = new() { Id = Guid.NewGuid() };
        User userMapped = new() { Id = user.Id };
     

        mapperMock.Map(user, cmd).ReturnsForAnyArgs(userMapped);
        
        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(user));

        repositoryMock
            .Update(user, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryMock.CreateUserHistoryItem(user, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateUserCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "User should NOT be null");
        response.Left.Id.Should()
            .Be(user.Id, because: "Returned user must mach the one provided in request");
        Received.InOrder(async () =>
        {
            mapperMock.Map(user, Arg.Any<UpdateUserCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<User>(), cancellationToken, transactionMock).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateUserCommandValidator validatorMock = Substitute.For<UpdateUserCommandValidator>();
        ILogger<UpdateUserHandler> loggerMock = Substitute.For<ILogger<UpdateUserHandler>>();
        IUserWriteRepository repositoryMock = Substitute.For<IUserWriteRepository>();
        IUpdateUserMapper mapperMock = Substitute.For<IUpdateUserMapper>();
        CancellationToken cancellationToken = default;

        UpdateUserHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateUserCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateUserCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateUserCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<User>(), Arg.Any<UpdateUserCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<User>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateUserCommandValidator validatorMock = Substitute.For<UpdateUserCommandValidator>();
        ILogger<UpdateUserHandler> loggerMock = Substitute.For<ILogger<UpdateUserHandler>>();
        IUserWriteRepository repositoryMock = Substitute.For<IUserWriteRepository>();
        IUpdateUserMapper mapperMock = Substitute.For<IUpdateUserMapper>();
        CancellationToken cancellationToken = default;
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateUserHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateUserCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        User user = new();
        User userMapped = new();
        mapperMock.Map(user, cmd).ReturnsForAnyArgs(userMapped);

        // TODO: change error type
        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(user));

        repositoryMock
            .Update(user, cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateUserCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateUserCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<User>(), Arg.Any<UpdateUserCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<User>(), cancellationToken, transactionMock).Received(1);
        });
    }
}