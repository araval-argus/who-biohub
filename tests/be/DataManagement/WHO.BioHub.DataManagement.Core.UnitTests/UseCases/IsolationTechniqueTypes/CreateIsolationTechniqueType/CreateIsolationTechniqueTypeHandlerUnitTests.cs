using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.CreateIsolationTechniqueType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.IsolationTechniqueTypes.CreateIsolationTechniqueType;

public class CreateIsolationTechniqueTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateIsolationTechniqueTypeCommandValidator validatorMock = Substitute.For<CreateIsolationTechniqueTypeCommandValidator>();
        ILogger<CreateIsolationTechniqueTypeHandler> loggerMock = Substitute.For<ILogger<CreateIsolationTechniqueTypeHandler>>();
        IIsolationTechniqueTypeWriteRepository repositoryMock = Substitute.For<IIsolationTechniqueTypeWriteRepository>();
        ICreateIsolationTechniqueTypeMapper mapperMock = Substitute.For<ICreateIsolationTechniqueTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateIsolationTechniqueTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateIsolationTechniqueTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        IsolationTechniqueType isolationtechniquetype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(isolationtechniquetype);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(isolationtechniquetype, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<IsolationTechniqueType, Errors>>(() =>
                {
                    isolationtechniquetype.Id = assignedId;
                    return new(isolationtechniquetype);
                }));

        // Act
        Either<CreateIsolationTechniqueTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.IsolationTechniqueType.Should()
            .NotBeNull(because: "IsolationTechniqueType should NOT be null");
        response.Left.IsolationTechniqueType.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the IsolationTechniqueType")
            .And.Be(assignedId, because: "Returned isolationtechniquetype Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateIsolationTechniqueTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateIsolationTechniqueTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<IsolationTechniqueType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateIsolationTechniqueTypeCommandValidator validatorMock = Substitute.For<CreateIsolationTechniqueTypeCommandValidator>();
        ILogger<CreateIsolationTechniqueTypeHandler> loggerMock = Substitute.For<ILogger<CreateIsolationTechniqueTypeHandler>>();
        IIsolationTechniqueTypeWriteRepository repositoryMock = Substitute.For<IIsolationTechniqueTypeWriteRepository>();
        ICreateIsolationTechniqueTypeMapper mapperMock = Substitute.For<ICreateIsolationTechniqueTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateIsolationTechniqueTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateIsolationTechniqueTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateIsolationTechniqueTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateIsolationTechniqueTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateIsolationTechniqueTypeCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<IsolationTechniqueType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateIsolationTechniqueTypeCommandValidator validatorMock = Substitute.For<CreateIsolationTechniqueTypeCommandValidator>();
        ILogger<CreateIsolationTechniqueTypeHandler> loggerMock = Substitute.For<ILogger<CreateIsolationTechniqueTypeHandler>>();
        IIsolationTechniqueTypeWriteRepository repositoryMock = Substitute.For<IIsolationTechniqueTypeWriteRepository>();
        ICreateIsolationTechniqueTypeMapper mapperMock = Substitute.For<ICreateIsolationTechniqueTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateIsolationTechniqueTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateIsolationTechniqueTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        IsolationTechniqueType isolationtechniquetype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(isolationtechniquetype);

        // TODO: change error type
        repositoryMock
            .Create(isolationtechniquetype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<IsolationTechniqueType, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateIsolationTechniqueTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateIsolationTechniqueTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateIsolationTechniqueTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<IsolationTechniqueType>(), cancellationToken).Received(1);
        });
    }
}