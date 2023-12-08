using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UploadFile;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.DocumentTemplates.UploadFile;

public class UploadFileHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        UploadFileCommandValidator validatorMock = Substitute.For<UploadFileCommandValidator>();
        ILogger<UploadFileHandler> loggerMock = Substitute.For<ILogger<UploadFileHandler>>();
        IDocumentTemplateWriteRepository repositoryMock = Substitute.For<IDocumentTemplateWriteRepository>();
        IUploadFileMapper mapperMock = Substitute.For<IUploadFileMapper>();
        IStorageAccountUtility storageAccountUtilityMock = Substitute.For<IStorageAccountUtility>();
        CancellationToken cancellationToken = default;

        UploadFileHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, storageAccountUtilityMock);
        UploadFileCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        DocumentTemplate documenttemplate = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(documenttemplate);

        Guid assignedId = Guid.NewGuid();

        repositoryMock.IsCurrentForUpload(cmd.DocumentTemplateFileType.GetValueOrDefault(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(true));
        var fileId = documenttemplate.Id.ToString() + "." + documenttemplate.Extension;

        storageAccountUtilityMock.UploadDocumentTemplate(cmd.File, fileId).ReturnsForAnyArgs(Task.FromResult(true));

        repositoryMock
            .Create(documenttemplate, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<DocumentTemplate, Errors>>(() =>
                {
                    documenttemplate.Id = assignedId;
                    return new(documenttemplate);
                }));

        // Act
        Either<UploadFileCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the DocumentTemplate")
            .And.Be(assignedId, because: "Returned documenttemplate Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UploadFileCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<UploadFileCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<DocumentTemplate>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UploadFileCommandValidator validatorMock = Substitute.For<UploadFileCommandValidator>();
        ILogger<UploadFileHandler> loggerMock = Substitute.For<ILogger<UploadFileHandler>>();
        IDocumentTemplateWriteRepository repositoryMock = Substitute.For<IDocumentTemplateWriteRepository>();
        IUploadFileMapper mapperMock = Substitute.For<IUploadFileMapper>();
        CancellationToken cancellationToken = default;
        IStorageAccountUtility storageAccountUtilityMock = Substitute.For<IStorageAccountUtility>();

        UploadFileHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, storageAccountUtilityMock);
        UploadFileCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UploadFileCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UploadFileCommandValidator validatorMock = Substitute.For<UploadFileCommandValidator>();
        ILogger<UploadFileHandler> loggerMock = Substitute.For<ILogger<UploadFileHandler>>();
        IDocumentTemplateWriteRepository repositoryMock = Substitute.For<IDocumentTemplateWriteRepository>();
        IUploadFileMapper mapperMock = Substitute.For<IUploadFileMapper>();
        CancellationToken cancellationToken = default;
        IStorageAccountUtility storageAccountUtilityMock = Substitute.For<IStorageAccountUtility>();

        UploadFileHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock, storageAccountUtilityMock);
        UploadFileCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        DocumentTemplate documenttemplate = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(documenttemplate);

        repositoryMock.IsCurrentForUpload(cmd.DocumentTemplateFileType.GetValueOrDefault(), cancellationToken).ReturnsForAnyArgs(Task.FromResult(true));
        var fileId = documenttemplate.Id.ToString() + "." + documenttemplate.Extension;

        storageAccountUtilityMock.UploadDocumentTemplate(cmd.File, fileId).ReturnsForAnyArgs(Task.FromResult(true));


        // TODO: change error type
        repositoryMock
            .Create(documenttemplate, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<DocumentTemplate, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<UploadFileCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UploadFileCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<UploadFileCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<DocumentTemplate>(), cancellationToken).Received(1);
        });
    }
}