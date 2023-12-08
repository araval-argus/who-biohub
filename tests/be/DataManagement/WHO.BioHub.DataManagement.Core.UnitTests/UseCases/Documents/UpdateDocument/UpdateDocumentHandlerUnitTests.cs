using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.UpdateDocument;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Documents.UpdateDocument;

public class UpdateDocumentHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateDocumentCommandValidator validatorMock = Substitute.For<UpdateDocumentCommandValidator>();
        ILogger<UpdateDocumentHandler> loggerMock = Substitute.For<ILogger<UpdateDocumentHandler>>();
        IDocumentWriteRepository repositoryMock = Substitute.For<IDocumentWriteRepository>();
        IUpdateDocumentMapper mapperMock = Substitute.For<IUpdateDocumentMapper>();
        CancellationToken cancellationToken = default;

        UpdateDocumentHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateDocumentCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Document document = new() { Id = Guid.NewGuid() };
        Document documentMapped = new() { Id = document.Id };

        mapperMock.Map(document, cmd).ReturnsForAnyArgs(documentMapped);

        repositoryMock
            .Update(document, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateDocumentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "Document should NOT be null");
        response.Left.Id.Should()
            .Be(documentMapped.Id, because: "Returned document must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateDocumentCommand>(), cancellationToken).Received(1);
            mapperMock.Map(document, Arg.Any<UpdateDocumentCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Document>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateDocumentCommandValidator validatorMock = Substitute.For<UpdateDocumentCommandValidator>();
        ILogger<UpdateDocumentHandler> loggerMock = Substitute.For<ILogger<UpdateDocumentHandler>>();
        IDocumentWriteRepository repositoryMock = Substitute.For<IDocumentWriteRepository>();
        IUpdateDocumentMapper mapperMock = Substitute.For<IUpdateDocumentMapper>();
        CancellationToken cancellationToken = default;

        UpdateDocumentHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateDocumentCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateDocumentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateDocumentCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Document>(), Arg.Any<UpdateDocumentCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<Document>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateDocumentCommandValidator validatorMock = Substitute.For<UpdateDocumentCommandValidator>();
        ILogger<UpdateDocumentHandler> loggerMock = Substitute.For<ILogger<UpdateDocumentHandler>>();
        IDocumentWriteRepository repositoryMock = Substitute.For<IDocumentWriteRepository>();
        IUpdateDocumentMapper mapperMock = Substitute.For<IUpdateDocumentMapper>();
        CancellationToken cancellationToken = default;

        UpdateDocumentHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateDocumentCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Document document = new();
        Document documentMapped = new();
        mapperMock.Map(document, cmd).ReturnsForAnyArgs(documentMapped);

        // TODO: change error type
        repositoryMock
            .Update(document, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateDocumentCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateDocumentCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Document>(), Arg.Any<UpdateDocumentCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Document>(), cancellationToken).Received(1);
        });
    }
}