using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItems;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItem;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItems;

public class ListSMTA1WorkflowItemsHandlerUnitTests
{
    [Fact]
    public async Task If_no_smta1workflowitems_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListSMTA1WorkflowItemsQueryValidator validatorMock = Substitute.For<ListSMTA1WorkflowItemsQueryValidator>();
        ILogger<ListSMTA1WorkflowItemsHandler> loggerMock = Substitute.For<ILogger<ListSMTA1WorkflowItemsHandler>>();
        ISMTA1WorkflowItemReadRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemReadRepository>();
        CancellationToken cancellationToken = default;
        IListSMTA1WorkflowItemMapper mapperMock = Substitute.For<IListSMTA1WorkflowItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListSMTA1WorkflowItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListSMTA1WorkflowItemsQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanReadSubmitSMTA1 } };
        mapperMock.Map(Arg.Any<List<SMTA1WorkflowItem>>()).ReturnsForAnyArgs(new List<SMTA1WorkflowItemDto>());

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<SMTA1WorkflowItem> smta1workflowitems = Array.Empty<SMTA1WorkflowItem>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(smta1workflowitems));
        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };


        roleRepositoryMock.GetRolesByPermissionName(Arg.Any<string>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(roles));

        // Act
        Either<ListSMTA1WorkflowItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);
        
        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.SMTA1WorkflowItems.Should()
            .BeEquivalentTo(smta1workflowitems, because: "Expected returned smta1workflowitems to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListSMTA1WorkflowItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_smta1workflowitem_exists_then_it_is_returned()
    {
        // Arrange
        ListSMTA1WorkflowItemsQueryValidator validatorMock = Substitute.For<ListSMTA1WorkflowItemsQueryValidator>();
        ILogger<ListSMTA1WorkflowItemsHandler> loggerMock = Substitute.For<ILogger<ListSMTA1WorkflowItemsHandler>>();
        ISMTA1WorkflowItemReadRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemReadRepository>();
        CancellationToken cancellationToken = default;
        IListSMTA1WorkflowItemMapper mapperMock = Substitute.For<IListSMTA1WorkflowItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListSMTA1WorkflowItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListSMTA1WorkflowItemsQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanReadSubmitSMTA1 } };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<SMTA1WorkflowItem> smta1workflowitems = new SMTA1WorkflowItem[1] { new() { Id = assignedId, Status = SMTA1WorkflowStatus.SubmitSMTA1 } };

        IEnumerable<SMTA1WorkflowItemDto> smta1workflowitemDtos = new SMTA1WorkflowItemDto[1] { new() { Id = assignedId, CurrentStatus = SMTA1WorkflowStatus.SubmitSMTA1 } };

        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(smta1workflowitems));

        mapperMock.Map(smta1workflowitems.ToList()).ReturnsForAnyArgs(smta1workflowitemDtos.ToList());

        roleRepositoryMock.GetRolesByPermissionName(Arg.Any<string>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(roles));

        // Act
        Either<ListSMTA1WorkflowItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.SMTA1WorkflowItems.Should()
            .BeEquivalentTo(smta1workflowitemDtos, because: "Expected returned smta1workflowitems to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListSMTA1WorkflowItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListSMTA1WorkflowItemsQueryValidator validatorMock = Substitute.For<ListSMTA1WorkflowItemsQueryValidator>();
        ILogger<ListSMTA1WorkflowItemsHandler> loggerMock = Substitute.For<ILogger<ListSMTA1WorkflowItemsHandler>>();
        ISMTA1WorkflowItemReadRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemReadRepository>();
        CancellationToken cancellationToken = default;
        IListSMTA1WorkflowItemMapper mapperMock = Substitute.For<IListSMTA1WorkflowItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListSMTA1WorkflowItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListSMTA1WorkflowItemsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListSMTA1WorkflowItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListSMTA1WorkflowItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}