using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsers;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUser;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Users.ListUsers;

public class ListUsersHandlerUnitTests
{
    [Fact]
    public async Task If_no_users_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListUsersQueryValidator validatorMock = Substitute.For<ListUsersQueryValidator>();
        ILogger<ListUsersHandler> loggerMock = Substitute.For<ILogger<ListUsersHandler>>();
        IUserReadRepository repositoryMock = Substitute.For<IUserReadRepository>();
        CancellationToken cancellationToken = default;

        IListUsersMapper mapperMock = Substitute.For<IListUsersMapper>();

        ListUsersHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ListUsersQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<User> users = Array.Empty<User>();
        repositoryMock
            .List(true, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(users));

        // Act
        Either<ListUsersQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Users.Should()
            .BeEquivalentTo(users, because: "Expected returned users to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListUsersQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(true, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_user_exists_then_it_is_returned()
    {
        // Arrange
        ListUsersQueryValidator validatorMock = Substitute.For<ListUsersQueryValidator>();
        ILogger<ListUsersHandler> loggerMock = Substitute.For<ILogger<ListUsersHandler>>();
        IUserReadRepository repositoryMock = Substitute.For<IUserReadRepository>();
        CancellationToken cancellationToken = default;

        IListUsersMapper mapperMock = Substitute.For<IListUsersMapper>();

        ListUsersHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);

        ListUsersQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<User> users = new User[1] { new() { Id = assignedId } };
        IEnumerable<UserViewModel> usersViewModel = new UserViewModel[1] { new() { Id = assignedId } };

        mapperMock.Map(users).ReturnsForAnyArgs(usersViewModel);

        repositoryMock
            .List(true, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(users));

        // Act
        Either<ListUsersQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Users.Should()
            .BeEquivalentTo(usersViewModel, because: "Expected returned users to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListUsersQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(true, cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListUsersQueryValidator validatorMock = Substitute.For<ListUsersQueryValidator>();
        ILogger<ListUsersHandler> loggerMock = Substitute.For<ILogger<ListUsersHandler>>();
        IUserReadRepository repositoryMock = Substitute.For<IUserReadRepository>();
        CancellationToken cancellationToken = default;

        IListUsersMapper mapperMock = Substitute.For<IListUsersMapper>();

        ListUsersHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);

        ListUsersQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListUsersQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListUsersQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(true, cancellationToken).Received(0);
        });
    }
}