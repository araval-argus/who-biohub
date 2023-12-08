using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.UpdateIsolationTechniqueType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.IsolationTechniqueTypes.UpdateIsolationTechniqueType;

public class UpdateIsolationTechniqueTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateIsolationTechniqueTypeCommandValidator validatorMock = Substitute.For<UpdateIsolationTechniqueTypeCommandValidator>();
        ILogger<UpdateIsolationTechniqueTypeHandler> loggerMock = Substitute.For<ILogger<UpdateIsolationTechniqueTypeHandler>>();
        IIsolationTechniqueTypeWriteRepository repositoryMock = Substitute.For<IIsolationTechniqueTypeWriteRepository>();
        IUpdateIsolationTechniqueTypeMapper mapperMock = Substitute.For<IUpdateIsolationTechniqueTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateIsolationTechniqueTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateIsolationTechniqueTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        IsolationTechniqueType isolationtechniquetype = new() { Id = Guid.NewGuid() };
        IsolationTechniqueType isolationtechniquetypeMapped = new() { Id = isolationtechniquetype.Id };

        mapperMock.Map(isolationtechniquetype, cmd).ReturnsForAnyArgs(isolationtechniquetypeMapped);

        repositoryMock
            .Update(isolationtechniquetype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateIsolationTechniqueTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.IsolationTechniqueType.Should()
            .NotBeNull(because: "IsolationTechniqueType should NOT be null");
        response.Left.IsolationTechniqueType.Should()
            .BeEquivalentTo(isolationtechniquetypeMapped, because: "Returned isolationtechniquetype must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateIsolationTechniqueTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(isolationtechniquetype, Arg.Any<UpdateIsolationTechniqueTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<IsolationTechniqueType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateIsolationTechniqueTypeCommandValidator validatorMock = Substitute.For<UpdateIsolationTechniqueTypeCommandValidator>();
        ILogger<UpdateIsolationTechniqueTypeHandler> loggerMock = Substitute.For<ILogger<UpdateIsolationTechniqueTypeHandler>>();
        IIsolationTechniqueTypeWriteRepository repositoryMock = Substitute.For<IIsolationTechniqueTypeWriteRepository>();
        IUpdateIsolationTechniqueTypeMapper mapperMock = Substitute.For<IUpdateIsolationTechniqueTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateIsolationTechniqueTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateIsolationTechniqueTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateIsolationTechniqueTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateIsolationTechniqueTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<IsolationTechniqueType>(), Arg.Any<UpdateIsolationTechniqueTypeCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<IsolationTechniqueType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateIsolationTechniqueTypeCommandValidator validatorMock = Substitute.For<UpdateIsolationTechniqueTypeCommandValidator>();
        ILogger<UpdateIsolationTechniqueTypeHandler> loggerMock = Substitute.For<ILogger<UpdateIsolationTechniqueTypeHandler>>();
        IIsolationTechniqueTypeWriteRepository repositoryMock = Substitute.For<IIsolationTechniqueTypeWriteRepository>();
        IUpdateIsolationTechniqueTypeMapper mapperMock = Substitute.For<IUpdateIsolationTechniqueTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateIsolationTechniqueTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateIsolationTechniqueTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        IsolationTechniqueType isolationtechniquetype = new();
        IsolationTechniqueType isolationtechniquetypeMapped = new();
        mapperMock.Map(isolationtechniquetype, cmd).ReturnsForAnyArgs(isolationtechniquetypeMapped);

        // TODO: change error type
        repositoryMock
            .Update(isolationtechniquetype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateIsolationTechniqueTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateIsolationTechniqueTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<IsolationTechniqueType>(), Arg.Any<UpdateIsolationTechniqueTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<IsolationTechniqueType>(), cancellationToken).Received(1);
        });
    }
}