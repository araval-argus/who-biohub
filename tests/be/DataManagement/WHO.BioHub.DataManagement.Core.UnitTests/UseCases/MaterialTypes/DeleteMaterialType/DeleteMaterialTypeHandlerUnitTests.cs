using FluentValidation.Results;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.DeleteMaterialType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.MaterialTypes.DeleteMaterialType;

public class DeleteMaterialTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_deleted_then_a_valid_response_is_returned()
    {
        // Arrange
        DeleteMaterialTypeCommandValidator validatorMock = Substitute.For<DeleteMaterialTypeCommandValidator>();
        ILogger<DeleteMaterialTypeHandler> loggerMock = Substitute.For<ILogger<DeleteMaterialTypeHandler>>();
        IMaterialTypeWriteRepository repositoryMock = Substitute.For<IMaterialTypeWriteRepository>();
        CancellationToken cancellationToken = default;

        DeleteMaterialTypeHandler handler = new(loggerMock, validatorMock, repositoryMock);
        DeleteMaterialTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Delete(id, cancellationToken)
            .ReturnsForAnyArgs(
                Task.FromResult<Errors?>(null));

        // Act
        Either<DeleteMaterialTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<DeleteMaterialTypeCommand>(), cancellationToken).Received(1);
            await repositoryMock.Delete(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        DeleteMaterialTypeCommandValidator validatorMock = Substitute.For<DeleteMaterialTypeCommandValidator>();
        ILogger<DeleteMaterialTypeHandler> loggerMock = Substitute.For<ILogger<DeleteMaterialTypeHandler>>();
        IMaterialTypeWriteRepository repositoryMock = Substitute.For<IMaterialTypeWriteRepository>();
        CancellationToken cancellationToken = default;

        DeleteMaterialTypeHandler handler = new(loggerMock, validatorMock, repositoryMock);
        DeleteMaterialTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<DeleteMaterialTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<DeleteMaterialTypeCommand>(), cancellationToken).Received(1);
            await repositoryMock.Delete(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        DeleteMaterialTypeCommandValidator validatorMock = Substitute.For<DeleteMaterialTypeCommandValidator>();
        ILogger<DeleteMaterialTypeHandler> loggerMock = Substitute.For<ILogger<DeleteMaterialTypeHandler>>();
        IMaterialTypeWriteRepository repositoryMock = Substitute.For<IMaterialTypeWriteRepository>();
        CancellationToken cancellationToken = default;

        DeleteMaterialTypeHandler handler = new(loggerMock, validatorMock, repositoryMock);

        DeleteMaterialTypeCommand cmd = new();
        Guid id = Guid.NewGuid();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        // TODO: change error type
        repositoryMock
            .Delete(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<DeleteMaterialTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<DeleteMaterialTypeCommand>(), cancellationToken).Received(1);
            await repositoryMock.Delete(id, cancellationToken).Received(1);
        });
    }
}