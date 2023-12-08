using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.DataManagement.Core.UseCases.Roles.UpdateRole;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Roles.UpdateRole;

public class UpdateRoleHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateRoleCommandValidator validatorMock = Substitute.For<UpdateRoleCommandValidator>();
        ILogger<UpdateRoleHandler> loggerMock = Substitute.For<ILogger<UpdateRoleHandler>>();
        IRoleWriteRepository repositoryMock = Substitute.For<IRoleWriteRepository>();
        IUpdateRoleMapper mapperMock = Substitute.For<IUpdateRoleMapper>();
        CancellationToken cancellationToken = default;

        UpdateRoleHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateRoleCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Role role = new() { Id = Guid.NewGuid() };
        Role roleMapped = new() { Id = role.Id };

        mapperMock.Map(role, cmd).ReturnsForAnyArgs(roleMapped);

        repositoryMock
            .Update(role, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateRoleCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Role.Should()
            .NotBeNull(because: "Role should NOT be null");
        response.Left.Role.Should()
            .BeEquivalentTo(roleMapped, because: "Returned role must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateRoleCommand>(), cancellationToken).Received(1);
            mapperMock.Map(role, Arg.Any<UpdateRoleCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Role>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateRoleCommandValidator validatorMock = Substitute.For<UpdateRoleCommandValidator>();
        ILogger<UpdateRoleHandler> loggerMock = Substitute.For<ILogger<UpdateRoleHandler>>();
        IRoleWriteRepository repositoryMock = Substitute.For<IRoleWriteRepository>();
        IUpdateRoleMapper mapperMock = Substitute.For<IUpdateRoleMapper>();
        CancellationToken cancellationToken = default;

        UpdateRoleHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateRoleCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateRoleCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateRoleCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Role>(), Arg.Any<UpdateRoleCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<Role>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateRoleCommandValidator validatorMock = Substitute.For<UpdateRoleCommandValidator>();
        ILogger<UpdateRoleHandler> loggerMock = Substitute.For<ILogger<UpdateRoleHandler>>();
        IRoleWriteRepository repositoryMock = Substitute.For<IRoleWriteRepository>();
        IUpdateRoleMapper mapperMock = Substitute.For<IUpdateRoleMapper>();
        CancellationToken cancellationToken = default;

        UpdateRoleHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateRoleCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Role role = new();
        Role roleMapped = new();
        mapperMock.Map(role, cmd).ReturnsForAnyArgs(roleMapped);

        // TODO: change error type
        repositoryMock
            .Update(role, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateRoleCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateRoleCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Role>(), Arg.Any<UpdateRoleCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Role>(), cancellationToken).Received(1);
        });
    }
}