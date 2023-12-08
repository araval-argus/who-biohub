using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ReadSMTA2WorkflowItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SMTA2WorkflowItems.ReadSMTA2WorkflowItem;

public class ReadSMTA2WorkflowItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadSMTA2WorkflowItemQueryValidator validatorMock = Substitute.For<ReadSMTA2WorkflowItemQueryValidator>();
        ILogger<ReadSMTA2WorkflowItemHandler> loggerMock = Substitute.For<ILogger<ReadSMTA2WorkflowItemHandler>>();
        ISMTA2WorkflowItemReadRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemReadRepository>();
        CancellationToken cancellationToken = default;
        IReadSMTA2WorkflowItemMapper mapperMock = Substitute.For<IReadSMTA2WorkflowItemMapper>();
        IDocumentTemplateReadRepository repositoryDocumentTemplateMock = Substitute.For<IDocumentTemplateReadRepository>();
        IDocumentReadRepository repositoryDocumentMock = Substitute.For<IDocumentReadRepository>();
        ReadSMTA2WorkflowItemHandler handler = new(loggerMock, validatorMock, repositoryMock, repositoryDocumentTemplateMock, mapperMock, repositoryDocumentMock);


        ReadSMTA2WorkflowItemQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanReadSubmitSMTA2 } };

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        SMTA2WorkflowItem smta2workflowitem = new() { Id = id, Status = SMTA2WorkflowStatus.SubmitSMTA2 };
        SMTA2WorkflowItemDto smta2workflowitemDto = new() { Id = id, CurrentStatus = SMTA2WorkflowStatus.SubmitSMTA2 };
        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };

        repositoryDocumentTemplateMock.GetCurrentDocumentTemplateByType(Arg.Any<DocumentFileType>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(new DocumentTemplate() { Name = "Name", Extension = "txt" }));



        mapperMock.Map(smta2workflowitem, cmd.UserPermissions).ReturnsForAnyArgs(smta2workflowitemDto);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .ReadByIdWithExtraInfo(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(smta2workflowitem));

        // Act
        Either<ReadSMTA2WorkflowItemQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        //response.Left.SMTA2WorkflowItem.Id.Should()
        //    .Be(id, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadSMTA2WorkflowItemQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadSMTA2WorkflowItemQueryValidator validatorMock = Substitute.For<ReadSMTA2WorkflowItemQueryValidator>();
        ILogger<ReadSMTA2WorkflowItemHandler> loggerMock = Substitute.For<ILogger<ReadSMTA2WorkflowItemHandler>>();
        ISMTA2WorkflowItemReadRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemReadRepository>();
        CancellationToken cancellationToken = default;
        IReadSMTA2WorkflowItemMapper mapperMock = Substitute.For<IReadSMTA2WorkflowItemMapper>();
        IDocumentTemplateReadRepository repositoryDocumentTemplateMock = Substitute.For<IDocumentTemplateReadRepository>();
        IDocumentReadRepository repositoryDocumentMock = Substitute.For<IDocumentReadRepository>();
        ReadSMTA2WorkflowItemHandler handler = new(loggerMock, validatorMock, repositoryMock, repositoryDocumentTemplateMock, mapperMock, repositoryDocumentMock);

        ReadSMTA2WorkflowItemQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadSMTA2WorkflowItemQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadSMTA2WorkflowItemQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}