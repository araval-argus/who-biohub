using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItems;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ReadWorklistToBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItem;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItems;

public class ListWorklistToBioHubItemsHandlerUnitTests
{
    [Fact]
    public async Task If_no_worklisttobiohubitems_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListWorklistToBioHubItemsQueryValidator validatorMock = Substitute.For<ListWorklistToBioHubItemsQueryValidator>();
        ILogger<ListWorklistToBioHubItemsHandler> loggerMock = Substitute.For<ILogger<ListWorklistToBioHubItemsHandler>>();
        IWorklistToBioHubItemReadRepository repositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        CancellationToken cancellationToken = default;
        IListWorklistToBioHubItemMapper mapperMock = Substitute.For<IListWorklistToBioHubItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListWorklistToBioHubItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);
        
        ListWorklistToBioHubItemsQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitAnnex2OfSMTA1 } }; ;

        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };

        roleRepositoryMock.GetRolesByPermissionName(Arg.Any<string>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(roles));


        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<WorklistToBioHubItem> worklisttobiohubitems = Array.Empty<WorklistToBioHubItem>();


        IEnumerable<WorklistToBioHubItemDto> worklisttobiohubitemDtos = Array.Empty<WorklistToBioHubItemDto>();
              

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklisttobiohubitems));

        roleRepositoryMock.GetRolesByPermissionName(PermissionNames.CanReadSubmitAnnex2OfSMTA1, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(roles));

        var list = worklisttobiohubitems.ToList();
        var mappedList = worklisttobiohubitemDtos.ToList();

        mapperMock.Map(list).ReturnsForAnyArgs(mappedList);


      

        // Act
        Either<ListWorklistToBioHubItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.WorklistToBioHubItems.Should()
            .BeEquivalentTo(worklisttobiohubitems, because: "Expected returned worklisttobiohubitems to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListWorklistToBioHubItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_worklisttobiohubitem_exists_then_it_is_returned()
    {
        // Arrange
        ListWorklistToBioHubItemsQueryValidator validatorMock = Substitute.For<ListWorklistToBioHubItemsQueryValidator>();
        ILogger<ListWorklistToBioHubItemsHandler> loggerMock = Substitute.For<ILogger<ListWorklistToBioHubItemsHandler>>();
        IWorklistToBioHubItemReadRepository repositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        CancellationToken cancellationToken = default;
        IListWorklistToBioHubItemMapper mapperMock = Substitute.For<IListWorklistToBioHubItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListWorklistToBioHubItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListWorklistToBioHubItemsQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitAnnex2OfSMTA1 } }; ;

        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };

        roleRepositoryMock.GetRolesByPermissionName(Arg.Any<string>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(roles));

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<WorklistToBioHubItem> worklisttobiohubitems = new WorklistToBioHubItem[1] { new() { Id = assignedId, Status = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 } };

        IEnumerable<WorklistToBioHubItemDto> worklisttobiohubitemDtos = new WorklistToBioHubItemDto[1] { new() { Id = assignedId, CurrentStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 } };
                

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklisttobiohubitems));

        roleRepositoryMock.GetRolesByPermissionName(PermissionNames.CanReadSubmitAnnex2OfSMTA1, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(roles));

        var list = worklisttobiohubitems.ToList();
        var mappedList = worklisttobiohubitemDtos.ToList();

        mapperMock.Map(list).ReturnsForAnyArgs(mappedList);

        // Act
        Either<ListWorklistToBioHubItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.WorklistToBioHubItems.Should()
            .BeEquivalentTo(worklisttobiohubitemDtos, because: "Expected returned worklisttobiohubitems to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListWorklistToBioHubItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListWorklistToBioHubItemsQueryValidator validatorMock = Substitute.For<ListWorklistToBioHubItemsQueryValidator>();
        ILogger<ListWorklistToBioHubItemsHandler> loggerMock = Substitute.For<ILogger<ListWorklistToBioHubItemsHandler>>();
        IWorklistToBioHubItemReadRepository repositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        CancellationToken cancellationToken = default;

        IListWorklistToBioHubItemMapper mapperMock = Substitute.For<IListWorklistToBioHubItemMapper>();

        IRoleReadRepository roleRepositoryMock = Substitute.For<IRoleReadRepository>();
        ListWorklistToBioHubItemsHandler handler = new(loggerMock, validatorMock, repositoryMock, roleRepositoryMock, mapperMock);

        ListWorklistToBioHubItemsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListWorklistToBioHubItemsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListWorklistToBioHubItemsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}