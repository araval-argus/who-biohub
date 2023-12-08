using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.UpdateCultivabilityType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.CultivabilityTypes.UpdateCultivabilityType;

public class UpdateCultivabilityTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateCultivabilityTypeCommandValidator validatorMock = Substitute.For<UpdateCultivabilityTypeCommandValidator>();
        ILogger<UpdateCultivabilityTypeHandler> loggerMock = Substitute.For<ILogger<UpdateCultivabilityTypeHandler>>();
        ICultivabilityTypeWriteRepository repositoryMock = Substitute.For<ICultivabilityTypeWriteRepository>();
        IUpdateCultivabilityTypeMapper mapperMock = Substitute.For<IUpdateCultivabilityTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateCultivabilityTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateCultivabilityTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        CultivabilityType cultivabilitytype = new() { Id = Guid.NewGuid() };
        CultivabilityType cultivabilitytypeMapped = new() { Id = cultivabilitytype.Id };

        mapperMock.Map(cultivabilitytype, cmd).ReturnsForAnyArgs(cultivabilitytypeMapped);

        repositoryMock
            .Update(cultivabilitytype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateCultivabilityTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.CultivabilityType.Should()
            .NotBeNull(because: "CultivabilityType should NOT be null");
        response.Left.CultivabilityType.Should()
            .BeEquivalentTo(cultivabilitytypeMapped, because: "Returned cultivabilitytype must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateCultivabilityTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(cultivabilitytype, Arg.Any<UpdateCultivabilityTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<CultivabilityType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateCultivabilityTypeCommandValidator validatorMock = Substitute.For<UpdateCultivabilityTypeCommandValidator>();
        ILogger<UpdateCultivabilityTypeHandler> loggerMock = Substitute.For<ILogger<UpdateCultivabilityTypeHandler>>();
        ICultivabilityTypeWriteRepository repositoryMock = Substitute.For<ICultivabilityTypeWriteRepository>();
        IUpdateCultivabilityTypeMapper mapperMock = Substitute.For<IUpdateCultivabilityTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateCultivabilityTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateCultivabilityTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateCultivabilityTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateCultivabilityTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CultivabilityType>(), Arg.Any<UpdateCultivabilityTypeCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<CultivabilityType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateCultivabilityTypeCommandValidator validatorMock = Substitute.For<UpdateCultivabilityTypeCommandValidator>();
        ILogger<UpdateCultivabilityTypeHandler> loggerMock = Substitute.For<ILogger<UpdateCultivabilityTypeHandler>>();
        ICultivabilityTypeWriteRepository repositoryMock = Substitute.For<ICultivabilityTypeWriteRepository>();
        IUpdateCultivabilityTypeMapper mapperMock = Substitute.For<IUpdateCultivabilityTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdateCultivabilityTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateCultivabilityTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        CultivabilityType cultivabilitytype = new();
        CultivabilityType cultivabilitytypeMapped = new();
        mapperMock.Map(cultivabilitytype, cmd).ReturnsForAnyArgs(cultivabilitytypeMapped);

        // TODO: change error type
        repositoryMock
            .Update(cultivabilitytype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateCultivabilityTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateCultivabilityTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CultivabilityType>(), Arg.Any<UpdateCultivabilityTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<CultivabilityType>(), cancellationToken).Received(1);
        });
    }
}