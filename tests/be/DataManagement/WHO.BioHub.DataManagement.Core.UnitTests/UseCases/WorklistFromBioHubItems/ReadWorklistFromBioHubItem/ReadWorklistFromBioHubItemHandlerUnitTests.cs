using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ReadWorklistFromBioHubItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistFromBioHubItems.ReadWorklistFromBioHubItem;

public class ReadWorklistFromBioHubItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadWorklistFromBioHubItemQueryValidator validatorMock = Substitute.For<ReadWorklistFromBioHubItemQueryValidator>();
        ILogger<ReadWorklistFromBioHubItemHandler> loggerMock = Substitute.For<ILogger<ReadWorklistFromBioHubItemHandler>>();
        IWorklistFromBioHubItemReadRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        CancellationToken cancellationToken = default;
        IReadWorklistFromBioHubItemMapper mapperMock = Substitute.For<IReadWorklistFromBioHubItemMapper>();
        IDocumentTemplateReadRepository repositoryDocumentTemplateMock = Substitute.For<IDocumentTemplateReadRepository>();
        IDocumentReadRepository repositoryDocumentMock = Substitute.For<IDocumentReadRepository>();
        ReadWorklistFromBioHubItemHandler handler = new(loggerMock, validatorMock, repositoryMock, repositoryDocumentTemplateMock, mapperMock, repositoryDocumentMock);

        ReadWorklistFromBioHubItemQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { { PermissionNames.CanReadSubmitAnnex2OfSMTA2 }, { PermissionNames.CanSubmitAnnex2OfSMTA2 } } };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        WorklistFromBioHubItem worklistFromBioHubitem = new() { Id = id, Status = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 };
        WorklistFromBioHubItemDto worklistFromBioHubitemMapped = new() { Id = id, CurrentStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 };
        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };


        var annex2OfSMTA2Conditions = new List<Annex2OfSMTA2Condition>();

        var biosafetyChecklistOfSMTA2 = new List<BiosafetyChecklistOfSMTA2>();

        repositoryMock.GetAnnex2OfSMTA2ConditionList(cancellationToken).ReturnsForAnyArgs(annex2OfSMTA2Conditions);

        repositoryMock.GetBiosafetyChecklistOfSMTA2List(cancellationToken).ReturnsForAnyArgs(biosafetyChecklistOfSMTA2);


        mapperMock.Map(worklistFromBioHubitem, cmd.UserPermissions, annex2OfSMTA2Conditions, biosafetyChecklistOfSMTA2).ReturnsForAnyArgs(worklistFromBioHubitemMapped);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .ReadByIdWithExtraInfo(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklistFromBioHubitem));

        repositoryDocumentTemplateMock.GetCurrentDocumentTemplateByType(Arg.Any<DocumentFileType>(), cancellationToken)
           .ReturnsForAnyArgs(Task.FromResult(new DocumentTemplate() { Name = "Name", Extension = "txt" }));

        repositoryDocumentMock.GetCurrentDocument(Arg.Any<Guid>(), Arg.Any<DocumentFileType>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(new Document() { Name = "Name", Extension = "txt" }));

        // Act
        Either<ReadWorklistFromBioHubItemQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.WorklistFromBioHubItemDto.Id.Should()
            .Be(id, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadWorklistFromBioHubItemQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadWorklistFromBioHubItemQueryValidator validatorMock = Substitute.For<ReadWorklistFromBioHubItemQueryValidator>();
        ILogger<ReadWorklistFromBioHubItemHandler> loggerMock = Substitute.For<ILogger<ReadWorklistFromBioHubItemHandler>>();
        IWorklistFromBioHubItemReadRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        CancellationToken cancellationToken = default;

        IReadWorklistFromBioHubItemMapper mapperMock = Substitute.For<IReadWorklistFromBioHubItemMapper>();

        IDocumentTemplateReadRepository repositoryDocumentTemplateMock = Substitute.For<IDocumentTemplateReadRepository>();

        IDocumentReadRepository repositoryDocumentMock = Substitute.For<IDocumentReadRepository>();
        ReadWorklistFromBioHubItemHandler handler = new(loggerMock, validatorMock, repositoryMock, repositoryDocumentTemplateMock, mapperMock, repositoryDocumentMock);

        ReadWorklistFromBioHubItemQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadWorklistFromBioHubItemQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadWorklistFromBioHubItemQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}