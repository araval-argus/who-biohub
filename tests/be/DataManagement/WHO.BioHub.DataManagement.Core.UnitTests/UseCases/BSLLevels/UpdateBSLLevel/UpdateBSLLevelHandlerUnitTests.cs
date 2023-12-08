using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.UpdateBSLLevel;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BSLLevels.UpdateBSLLevel;

public class UpdateBSLLevelHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateBSLLevelCommandValidator validatorMock = Substitute.For<UpdateBSLLevelCommandValidator>();
        ILogger<UpdateBSLLevelHandler> loggerMock = Substitute.For<ILogger<UpdateBSLLevelHandler>>();
        IBSLLevelWriteRepository repositoryMock = Substitute.For<IBSLLevelWriteRepository>();
        IUpdateBSLLevelMapper mapperMock = Substitute.For<IUpdateBSLLevelMapper>();
        CancellationToken cancellationToken = default;

        UpdateBSLLevelHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBSLLevelCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BSLLevel bsllevel = new() { Id = Guid.NewGuid() };
        BSLLevel bsllevelMapped = new() { Id = bsllevel.Id };

        mapperMock.Map(bsllevel, cmd).ReturnsForAnyArgs(bsllevelMapped);

        repositoryMock
            .Update(bsllevel, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateBSLLevelCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BSLLevel.Should()
            .NotBeNull(because: "BSLLevel should NOT be null");
        response.Left.BSLLevel.Should()
            .BeEquivalentTo(bsllevelMapped, because: "Returned bsllevel must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateBSLLevelCommand>(), cancellationToken).Received(1);
            mapperMock.Map(bsllevel, Arg.Any<UpdateBSLLevelCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<BSLLevel>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateBSLLevelCommandValidator validatorMock = Substitute.For<UpdateBSLLevelCommandValidator>();
        ILogger<UpdateBSLLevelHandler> loggerMock = Substitute.For<ILogger<UpdateBSLLevelHandler>>();
        IBSLLevelWriteRepository repositoryMock = Substitute.For<IBSLLevelWriteRepository>();
        IUpdateBSLLevelMapper mapperMock = Substitute.For<IUpdateBSLLevelMapper>();
        CancellationToken cancellationToken = default;

        UpdateBSLLevelHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBSLLevelCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateBSLLevelCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateBSLLevelCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<BSLLevel>(), Arg.Any<UpdateBSLLevelCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<BSLLevel>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateBSLLevelCommandValidator validatorMock = Substitute.For<UpdateBSLLevelCommandValidator>();
        ILogger<UpdateBSLLevelHandler> loggerMock = Substitute.For<ILogger<UpdateBSLLevelHandler>>();
        IBSLLevelWriteRepository repositoryMock = Substitute.For<IBSLLevelWriteRepository>();
        IUpdateBSLLevelMapper mapperMock = Substitute.For<IUpdateBSLLevelMapper>();
        CancellationToken cancellationToken = default;

        UpdateBSLLevelHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBSLLevelCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BSLLevel bsllevel = new();
        BSLLevel bsllevelMapped = new();
        mapperMock.Map(bsllevel, cmd).ReturnsForAnyArgs(bsllevelMapped);

        // TODO: change error type
        repositoryMock
            .Update(bsllevel, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateBSLLevelCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateBSLLevelCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<BSLLevel>(), Arg.Any<UpdateBSLLevelCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<BSLLevel>(), cancellationToken).Received(1);
        });
    }
}