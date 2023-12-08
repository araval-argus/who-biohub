using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItems;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ReadWorklistFromBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItem;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItems;

public class ListWorklistFromBioHubItemsHandlerUnitTests
{
    [Fact]
    public async Task If_no_worklistFromBioHubitems_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListWorklistFromBioHubItemsQueryValidator validatorMock = Substitute.For<ListWorklistFromBioHubItemsQueryValidator>();
        ILogger<ListWorklistFromBioHubItemsHandler> loggerMock = Substitute.For<ILogger<ListWorklistFromBioHubItemsHandler>>();
        IWorklistFromBioHubItemReadRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        CancellationToken cancellationToken = default;
        IListWorklistFromBioHubItemMapper mapperMock = Substitute.For<IListWorklistFromBioHubItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListWorklistFromBioHubItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListWorklistFromBioHubItemsQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitAnnex2OfSMTA1 } }; ;

        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };

        roleRepositoryMock.GetRolesByPermissionName(Arg.Any<string>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(roles));


        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<WorklistFromBioHubItem> worklistFromBioHubitems = Array.Empty<WorklistFromBioHubItem>();


        IEnumerable<WorklistFromBioHubItemDto> worklistFromBioHubitemDtos = Array.Empty<WorklistFromBioHubItemDto>();


        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklistFromBioHubitems));

        roleRepositoryMock.GetRolesByPermissionName(PermissionNames.CanReadSubmitAnnex2OfSMTA1, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(roles));

        var list = worklistFromBioHubitems.ToList();
        var mappedList = worklistFromBioHubitemDtos.ToList();

        mapperMock.Map(list).ReturnsForAnyArgs(mappedList);




        // Act
        Either<ListWorklistFromBioHubItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.WorklistFromBioHubItems.Should()
            .BeEquivalentTo(worklistFromBioHubitems, because: "Expected returned worklistFromBioHubitems to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListWorklistFromBioHubItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_worklistFromBioHubitem_exists_then_it_is_returned()
    {
        // Arrange
        ListWorklistFromBioHubItemsQueryValidator validatorMock = Substitute.For<ListWorklistFromBioHubItemsQueryValidator>();
        ILogger<ListWorklistFromBioHubItemsHandler> loggerMock = Substitute.For<ILogger<ListWorklistFromBioHubItemsHandler>>();
        IWorklistFromBioHubItemReadRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        CancellationToken cancellationToken = default;
        IListWorklistFromBioHubItemMapper mapperMock = Substitute.For<IListWorklistFromBioHubItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListWorklistFromBioHubItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListWorklistFromBioHubItemsQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitAnnex2OfSMTA1 } }; ;

        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };

        roleRepositoryMock.GetRolesByPermissionName(Arg.Any<string>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(roles));

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<WorklistFromBioHubItem> worklistFromBioHubitems = new WorklistFromBioHubItem[1] { new() { Id = assignedId, Status = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 } };

        IEnumerable<WorklistFromBioHubItemDto> worklistFromBioHubitemDtos = new WorklistFromBioHubItemDto[1] { new() { Id = assignedId, CurrentStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 } };


        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklistFromBioHubitems));

        roleRepositoryMock.GetRolesByPermissionName(PermissionNames.CanReadSubmitAnnex2OfSMTA1, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(roles));

        var list = worklistFromBioHubitems.ToList();
        var mappedList = worklistFromBioHubitemDtos.ToList();

        mapperMock.Map(list).ReturnsForAnyArgs(mappedList);

        // Act
        Either<ListWorklistFromBioHubItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.WorklistFromBioHubItems.Should()
            .BeEquivalentTo(worklistFromBioHubitemDtos, because: "Expected returned worklistFromBioHubitems to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListWorklistFromBioHubItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListWorklistFromBioHubItemsQueryValidator validatorMock = Substitute.For<ListWorklistFromBioHubItemsQueryValidator>();
        ILogger<ListWorklistFromBioHubItemsHandler> loggerMock = Substitute.For<ILogger<ListWorklistFromBioHubItemsHandler>>();
        IWorklistFromBioHubItemReadRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        CancellationToken cancellationToken = default;

        IListWorklistFromBioHubItemMapper mapperMock = Substitute.For<IListWorklistFromBioHubItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListWorklistFromBioHubItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListWorklistFromBioHubItemsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListWorklistFromBioHubItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListWorklistFromBioHubItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}