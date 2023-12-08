using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.WorkflowEngine.Commands;
using NSubstitute.ExceptionExtensions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItem;

public class UpdateWorklistFromBioHubItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        IWorklistFromBioHubItemReadRepository readRepositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        UpdateWorklistFromBioHubItemCommandValidator validatorMock = Substitute.For<UpdateWorklistFromBioHubItemCommandValidator>(documentReadRepositoryMock);
        ILogger<UpdateWorklistFromBioHubItemHandler> loggerMock = Substitute.For<ILogger<UpdateWorklistFromBioHubItemHandler>>();
        IWorklistFromBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemWriteRepository>();
        IUpdateWorklistFromBioHubItemMapper mapperMock = Substitute.For<IUpdateWorklistFromBioHubItemMapper>();
        CancellationToken cancellationToken = default;
        IWorklistFromBioHubEngine engineMock = Substitute.For<IWorklistFromBioHubEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();
        IWorklistFromBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistFromBioHubHistoryItemWriteRepository>();

        IWorklistFromBioHubEmailNotifier worklistFromBioHubEmailNotifierMock = Substitute.For<IWorklistFromBioHubEmailNotifier>();
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateWorklistFromBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, worklistFromBioHubEmailNotifierMock);

        Guid id = Guid.NewGuid();
        Guid referenceId = Guid.NewGuid();

        UpdateWorklistFromBioHubItemCommand cmd = new() { LastSubmissionApproved = true, ReferenceId = referenceId, CurrentStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2, RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitAnnex2OfSMTA2 } }; ;

        WorklistFromBioHubItem worklistFromBioHubitem = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = Guid.NewGuid(), Status = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 };
        WorklistFromBioHubItem worklistFromBioHubitemMapped = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = worklistFromBioHubitem.Id, Status = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 };

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryHistoryMock.Create(Arg.Any<WorklistFromBioHubHistoryItem>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult(new Either<WorklistFromBioHubHistoryItem, Errors>(null)));

        repositoryHistoryMock.CopyLinkDocumentFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkBiosafetyChecklistOfSMTA2sFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkBiosafetyChecklistCommentsFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkLaboratoryFocalPointsFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkBookingFormsFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkFeedbacksFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkAnnex2OfSMTA2ConditionsFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
          .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));


        engineMock.MoveToNextStatusUponApproveOrSaveDraft(worklistFromBioHubitemMapped, Arg.Any<MoveToNextStatusFromBioHubEngineCommand>(), cancellationToken)
            .ReturnsForAnyArgs(worklistFromBioHubitem);

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        documentReadRepositoryMock.IsDocumentSignedByLaboratoryId(Arg.Any<Guid>(), Arg.Any<DocumentFileType>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        worklistFromBioHubEmailNotifierMock.NotifyUsers(worklistFromBioHubitemMapped, cancellationToken).ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        mapperMock.Map(worklistFromBioHubitem, cmd).ReturnsForAnyArgs(worklistFromBioHubitemMapped);

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklistFromBioHubitem));

        // Act
        Either<UpdateWorklistFromBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Should()
            .NotBeNull(because: "WorklistFromBioHubItem should NOT be null");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateWorklistFromBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(worklistFromBioHubitem, Arg.Any<UpdateWorklistFromBioHubItemCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<WorklistFromBioHubItem>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        IWorklistFromBioHubItemReadRepository readRepositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        UpdateWorklistFromBioHubItemCommandValidator validatorMock = Substitute.For<UpdateWorklistFromBioHubItemCommandValidator>(documentReadRepositoryMock);
        ILogger<UpdateWorklistFromBioHubItemHandler> loggerMock = Substitute.For<ILogger<UpdateWorklistFromBioHubItemHandler>>();
        IWorklistFromBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemWriteRepository>();
        IUpdateWorklistFromBioHubItemMapper mapperMock = Substitute.For<IUpdateWorklistFromBioHubItemMapper>();
        CancellationToken cancellationToken = default;

        IWorklistFromBioHubEngine engineMock = Substitute.For<IWorklistFromBioHubEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();

        IWorklistFromBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistFromBioHubHistoryItemWriteRepository>();

        IWorklistFromBioHubEmailNotifier worklistFromBioHubEmailNotifierMock = Substitute.For<IWorklistFromBioHubEmailNotifier>();

        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateWorklistFromBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, worklistFromBioHubEmailNotifierMock);

        Guid id = Guid.NewGuid();
        Guid referenceId = Guid.NewGuid();

        UpdateWorklistFromBioHubItemCommand cmd = new() { LastSubmissionApproved = true, ReferenceId = referenceId, CurrentStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2, RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitAnnex2OfSMTA2 } }; ;

        WorklistFromBioHubItem worklistFromBioHubitem = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = Guid.NewGuid(), Status = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 };
        WorklistFromBioHubItem worklistFromBioHubitemMapped = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = worklistFromBioHubitem.Id, Status = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 };


        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateWorklistFromBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            .Be(ErrorType.RequestParsing, because: "Validation Errors are expected in this scenario");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateWorklistFromBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<WorklistFromBioHubItem>(), Arg.Any<UpdateWorklistFromBioHubItemCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<WorklistFromBioHubItem>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        IWorklistFromBioHubItemReadRepository readRepositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        UpdateWorklistFromBioHubItemCommandValidator validatorMock = Substitute.For<UpdateWorklistFromBioHubItemCommandValidator>(documentReadRepositoryMock);
        ILogger<UpdateWorklistFromBioHubItemHandler> loggerMock = Substitute.For<ILogger<UpdateWorklistFromBioHubItemHandler>>();
        IWorklistFromBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistFromBioHubItemWriteRepository>();
        IUpdateWorklistFromBioHubItemMapper mapperMock = Substitute.For<IUpdateWorklistFromBioHubItemMapper>();
        CancellationToken cancellationToken = default;
        IWorklistFromBioHubEngine engineMock = Substitute.For<IWorklistFromBioHubEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();
        IWorklistFromBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistFromBioHubHistoryItemWriteRepository>();

        IWorklistFromBioHubEmailNotifier worklistFromBioHubEmailNotifierMock = Substitute.For<IWorklistFromBioHubEmailNotifier>();
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateWorklistFromBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, worklistFromBioHubEmailNotifierMock);

        Guid id = Guid.NewGuid();
        Guid referenceId = Guid.NewGuid();

        UpdateWorklistFromBioHubItemCommand cmd = new() { LastSubmissionApproved = true, ReferenceId = referenceId, CurrentStatus = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2, RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitAnnex2OfSMTA2 } }; ;

        WorklistFromBioHubItem worklistFromBioHubitem = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = Guid.NewGuid(), Status = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 };
        WorklistFromBioHubItem worklistFromBioHubitemMapped = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = worklistFromBioHubitem.Id, Status = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 };

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryHistoryMock.Create(Arg.Any<WorklistFromBioHubHistoryItem>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult(new Either<WorklistFromBioHubHistoryItem, Errors>(null)));

        repositoryHistoryMock.CopyLinkDocumentFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkBiosafetyChecklistOfSMTA2sFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkBiosafetyChecklistCommentsFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkLaboratoryFocalPointsFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkBookingFormsFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkFeedbacksFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkAnnex2OfSMTA2ConditionsFromWorklistFromBioHubItem(worklistFromBioHubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
          .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));


        engineMock.MoveToNextStatusUponApproveOrSaveDraft(worklistFromBioHubitemMapped, Arg.Any<MoveToNextStatusFromBioHubEngineCommand>(), cancellationToken)
            .Throws(new Exception("test"));

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        documentReadRepositoryMock.IsDocumentSignedByLaboratoryId(Arg.Any<Guid>(), Arg.Any<DocumentFileType>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        worklistFromBioHubEmailNotifierMock.NotifyUsers(worklistFromBioHubitemMapped, cancellationToken).ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        mapperMock.Map(worklistFromBioHubitem, cmd).ReturnsForAnyArgs(worklistFromBioHubitemMapped);

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklistFromBioHubitem));

        // Act
        Either<UpdateWorklistFromBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateWorklistFromBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<WorklistFromBioHubItem>(), Arg.Any<UpdateWorklistFromBioHubItemCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<WorklistFromBioHubItem>(), cancellationToken).Received(1);
        });
    }
}