using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;

public class ListGeneticSequenceDatasHandlerUnitTests
{
    [Fact]
    public async Task If_no_geneticsequencedatas_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListGeneticSequenceDatasQueryValidator validatorMock = Substitute.For<ListGeneticSequenceDatasQueryValidator>();
        ILogger<ListGeneticSequenceDatasHandler> loggerMock = Substitute.For<ILogger<ListGeneticSequenceDatasHandler>>();
        IGeneticSequenceDataReadRepository repositoryMock = Substitute.For<IGeneticSequenceDataReadRepository>();
        CancellationToken cancellationToken = default;

        ListGeneticSequenceDatasHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListGeneticSequenceDatasQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<GeneticSequenceData> geneticsequencedatas = Array.Empty<GeneticSequenceData>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(geneticsequencedatas));

        // Act
        Either<ListGeneticSequenceDatasQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.GeneticSequenceDatas.Should()
            .BeEquivalentTo(geneticsequencedatas, because: "Expected returned geneticsequencedatas to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListGeneticSequenceDatasQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_geneticsequencedata_exists_then_it_is_returned()
    {
        // Arrange
        ListGeneticSequenceDatasQueryValidator validatorMock = Substitute.For<ListGeneticSequenceDatasQueryValidator>();
        ILogger<ListGeneticSequenceDatasHandler> loggerMock = Substitute.For<ILogger<ListGeneticSequenceDatasHandler>>();
        IGeneticSequenceDataReadRepository repositoryMock = Substitute.For<IGeneticSequenceDataReadRepository>();
        CancellationToken cancellationToken = default;

        ListGeneticSequenceDatasHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListGeneticSequenceDatasQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<GeneticSequenceData> geneticsequencedatas = new GeneticSequenceData[1] { new() { Id = assignedId } };

        IEnumerable<GeneticSequenceDataDto> geneticsequencedataDtos = new GeneticSequenceDataDto[1] { new() { Id = assignedId } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(geneticsequencedatas));

        // Act
        Either<ListGeneticSequenceDatasQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.GeneticSequenceDatas.Should()
            .BeEquivalentTo(geneticsequencedataDtos, because: "Expected returned geneticsequencedatas to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListGeneticSequenceDatasQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListGeneticSequenceDatasQueryValidator validatorMock = Substitute.For<ListGeneticSequenceDatasQueryValidator>();
        ILogger<ListGeneticSequenceDatasHandler> loggerMock = Substitute.For<ILogger<ListGeneticSequenceDatasHandler>>();
        IGeneticSequenceDataReadRepository repositoryMock = Substitute.For<IGeneticSequenceDataReadRepository>();
        CancellationToken cancellationToken = default;

        ListGeneticSequenceDatasHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListGeneticSequenceDatasQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListGeneticSequenceDatasQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListGeneticSequenceDatasQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}