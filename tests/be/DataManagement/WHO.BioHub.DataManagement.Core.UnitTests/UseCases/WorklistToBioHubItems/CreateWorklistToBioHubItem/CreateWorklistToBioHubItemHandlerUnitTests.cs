using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.CreateWorklistToBioHubItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.WorkflowEngine.Commands;
using WHO.BioHub.Shared.Enums;
using NSubstitute.ExceptionExtensions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistToBioHubItems.CreateWorklistToBioHubItem;

public class CreateWorklistToBioHubItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateWorklistToBioHubItemCommandValidator validatorMock = Substitute.For<CreateWorklistToBioHubItemCommandValidator>();
        ILogger<CreateWorklistToBioHubItemHandler> loggerMock = Substitute.For<ILogger<CreateWorklistToBioHubItemHandler>>();
        IWorklistToBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubItemWriteRepository>();
        ICreateWorklistToBioHubItemMapper mapperMock = Substitute.For<ICreateWorklistToBioHubItemMapper>();
        CancellationToken cancellationToken = default;
        IWorklistToBioHubEngine engineMock = Substitute.For<IWorklistToBioHubEngine>();
        IWorklistToBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistToBioHubHistoryItemWriteRepository>();
        IWorklistToBioHubItemReadRepository repositoryReadMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();
        IWorklistItemUsedReferenceNumberReadRepository worklistItemUsedReferenceNumberReadRepositoryReadMock = Substitute.For<IWorklistItemUsedReferenceNumberReadRepository>();

        CreateWorklistToBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, repositoryReadMock, documentTemplateReadRepositoryReadMock, engineMock, repositoryHistoryMock, worklistItemUsedReferenceNumberReadRepositoryReadMock);
        CreateWorklistToBioHubItemCommand cmd = new() { LaboratoryId = Guid.NewGuid() };
        
        Guid assignedId = Guid.NewGuid();

        WorklistToBioHubItem worklisttobiohubitem = new() { Id = assignedId };

        documentTemplateReadRepositoryReadMock.TemplatesPresent(Arg.Any<List<DocumentFileType?>>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(true));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(worklisttobiohubitem, Arg.Any<MoveToNextStatusToBioHubEngineCommand>(), cancellationToken)
            .ReturnsForAnyArgs(worklisttobiohubitem);

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        
        mapperMock.Map(cmd).ReturnsForAnyArgs(worklisttobiohubitem);      
       

        // Act
        Either<CreateWorklistToBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Should()
            .NotBeNull(because: "WorklistToBioHubItem should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the WorklistToBioHubItem")
            .And.Be(assignedId, because: "Returned worklisttobiohubitem Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateWorklistToBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateWorklistToBioHubItemCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<WorklistToBioHubItem>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateWorklistToBioHubItemCommandValidator validatorMock = Substitute.For<CreateWorklistToBioHubItemCommandValidator>();
        ILogger<CreateWorklistToBioHubItemHandler> loggerMock = Substitute.For<ILogger<CreateWorklistToBioHubItemHandler>>();
        IWorklistToBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubItemWriteRepository>();
        ICreateWorklistToBioHubItemMapper mapperMock = Substitute.For<ICreateWorklistToBioHubItemMapper>();
        CancellationToken cancellationToken = default;

        IWorklistToBioHubEngine engineMock = Substitute.For<IWorklistToBioHubEngine>();
        IWorklistToBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistToBioHubHistoryItemWriteRepository>();
        IWorklistToBioHubItemReadRepository repositoryReadMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();
        IWorklistItemUsedReferenceNumberReadRepository worklistItemUsedReferenceNumberReadRepositoryReadMock = Substitute.For<IWorklistItemUsedReferenceNumberReadRepository>();

        CreateWorklistToBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, repositoryReadMock, documentTemplateReadRepositoryReadMock, engineMock, repositoryHistoryMock, worklistItemUsedReferenceNumberReadRepositoryReadMock);

        CreateWorklistToBioHubItemCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateWorklistToBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateWorklistToBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateWorklistToBioHubItemCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<WorklistToBioHubItem>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateWorklistToBioHubItemCommandValidator validatorMock = Substitute.For<CreateWorklistToBioHubItemCommandValidator>();
        ILogger<CreateWorklistToBioHubItemHandler> loggerMock = Substitute.For<ILogger<CreateWorklistToBioHubItemHandler>>();
        IWorklistToBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubItemWriteRepository>();
        ICreateWorklistToBioHubItemMapper mapperMock = Substitute.For<ICreateWorklistToBioHubItemMapper>();
        CancellationToken cancellationToken = default;
        IWorklistToBioHubEngine engineMock = Substitute.For<IWorklistToBioHubEngine>();
        IWorklistToBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistToBioHubHistoryItemWriteRepository>();
        IWorklistToBioHubItemReadRepository repositoryReadMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();
        IWorklistItemUsedReferenceNumberReadRepository worklistItemUsedReferenceNumberReadRepositoryReadMock = Substitute.For<IWorklistItemUsedReferenceNumberReadRepository>();

        CreateWorklistToBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, repositoryReadMock, documentTemplateReadRepositoryReadMock, engineMock, repositoryHistoryMock, worklistItemUsedReferenceNumberReadRepositoryReadMock);
        CreateWorklistToBioHubItemCommand cmd = new() { LaboratoryId = Guid.NewGuid() };

        Guid assignedId = Guid.NewGuid();

        WorklistToBioHubItem worklisttobiohubitem = new() { Id = assignedId };

        documentTemplateReadRepositoryReadMock.TemplatesPresent(Arg.Any<List<DocumentFileType?>>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(true));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(worklisttobiohubitem, Arg.Any<MoveToNextStatusToBioHubEngineCommand>(), cancellationToken)
            .Throws(new Exception("test"));

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());


        mapperMock.Map(cmd).ReturnsForAnyArgs(worklisttobiohubitem);


        

        // Act
        Either<CreateWorklistToBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateWorklistToBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateWorklistToBioHubItemCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<WorklistToBioHubItem>(), cancellationToken).Received(1);
        });
    }
}