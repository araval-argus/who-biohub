using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ReadSMTA1WorkflowItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SMTA1WorkflowItems.ReadSMTA1WorkflowItem;

public class ReadSMTA1WorkflowItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadSMTA1WorkflowItemQueryValidator validatorMock = Substitute.For<ReadSMTA1WorkflowItemQueryValidator>();
        ILogger<ReadSMTA1WorkflowItemHandler> loggerMock = Substitute.For<ILogger<ReadSMTA1WorkflowItemHandler>>();
        ISMTA1WorkflowItemReadRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemReadRepository>();
        CancellationToken cancellationToken = default;
        IReadSMTA1WorkflowItemMapper mapperMock = Substitute.For<IReadSMTA1WorkflowItemMapper>();
        IDocumentTemplateReadRepository repositoryDocumentTemplateMock = Substitute.For<IDocumentTemplateReadRepository>();
        IDocumentReadRepository repositoryDocumentMock = Substitute.For<IDocumentReadRepository>();
        ReadSMTA1WorkflowItemHandler handler = new(loggerMock, validatorMock, repositoryMock, repositoryDocumentTemplateMock, mapperMock, repositoryDocumentMock);


        ReadSMTA1WorkflowItemQuery cmd = new() { RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanReadSubmitSMTA1 } }; 

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        SMTA1WorkflowItem smta1workflowitem = new() { Id = id, Status = SMTA1WorkflowStatus.SubmitSMTA1 };
        SMTA1WorkflowItemDto smta1workflowitemDto = new() { Id = id, CurrentStatus = SMTA1WorkflowStatus.SubmitSMTA1 };
        IEnumerable<Role> roles = new Role[1] { new() { RoleType = RoleType.WHO } };

        repositoryDocumentTemplateMock.GetCurrentDocumentTemplateByType(Arg.Any<DocumentFileType>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(new DocumentTemplate() { Name = "Name", Extension = "txt" }));

       

        mapperMock.Map(smta1workflowitem, cmd.UserPermissions).ReturnsForAnyArgs(smta1workflowitemDto);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .ReadByIdWithExtraInfo(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(smta1workflowitem));

        // Act
        Either<ReadSMTA1WorkflowItemQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        //response.Left.SMTA1WorkflowItem.Id.Should()
        //    .Be(id, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadSMTA1WorkflowItemQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadSMTA1WorkflowItemQueryValidator validatorMock = Substitute.For<ReadSMTA1WorkflowItemQueryValidator>();
        ILogger<ReadSMTA1WorkflowItemHandler> loggerMock = Substitute.For<ILogger<ReadSMTA1WorkflowItemHandler>>();
        ISMTA1WorkflowItemReadRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemReadRepository>();
        CancellationToken cancellationToken = default;
        IReadSMTA1WorkflowItemMapper mapperMock = Substitute.For<IReadSMTA1WorkflowItemMapper>();
        IDocumentTemplateReadRepository repositoryDocumentTemplateMock = Substitute.For<IDocumentTemplateReadRepository>();
        IDocumentReadRepository repositoryDocumentMock = Substitute.For<IDocumentReadRepository>();
        ReadSMTA1WorkflowItemHandler handler = new(loggerMock, validatorMock, repositoryMock, repositoryDocumentTemplateMock, mapperMock, repositoryDocumentMock);

        ReadSMTA1WorkflowItemQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadSMTA1WorkflowItemQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadSMTA1WorkflowItemQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}