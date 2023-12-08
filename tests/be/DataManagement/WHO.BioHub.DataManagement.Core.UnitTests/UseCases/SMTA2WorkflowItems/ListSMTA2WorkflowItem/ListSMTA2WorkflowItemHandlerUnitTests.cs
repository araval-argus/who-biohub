using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItems;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItem;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItems;

public class ListSMTA2WorkflowItemsHandlerUnitTests
{
    [Fact]
    public async Task If_no_smta2workflowitems_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListSMTA2WorkflowItemsQueryValidator validatorMock = Substitute.For<ListSMTA2WorkflowItemsQueryValidator>();
        ILogger<ListSMTA2WorkflowItemsHandler> loggerMock = Substitute.For<ILogger<ListSMTA2WorkflowItemsHandler>>();
        ISMTA2WorkflowItemReadRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemReadRepository>();
        CancellationToken cancellationToken = default;
        IListSMTA2WorkflowItemMapper mapperMock = Substitute.For<IListSMTA2WorkflowItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListSMTA2WorkflowItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListSMTA2WorkflowItemsQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanReadSubmitSMTA2 } };
        mapperMock.Map(Arg.Any<List<SMTA2WorkflowItem>>()).ReturnsForAnyArgs(new List<SMTA2WorkflowItemDto>());

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<SMTA2WorkflowItem> smta2workflowitems = Array.Empty<SMTA2WorkflowItem>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(smta2workflowitems));
        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };


        roleRepositoryMock.GetRolesByPermissionName(Arg.Any<string>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(roles));

        // Act
        Either<ListSMTA2WorkflowItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.SMTA2WorkflowItems.Should()
            .BeEquivalentTo(smta2workflowitems, because: "Expected returned smta2workflowitems to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListSMTA2WorkflowItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_smta2workflowitem_exists_then_it_is_returned()
    {
        // Arrange
        ListSMTA2WorkflowItemsQueryValidator validatorMock = Substitute.For<ListSMTA2WorkflowItemsQueryValidator>();
        ILogger<ListSMTA2WorkflowItemsHandler> loggerMock = Substitute.For<ILogger<ListSMTA2WorkflowItemsHandler>>();
        ISMTA2WorkflowItemReadRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemReadRepository>();
        CancellationToken cancellationToken = default;
        IListSMTA2WorkflowItemMapper mapperMock = Substitute.For<IListSMTA2WorkflowItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListSMTA2WorkflowItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListSMTA2WorkflowItemsQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanReadSubmitSMTA2 } };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<SMTA2WorkflowItem> smta2workflowitems = new SMTA2WorkflowItem[1] { new() { Id = assignedId, Status = SMTA2WorkflowStatus.SubmitSMTA2 } };

        IEnumerable<SMTA2WorkflowItemDto> smta2workflowitemDtos = new SMTA2WorkflowItemDto[1] { new() { Id = assignedId, CurrentStatus = SMTA2WorkflowStatus.SubmitSMTA2 } };

        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(smta2workflowitems));

        mapperMock.Map(smta2workflowitems.ToList()).ReturnsForAnyArgs(smta2workflowitemDtos.ToList());

        roleRepositoryMock.GetRolesByPermissionName(Arg.Any<string>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(roles));

        // Act
        Either<ListSMTA2WorkflowItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.SMTA2WorkflowItems.Should()
            .BeEquivalentTo(smta2workflowitemDtos, because: "Expected returned smta2workflowitems to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListSMTA2WorkflowItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListSMTA2WorkflowItemsQueryValidator validatorMock = Substitute.For<ListSMTA2WorkflowItemsQueryValidator>();
        ILogger<ListSMTA2WorkflowItemsHandler> loggerMock = Substitute.For<ILogger<ListSMTA2WorkflowItemsHandler>>();
        ISMTA2WorkflowItemReadRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemReadRepository>();
        CancellationToken cancellationToken = default;
        IListSMTA2WorkflowItemMapper mapperMock = Substitute.For<IListSMTA2WorkflowItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListSMTA2WorkflowItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListSMTA2WorkflowItemsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListSMTA2WorkflowItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListSMTA2WorkflowItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}