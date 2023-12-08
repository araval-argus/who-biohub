using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SpecimenTypes;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.ListSpecimenTypes;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.SpecimenTypes.ListSpecimenTypes;

public class ListSpecimenTypesHandlerUnitTests
{
    [Fact]
    public async Task If_no_specimenttypes_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListSpecimenTypesQueryValidator validatorMock = Substitute.For<ListSpecimenTypesQueryValidator>();
        ILogger<ListSpecimenTypesHandler> loggerMock = Substitute.For<ILogger<ListSpecimenTypesHandler>>();
        ISpecimenTypeReadRepository repositoryMock = Substitute.For<ISpecimenTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListSpecimenTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListSpecimenTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<SpecimenType> specimenttypes = Array.Empty<SpecimenType>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(specimenttypes));

        // Act
        Either<ListSpecimenTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.SpecimenTypes.Should()
            .BeEquivalentTo(specimenttypes, because: "Expected returned specimenttypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListSpecimenTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_specimenttype_exists_then_it_is_returned()
    {
        // Arrange
        ListSpecimenTypesQueryValidator validatorMock = Substitute.For<ListSpecimenTypesQueryValidator>();
        ILogger<ListSpecimenTypesHandler> loggerMock = Substitute.For<ILogger<ListSpecimenTypesHandler>>();
        ISpecimenTypeReadRepository repositoryMock = Substitute.For<ISpecimenTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListSpecimenTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListSpecimenTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<SpecimenType> specimenttypes = new SpecimenType[1] { new() { Id = assignedId } };

        IEnumerable<SpecimenTypeDto> specimenttypeDtos = new SpecimenTypeDto[1] { new() { Id = assignedId } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(specimenttypes));

        // Act
        Either<ListSpecimenTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.SpecimenTypes.Should()
            .BeEquivalentTo(specimenttypeDtos, because: "Expected returned specimenttypes to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListSpecimenTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListSpecimenTypesQueryValidator validatorMock = Substitute.For<ListSpecimenTypesQueryValidator>();
        ILogger<ListSpecimenTypesHandler> loggerMock = Substitute.For<ILogger<ListSpecimenTypesHandler>>();
        ISpecimenTypeReadRepository repositoryMock = Substitute.For<ISpecimenTypeReadRepository>();
        CancellationToken cancellationToken = default;

        ListSpecimenTypesHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListSpecimenTypesQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListSpecimenTypesQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListSpecimenTypesQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}