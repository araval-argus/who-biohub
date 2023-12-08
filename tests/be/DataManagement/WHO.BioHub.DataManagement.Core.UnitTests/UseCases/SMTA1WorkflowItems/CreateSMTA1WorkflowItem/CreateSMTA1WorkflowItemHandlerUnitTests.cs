using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.CreateSMTA1WorkflowItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.WorkflowEngine.Commands;
using NSubstitute.ExceptionExtensions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SMTA1WorkflowItems.CreateSMTA1WorkflowItem;

public class CreateSMTA1WorkflowItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateSMTA1WorkflowItemCommandValidator validatorMock = Substitute.For<CreateSMTA1WorkflowItemCommandValidator>();
        ILogger<CreateSMTA1WorkflowItemHandler> loggerMock = Substitute.For<ILogger<CreateSMTA1WorkflowItemHandler>>();
        ISMTA1WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemWriteRepository>();
        ICreateSMTA1WorkflowItemMapper mapperMock = Substitute.For<ICreateSMTA1WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA1WorkflowEngine engineMock = Substitute.For<ISMTA1WorkflowEngine>();
        ISMTA1WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA1WorkflowHistoryItemWriteRepository>();
        ISMTA1WorkflowItemReadRepository repositoryReadMock = Substitute.For<ISMTA1WorkflowItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();

        Guid assignedId = Guid.NewGuid();

        SMTA1WorkflowItem smta1workflowitem = new() { Id = assignedId };

        CreateSMTA1WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, documentTemplateReadRepositoryReadMock, engineMock);
        
        CreateSMTA1WorkflowItemCommand cmd = new() { LaboratoryId = Guid.NewGuid() };

        documentTemplateReadRepositoryReadMock.TemplatesPresent(Arg.Any<List<DocumentFileType?>>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(true));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(smta1workflowitem, Arg.Any<MoveToNextStatusSMTA1WorkflowEngineCommand>(), cancellationToken)
            .ReturnsForAnyArgs(smta1workflowitem);

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        
        mapperMock.Map(cmd).ReturnsForAnyArgs(smta1workflowitem);

       
        repositoryMock
            .Create(smta1workflowitem, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<SMTA1WorkflowItem, Errors>>(() =>
                {
                    smta1workflowitem.Id = assignedId;
                    return new(smta1workflowitem);
                }));

        

        // Act
        Either<CreateSMTA1WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");        
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the SMTA1WorkflowItem")
            .And.Be(assignedId, because: "Returned smta1workflowitem Id must mach the one assigned by repository");        
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateSMTA1WorkflowItemCommandValidator validatorMock = Substitute.For<CreateSMTA1WorkflowItemCommandValidator>();
        ILogger<CreateSMTA1WorkflowItemHandler> loggerMock = Substitute.For<ILogger<CreateSMTA1WorkflowItemHandler>>();
        ISMTA1WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemWriteRepository>();
        ICreateSMTA1WorkflowItemMapper mapperMock = Substitute.For<ICreateSMTA1WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA1WorkflowEngine engineMock = Substitute.For<ISMTA1WorkflowEngine>();
        ISMTA1WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA1WorkflowHistoryItemWriteRepository>();
        ISMTA1WorkflowItemReadRepository repositoryReadMock = Substitute.For<ISMTA1WorkflowItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();

        CreateSMTA1WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, documentTemplateReadRepositoryReadMock, engineMock);
        CreateSMTA1WorkflowItemCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateSMTA1WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateSMTA1WorkflowItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateSMTA1WorkflowItemCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<SMTA1WorkflowItem>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateSMTA1WorkflowItemCommandValidator validatorMock = Substitute.For<CreateSMTA1WorkflowItemCommandValidator>();
        ILogger<CreateSMTA1WorkflowItemHandler> loggerMock = Substitute.For<ILogger<CreateSMTA1WorkflowItemHandler>>();
        ISMTA1WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemWriteRepository>();
        ICreateSMTA1WorkflowItemMapper mapperMock = Substitute.For<ICreateSMTA1WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA1WorkflowEngine engineMock = Substitute.For<ISMTA1WorkflowEngine>();
        ISMTA1WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA1WorkflowHistoryItemWriteRepository>();
        ISMTA1WorkflowItemReadRepository repositoryReadMock = Substitute.For<ISMTA1WorkflowItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();

        CreateSMTA1WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, documentTemplateReadRepositoryReadMock, engineMock);
        Guid assignedId = Guid.NewGuid();

        SMTA1WorkflowItem smta1workflowitem = new() { Id = assignedId };

        
        CreateSMTA1WorkflowItemCommand cmd = new() { LaboratoryId = Guid.NewGuid() };

        documentTemplateReadRepositoryReadMock.TemplatesPresent(Arg.Any<List<DocumentFileType?>>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(true));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(smta1workflowitem, Arg.Any<MoveToNextStatusSMTA1WorkflowEngineCommand>(), cancellationToken)
            .Throws(new Exception("Error"));

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        
        mapperMock.Map(cmd).ReturnsForAnyArgs(smta1workflowitem);        
        

        // Act
        Either<CreateSMTA1WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateSMTA1WorkflowItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateSMTA1WorkflowItemCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<SMTA1WorkflowItem>(), cancellationToken).Received(1);
        });
    }
}