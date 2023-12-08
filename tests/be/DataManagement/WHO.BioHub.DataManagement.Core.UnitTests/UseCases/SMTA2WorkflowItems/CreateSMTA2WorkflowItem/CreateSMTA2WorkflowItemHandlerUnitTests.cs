using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.CreateSMTA2WorkflowItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.WorkflowEngine.Commands;
using NSubstitute.ExceptionExtensions;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SMTA2WorkflowItems.CreateSMTA2WorkflowItem;

public class CreateSMTA2WorkflowItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateSMTA2WorkflowItemCommandValidator validatorMock = Substitute.For<CreateSMTA2WorkflowItemCommandValidator>();
        ILogger<CreateSMTA2WorkflowItemHandler> loggerMock = Substitute.For<ILogger<CreateSMTA2WorkflowItemHandler>>();
        ISMTA2WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemWriteRepository>();
        ICreateSMTA2WorkflowItemMapper mapperMock = Substitute.For<ICreateSMTA2WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA2WorkflowEngine engineMock = Substitute.For<ISMTA2WorkflowEngine>();
        ISMTA2WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA2WorkflowHistoryItemWriteRepository>();
        ISMTA2WorkflowItemReadRepository repositoryReadMock = Substitute.For<ISMTA2WorkflowItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();

        Guid assignedId = Guid.NewGuid();

        SMTA2WorkflowItem smta2workflowitem = new() { Id = assignedId };

        CreateSMTA2WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, documentTemplateReadRepositoryReadMock, engineMock);

        CreateSMTA2WorkflowItemCommand cmd = new() { LaboratoryId = Guid.NewGuid() };

        documentTemplateReadRepositoryReadMock.TemplatesPresent(Arg.Any<List<DocumentFileType?>>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(true));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(smta2workflowitem, Arg.Any<MoveToNextStatusSMTA2WorkflowEngineCommand>(), cancellationToken)
            .ReturnsForAnyArgs(smta2workflowitem);

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());


        mapperMock.Map(cmd).ReturnsForAnyArgs(smta2workflowitem);


        repositoryMock
            .Create(smta2workflowitem, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<SMTA2WorkflowItem, Errors>>(() =>
                {
                    smta2workflowitem.Id = assignedId;
                    return new(smta2workflowitem);
                }));



        // Act
        Either<CreateSMTA2WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the SMTA2WorkflowItem")
            .And.Be(assignedId, because: "Returned smta2workflowitem Id must mach the one assigned by repository");
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateSMTA2WorkflowItemCommandValidator validatorMock = Substitute.For<CreateSMTA2WorkflowItemCommandValidator>();
        ILogger<CreateSMTA2WorkflowItemHandler> loggerMock = Substitute.For<ILogger<CreateSMTA2WorkflowItemHandler>>();
        ISMTA2WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemWriteRepository>();
        ICreateSMTA2WorkflowItemMapper mapperMock = Substitute.For<ICreateSMTA2WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA2WorkflowEngine engineMock = Substitute.For<ISMTA2WorkflowEngine>();
        ISMTA2WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA2WorkflowHistoryItemWriteRepository>();
        ISMTA2WorkflowItemReadRepository repositoryReadMock = Substitute.For<ISMTA2WorkflowItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();

        CreateSMTA2WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, documentTemplateReadRepositoryReadMock, engineMock);
        CreateSMTA2WorkflowItemCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateSMTA2WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateSMTA2WorkflowItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateSMTA2WorkflowItemCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<SMTA2WorkflowItem>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateSMTA2WorkflowItemCommandValidator validatorMock = Substitute.For<CreateSMTA2WorkflowItemCommandValidator>();
        ILogger<CreateSMTA2WorkflowItemHandler> loggerMock = Substitute.For<ILogger<CreateSMTA2WorkflowItemHandler>>();
        ISMTA2WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemWriteRepository>();
        ICreateSMTA2WorkflowItemMapper mapperMock = Substitute.For<ICreateSMTA2WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA2WorkflowEngine engineMock = Substitute.For<ISMTA2WorkflowEngine>();
        ISMTA2WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA2WorkflowHistoryItemWriteRepository>();
        ISMTA2WorkflowItemReadRepository repositoryReadMock = Substitute.For<ISMTA2WorkflowItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();

        CreateSMTA2WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, documentTemplateReadRepositoryReadMock, engineMock);
        Guid assignedId = Guid.NewGuid();

        SMTA2WorkflowItem smta2workflowitem = new() { Id = assignedId };


        CreateSMTA2WorkflowItemCommand cmd = new() { LaboratoryId = Guid.NewGuid() };

        documentTemplateReadRepositoryReadMock.TemplatesPresent(Arg.Any<List<DocumentFileType?>>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(true));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(smta2workflowitem, Arg.Any<MoveToNextStatusSMTA2WorkflowEngineCommand>(), cancellationToken)
            .Throws(new Exception("Error"));

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());


        mapperMock.Map(cmd).ReturnsForAnyArgs(smta2workflowitem);


        // Act
        Either<CreateSMTA2WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            .Be(ErrorType.Internal, because: "Validation Errors are expected in this scenario");
        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateSMTA2WorkflowItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateSMTA2WorkflowItemCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<SMTA2WorkflowItem>(), cancellationToken).Received(1);
        });
    }
}