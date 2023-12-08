using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ReadBSLLevel;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BSLLevels.ReadBSLLevel;

public class ReadBSLLevelHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadBSLLevelQueryValidator validatorMock = Substitute.For<ReadBSLLevelQueryValidator>();
        ILogger<ReadBSLLevelHandler> loggerMock = Substitute.For<ILogger<ReadBSLLevelHandler>>();
        IBSLLevelReadRepository repositoryMock = Substitute.For<IBSLLevelReadRepository>();
        CancellationToken cancellationToken = default;

        ReadBSLLevelHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ReadBSLLevelQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        BSLLevel bsllevel = new() { Id = id };

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Read(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(bsllevel));

        // Act
        Either<ReadBSLLevelQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BSLLevel.Id.Should()
            .Be(id, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadBSLLevelQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadBSLLevelQueryValidator validatorMock = Substitute.For<ReadBSLLevelQueryValidator>();
        ILogger<ReadBSLLevelHandler> loggerMock = Substitute.For<ILogger<ReadBSLLevelHandler>>();
        IBSLLevelReadRepository repositoryMock = Substitute.For<IBSLLevelReadRepository>();
        CancellationToken cancellationToken = default;

        ReadBSLLevelHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ReadBSLLevelQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadBSLLevelQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadBSLLevelQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}