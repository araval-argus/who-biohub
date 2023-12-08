using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.CreateTransportCategory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.TransportCategories.CreateTransportCategory;

public class CreateTransportCategoryHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateTransportCategoryCommandValidator validatorMock = Substitute.For<CreateTransportCategoryCommandValidator>();
        ILogger<CreateTransportCategoryHandler> loggerMock = Substitute.For<ILogger<CreateTransportCategoryHandler>>();
        ITransportCategoryWriteRepository repositoryMock = Substitute.For<ITransportCategoryWriteRepository>();
        ICreateTransportCategoryMapper mapperMock = Substitute.For<ICreateTransportCategoryMapper>();
        CancellationToken cancellationToken = default;

        CreateTransportCategoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateTransportCategoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TransportCategory transportcategory = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(transportcategory);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(transportcategory, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<TransportCategory, Errors>>(() =>
                {
                    transportcategory.Id = assignedId;
                    return new(transportcategory);
                }));

        // Act
        Either<CreateTransportCategoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.TransportCategory.Should()
            .NotBeNull(because: "TransportCategory should NOT be null");
        response.Left.TransportCategory.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the TransportCategory")
            .And.Be(assignedId, because: "Returned transportcategory Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateTransportCategoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateTransportCategoryCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<TransportCategory>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateTransportCategoryCommandValidator validatorMock = Substitute.For<CreateTransportCategoryCommandValidator>();
        ILogger<CreateTransportCategoryHandler> loggerMock = Substitute.For<ILogger<CreateTransportCategoryHandler>>();
        ITransportCategoryWriteRepository repositoryMock = Substitute.For<ITransportCategoryWriteRepository>();
        ICreateTransportCategoryMapper mapperMock = Substitute.For<ICreateTransportCategoryMapper>();
        CancellationToken cancellationToken = default;

        CreateTransportCategoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateTransportCategoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateTransportCategoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateTransportCategoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateTransportCategoryCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<TransportCategory>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateTransportCategoryCommandValidator validatorMock = Substitute.For<CreateTransportCategoryCommandValidator>();
        ILogger<CreateTransportCategoryHandler> loggerMock = Substitute.For<ILogger<CreateTransportCategoryHandler>>();
        ITransportCategoryWriteRepository repositoryMock = Substitute.For<ITransportCategoryWriteRepository>();
        ICreateTransportCategoryMapper mapperMock = Substitute.For<ICreateTransportCategoryMapper>();
        CancellationToken cancellationToken = default;

        CreateTransportCategoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateTransportCategoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        TransportCategory transportcategory = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(transportcategory);

        // TODO: change error type
        repositoryMock
            .Create(transportcategory, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<TransportCategory, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateTransportCategoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateTransportCategoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateTransportCategoryCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<TransportCategory>(), cancellationToken).Received(1);
        });
    }
}