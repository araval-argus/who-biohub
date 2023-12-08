using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.CreateBSLLevel;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BSLLevels.CreateBSLLevel;

public class CreateBSLLevelHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateBSLLevelCommandValidator validatorMock = Substitute.For<CreateBSLLevelCommandValidator>();
        ILogger<CreateBSLLevelHandler> loggerMock = Substitute.For<ILogger<CreateBSLLevelHandler>>();
        IBSLLevelWriteRepository repositoryMock = Substitute.For<IBSLLevelWriteRepository>();
        ICreateBSLLevelMapper mapperMock = Substitute.For<ICreateBSLLevelMapper>();
        CancellationToken cancellationToken = default;

        CreateBSLLevelHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBSLLevelCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BSLLevel bsllevel = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(bsllevel);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(bsllevel, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<BSLLevel, Errors>>(() =>
                {
                    bsllevel.Id = assignedId;
                    return new(bsllevel);
                }));

        // Act
        Either<CreateBSLLevelCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BSLLevel.Should()
            .NotBeNull(because: "BSLLevel should NOT be null");
        response.Left.BSLLevel.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the BSLLevel")
            .And.Be(assignedId, because: "Returned bsllevel Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateBSLLevelCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBSLLevelCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<BSLLevel>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateBSLLevelCommandValidator validatorMock = Substitute.For<CreateBSLLevelCommandValidator>();
        ILogger<CreateBSLLevelHandler> loggerMock = Substitute.For<ILogger<CreateBSLLevelHandler>>();
        IBSLLevelWriteRepository repositoryMock = Substitute.For<IBSLLevelWriteRepository>();
        ICreateBSLLevelMapper mapperMock = Substitute.For<ICreateBSLLevelMapper>();
        CancellationToken cancellationToken = default;

        CreateBSLLevelHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBSLLevelCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateBSLLevelCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateBSLLevelCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBSLLevelCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<BSLLevel>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateBSLLevelCommandValidator validatorMock = Substitute.For<CreateBSLLevelCommandValidator>();
        ILogger<CreateBSLLevelHandler> loggerMock = Substitute.For<ILogger<CreateBSLLevelHandler>>();
        IBSLLevelWriteRepository repositoryMock = Substitute.For<IBSLLevelWriteRepository>();
        ICreateBSLLevelMapper mapperMock = Substitute.For<ICreateBSLLevelMapper>();
        CancellationToken cancellationToken = default;

        CreateBSLLevelHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBSLLevelCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BSLLevel bsllevel = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(bsllevel);

        // TODO: change error type
        repositoryMock
            .Create(bsllevel, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<BSLLevel, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateBSLLevelCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateBSLLevelCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBSLLevelCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<BSLLevel>(), cancellationToken).Received(1);
        });
    }
}