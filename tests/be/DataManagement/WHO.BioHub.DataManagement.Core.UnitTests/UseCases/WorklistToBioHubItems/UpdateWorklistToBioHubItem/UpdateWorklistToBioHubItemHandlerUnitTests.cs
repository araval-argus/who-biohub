using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItem;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.WorkflowEngine.Commands;
using NSubstitute.ExceptionExtensions;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItem;

public class UpdateWorklistToBioHubItemHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        IWorklistToBioHubItemReadRepository readRepositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        UpdateWorklistToBioHubItemCommandValidator validatorMock = Substitute.For<UpdateWorklistToBioHubItemCommandValidator>(documentReadRepositoryMock, readRepositoryMock);
        ILogger<UpdateWorklistToBioHubItemHandler> loggerMock = Substitute.For<ILogger<UpdateWorklistToBioHubItemHandler>>();
        IWorklistToBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubItemWriteRepository>();
        IUpdateWorklistToBioHubItemMapper mapperMock = Substitute.For<IUpdateWorklistToBioHubItemMapper>();
        CancellationToken cancellationToken = default;
        IWorklistToBioHubEngine engineMock = Substitute.For<IWorklistToBioHubEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();
        IWorklistToBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistToBioHubHistoryItemWriteRepository>();

        IWorklistToBioHubEmailNotifier worklistToBioHubEmailNotifierMock = Substitute.For<IWorklistToBioHubEmailNotifier>();
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateWorklistToBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, worklistToBioHubEmailNotifierMock);

        Guid id = Guid.NewGuid();
        Guid referenceId = Guid.NewGuid();

        UpdateWorklistToBioHubItemCommand cmd = new() { LastSubmissionApproved = true, ReferenceId = referenceId, CurrentStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitAnnex2OfSMTA1 } }; ;
        
        WorklistToBioHubItem worklisttobiohubitem = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = Guid.NewGuid(), Status = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 };
        WorklistToBioHubItem worklisttobiohubitemMapped = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = worklisttobiohubitem.Id, Status = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 };

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryHistoryMock.Create(Arg.Any<WorklistToBioHubHistoryItem>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult(new Either<WorklistToBioHubHistoryItem, Errors>(null)));

        repositoryHistoryMock.CopyLinkDocumentFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkMaterialShippingInformationFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkLaboratoryFocalPointsFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkBookingFormsFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkFeedbacksFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkBioHubFacilityFocalPointsFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
          .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));
       

        engineMock.MoveToNextStatusUponApproveOrSaveDraft(worklisttobiohubitemMapped, Arg.Any<MoveToNextStatusToBioHubEngineCommand>(), cancellationToken)
            .ReturnsForAnyArgs(worklisttobiohubitem);

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        documentReadRepositoryMock.IsDocumentSignedByLaboratoryId(Arg.Any<Guid>(), Arg.Any<DocumentFileType>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        worklistToBioHubEmailNotifierMock.NotifyUsers(worklisttobiohubitemMapped, cancellationToken).ReturnsForAnyArgs(Task.FromResult<Errors?>(null));
        
        mapperMock.Map(worklisttobiohubitem, cmd).ReturnsForAnyArgs(worklisttobiohubitemMapped);

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklisttobiohubitem));

        // Act
        Either<UpdateWorklistToBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Should()
            .NotBeNull(because: "WorklistToBioHubItem should NOT be null");
        
        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateWorklistToBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(worklisttobiohubitem, Arg.Any<UpdateWorklistToBioHubItemCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<WorklistToBioHubItem>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        IWorklistToBioHubItemReadRepository readRepositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        UpdateWorklistToBioHubItemCommandValidator validatorMock = Substitute.For<UpdateWorklistToBioHubItemCommandValidator>(documentReadRepositoryMock, readRepositoryMock);
        ILogger<UpdateWorklistToBioHubItemHandler> loggerMock = Substitute.For<ILogger<UpdateWorklistToBioHubItemHandler>>();
        IWorklistToBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubItemWriteRepository>();
        IUpdateWorklistToBioHubItemMapper mapperMock = Substitute.For<IUpdateWorklistToBioHubItemMapper>();
        CancellationToken cancellationToken = default;

        IWorklistToBioHubEngine engineMock = Substitute.For<IWorklistToBioHubEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();

        IWorklistToBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistToBioHubHistoryItemWriteRepository>();

        IWorklistToBioHubEmailNotifier worklistToBioHubEmailNotifierMock = Substitute.For<IWorklistToBioHubEmailNotifier>();
        
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateWorklistToBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, worklistToBioHubEmailNotifierMock);

        Guid id = Guid.NewGuid();
        Guid referenceId = Guid.NewGuid();

        UpdateWorklistToBioHubItemCommand cmd = new() { LastSubmissionApproved = true, ReferenceId = referenceId, CurrentStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitAnnex2OfSMTA1 } }; ;

        WorklistToBioHubItem worklisttobiohubitem = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = Guid.NewGuid(), Status = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 };
        WorklistToBioHubItem worklisttobiohubitemMapped = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = worklisttobiohubitem.Id, Status = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 };


        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateWorklistToBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateWorklistToBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<WorklistToBioHubItem>(), Arg.Any<UpdateWorklistToBioHubItemCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<WorklistToBioHubItem>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        IDocumentReadRepository documentReadRepositoryMock = Substitute.For<IDocumentReadRepository>();
        IWorklistToBioHubItemReadRepository readRepositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        UpdateWorklistToBioHubItemCommandValidator validatorMock = Substitute.For<UpdateWorklistToBioHubItemCommandValidator>(documentReadRepositoryMock, readRepositoryMock);
        ILogger<UpdateWorklistToBioHubItemHandler> loggerMock = Substitute.For<ILogger<UpdateWorklistToBioHubItemHandler>>();
        IWorklistToBioHubItemWriteRepository repositoryMock = Substitute.For<IWorklistToBioHubItemWriteRepository>();
        IUpdateWorklistToBioHubItemMapper mapperMock = Substitute.For<IUpdateWorklistToBioHubItemMapper>();
        CancellationToken cancellationToken = default;
        IWorklistToBioHubEngine engineMock = Substitute.For<IWorklistToBioHubEngine>();
        IStorageAccountUtility mapperStorageAccountUtility = Substitute.For<IStorageAccountUtility>();
        IDocumentWriteRepository mapperDocumentWriteRepository = Substitute.For<IDocumentWriteRepository>();
        IWorklistToBioHubHistoryItemWriteRepository repositoryHistoryMock = Substitute.For<IWorklistToBioHubHistoryItemWriteRepository>();

        IWorklistToBioHubEmailNotifier worklistToBioHubEmailNotifierMock = Substitute.For<IWorklistToBioHubEmailNotifier>();
        IDbContextTransaction transactionMock = Substitute.For<IDbContextTransaction>();

        UpdateWorklistToBioHubItemHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, engineMock, repositoryHistoryMock, worklistToBioHubEmailNotifierMock);

        Guid id = Guid.NewGuid();
        Guid referenceId = Guid.NewGuid();

        UpdateWorklistToBioHubItemCommand cmd = new() { LastSubmissionApproved = true, ReferenceId = referenceId, CurrentStatus = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, RoleType = RoleType.WHO, UserPermissions = new List<string>() { PermissionNames.CanSubmitAnnex2OfSMTA1 } }; ;

        WorklistToBioHubItem worklisttobiohubitem = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = Guid.NewGuid(), Status = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 };
        WorklistToBioHubItem worklisttobiohubitemMapped = new() { LastSubmissionApproved = true, ReferenceId = referenceId, Id = worklisttobiohubitem.Id, Status = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 };

        repositoryMock.BeginTransactionAsync().ReturnsForAnyArgs(Task.FromResult(transactionMock));

        repositoryHistoryMock.Create(Arg.Any<WorklistToBioHubHistoryItem>(), cancellationToken, transactionMock)
            .ReturnsForAnyArgs(Task.FromResult(new Either<WorklistToBioHubHistoryItem, Errors>(null)));

        repositoryHistoryMock.CopyLinkDocumentFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkMaterialShippingInformationFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkLaboratoryFocalPointsFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkBookingFormsFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkFeedbacksFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
           .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        repositoryHistoryMock.LinkBioHubFacilityFocalPointsFromWorklistToBioHubItem(worklisttobiohubitemMapped.Id, Arg.Any<Guid>(), cancellationToken, transactionMock)
          .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));


        engineMock.MoveToNextStatusUponApproveOrSaveDraft(worklisttobiohubitemMapped, Arg.Any<MoveToNextStatusToBioHubEngineCommand>(), cancellationToken)
            .Throws(new Exception("test"));

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        documentReadRepositoryMock.IsDocumentSignedByLaboratoryId(Arg.Any<Guid>(), Arg.Any<DocumentFileType>(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(false));

        worklistToBioHubEmailNotifierMock.NotifyUsers(worklisttobiohubitemMapped, cancellationToken).ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        mapperMock.Map(worklisttobiohubitem, cmd).ReturnsForAnyArgs(worklisttobiohubitemMapped);

        repositoryMock
            .ReadForUpdate(cmd.Id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(worklisttobiohubitem));

        // Act
        Either<UpdateWorklistToBioHubItemCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateWorklistToBioHubItemCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<WorklistToBioHubItem>(), Arg.Any<UpdateWorklistToBioHubItemCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<WorklistToBioHubItem>(), cancellationToken).Received(1);
        });
    }
}