using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.ListTransportModes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.TransportModes.ListTransportModes;

public class ListTransportModesHandlerUnitTests
{
    [Fact]
    public async Task If_no_transportmodes_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListTransportModesQueryValidator validatorMock = Substitute.For<ListTransportModesQueryValidator>();
        ILogger<ListTransportModesHandler> loggerMock = Substitute.For<ILogger<ListTransportModesHandler>>();
        ITransportModeReadRepository repositoryMock = Substitute.For<ITransportModeReadRepository>();
        CancellationToken cancellationToken = default;

        ListTransportModesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListTransportModesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<TransportMode> transportmodes = Array.Empty<TransportMode>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(transportmodes));

        // Act
        Either<ListTransportModesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TransportModes.Should()
            .BeEquivalentTo(transportmodes, because: "Expected returned transportmodes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListTransportModesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_transportmode_exists_then_it_is_returned()
    {
        // Arrange
        ListTransportModesQueryValidator validatorMock = Substitute.For<ListTransportModesQueryValidator>();
        ILogger<ListTransportModesHandler> loggerMock = Substitute.For<ILogger<ListTransportModesHandler>>();
        ITransportModeReadRepository repositoryMock = Substitute.For<ITransportModeReadRepository>();
        CancellationToken cancellationToken = default;

        ListTransportModesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListTransportModesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<TransportMode> transportmodes = new TransportMode[1] { new() { Id = assignedId } };

        IEnumerable<TransportModeDto> transportmodeDtos = new TransportModeDto[1] { new() { Id = assignedId } };


        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(transportmodes));

        // Act
        Either<ListTransportModesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TransportModes.Should()
            .BeEquivalentTo(transportmodeDtos, because: "Expected returned transportmodes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListTransportModesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListTransportModesQueryValidator validatorMock = Substitute.For<ListTransportModesQueryValidator>();
        ILogger<ListTransportModesHandler> loggerMock = Substitute.For<ILogger<ListTransportModesHandler>>();
        ITransportModeReadRepository repositoryMock = Substitute.For<ITransportModeReadRepository>();
        CancellationToken cancellationToken = default;

        ListTransportModesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListTransportModesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListTransportModesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListTransportModesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}