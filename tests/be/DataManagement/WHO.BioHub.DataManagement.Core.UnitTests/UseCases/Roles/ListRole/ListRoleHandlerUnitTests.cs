using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.DataManagement.Core.UseCases.Roles.ListRoles;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Roles.ListRoles;

public class ListRolesHandlerUnitTests
{
    [Fact]
    public async Task If_no_roles_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListRolesQueryValidator validatorMock = Substitute.For<ListRolesQueryValidator>();
        ILogger<ListRolesHandler> loggerMock = Substitute.For<ILogger<ListRolesHandler>>();
        IRoleReadRepository repositoryMock = Substitute.For<IRoleReadRepository>();
        CancellationToken cancellationToken = default;

        ListRolesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListRolesQuery cmd = new() { UserPermissions = new List<string>() }; ;

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Role> roles = Array.Empty<Role>();
        repositoryMock
            .List(true, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(roles));

        // Act
        Either<ListRolesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Roles.Should()
            .BeEquivalentTo(roles, because: "Expected returned roles to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListRolesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(true, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_role_exists_then_it_is_returned()
    {
        // Arrange
        ListRolesQueryValidator validatorMock = Substitute.For<ListRolesQueryValidator>();
        ILogger<ListRolesHandler> loggerMock = Substitute.For<ILogger<ListRolesHandler>>();
        IRoleReadRepository repositoryMock = Substitute.For<IRoleReadRepository>();
        CancellationToken cancellationToken = default;

        ListRolesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListRolesQuery cmd = new() { UserPermissions = new List<string>() };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Role> roles = new Role[1] { new() { Id = assignedId } };

        IEnumerable<RoleDto> roleDtos = new RoleDto[1] { new() { Id = assignedId } };


        repositoryMock
            .List(true, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(roles));

        // Act
        Either<ListRolesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Roles.Should()
            .BeEquivalentTo(roleDtos, because: "Expected returned roles to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListRolesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(true, cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListRolesQueryValidator validatorMock = Substitute.For<ListRolesQueryValidator>();
        ILogger<ListRolesHandler> loggerMock = Substitute.For<ILogger<ListRolesHandler>>();
        IRoleReadRepository repositoryMock = Substitute.For<IRoleReadRepository>();
        CancellationToken cancellationToken = default;

        ListRolesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListRolesQuery cmd = new() { UserPermissions = new List<string>() }; ;

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListRolesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListRolesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(true, cancellationToken).Received(0);
        });
    }
}