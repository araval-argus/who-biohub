using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.CreateGeneticSequenceData;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.GeneticSequenceDatas.CreateGeneticSequenceData;

public class CreateGeneticSequenceDataHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateGeneticSequenceDataCommandValidator validatorMock = Substitute.For<CreateGeneticSequenceDataCommandValidator>();
        ILogger<CreateGeneticSequenceDataHandler> loggerMock = Substitute.For<ILogger<CreateGeneticSequenceDataHandler>>();
        IGeneticSequenceDataWriteRepository repositoryMock = Substitute.For<IGeneticSequenceDataWriteRepository>();
        ICreateGeneticSequenceDataMapper mapperMock = Substitute.For<ICreateGeneticSequenceDataMapper>();
        CancellationToken cancellationToken = default;

        CreateGeneticSequenceDataHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateGeneticSequenceDataCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        GeneticSequenceData geneticsequencedata = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(geneticsequencedata);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(geneticsequencedata, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<GeneticSequenceData, Errors>>(() =>
                {
                    geneticsequencedata.Id = assignedId;
                    return new(geneticsequencedata);
                }));

        // Act
        Either<CreateGeneticSequenceDataCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.GeneticSequenceData.Should()
            .NotBeNull(because: "GeneticSequenceData should NOT be null");
        response.Left.GeneticSequenceData.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the GeneticSequenceData")
            .And.Be(assignedId, because: "Returned geneticsequencedata Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateGeneticSequenceDataCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateGeneticSequenceDataCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<GeneticSequenceData>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateGeneticSequenceDataCommandValidator validatorMock = Substitute.For<CreateGeneticSequenceDataCommandValidator>();
        ILogger<CreateGeneticSequenceDataHandler> loggerMock = Substitute.For<ILogger<CreateGeneticSequenceDataHandler>>();
        IGeneticSequenceDataWriteRepository repositoryMock = Substitute.For<IGeneticSequenceDataWriteRepository>();
        ICreateGeneticSequenceDataMapper mapperMock = Substitute.For<ICreateGeneticSequenceDataMapper>();
        CancellationToken cancellationToken = default;

        CreateGeneticSequenceDataHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateGeneticSequenceDataCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateGeneticSequenceDataCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateGeneticSequenceDataCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateGeneticSequenceDataCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<GeneticSequenceData>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateGeneticSequenceDataCommandValidator validatorMock = Substitute.For<CreateGeneticSequenceDataCommandValidator>();
        ILogger<CreateGeneticSequenceDataHandler> loggerMock = Substitute.For<ILogger<CreateGeneticSequenceDataHandler>>();
        IGeneticSequenceDataWriteRepository repositoryMock = Substitute.For<IGeneticSequenceDataWriteRepository>();
        ICreateGeneticSequenceDataMapper mapperMock = Substitute.For<ICreateGeneticSequenceDataMapper>();
        CancellationToken cancellationToken = default;

        CreateGeneticSequenceDataHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateGeneticSequenceDataCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        GeneticSequenceData geneticsequencedata = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(geneticsequencedata);

        // TODO: change error type
        repositoryMock
            .Create(geneticsequencedata, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<GeneticSequenceData, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateGeneticSequenceDataCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateGeneticSequenceDataCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateGeneticSequenceDataCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<GeneticSequenceData>(), cancellationToken).Received(1);
        });
    }
}