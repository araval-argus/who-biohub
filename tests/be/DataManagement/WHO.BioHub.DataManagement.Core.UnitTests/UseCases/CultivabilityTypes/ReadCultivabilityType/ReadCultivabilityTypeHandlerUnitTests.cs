using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.ReadCultivabilityType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.CultivabilityTypes.ReadCultivabilityType;

public class ReadCultivabilityTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadCultivabilityTypeQueryValidator validatorMock = Substitute.For<ReadCultivabilityTypeQueryValidator>();
        ILogger<ReadCultivabilityTypeHandler> loggerMock = Substitute.For<ILogger<ReadCultivabilityTypeHandler>>();
        ICultivabilityTypeReadRepository repositoryMock = Substitute.For<ICultivabilityTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ReadCultivabilityTypeHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ReadCultivabilityTypeQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        CultivabilityType cultivabilitytype = new() { Id = id };

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Read(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(cultivabilitytype));

        // Act
        Either<ReadCultivabilityTypeQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.CultivabilityType.Id.Should()
            .Be(id, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadCultivabilityTypeQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadCultivabilityTypeQueryValidator validatorMock = Substitute.For<ReadCultivabilityTypeQueryValidator>();
        ILogger<ReadCultivabilityTypeHandler> loggerMock = Substitute.For<ILogger<ReadCultivabilityTypeHandler>>();
        ICultivabilityTypeReadRepository repositoryMock = Substitute.For<ICultivabilityTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ReadCultivabilityTypeHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ReadCultivabilityTypeQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadCultivabilityTypeQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadCultivabilityTypeQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}