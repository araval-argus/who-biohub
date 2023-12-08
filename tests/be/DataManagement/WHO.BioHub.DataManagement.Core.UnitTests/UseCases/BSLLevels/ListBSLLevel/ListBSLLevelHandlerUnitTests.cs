using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ListBSLLevels;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BSLLevels.ListBSLLevels;

public class ListBSLLevelsHandlerUnitTests
{
    [Fact]
    public async Task If_no_bsllevels_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListBSLLevelsQueryValidator validatorMock = Substitute.For<ListBSLLevelsQueryValidator>();
        ILogger<ListBSLLevelsHandler> loggerMock = Substitute.For<ILogger<ListBSLLevelsHandler>>();
        IBSLLevelReadRepository repositoryMock = Substitute.For<IBSLLevelReadRepository>();
        CancellationToken cancellationToken = default;

        ListBSLLevelsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListBSLLevelsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<BSLLevel> bsllevels = Array.Empty<BSLLevel>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(bsllevels));

        // Act
        Either<ListBSLLevelsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BSLLevels.Should()
            .BeEquivalentTo(bsllevels, because: "Expected returned bsllevels to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListBSLLevelsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_bsllevel_exists_then_it_is_returned()
    {
        // Arrange
        ListBSLLevelsQueryValidator validatorMock = Substitute.For<ListBSLLevelsQueryValidator>();
        ILogger<ListBSLLevelsHandler> loggerMock = Substitute.For<ILogger<ListBSLLevelsHandler>>();
        IBSLLevelReadRepository repositoryMock = Substitute.For<IBSLLevelReadRepository>();
        CancellationToken cancellationToken = default;

        ListBSLLevelsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListBSLLevelsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<BSLLevel> bsllevels = new BSLLevel[1] { new() { Id = assignedId } };

        IEnumerable<BSLLevelDto> BSLLevelDtos = new BSLLevelDto[1] { new() { Id = assignedId } };

        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(bsllevels));

        // Act
        Either<ListBSLLevelsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BSLLevels.Should()
            .BeEquivalentTo(BSLLevelDtos, because: "Expected returned bsllevels to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListBSLLevelsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListBSLLevelsQueryValidator validatorMock = Substitute.For<ListBSLLevelsQueryValidator>();
        ILogger<ListBSLLevelsHandler> loggerMock = Substitute.For<ILogger<ListBSLLevelsHandler>>();
        IBSLLevelReadRepository repositoryMock = Substitute.For<IBSLLevelReadRepository>();
        CancellationToken cancellationToken = default;

        ListBSLLevelsHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListBSLLevelsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListBSLLevelsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListBSLLevelsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}