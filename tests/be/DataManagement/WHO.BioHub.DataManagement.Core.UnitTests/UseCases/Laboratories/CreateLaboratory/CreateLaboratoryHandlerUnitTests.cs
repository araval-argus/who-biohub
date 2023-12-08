using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Laboratories.CreateLaboratory;

public class CreateLaboratoryHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateLaboratoryCommandValidator validatorMock = Substitute.For<CreateLaboratoryCommandValidator>();
        ILogger<CreateLaboratoryHandler> loggerMock = Substitute.For<ILogger<CreateLaboratoryHandler>>();
        ILaboratoryWriteRepository repositoryMock = Substitute.For<ILaboratoryWriteRepository>();
        ICreateLaboratoryMapper mapperMock = Substitute.For<ICreateLaboratoryMapper>();
        CancellationToken cancellationToken = default;

        CreateLaboratoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateLaboratoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Laboratory laboratory = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(laboratory);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(laboratory, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<Laboratory, Errors>>(() =>
                {
                    laboratory.Id = assignedId;
                    return new(laboratory);
                }));

        // Act
        Either<CreateLaboratoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.Should()
            .NotBeNull(because: "Laboratory should NOT be null");
        response.Left.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the Laboratory")
            .And.Be(assignedId, because: "Returned laboratory Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateLaboratoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateLaboratoryCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Laboratory>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateLaboratoryCommandValidator validatorMock = Substitute.For<CreateLaboratoryCommandValidator>();
        ILogger<CreateLaboratoryHandler> loggerMock = Substitute.For<ILogger<CreateLaboratoryHandler>>();
        ILaboratoryWriteRepository repositoryMock = Substitute.For<ILaboratoryWriteRepository>();
        ICreateLaboratoryMapper mapperMock = Substitute.For<ICreateLaboratoryMapper>();
        CancellationToken cancellationToken = default;

        CreateLaboratoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateLaboratoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateLaboratoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateLaboratoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateLaboratoryCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<Laboratory>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateLaboratoryCommandValidator validatorMock = Substitute.For<CreateLaboratoryCommandValidator>();
        ILogger<CreateLaboratoryHandler> loggerMock = Substitute.For<ILogger<CreateLaboratoryHandler>>();
        ILaboratoryWriteRepository repositoryMock = Substitute.For<ILaboratoryWriteRepository>();
        ICreateLaboratoryMapper mapperMock = Substitute.For<ICreateLaboratoryMapper>();
        CancellationToken cancellationToken = default;

        CreateLaboratoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateLaboratoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Laboratory laboratory = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(laboratory);

        // TODO: change error type
        repositoryMock
            .Create(laboratory, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<Laboratory, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateLaboratoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateLaboratoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateLaboratoryCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Laboratory>(), cancellationToken).Received(1);
        });
    }
}