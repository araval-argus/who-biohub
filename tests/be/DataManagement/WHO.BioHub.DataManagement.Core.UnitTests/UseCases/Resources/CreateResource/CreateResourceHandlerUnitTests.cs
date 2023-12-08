using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateResource;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Resources.CreateResource;

public class CreateResourceHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateResourceCommandValidator validatorMock = Substitute.For<CreateResourceCommandValidator>();
        ILogger<CreateResourceHandler> loggerMock = Substitute.For<ILogger<CreateResourceHandler>>();
        IResourceWriteRepository repositoryMock = Substitute.For<IResourceWriteRepository>();
        ICreateResourceMapper mapperMock = Substitute.For<ICreateResourceMapper>();
        CancellationToken cancellationToken = default;

        CreateResourceHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateResourceCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Resource resource = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(resource);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(resource, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<Resource, Errors>>(() =>
                {
                    resource.Id = assignedId;
                    return new(resource);
                }));

        // Act
        Either<CreateResourceCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");      
           

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateResourceCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateResourceCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Resource>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateResourceCommandValidator validatorMock = Substitute.For<CreateResourceCommandValidator>();
        ILogger<CreateResourceHandler> loggerMock = Substitute.For<ILogger<CreateResourceHandler>>();
        IResourceWriteRepository repositoryMock = Substitute.For<IResourceWriteRepository>();
        ICreateResourceMapper mapperMock = Substitute.For<ICreateResourceMapper>();
        CancellationToken cancellationToken = default;

        CreateResourceHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateResourceCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateResourceCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateResourceCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateResourceCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<Resource>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateResourceCommandValidator validatorMock = Substitute.For<CreateResourceCommandValidator>();
        ILogger<CreateResourceHandler> loggerMock = Substitute.For<ILogger<CreateResourceHandler>>();
        IResourceWriteRepository repositoryMock = Substitute.For<IResourceWriteRepository>();
        ICreateResourceMapper mapperMock = Substitute.For<ICreateResourceMapper>();
        CancellationToken cancellationToken = default;

        CreateResourceHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateResourceCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Resource resource = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(resource);

        // TODO: change error type
        repositoryMock
            .Create(resource, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<Resource, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateResourceCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateResourceCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateResourceCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<Resource>(), cancellationToken).Received(1);
        });
    }
}