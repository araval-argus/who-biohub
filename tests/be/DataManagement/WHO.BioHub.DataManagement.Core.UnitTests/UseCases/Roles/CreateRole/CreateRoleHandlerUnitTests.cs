using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.DataManagement.Core.UseCases.Roles.CreateRole;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Roles.CreateRole;

public class CreateRoleHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateRoleCommandValidator validatorMock = Substitute.For<CreateRoleCommandValidator>();
        ILogger<CreateRoleHandler> loggerMock = Substitute.For<ILogger<CreateRoleHandler>>();
        IRoleWriteRepository repositoryMock = Substitute.For<IRoleWriteRepository>();
        ICreateRoleMapper mapperMock = Substitute.For<ICreateRoleMapper>();
        CancellationToken cancellationToken = default;

        CreateRoleHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateRoleCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Role role = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(role);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(role, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<Role, Errors>>(() =>
                {
                    role.Id = assignedId;
                    return new(role);
                }));

        // Act
        Either<CreateRoleCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Role.Should()
            .NotBeNull(because: "Role should NOT be null");
        response.Left.Role.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the Role")
            .And.Be(assignedId, because: "Returned role Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateRoleCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateRoleCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Role>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateRoleCommandValidator validatorMock = Substitute.For<CreateRoleCommandValidator>();
        ILogger<CreateRoleHandler> loggerMock = Substitute.For<ILogger<CreateRoleHandler>>();
        IRoleWriteRepository repositoryMock = Substitute.For<IRoleWriteRepository>();
        ICreateRoleMapper mapperMock = Substitute.For<ICreateRoleMapper>();
        CancellationToken cancellationToken = default;

        CreateRoleHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateRoleCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateRoleCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateRoleCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateRoleCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<Role>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateRoleCommandValidator validatorMock = Substitute.For<CreateRoleCommandValidator>();
        ILogger<CreateRoleHandler> loggerMock = Substitute.For<ILogger<CreateRoleHandler>>();
        IRoleWriteRepository repositoryMock = Substitute.For<IRoleWriteRepository>();
        ICreateRoleMapper mapperMock = Substitute.For<ICreateRoleMapper>();
        CancellationToken cancellationToken = default;

        CreateRoleHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateRoleCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Role role = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(role);

        // TODO: change error type
        repositoryMock
            .Create(role, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<Role, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateRoleCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateRoleCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateRoleCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Role>(), cancellationToken).Received(1);
        });
    }
}