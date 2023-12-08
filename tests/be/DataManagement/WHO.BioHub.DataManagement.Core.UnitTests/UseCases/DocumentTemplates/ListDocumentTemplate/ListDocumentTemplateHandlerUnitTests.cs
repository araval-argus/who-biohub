using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ListDocumentTemplates;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.DocumentTemplates.ListDocumentTemplates;

public class ListDocumentTemplatesHandlerUnitTests
{
    [Fact]
    public async Task If_no_documenttemplates_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListDocumentTemplatesQueryValidator validatorMock = Substitute.For<ListDocumentTemplatesQueryValidator>();
        ILogger<ListDocumentTemplatesHandler> loggerMock = Substitute.For<ILogger<ListDocumentTemplatesHandler>>();
        IDocumentTemplateReadRepository repositoryMock = Substitute.For<IDocumentTemplateReadRepository>();
        CancellationToken cancellationToken = default;

        ListDocumentTemplatesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListDocumentTemplatesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<DocumentTemplate> documenttemplates = Array.Empty<DocumentTemplate>();



        repositoryMock
            .List(assignedId, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(documenttemplates));



        // Act
        Either<ListDocumentTemplatesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.DocumentTemplates.Should()
            .BeEquivalentTo(documenttemplates, because: "Expected returned documenttemplates to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListDocumentTemplatesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(assignedId, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_documenttemplate_exists_then_it_is_returned()
    {
        // Arrange
        ListDocumentTemplatesQueryValidator validatorMock = Substitute.For<ListDocumentTemplatesQueryValidator>();
        ILogger<ListDocumentTemplatesHandler> loggerMock = Substitute.For<ILogger<ListDocumentTemplatesHandler>>();
        IDocumentTemplateReadRepository repositoryMock = Substitute.For<IDocumentTemplateReadRepository>();
        CancellationToken cancellationToken = default;

        ListDocumentTemplatesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListDocumentTemplatesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<DocumentTemplate> documenttemplates = new DocumentTemplate[1] { new() { Id = assignedId, Type = DocumentTemplateType.Folder } };
        IEnumerable<DocumentTemplateItem> documenttemplateDtos = new DocumentTemplateItem[1] { new() { Id = assignedId, UploadedBy = String.Empty } };
        
        repositoryMock
            .List(assignedId, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(documenttemplates));

        // Act
        Either<ListDocumentTemplatesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.DocumentTemplates.Should()
            .BeEquivalentTo(documenttemplateDtos, because: "Expected returned documenttemplates to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListDocumentTemplatesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(assignedId, cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListDocumentTemplatesQueryValidator validatorMock = Substitute.For<ListDocumentTemplatesQueryValidator>();
        ILogger<ListDocumentTemplatesHandler> loggerMock = Substitute.For<ILogger<ListDocumentTemplatesHandler>>();
        IDocumentTemplateReadRepository repositoryMock = Substitute.For<IDocumentTemplateReadRepository>();
        CancellationToken cancellationToken = default;

        ListDocumentTemplatesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListDocumentTemplatesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListDocumentTemplatesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);
        Guid assignedId = Guid.NewGuid();

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
            await validatorMock.ValidateAsync(Arg.Any<ListDocumentTemplatesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(assignedId, cancellationToken).Received(0);
        });
    }
}