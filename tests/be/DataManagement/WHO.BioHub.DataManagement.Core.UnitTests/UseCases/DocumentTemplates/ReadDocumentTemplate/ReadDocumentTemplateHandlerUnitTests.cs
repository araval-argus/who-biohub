using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadFile;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.DocumentTemplates.ReadFile;

public class ReadFileHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadFileQueryValidator validatorMock = Substitute.For<ReadFileQueryValidator>();
        ILogger<ReadFileHandler> loggerMock = Substitute.For<ILogger<ReadFileHandler>>();
        IDocumentTemplateReadRepository repositoryMock = Substitute.For<IDocumentTemplateReadRepository>();
        CancellationToken cancellationToken = default;
        IStorageAccountUtility storageAccountUtilityMock = Substitute.For<IStorageAccountUtility>();

        ReadFileHandler handler = new(loggerMock, validatorMock, repositoryMock, storageAccountUtilityMock);
        ReadFileQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        DocumentTemplate documenttemplate = new() { Id = id };     


        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Read(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(documenttemplate));

        HttpResponseMessage file = new();
        var fileId = documenttemplate.Id.ToString() + "." + documenttemplate.Extension;


        storageAccountUtilityMock.DownloadDocumentTemplate(fileId, documenttemplate.Name).ReturnsForAnyArgs(Task.FromResult(new Either<HttpResponseMessage, Errors> (file)));

        // Act
        Either<ReadFileQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.DownloadedFile.Should()
            .NotBeNull(because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadFileQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadFileQueryValidator validatorMock = Substitute.For<ReadFileQueryValidator>();
        ILogger<ReadFileHandler> loggerMock = Substitute.For<ILogger<ReadFileHandler>>();
        IDocumentTemplateReadRepository repositoryMock = Substitute.For<IDocumentTemplateReadRepository>();
        CancellationToken cancellationToken = default;
        IStorageAccountUtility storageAccountUtilityMock = Substitute.For<IStorageAccountUtility>();

        ReadFileHandler handler = new(loggerMock, validatorMock, repositoryMock, storageAccountUtilityMock);
        ReadFileQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadFileQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadFileQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}