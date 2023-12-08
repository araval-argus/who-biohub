using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.UpdateIsolationHostType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.IsolationHostTypes.UpdateIsolationHostType;

public class UpdateIsolationHostTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateIsolationHostTypeCommandValidator validatorMock = Substitute.For<UpdateIsolationHostTypeCommandValidator>();
        ILogger<UpdateIsolationHostTypeHandler> loggerMock = Substitute.For<ILogger<UpdateIsolationHostTypeHandler>>();
        IIsolationHostTypeWriteRepository repositoryMock = Substitute.For<IIsolationHostTypeWriteRepository>();
        IUpdateIsolationHostTypeMapper mapperMock = Substitute.For<IUpdateIsolationHostTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateIsolationHostTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateIsolationHostTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        IsolationHostType isolationhosttype = new() { Id = Guid.NewGuid() };
        IsolationHostType isolationhosttypeMapped = new() { Id = isolationhosttype.Id };

        mapperMock.Map(isolationhosttype, cmd).ReturnsForAnyArgs(isolationhosttypeMapped);

        repositoryMock
            .Update(isolationhosttype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateIsolationHostTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.IsolationHostType.Should()
            .NotBeNull(because: "IsolationHostType should NOT be null");
        response.Left.IsolationHostType.Should()
            .BeEquivalentTo(isolationhosttypeMapped, because: "Returned isolationhosttype must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateIsolationHostTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(isolationhosttype, Arg.Any<UpdateIsolationHostTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<IsolationHostType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateIsolationHostTypeCommandValidator validatorMock = Substitute.For<UpdateIsolationHostTypeCommandValidator>();
        ILogger<UpdateIsolationHostTypeHandler> loggerMock = Substitute.For<ILogger<UpdateIsolationHostTypeHandler>>();
        IIsolationHostTypeWriteRepository repositoryMock = Substitute.For<IIsolationHostTypeWriteRepository>();
        IUpdateIsolationHostTypeMapper mapperMock = Substitute.For<IUpdateIsolationHostTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateIsolationHostTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateIsolationHostTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateIsolationHostTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateIsolationHostTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<IsolationHostType>(), Arg.Any<UpdateIsolationHostTypeCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<IsolationHostType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateIsolationHostTypeCommandValidator validatorMock = Substitute.For<UpdateIsolationHostTypeCommandValidator>();
        ILogger<UpdateIsolationHostTypeHandler> loggerMock = Substitute.For<ILogger<UpdateIsolationHostTypeHandler>>();
        IIsolationHostTypeWriteRepository repositoryMock = Substitute.For<IIsolationHostTypeWriteRepository>();
        IUpdateIsolationHostTypeMapper mapperMock = Substitute.For<IUpdateIsolationHostTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateIsolationHostTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateIsolationHostTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        IsolationHostType isolationhosttype = new();
        IsolationHostType isolationhosttypeMapped = new();
        mapperMock.Map(isolationhosttype, cmd).ReturnsForAnyArgs(isolationhosttypeMapped);

        // TODO: change error type
        repositoryMock
            .Update(isolationhosttype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateIsolationHostTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateIsolationHostTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<IsolationHostType>(), Arg.Any<UpdateIsolationHostTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<IsolationHostType>(), cancellationToken).Received(1);
        });
    }
}