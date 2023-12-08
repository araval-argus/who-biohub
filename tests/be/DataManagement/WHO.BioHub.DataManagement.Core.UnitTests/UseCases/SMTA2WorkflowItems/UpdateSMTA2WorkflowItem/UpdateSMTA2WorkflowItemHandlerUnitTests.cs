using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.UpdateSMTA2WorkflowItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Enums;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.WorkflowEngine.Commands;
using NSubstitute.ExceptionExtensions;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SMTA2WorkflowItems.UpdateSMTA2WorkflowItem;

public class UpdateSMTA2WorkflowItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        UpdateSMTA2WorkflowItemCommandValidator validatorMock = Substitute.For<UpdateSMTA2WorkflowItemCommandValidator>(documentReadRepositoryMock);
        ILogger<UpdateSMTA2WorkflowItemHandler> loggerMock = Substitute.For<ILogger<UpdateSMTA2WorkflowItemHandler>>();
        ISMTA2WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemWriteRepository>();
        IUpdateSMTA2WorkflowItemMapper mapperMock = Substitute.For<IUpdateSMTA2WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA2WorkflowEngine engineMock = Substitute.For<ISMTA2WorkflowEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();
        ISMTA2WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA2WorkflowHistoryItemWriteRepository>();

        ISMTA2WorkflowEmailNotifier SMTA2WorkflowEmailNotifierMock = Substitute.For<ISMTA2WorkflowEmailNotifier>();
        UpdateSMTA2WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, SMTA2WorkflowEmailNotifierMock);
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();


        Guid id = Guid.NewGuid();
        Guid referenceId = Guid.NewGuid();

        UpdateSMTA2WorkflowItemCommand cmd = new() { LastSubmissionApproved = true, ReferenceId = referenceId, CurrentStatus = SMTA2WorkflowStatus.SubmitSMTA2, RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitSMTA2 } };

        SMTA2WorkflowItem smta2workflowitem = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = id, Status = SMTA2WorkflowStatus.SubmitSMTA2 };
        SMTA2WorkflowItem smta2workflowitemMapped = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = smta2workflowitem.Id, Status = SMTA2WorkflowStatus.SubmitSMTA2 };

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryHistoryMock.Create(Arg.Any<SMTA2WorkflowHistoryItem>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult(new Either<SMTA2WorkflowHistoryItem, Errors>(null)));


        repositoryHistoryMock.CopyLinkDocumentFromSMTA2WorkflowItem(smta2workflowitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(smta2workflowitemMapped, Arg.Any<MoveToNextStatusSMTA2WorkflowEngineCommand>(), cancellationToken)
            .ReturnsForAnyArgs(smta2workflowitem);

        documentReadRepositoryMock.IsDocumentSignedByLaboratoryId(Arg.Any<Guid>(), Arg.Any<DocumentFileType>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));
        SMTA2WorkflowEmailNotifierMock.NotifyUsers(smta2workflowitemMapped, cancellationToken).ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        validatorMock
           .ValidateAsync(cmd, cancellationToken)
           .ReturnsForAnyArgs(new ValidationResult());

        mapperMock.Map(smta2workflowitem, cmd).ReturnsForAnyArgs(smta2workflowitemMapped);

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(smta2workflowitem));



        // Act
        Either<UpdateSMTA2WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        //response.Left.SMTA2WorkflowItem.Should()
        //    .NotBeNull(because: "SMTA2WorkflowItem should NOT be null");
        //response.Left.SMTA2WorkflowItem.Should()
        //    .BeEquivalentTo(smta2workflowitemMapped, because: "Returned smta2workflowitem must mach the one provided in request");


    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        UpdateSMTA2WorkflowItemCommandValidator validatorMock = Substitute.For<UpdateSMTA2WorkflowItemCommandValidator>(documentReadRepositoryMock);
        ILogger<UpdateSMTA2WorkflowItemHandler> loggerMock = Substitute.For<ILogger<UpdateSMTA2WorkflowItemHandler>>();
        ISMTA2WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemWriteRepository>();
        IUpdateSMTA2WorkflowItemMapper mapperMock = Substitute.For<IUpdateSMTA2WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA2WorkflowEngine engineMock = Substitute.For<ISMTA2WorkflowEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();
        ISMTA2WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA2WorkflowHistoryItemWriteRepository>();

        ISMTA2WorkflowEmailNotifier SMTA2WorkflowEmailNotifierMock = Substitute.For<ISMTA2WorkflowEmailNotifier>();
        UpdateSMTA2WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, SMTA2WorkflowEmailNotifierMock);

        UpdateSMTA2WorkflowItemCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateSMTA2WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateSMTA2WorkflowItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<SMTA2WorkflowItem>(), Arg.Any<UpdateSMTA2WorkflowItemCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<SMTA2WorkflowItem>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        UpdateSMTA2WorkflowItemCommandValidator validatorMock = Substitute.For<UpdateSMTA2WorkflowItemCommandValidator>(documentReadRepositoryMock);
        ILogger<UpdateSMTA2WorkflowItemHandler> loggerMock = Substitute.For<ILogger<UpdateSMTA2WorkflowItemHandler>>();
        ISMTA2WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA2WorkflowItemWriteRepository>();
        IUpdateSMTA2WorkflowItemMapper mapperMock = Substitute.For<IUpdateSMTA2WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA2WorkflowEngine engineMock = Substitute.For<ISMTA2WorkflowEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();
        ISMTA2WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA2WorkflowHistoryItemWriteRepository>();
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();
        ISMTA2WorkflowEmailNotifier SMTA2WorkflowEmailNotifierMock = Substitute.For<ISMTA2WorkflowEmailNotifier>();
        Guid id = Guid.NewGuid();
        Guid referenceId = Guid.NewGuid();

        UpdateSMTA2WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, SMTA2WorkflowEmailNotifierMock);


        UpdateSMTA2WorkflowItemCommand cmd = new() { LastSubmissionApproved = true, ReferenceId = referenceId, CurrentStatus = SMTA2WorkflowStatus.SubmitSMTA2, RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitSMTA2 } };

        SMTA2WorkflowItem smta2workflowitem = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = id, Status = SMTA2WorkflowStatus.SubmitSMTA2 };
        SMTA2WorkflowItem smta2workflowitemMapped = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = smta2workflowitem.Id, Status = SMTA2WorkflowStatus.SubmitSMTA2 };

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryHistoryMock.Create(Arg.Any<SMTA2WorkflowHistoryItem>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult(new Either<SMTA2WorkflowHistoryItem, Errors>(null)));


        repositoryHistoryMock.CopyLinkDocumentFromSMTA2WorkflowItem(smta2workflowitem.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(smta2workflowitem, Arg.Any<MoveToNextStatusSMTA2WorkflowEngineCommand>(), cancellationToken)
            .Throws(new Exception("test"));

        documentReadRepositoryMock.IsDocumentSignedByLaboratoryId(Arg.Any<Guid>(), Arg.Any<DocumentFileType>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));
        SMTA2WorkflowEmailNotifierMock.NotifyUsers(smta2workflowitem, cancellationToken).ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        validatorMock
           .ValidateAsync(cmd, cancellationToken)
           .ReturnsForAnyArgs(new ValidationResult());

        mapperMock.Map(smta2workflowitem, cmd).ReturnsForAnyArgs(smta2workflowitemMapped);

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(smta2workflowitem));

        // Act
        Either<UpdateSMTA2WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateSMTA2WorkflowItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<SMTA2WorkflowItem>(), Arg.Any<UpdateSMTA2WorkflowItemCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<SMTA2WorkflowItem>(), cancellationToken).Received(1);
        });
    }
}