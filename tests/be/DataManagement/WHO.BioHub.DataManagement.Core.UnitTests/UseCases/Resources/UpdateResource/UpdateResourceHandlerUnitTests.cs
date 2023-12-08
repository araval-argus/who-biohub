using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.UpdateResource;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.Resources.UpdateResource;

public class UpdateResourceHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateResourceCommandValidator validatorMock = Substitute.For<UpdateResourceCommandValidator>();
        ILogger<UpdateResourceHandler> loggerMock = Substitute.For<ILogger<UpdateResourceHandler>>();
        IResourceWriteRepository repositoryMock = Substitute.For<IResourceWriteRepository>();
        IUpdateResourceMapper mapperMock = Substitute.For<IUpdateResourceMapper>();
        CancellationToken cancellationToken = default;

        UpdateResourceHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateResourceCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Resource resource = new() { Id = Guid.NewGuid() };
        Resource resourceMapped = new() { Id = resource.Id };        

        repositoryMock
           .ReadForUpdate(cmd.Id, cancellationToken)
           .ReturnsForAnyArgs(
               Task.FromResult(resource));

        mapperMock.Map(resource, cmd).ReturnsForAnyArgs(resourceMapped);

        repositoryMock
            .Update(resource, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateResourceCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        
        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateResourceCommand>(), cancellationToken).Received(1);
            mapperMock.Map(resource, Arg.Any<UpdateResourceCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Resource>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateResourceCommandValidator validatorMock = Substitute.For<UpdateResourceCommandValidator>();
        ILogger<UpdateResourceHandler> loggerMock = Substitute.For<ILogger<UpdateResourceHandler>>();
        IResourceWriteRepository repositoryMock = Substitute.For<IResourceWriteRepository>();
        IUpdateResourceMapper mapperMock = Substitute.For<IUpdateResourceMapper>();
        CancellationToken cancellationToken = default;

        UpdateResourceHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateResourceCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateResourceCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            .Be(ErrorType.RequestParsing, because: "Validation Errors are expected in this scenario");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateResourceCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Resource>(), Arg.Any<UpdateResourceCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<Resource>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateResourceCommandValidator validatorMock = Substitute.For<UpdateResourceCommandValidator>();
        ILogger<UpdateResourceHandler> loggerMock = Substitute.For<ILogger<UpdateResourceHandler>>();
        IResourceWriteRepository repositoryMock = Substitute.For<IResourceWriteRepository>();
        IUpdateResourceMapper mapperMock = Substitute.For<IUpdateResourceMapper>();
        CancellationToken cancellationToken = default;

        UpdateResourceHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateResourceCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Resource resource = new();
        Resource resourceMapped = new();
        mapperMock.Map(resource, cmd).ReturnsForAnyArgs(resourceMapped);

        repositoryMock
           .ReadForUpdate(cmd.Id, cancellationToken)
           .ReturnsForAnyArgs(
               Task.FromResult(resource));

        // TODO: change error type
        repositoryMock
            .Update(resource, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateResourceCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateResourceCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<Resource>(), Arg.Any<UpdateResourceCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<Resource>(), cancellationToken).Received(1);
        });
    }
}