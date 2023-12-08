using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.CreateCultivabilityType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.CultivabilityTypes.CreateCultivabilityType;

public class CreateCultivabilityTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateCultivabilityTypeCommandValidator validatorMock = Substitute.For<CreateCultivabilityTypeCommandValidator>();
        ILogger<CreateCultivabilityTypeHandler> loggerMock = Substitute.For<ILogger<CreateCultivabilityTypeHandler>>();
        ICultivabilityTypeWriteRepository repositoryMock = Substitute.For<ICultivabilityTypeWriteRepository>();
        ICreateCultivabilityTypeMapper mapperMock = Substitute.For<ICreateCultivabilityTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateCultivabilityTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateCultivabilityTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        CultivabilityType cultivabilitytype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(cultivabilitytype);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(cultivabilitytype, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<CultivabilityType, Errors>>(() =>
                {
                    cultivabilitytype.Id = assignedId;
                    return new(cultivabilitytype);
                }));

        // Act
        Either<CreateCultivabilityTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.CultivabilityType.Should()
            .NotBeNull(because: "CultivabilityType should NOT be null");
        response.Left.CultivabilityType.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the CultivabilityType")
            .And.Be(assignedId, because: "Returned cultivabilitytype Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateCultivabilityTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateCultivabilityTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<CultivabilityType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateCultivabilityTypeCommandValidator validatorMock = Substitute.For<CreateCultivabilityTypeCommandValidator>();
        ILogger<CreateCultivabilityTypeHandler> loggerMock = Substitute.For<ILogger<CreateCultivabilityTypeHandler>>();
        ICultivabilityTypeWriteRepository repositoryMock = Substitute.For<ICultivabilityTypeWriteRepository>();
        ICreateCultivabilityTypeMapper mapperMock = Substitute.For<ICreateCultivabilityTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateCultivabilityTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateCultivabilityTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateCultivabilityTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateCultivabilityTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateCultivabilityTypeCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<CultivabilityType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateCultivabilityTypeCommandValidator validatorMock = Substitute.For<CreateCultivabilityTypeCommandValidator>();
        ILogger<CreateCultivabilityTypeHandler> loggerMock = Substitute.For<ILogger<CreateCultivabilityTypeHandler>>();
        ICultivabilityTypeWriteRepository repositoryMock = Substitute.For<ICultivabilityTypeWriteRepository>();
        ICreateCultivabilityTypeMapper mapperMock = Substitute.For<ICreateCultivabilityTypeMapper>();
        CancellationToken cancellationToken = default;

        CreateCultivabilityTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateCultivabilityTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        CultivabilityType cultivabilitytype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(cultivabilitytype);

        // TODO: change error type
        repositoryMock
            .Create(cultivabilitytype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<CultivabilityType, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateCultivabilityTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateCultivabilityTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateCultivabilityTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<CultivabilityType>(), cancellationToken).Received(1);
        });
    }
}