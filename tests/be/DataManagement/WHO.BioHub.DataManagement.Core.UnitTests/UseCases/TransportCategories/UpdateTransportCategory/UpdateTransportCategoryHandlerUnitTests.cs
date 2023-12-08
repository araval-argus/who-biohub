using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.UpdateTransportCategory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.TransportCategories.UpdateTransportCategory;

public class UpdateTransportCategoryHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateTransportCategoryCommandValidator validatorMock = Substitute.For<UpdateTransportCategoryCommandValidator>();
        ILogger<UpdateTransportCategoryHandler> loggerMock = Substitute.For<ILogger<UpdateTransportCategoryHandler>>();
        ITransportCategoryWriteRepository repositoryMock = Substitute.For<ITransportCategoryWriteRepository>();
        IUpdateTransportCategoryMapper mapperMock = Substitute.For<IUpdateTransportCategoryMapper>();
        CancellationToken cancellationToken = default;

        UpdateTransportCategoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateTransportCategoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TransportCategory transportcategory = new() { Id = Guid.NewGuid() };
        TransportCategory transportcategoryMapped = new() { Id = transportcategory.Id };

        mapperMock.Map(transportcategory, cmd).ReturnsForAnyArgs(transportcategoryMapped);

        repositoryMock
            .Update(transportcategory, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateTransportCategoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TransportCategory.Should()
            .NotBeNull(because: "TransportCategory should NOT be null");
        response.Left.TransportCategory.Should()
            .BeEquivalentTo(transportcategoryMapped, because: "Returned transportcategory must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateTransportCategoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(transportcategory, Arg.Any<UpdateTransportCategoryCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<TransportCategory>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateTransportCategoryCommandValidator validatorMock = Substitute.For<UpdateTransportCategoryCommandValidator>();
        ILogger<UpdateTransportCategoryHandler> loggerMock = Substitute.For<ILogger<UpdateTransportCategoryHandler>>();
        ITransportCategoryWriteRepository repositoryMock = Substitute.For<ITransportCategoryWriteRepository>();
        IUpdateTransportCategoryMapper mapperMock = Substitute.For<IUpdateTransportCategoryMapper>();
        CancellationToken cancellationToken = default;

        UpdateTransportCategoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateTransportCategoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateTransportCategoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateTransportCategoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<TransportCategory>(), Arg.Any<UpdateTransportCategoryCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<TransportCategory>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateTransportCategoryCommandValidator validatorMock = Substitute.For<UpdateTransportCategoryCommandValidator>();
        ILogger<UpdateTransportCategoryHandler> loggerMock = Substitute.For<ILogger<UpdateTransportCategoryHandler>>();
        ITransportCategoryWriteRepository repositoryMock = Substitute.For<ITransportCategoryWriteRepository>();
        IUpdateTransportCategoryMapper mapperMock = Substitute.For<IUpdateTransportCategoryMapper>();
        CancellationToken cancellationToken = default;

        UpdateTransportCategoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateTransportCategoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TransportCategory transportcategory = new();
        TransportCategory transportcategoryMapped = new();
        mapperMock.Map(transportcategory, cmd).ReturnsForAnyArgs(transportcategoryMapped);

        // TODO: change error type
        repositoryMock
            .Update(transportcategory, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateTransportCategoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateTransportCategoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<TransportCategory>(), Arg.Any<UpdateTransportCategoryCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<TransportCategory>(), cancellationToken).Received(1);
        });
    }
}