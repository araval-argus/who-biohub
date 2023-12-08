using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ReadWorklistToBioHubItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistToBioHubItems.ReadWorklistToBioHubItem;

public class ReadWorklistToBioHubItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadWorklistToBioHubItemQueryValidator validatorMock = Substitute.For<ReadWorklistToBioHubItemQueryValidator>();
        ILogger<ReadWorklistToBioHubItemHandler> loggerMock = Substitute.For<ILogger<ReadWorklistToBioHubItemHandler>>();
        IWorklistToBioHubItemReadRepository repositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        CancellationToken cancellationToken = default;
        IReadWorklistToBioHubItemMapper mapperMock = Substitute.For<IReadWorklistToBioHubItemMapper>();
        IDocumentTemplateReadRepository repositoryDocumentTemplateMock = Substitute.For<IDocumentTemplateReadRepository>();
        IDocumentReadRepository repositoryDocumentMock = Substitute.For<IDocumentReadRepository>();
        ReadWorklistToBioHubItemHandler handler = new(loggerMock, validatorMock, repositoryMock, repositoryDocumentTemplateMock, mapperMock, repositoryDocumentMock);

        ReadWorklistToBioHubItemQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { {PermissionNames.CanReadSubmitAnnex2OfSMTA1 }, { PermissionNames.CanSubmitAnnex2OfSMTA1 } } }; 

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        WorklistToBioHubItem worklisttobiohubitem = new() { Id = id, Status = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 };
        WorklistToBioHubItemDto worklisttobiohubitemMapped = new() { Id = id, CurrentStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 };
        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };
        mapperMock.Map(worklisttobiohubitem, cmd.UserPermissions).ReturnsForAnyArgs(worklisttobiohubitemMapped);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .ReadByIdWithExtraInfo(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklisttobiohubitem));

        repositoryDocumentTemplateMock.GetCurrentDocumentTemplateByType(Arg.Any<DocumentFileType>(), cancellationToken)
           .ReturnsForAnyArgs(Task.FromResult(new DocumentTemplate() { Name = "Name", Extension = "txt" }));

        repositoryDocumentMock.GetCurrentDocument(Arg.Any<Guid>(), Arg.Any<DocumentFileType>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(new Document() { Name = "Name", Extension = "txt" }));

        // Act
        Either<ReadWorklistToBioHubItemQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.WorklistToBioHubItemDto.Id.Should()
            .Be(id, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadWorklistToBioHubItemQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadWorklistToBioHubItemQueryValidator validatorMock = Substitute.For<ReadWorklistToBioHubItemQueryValidator>();
        ILogger<ReadWorklistToBioHubItemHandler> loggerMock = Substitute.For<ILogger<ReadWorklistToBioHubItemHandler>>();
        IWorklistToBioHubItemReadRepository repositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        CancellationToken cancellationToken = default;

        IReadWorklistToBioHubItemMapper mapperMock = Substitute.For<IReadWorklistToBioHubItemMapper>();

        IDocumentTemplateReadRepository repositoryDocumentTemplateMock = Substitute.For<IDocumentTemplateReadRepository>();

        IDocumentReadRepository repositoryDocumentMock = Substitute.For<IDocumentReadRepository>();
        ReadWorklistToBioHubItemHandler handler = new(loggerMock, validatorMock, repositoryMock, repositoryDocumentTemplateMock, mapperMock, repositoryDocumentMock);

        ReadWorklistToBioHubItemQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadWorklistToBioHubItemQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadWorklistToBioHubItemQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}