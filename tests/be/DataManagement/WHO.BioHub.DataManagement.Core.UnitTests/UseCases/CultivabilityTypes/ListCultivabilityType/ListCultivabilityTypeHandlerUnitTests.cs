using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.ListCultivabilityTypes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.CultivabilityTypes.ListCultivabilityTypes;

public class ListCultivabilityTypesHandlerUnitTests
{
    [Fact]
    public async Task If_no_cultivabilitytypes_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListCultivabilityTypesQueryValidator validatorMock = Substitute.For<ListCultivabilityTypesQueryValidator>();
        ILogger<ListCultivabilityTypesHandler> loggerMock = Substitute.For<ILogger<ListCultivabilityTypesHandler>>();
        ICultivabilityTypeReadRepository repositoryMock = Substitute.For<ICultivabilityTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListCultivabilityTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListCultivabilityTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<CultivabilityType> cultivabilitytypes = Array.Empty<CultivabilityType>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(cultivabilitytypes));

        // Act
        Either<ListCultivabilityTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.CultivabilityTypes.Should()
            .BeEquivalentTo(cultivabilitytypes, because: "Expected returned cultivabilitytypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListCultivabilityTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_cultivabilitytype_exists_then_it_is_returned()
    {
        // Arrange
        ListCultivabilityTypesQueryValidator validatorMock = Substitute.For<ListCultivabilityTypesQueryValidator>();
        ILogger<ListCultivabilityTypesHandler> loggerMock = Substitute.For<ILogger<ListCultivabilityTypesHandler>>();
        ICultivabilityTypeReadRepository repositoryMock = Substitute.For<ICultivabilityTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListCultivabilityTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListCultivabilityTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<CultivabilityType> cultivabilitytypes = new CultivabilityType[1] { new() { Id = assignedId } };
        IEnumerable<CultivabilityTypeDto> cultivabilitytypeDtos = new CultivabilityTypeDto[1] { new() { Id = assignedId } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(cultivabilitytypes));

        // Act
        Either<ListCultivabilityTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.CultivabilityTypes.Should()
            .BeEquivalentTo(cultivabilitytypeDtos, because: "Expected returned cultivabilitytypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListCultivabilityTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListCultivabilityTypesQueryValidator validatorMock = Substitute.For<ListCultivabilityTypesQueryValidator>();
        ILogger<ListCultivabilityTypesHandler> loggerMock = Substitute.For<ILogger<ListCultivabilityTypesHandler>>();
        ICultivabilityTypeReadRepository repositoryMock = Substitute.For<ICultivabilityTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListCultivabilityTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListCultivabilityTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListCultivabilityTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListCultivabilityTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}