using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.CreateTransportMode;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.TransportModes.CreateTransportMode;

public class CreateTransportModeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateTransportModeCommandValidator validatorMock = Substitute.For<CreateTransportModeCommandValidator>();
        ILogger<CreateTransportModeHandler> loggerMock = Substitute.For<ILogger<CreateTransportModeHandler>>();
        ITransportModeWriteRepository repositoryMock = Substitute.For<ITransportModeWriteRepository>();
        ICreateTransportModeMapper mapperMock = Substitute.For<ICreateTransportModeMapper>();
        CancellationToken cancellationToken = default;

        CreateTransportModeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateTransportModeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TransportMode transportmode = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(transportmode);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(transportmode, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<TransportMode, Errors>>(() =>
                {
                    transportmode.Id = assignedId;
                    return new(transportmode);
                }));

        // Act
        Either<CreateTransportModeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TransportMode.Should()
            .NotBeNull(because: "TransportMode should NOT be null");
        response.Left.TransportMode.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the TransportMode")
            .And.Be(assignedId, because: "Returned transportmode Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateTransportModeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateTransportModeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<TransportMode>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateTransportModeCommandValidator validatorMock = Substitute.For<CreateTransportModeCommandValidator>();
        ILogger<CreateTransportModeHandler> loggerMock = Substitute.For<ILogger<CreateTransportModeHandler>>();
        ITransportModeWriteRepository repositoryMock = Substitute.For<ITransportModeWriteRepository>();
        ICreateTransportModeMapper mapperMock = Substitute.For<ICreateTransportModeMapper>();
        CancellationToken cancellationToken = default;

        CreateTransportModeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateTransportModeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateTransportModeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateTransportModeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateTransportModeCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<TransportMode>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateTransportModeCommandValidator validatorMock = Substitute.For<CreateTransportModeCommandValidator>();
        ILogger<CreateTransportModeHandler> loggerMock = Substitute.For<ILogger<CreateTransportModeHandler>>();
        ITransportModeWriteRepository repositoryMock = Substitute.For<ITransportModeWriteRepository>();
        ICreateTransportModeMapper mapperMock = Substitute.For<ICreateTransportModeMapper>();
        CancellationToken cancellationToken = default;

        CreateTransportModeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateTransportModeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TransportMode transportmode = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(transportmode);

        // TODO: change error type
        repositoryMock
            .Create(transportmode, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<TransportMode, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateTransportModeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateTransportModeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateTransportModeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<TransportMode>(), cancellationToken).Received(1);
        });
    }
}