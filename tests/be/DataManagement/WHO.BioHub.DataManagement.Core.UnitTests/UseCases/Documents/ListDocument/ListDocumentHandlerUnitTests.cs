using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocuments;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocument;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Documents.ListDocuments;

public class ListDocumentsHandlerUnitTests
{
    [Fact]
    public async Task If_no_documents_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListDocumentsQueryValidator validatorMock = Substitute.For<ListDocumentsQueryValidator>();
        ILogger<ListDocumentsHandler> loggerMock = Substitute.For<ILogger<ListDocumentsHandler>>();
        IDocumentReadRepository repositoryMock = Substitute.For<IDocumentReadRepository>();
        CancellationToken cancellationToken = default;

        IListDocumentsMapper mapperMock = Substitute.For<IListDocumentsMapper>();
        IWorklistToBioHubItemReadRepository worklistToBioHubItemReadRepositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        IWorklistFromBioHubItemReadRepository worklistFromBioHubItemReadRepositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        ListDocumentsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock, worklistToBioHubItemReadRepositoryMock, worklistFromBioHubItemReadRepositoryMock);
        ListDocumentsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Document> documents = Array.Empty<Document>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(documents));

        // Act
        Either<ListDocumentsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Documents.Should()
            .BeEquivalentTo(documents, because: "Expected returned documents to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListDocumentsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_document_exists_then_it_is_returned()
    {
        // Arrange
        ListDocumentsQueryValidator validatorMock = Substitute.For<ListDocumentsQueryValidator>();
        ILogger<ListDocumentsHandler> loggerMock = Substitute.For<ILogger<ListDocumentsHandler>>();
        IDocumentReadRepository repositoryMock = Substitute.For<IDocumentReadRepository>();
        CancellationToken cancellationToken = default;

        IListDocumentsMapper mapperMock = Substitute.For<IListDocumentsMapper>();
        IWorklistToBioHubItemReadRepository worklistToBioHubItemReadRepositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        IWorklistFromBioHubItemReadRepository worklistFromBioHubItemReadRepositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        ListDocumentsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock, worklistToBioHubItemReadRepositoryMock, worklistFromBioHubItemReadRepositoryMock);

        ListDocumentsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<Document> documents = new Document[1] { new() { Id = assignedId, WorklistToBioHubItemDocuments = new List<WorklistToBioHubItemDocument>(), WorklistFromBioHubItemDocuments = new List<WorklistFromBioHubItemDocument>() } };
        IEnumerable<DocumentItemDto> documentItems = new DocumentItemDto[1] { new() { Id = assignedId } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(documents));

        mapperMock.Map(documents.ToList(), Arg.Any<List<WorklistToBioHubItem>>(), Arg.Any<List<WorklistFromBioHubItem>>()).ReturnsForAnyArgs(documentItems);

        // Act
        Either<ListDocumentsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Documents.Should()
            .BeEquivalentTo(documentItems, because: "Expected returned documents to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListDocumentsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListDocumentsQueryValidator validatorMock = Substitute.For<ListDocumentsQueryValidator>();
        ILogger<ListDocumentsHandler> loggerMock = Substitute.For<ILogger<ListDocumentsHandler>>();
        IDocumentReadRepository repositoryMock = Substitute.For<IDocumentReadRepository>();
        CancellationToken cancellationToken = default;

        IListDocumentsMapper mapperMock = Substitute.For<IListDocumentsMapper>();
        IWorklistToBioHubItemReadRepository worklistToBioHubItemReadRepositoryMock = Substitute.For<IWorklistToBioHubItemReadRepository>();
        IWorklistFromBioHubItemReadRepository worklistFromBioHubItemReadRepositoryMock = Substitute.For<IWorklistFromBioHubItemReadRepository>();
        ListDocumentsHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock, worklistToBioHubItemReadRepositoryMock, worklistFromBioHubItemReadRepositoryMock);

        ListDocumentsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListDocumentsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListDocumentsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}