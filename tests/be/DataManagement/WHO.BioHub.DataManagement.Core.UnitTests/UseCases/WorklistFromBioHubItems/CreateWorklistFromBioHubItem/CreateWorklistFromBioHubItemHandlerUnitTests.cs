using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.CreateWorklistFromBioHubItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.WorkflowEngine.Commands;
using WHO.BioHub.Shared.Enums;
using NSubstitute.ExceptionExtensions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistFromBioHubItems.CreateWorklistFromBioHubItem;

public class CreateWorklistFromBioHubItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateWorklistFromBioHubItemCommandValidator validatorMock = Substitute.For<CreateWorklistFromBioHubItemCommandValidator>();
        ILogger<CreateWorklistFromBioHubItemHandler> loggerMock = Substitute.For<ILogger<CreateWorklistFromBioHubItemHandler>>();
        IWorklistFromBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemWriteRepository>();
        ICreateWorklistFromBioHubItemMapper mapperMock = Substitute.For<ICreateWorklistFromBioHubItemMapper>();
        CancellationToken cancellationToken = default;
        IWorklistFromBioHubEngine engineMock = Substitute.For<IWorklistFromBioHubEngine>();
        IWorklistFromBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistFromBioHubHistoryItemWriteRepository>();
        IWorklistFromBioHubItemReadRepository repositoryReadMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();
        IWorklistItemUsedReferenceNumberReadRepository worklistItemUsedReferenceNumberReadRepositoryReadMock = Substitute.For<IWorklistItemUsedReferenceNumberReadRepository>();

        CreateWorklistFromBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, repositoryReadMock, documentTemplateReadRepositoryReadMock, engineMock, repositoryHistoryMock, worklistItemUsedReferenceNumberReadRepositoryReadMock);
        CreateWorklistFromBioHubItemCommand cmd = new() { LaboratoryId = Guid.NewGuid() };

        Guid assignedId = Guid.NewGuid();

        WorklistFromBioHubItem worklistfrombiohubitem = new() { Id = assignedId };

        documentTemplateReadRepositoryReadMock.TemplatesPresent(Arg.Any<List<DocumentFileType?>>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(true));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(worklistfrombiohubitem, Arg.Any<MoveToNextStatusFromBioHubEngineCommand>(), cancellationToken)
            .ReturnsForAnyArgs(worklistfrombiohubitem);

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());


        mapperMock.Map(cmd).ReturnsForAnyArgs(worklistfrombiohubitem);


        // Act
        Either<CreateWorklistFromBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Should()
            .NotBeNull(because: "WorklistFromBioHubItem should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the WorklistFromBioHubItem")
            .And.Be(assignedId, because: "Returned worklistfrombiohubitem Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateWorklistFromBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateWorklistFromBioHubItemCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<WorklistFromBioHubItem>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateWorklistFromBioHubItemCommandValidator validatorMock = Substitute.For<CreateWorklistFromBioHubItemCommandValidator>();
        ILogger<CreateWorklistFromBioHubItemHandler> loggerMock = Substitute.For<ILogger<CreateWorklistFromBioHubItemHandler>>();
        IWorklistFromBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemWriteRepository>();
        ICreateWorklistFromBioHubItemMapper mapperMock = Substitute.For<ICreateWorklistFromBioHubItemMapper>();
        CancellationToken cancellationToken = default;

        IWorklistFromBioHubEngine engineMock = Substitute.For<IWorklistFromBioHubEngine>();
        IWorklistFromBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistFromBioHubHistoryItemWriteRepository>();
        IWorklistFromBioHubItemReadRepository repositoryReadMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();
        IWorklistItemUsedReferenceNumberReadRepository worklistItemUsedReferenceNumberReadRepositoryReadMock = Substitute.For<IWorklistItemUsedReferenceNumberReadRepository>();

        CreateWorklistFromBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, repositoryReadMock, documentTemplateReadRepositoryReadMock, engineMock, repositoryHistoryMock, worklistItemUsedReferenceNumberReadRepositoryReadMock);

        CreateWorklistFromBioHubItemCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateWorklistFromBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateWorklistFromBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateWorklistFromBioHubItemCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<WorklistFromBioHubItem>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateWorklistFromBioHubItemCommandValidator validatorMock = Substitute.For<CreateWorklistFromBioHubItemCommandValidator>();
        ILogger<CreateWorklistFromBioHubItemHandler> loggerMock = Substitute.For<ILogger<CreateWorklistFromBioHubItemHandler>>();
        IWorklistFromBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemWriteRepository>();
        ICreateWorklistFromBioHubItemMapper mapperMock = Substitute.For<ICreateWorklistFromBioHubItemMapper>();
        CancellationToken cancellationToken = default;
        IWorklistFromBioHubEngine engineMock = Substitute.For<IWorklistFromBioHubEngine>();
        IWorklistFromBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistFromBioHubHistoryItemWriteRepository>();
        IWorklistFromBioHubItemReadRepository repositoryReadMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        IDocumentTemplateReadRepository documentTemplateReadRepositoryReadMock = Substitute.For<IDocumentTemplateReadRepository>();
        IWorklistItemUsedReferenceNumberReadRepository worklistItemUsedReferenceNumberReadRepositoryReadMock = Substitute.For<IWorklistItemUsedReferenceNumberReadRepository>();

        CreateWorklistFromBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, repositoryReadMock, documentTemplateReadRepositoryReadMock, engineMock, repositoryHistoryMock, worklistItemUsedReferenceNumberReadRepositoryReadMock);
        CreateWorklistFromBioHubItemCommand cmd = new() { LaboratoryId = Guid.NewGuid() };

        Guid assignedId = Guid.NewGuid();

        WorklistFromBioHubItem worklistfrombiohubitem = new() { Id = assignedId };

        documentTemplateReadRepositoryReadMock.TemplatesPresent(Arg.Any<List<DocumentFileType?>>(), cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(true));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(worklistfrombiohubitem, Arg.Any<MoveToNextStatusFromBioHubEngineCommand>(), cancellationToken)
            .Throws(new Exception("test"));

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());


        mapperMock.Map(cmd).ReturnsForAnyArgs(worklistfrombiohubitem);




        // Act
        Either<CreateWorklistFromBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateWorklistFromBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateWorklistFromBioHubItemCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<WorklistFromBioHubItem>(), cancellationToken).Received(1);
        });
    }
}