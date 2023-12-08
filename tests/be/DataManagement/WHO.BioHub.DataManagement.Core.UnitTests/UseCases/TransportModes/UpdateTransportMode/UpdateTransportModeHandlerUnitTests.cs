using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.UpdateTransportMode;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.TransportModes.UpdateTransportMode;

public class UpdateTransportModeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateTransportModeCommandValidator validatorMock = Substitute.For<UpdateTransportModeCommandValidator>();
        ILogger<UpdateTransportModeHandler> loggerMock = Substitute.For<ILogger<UpdateTransportModeHandler>>();
        ITransportModeWriteRepository repositoryMock = Substitute.For<ITransportModeWriteRepository>();
        IUpdateTransportModeMapper mapperMock = Substitute.For<IUpdateTransportModeMapper>();
        CancellationToken cancellationToken = default;

        UpdateTransportModeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateTransportModeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TransportMode transportmode = new() { Id = Guid.NewGuid() };
        TransportMode transportmodeMapped = new() { Id = transportmode.Id };

        mapperMock.Map(transportmode, cmd).ReturnsForAnyArgs(transportmodeMapped);

        repositoryMock
            .Update(transportmode, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateTransportModeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TransportMode.Should()
            .NotBeNull(because: "TransportMode should NOT be null");
        response.Left.TransportMode.Should()
            .BeEquivalentTo(transportmodeMapped, because: "Returned transportmode must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateTransportModeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(transportmode, Arg.Any<UpdateTransportModeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<TransportMode>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateTransportModeCommandValidator validatorMock = Substitute.For<UpdateTransportModeCommandValidator>();
        ILogger<UpdateTransportModeHandler> loggerMock = Substitute.For<ILogger<UpdateTransportModeHandler>>();
        ITransportModeWriteRepository repositoryMock = Substitute.For<ITransportModeWriteRepository>();
        IUpdateTransportModeMapper mapperMock = Substitute.For<IUpdateTransportModeMapper>();
        CancellationToken cancellationToken = default;

        UpdateTransportModeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateTransportModeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateTransportModeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateTransportModeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<TransportMode>(), Arg.Any<UpdateTransportModeCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<TransportMode>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateTransportModeCommandValidator validatorMock = Substitute.For<UpdateTransportModeCommandValidator>();
        ILogger<UpdateTransportModeHandler> loggerMock = Substitute.For<ILogger<UpdateTransportModeHandler>>();
        ITransportModeWriteRepository repositoryMock = Substitute.For<ITransportModeWriteRepository>();
        IUpdateTransportModeMapper mapperMock = Substitute.For<IUpdateTransportModeMapper>();
        CancellationToken cancellationToken = default;

        UpdateTransportModeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateTransportModeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TransportMode transportmode = new();
        TransportMode transportmodeMapped = new();
        mapperMock.Map(transportmode, cmd).ReturnsForAnyArgs(transportmodeMapped);

        // TODO: change error type
        repositoryMock
            .Update(transportmode, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateTransportModeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateTransportModeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<TransportMode>(), Arg.Any<UpdateTransportModeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<TransportMode>(), cancellationToken).Received(1);
        });
    }
}