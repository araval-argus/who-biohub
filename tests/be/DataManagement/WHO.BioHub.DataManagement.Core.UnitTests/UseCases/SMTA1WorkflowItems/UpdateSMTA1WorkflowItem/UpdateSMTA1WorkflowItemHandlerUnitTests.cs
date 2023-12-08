using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.UpdateSMTA1WorkflowItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Enums;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.WorkflowEngine.Commands;
using NSubstitute.ExceptionExtensions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SMTA1WorkflowItems.UpdateSMTA1WorkflowItem;

public class UpdateSMTA1WorkflowItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        UpdateSMTA1WorkflowItemCommandValidator validatorMock = Substitute.For<UpdateSMTA1WorkflowItemCommandValidator>(documentReadRepositoryMock);
        ILogger<UpdateSMTA1WorkflowItemHandler> loggerMock = Substitute.For<ILogger<UpdateSMTA1WorkflowItemHandler>>();
        ISMTA1WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemWriteRepository>();
        IUpdateSMTA1WorkflowItemMapper mapperMock = Substitute.For<IUpdateSMTA1WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA1WorkflowEngine engineMock = Substitute.For<ISMTA1WorkflowEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();
        ISMTA1WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA1WorkflowHistoryItemWriteRepository>();

        ISMTA1WorkflowEmailNotifier SMTA1WorkflowEmailNotifierMock = Substitute.For<ISMTA1WorkflowEmailNotifier>();
        UpdateSMTA1WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, SMTA1WorkflowEmailNotifierMock);
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();


        Guid id = Guid.NewGuid();
        Guid referenceId = Guid.NewGuid();

        UpdateSMTA1WorkflowItemCommand cmd = new() { LastSubmissionApproved = true, ReferenceId = referenceId, CurrentStatus = SMTA1WorkflowStatus.SubmitSMTA1, RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitSMTA1 } };

        SMTA1WorkflowItem smta1workflowitem = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = id, Status = SMTA1WorkflowStatus.SubmitSMTA1 };
        SMTA1WorkflowItem smta1workflowitemMapped = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = smta1workflowitem.Id, Status = SMTA1WorkflowStatus.SubmitSMTA1 };
        
        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryHistoryMock.Create(Arg.Any<SMTA1WorkflowHistoryItem>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult(new Either<SMTA1WorkflowHistoryItem,Errors>(null)));


        repositoryHistoryMock.CopyLinkDocumentFromSMTA1WorkflowItem(smta1workflowitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(smta1workflowitemMapped, Arg.Any<MoveToNextStatusSMTA1WorkflowEngineCommand>(), cancellationToken)
            .ReturnsForAnyArgs(smta1workflowitem);

        documentReadRepositoryMock.IsDocumentSignedByLaboratoryId(Arg.Any<Guid>(), Arg.Any<DocumentFileType>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));
        SMTA1WorkflowEmailNotifierMock.NotifyUsers(smta1workflowitemMapped, cancellationToken).ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        validatorMock
           .ValidateAsync(cmd, cancellationToken)
           .ReturnsForAnyArgs(new ValidationResult());        

        mapperMock.Map(smta1workflowitem, cmd).ReturnsForAnyArgs(smta1workflowitemMapped);

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(smta1workflowitem));

        

        // Act
        Either<UpdateSMTA1WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        //response.Left.SMTA1WorkflowItem.Should()
        //    .NotBeNull(because: "SMTA1WorkflowItem should NOT be null");
        //response.Left.SMTA1WorkflowItem.Should()
        //    .BeEquivalentTo(smta1workflowitemMapped, because: "Returned smta1workflowitem must mach the one provided in request");

        
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        UpdateSMTA1WorkflowItemCommandValidator validatorMock = Substitute.For<UpdateSMTA1WorkflowItemCommandValidator>(documentReadRepositoryMock);
        ILogger<UpdateSMTA1WorkflowItemHandler> loggerMock = Substitute.For<ILogger<UpdateSMTA1WorkflowItemHandler>>();
        ISMTA1WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemWriteRepository>();
        IUpdateSMTA1WorkflowItemMapper mapperMock = Substitute.For<IUpdateSMTA1WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA1WorkflowEngine engineMock = Substitute.For<ISMTA1WorkflowEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();
        ISMTA1WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA1WorkflowHistoryItemWriteRepository>();

        ISMTA1WorkflowEmailNotifier SMTA1WorkflowEmailNotifierMock = Substitute.For<ISMTA1WorkflowEmailNotifier>();
        UpdateSMTA1WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, SMTA1WorkflowEmailNotifierMock);

        UpdateSMTA1WorkflowItemCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateSMTA1WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateSMTA1WorkflowItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<SMTA1WorkflowItem>(), Arg.Any<UpdateSMTA1WorkflowItemCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<SMTA1WorkflowItem>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        UpdateSMTA1WorkflowItemCommandValidator validatorMock = Substitute.For<UpdateSMTA1WorkflowItemCommandValidator>(documentReadRepositoryMock);
        ILogger<UpdateSMTA1WorkflowItemHandler> loggerMock = Substitute.For<ILogger<UpdateSMTA1WorkflowItemHandler>>();
        ISMTA1WorkflowItemWriteRepository repositoryMock = Substitute.For<ISMTA1WorkflowItemWriteRepository>();
        IUpdateSMTA1WorkflowItemMapper mapperMock = Substitute.For<IUpdateSMTA1WorkflowItemMapper>();
        CancellationToken cancellationToken = default;
        ISMTA1WorkflowEngine engineMock = Substitute.For<ISMTA1WorkflowEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();
        ISMTA1WorkflowHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<ISMTA1WorkflowHistoryItemWriteRepository>();
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();
        ISMTA1WorkflowEmailNotifier SMTA1WorkflowEmailNotifierMock = Substitute.For<ISMTA1WorkflowEmailNotifier>();
        Guid id = Guid.NewGuid();
        Guid referenceId = Guid.NewGuid();

        UpdateSMTA1WorkflowItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, SMTA1WorkflowEmailNotifierMock);


        UpdateSMTA1WorkflowItemCommand cmd = new() { LastSubmissionApproved = true, ReferenceId = referenceId, CurrentStatus = SMTA1WorkflowStatus.SubmitSMTA1, RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitSMTA1 } };

        SMTA1WorkflowItem smta1workflowitem = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = id, Status = SMTA1WorkflowStatus.SubmitSMTA1 };
        SMTA1WorkflowItem smta1workflowitemMapped = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = smta1workflowitem.Id, Status = SMTA1WorkflowStatus.SubmitSMTA1 };

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryHistoryMock.Create(Arg.Any<SMTA1WorkflowHistoryItem>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult(new Either<SMTA1WorkflowHistoryItem, Errors>(null)));


        repositoryHistoryMock.CopyLinkDocumentFromSMTA1WorkflowItem(smta1workflowitem.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(smta1workflowitem, Arg.Any<MoveToNextStatusSMTA1WorkflowEngineCommand>(), cancellationToken)
            .Throws(new Exception("test"));

        documentReadRepositoryMock.IsDocumentSignedByLaboratoryId(Arg.Any<Guid>(), Arg.Any<DocumentFileType>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));
        SMTA1WorkflowEmailNotifierMock.NotifyUsers(smta1workflowitem, cancellationToken).ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        validatorMock
           .ValidateAsync(cmd, cancellationToken)
           .ReturnsForAnyArgs(new ValidationResult());

        mapperMock.Map(smta1workflowitem, cmd).ReturnsForAnyArgs(smta1workflowitemMapped);

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(smta1workflowitem));

        // Act
        Either<UpdateSMTA1WorkflowItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateSMTA1WorkflowItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<SMTA1WorkflowItem>(), Arg.Any<UpdateSMTA1WorkflowItemCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<SMTA1WorkflowItem>(), cancellationToken).Received(1);
        });
    }
}