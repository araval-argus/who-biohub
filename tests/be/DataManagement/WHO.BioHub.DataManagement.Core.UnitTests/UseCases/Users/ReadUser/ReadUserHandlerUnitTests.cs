using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUser;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Users.ReadUser;

public class ReadUserHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadUserQueryValidator validatorMock = Substitute.For<ReadUserQueryValidator>();
        ILogger<ReadUserHandler> loggerMock = Substitute.For<ILogger<ReadUserHandler>>();
        IUserReadRepository repositoryMock = Substitute.For<IUserReadRepository>();
        CancellationToken cancellationToken = default;

        IReadUserMapper mapperMock = Substitute.For<IReadUserMapper>();
        ReadUserHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ReadUserQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        User user = new() { Id = id };
        UserViewModel userViewModel = new() { Id = id };
        mapperMock.Map(user).ReturnsForAnyArgs(userViewModel);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Read(id, true, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(user));

        // Act
        Either<ReadUserQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.User.Id.Should()
            .Be(id, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadUserQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, true, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadUserQueryValidator validatorMock = Substitute.For<ReadUserQueryValidator>();
        ILogger<ReadUserHandler> loggerMock = Substitute.For<ILogger<ReadUserHandler>>();
        IUserReadRepository repositoryMock = Substitute.For<IUserReadRepository>();
        CancellationToken cancellationToken = default;

        IReadUserMapper mapperMock = Substitute.For<IReadUserMapper>();
        ReadUserHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ReadUserQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadUserQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadUserQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), true, cancellationToken).Received(0);
        });
    }
}