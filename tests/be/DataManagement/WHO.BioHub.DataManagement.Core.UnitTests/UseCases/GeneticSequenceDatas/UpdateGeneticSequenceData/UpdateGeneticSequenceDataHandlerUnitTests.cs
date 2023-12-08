using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.UpdateGeneticSequenceData;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.GeneticSequenceDatas.UpdateGeneticSequenceData;

public class UpdateGeneticSequenceDataHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateGeneticSequenceDataCommandValidator validatorMock = Substitute.For<UpdateGeneticSequenceDataCommandValidator>();
        ILogger<UpdateGeneticSequenceDataHandler> loggerMock = Substitute.For<ILogger<UpdateGeneticSequenceDataHandler>>();
        IGeneticSequenceDataWriteRepository repositoryMock = Substitute.For<IGeneticSequenceDataWriteRepository>();
        IUpdateGeneticSequenceDataMapper mapperMock = Substitute.For<IUpdateGeneticSequenceDataMapper>();
        CancellationToken cancellationToken = default;

        UpdateGeneticSequenceDataHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateGeneticSequenceDataCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        GeneticSequenceData geneticsequencedata = new() { Id = Guid.NewGuid() };
        GeneticSequenceData geneticsequencedataMapped = new() { Id = geneticsequencedata.Id };

        mapperMock.Map(geneticsequencedata, cmd).ReturnsForAnyArgs(geneticsequencedataMapped);

        repositoryMock
            .Update(geneticsequencedata, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateGeneticSequenceDataCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.GeneticSequenceData.Should()
            .NotBeNull(because: "GeneticSequenceData should NOT be null");
        response.Left.GeneticSequenceData.Should()
            .BeEquivalentTo(geneticsequencedataMapped, because: "Returned geneticsequencedata must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateGeneticSequenceDataCommand>(), cancellationToken).Received(1);
            mapperMock.Map(geneticsequencedata, Arg.Any<UpdateGeneticSequenceDataCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<GeneticSequenceData>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateGeneticSequenceDataCommandValidator validatorMock = Substitute.For<UpdateGeneticSequenceDataCommandValidator>();
        ILogger<UpdateGeneticSequenceDataHandler> loggerMock = Substitute.For<ILogger<UpdateGeneticSequenceDataHandler>>();
        IGeneticSequenceDataWriteRepository repositoryMock = Substitute.For<IGeneticSequenceDataWriteRepository>();
        IUpdateGeneticSequenceDataMapper mapperMock = Substitute.For<IUpdateGeneticSequenceDataMapper>();
        CancellationToken cancellationToken = default;

        UpdateGeneticSequenceDataHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateGeneticSequenceDataCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateGeneticSequenceDataCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateGeneticSequenceDataCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<GeneticSequenceData>(), Arg.Any<UpdateGeneticSequenceDataCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<GeneticSequenceData>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateGeneticSequenceDataCommandValidator validatorMock = Substitute.For<UpdateGeneticSequenceDataCommandValidator>();
        ILogger<UpdateGeneticSequenceDataHandler> loggerMock = Substitute.For<ILogger<UpdateGeneticSequenceDataHandler>>();
        IGeneticSequenceDataWriteRepository repositoryMock = Substitute.For<IGeneticSequenceDataWriteRepository>();
        IUpdateGeneticSequenceDataMapper mapperMock = Substitute.For<IUpdateGeneticSequenceDataMapper>();
        CancellationToken cancellationToken = default;

        UpdateGeneticSequenceDataHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateGeneticSequenceDataCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        GeneticSequenceData geneticsequencedata = new();
        GeneticSequenceData geneticsequencedataMapped = new();
        mapperMock.Map(geneticsequencedata, cmd).ReturnsForAnyArgs(geneticsequencedataMapped);

        // TODO: change error type
        repositoryMock
            .Update(geneticsequencedata, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateGeneticSequenceDataCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateGeneticSequenceDataCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<GeneticSequenceData>(), Arg.Any<UpdateGeneticSequenceDataCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<GeneticSequenceData>(), cancellationToken).Received(1);
        });
    }
}