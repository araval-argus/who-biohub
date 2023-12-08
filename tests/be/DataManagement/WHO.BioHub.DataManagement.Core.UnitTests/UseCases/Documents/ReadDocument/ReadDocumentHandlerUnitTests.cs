using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ReadDocument;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Documents.ReadDocument;

public class ReadDocumentHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadDocumentQueryValidator validatorMock = Substitute.For<ReadDocumentQueryValidator>();
        ILogger<ReadDocumentHandler> loggerMock = Substitute.For<ILogger<ReadDocumentHandler>>();
        IDocumentReadRepository repositoryMock = Substitute.For<IDocumentReadRepository>();
        CancellationToken cancellationToken = default;
        IStorageAccountUtility storageAccountUtilityMock = Substitute.For<IStorageAccountUtility>();

        ReadDocumentHandler handler = new(loggerMock, validatorMock, repositoryMock, storageAccountUtilityMock);
        ReadDocumentQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        Document document = new() { Id = id, LaboratoryId = Guid.NewGuid(), Type = DocumentFileType.SMTA1 };
        DocumentItemDto documentItemDto = new() { Id = id };       
       

        var file = new HttpResponseMessage();

        storageAccountUtilityMock.DownloadDocument(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(new Either<HttpResponseMessage, Errors>(file)));

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .ReadForDocumentMenu(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(document));

        // Act
        Either<ReadDocumentQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Should()
            .NotBeNull(because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadDocumentQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadDocumentQueryValidator validatorMock = Substitute.For<ReadDocumentQueryValidator>();
        ILogger<ReadDocumentHandler> loggerMock = Substitute.For<ILogger<ReadDocumentHandler>>();
        IDocumentReadRepository repositoryMock = Substitute.For<IDocumentReadRepository>();
        CancellationToken cancellationToken = default;

        IStorageAccountUtility storageAccountUtilityMock = Substitute.For<IStorageAccountUtility>();

        ReadDocumentHandler handler = new(loggerMock, validatorMock, repositoryMock, storageAccountUtilityMock);

        ReadDocumentQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadDocumentQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadDocumentQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}