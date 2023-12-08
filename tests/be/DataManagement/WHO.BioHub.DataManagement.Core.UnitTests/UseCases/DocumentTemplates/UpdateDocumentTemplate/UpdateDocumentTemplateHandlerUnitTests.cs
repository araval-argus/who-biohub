using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UpdateDocumentTemplate;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.DocumentTemplates.UpdateDocumentTemplate;

public class UpdateDocumentTemplateHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateDocumentTemplateCommandValidator validatorMock = Substitute.For<UpdateDocumentTemplateCommandValidator>();
        ILogger<UpdateDocumentTemplateHandler> loggerMock = Substitute.For<ILogger<UpdateDocumentTemplateHandler>>();
        IDocumentTemplateWriteRepository repositoryMock = Substitute.For<IDocumentTemplateWriteRepository>();
        IUpdateDocumentTemplateMapper mapperMock = Substitute.For<IUpdateDocumentTemplateMapper>();
        CancellationToken cancellationToken = default;

        UpdateDocumentTemplateHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateDocumentTemplateCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        DocumentTemplate documenttemplate = new() { Id = Guid.NewGuid(), Type = DocumentTemplateType.File, Current = false };
        DocumentTemplate documenttemplateMapped = new() { Id = documenttemplate.Id, Type = DocumentTemplateType.File, Current = false };
        
        repositoryMock
           .ReadForUpdate(cmd.Id, cancellationToken)
           .ReturnsForAnyArgs(
               Task.FromResult(documenttemplate));

        mapperMock.Map(documenttemplate, cmd).ReturnsForAnyArgs(documenttemplateMapped);

        repositoryMock
            .Update(documenttemplate, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateDocumentTemplateCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "DocumentTemplate should NOT be null");
        response.Left.Id.Should()
            .Be(documenttemplateMapped.Id, because: "Returned documenttemplate must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateDocumentTemplateCommand>(), cancellationToken).Received(1);
            mapperMock.Map(documenttemplate, Arg.Any<UpdateDocumentTemplateCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<DocumentTemplate>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateDocumentTemplateCommandValidator validatorMock = Substitute.For<UpdateDocumentTemplateCommandValidator>();
        ILogger<UpdateDocumentTemplateHandler> loggerMock = Substitute.For<ILogger<UpdateDocumentTemplateHandler>>();
        IDocumentTemplateWriteRepository repositoryMock = Substitute.For<IDocumentTemplateWriteRepository>();
        IUpdateDocumentTemplateMapper mapperMock = Substitute.For<IUpdateDocumentTemplateMapper>();
        CancellationToken cancellationToken = default;

        UpdateDocumentTemplateHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateDocumentTemplateCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateDocumentTemplateCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateDocumentTemplateCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<DocumentTemplate>(), Arg.Any<UpdateDocumentTemplateCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<DocumentTemplate>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateDocumentTemplateCommandValidator validatorMock = Substitute.For<UpdateDocumentTemplateCommandValidator>();
        ILogger<UpdateDocumentTemplateHandler> loggerMock = Substitute.For<ILogger<UpdateDocumentTemplateHandler>>();
        IDocumentTemplateWriteRepository repositoryMock = Substitute.For<IDocumentTemplateWriteRepository>();
        IUpdateDocumentTemplateMapper mapperMock = Substitute.For<IUpdateDocumentTemplateMapper>();
        CancellationToken cancellationToken = default;

        UpdateDocumentTemplateHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateDocumentTemplateCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());
               

        DocumentTemplate documenttemplate = new() { Id = Guid.NewGuid(), Type = DocumentTemplateType.File, Current = false };
        DocumentTemplate documenttemplateMapped = new() { Id = documenttemplate.Id, Type = DocumentTemplateType.File, Current = false };

        repositoryMock
           .ReadForUpdate(cmd.Id, cancellationToken)
           .ReturnsForAnyArgs(
               Task.FromResult(documenttemplate));

        mapperMock.Map(documenttemplate, cmd).ReturnsForAnyArgs(documenttemplateMapped);

        // TODO: change error type
        repositoryMock
            .Update(documenttemplate, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateDocumentTemplateCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateDocumentTemplateCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<DocumentTemplate>(), Arg.Any<UpdateDocumentTemplateCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<DocumentTemplate>(), cancellationToken).Received(1);
        });
    }
}