using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.CreateIsolationHostType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.IsolationHostTypes.CreateIsolationHostType;

public class CreateIsolationHostTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateIsolationHostTypeCommandValidator validatorMock = Substitute.For<CreateIsolationHostTypeCommandValidator>();
        ILogger<CreateIsolationHostTypeHandler> loggerMock = Substitute.For<ILogger<CreateIsolationHostTypeHandler>>();
        IIsolationHostTypeWriteRepository repositoryMock = Substitute.For<IIsolationHostTypeWriteRepository>();
        ICreateIsolationHostTypeMapper mapperMock = Substitute.For<ICreateIsolationHostTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateIsolationHostTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateIsolationHostTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        IsolationHostType isolationhosttype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(isolationhosttype);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(isolationhosttype, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<IsolationHostType, Errors>>(() =>
                {
                    isolationhosttype.Id = assignedId;
                    return new(isolationhosttype);
                }));

        // Act
        Either<CreateIsolationHostTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.IsolationHostType.Should()
            .NotBeNull(because: "IsolationHostType should NOT be null");
        response.Left.IsolationHostType.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the IsolationHostType")
            .And.Be(assignedId, because: "Returned isolationhosttype Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateIsolationHostTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateIsolationHostTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<IsolationHostType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateIsolationHostTypeCommandValidator validatorMock = Substitute.For<CreateIsolationHostTypeCommandValidator>();
        ILogger<CreateIsolationHostTypeHandler> loggerMock = Substitute.For<ILogger<CreateIsolationHostTypeHandler>>();
        IIsolationHostTypeWriteRepository repositoryMock = Substitute.For<IIsolationHostTypeWriteRepository>();
        ICreateIsolationHostTypeMapper mapperMock = Substitute.For<ICreateIsolationHostTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateIsolationHostTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateIsolationHostTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateIsolationHostTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateIsolationHostTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateIsolationHostTypeCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<IsolationHostType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateIsolationHostTypeCommandValidator validatorMock = Substitute.For<CreateIsolationHostTypeCommandValidator>();
        ILogger<CreateIsolationHostTypeHandler> loggerMock = Substitute.For<ILogger<CreateIsolationHostTypeHandler>>();
        IIsolationHostTypeWriteRepository repositoryMock = Substitute.For<IIsolationHostTypeWriteRepository>();
        ICreateIsolationHostTypeMapper mapperMock = Substitute.For<ICreateIsolationHostTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateIsolationHostTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateIsolationHostTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        IsolationHostType isolationhosttype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(isolationhosttype);

        // TODO: change error type
        repositoryMock
            .Create(isolationhosttype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<IsolationHostType, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateIsolationHostTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateIsolationHostTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateIsolationHostTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<IsolationHostType>(), cancellationToken).Received(1);
        });
    }
}