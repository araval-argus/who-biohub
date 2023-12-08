using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.CreatePriorityRequestType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.PriorityRequestTypes.CreatePriorityRequestType;

public class CreatePriorityRequestTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreatePriorityRequestTypeCommandValidator validatorMock = Substitute.For<CreatePriorityRequestTypeCommandValidator>();
        ILogger<CreatePriorityRequestTypeHandler> loggerMock = Substitute.For<ILogger<CreatePriorityRequestTypeHandler>>();
        IPriorityRequestTypeWriteRepository repositoryMock = Substitute.For<IPriorityRequestTypeWriteRepository>();
        ICreatePriorityRequestTypeMapper mapperMock = Substitute.For<ICreatePriorityRequestTypeMapper>();
        CancellationToken cancellationToken = default;

        CreatePriorityRequestTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreatePriorityRequestTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        PriorityRequestType priorityrequesttype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(priorityrequesttype);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(priorityrequesttype, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<PriorityRequestType, Errors>>(() =>
                {
                    priorityrequesttype.Id = assignedId;
                    return new(priorityrequesttype);
                }));

        // Act
        Either<CreatePriorityRequestTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.PriorityRequestType.Should()
            .NotBeNull(because: "PriorityRequestType should NOT be null");
        response.Left.PriorityRequestType.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the PriorityRequestType")
            .And.Be(assignedId, because: "Returned priorityrequesttype Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreatePriorityRequestTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreatePriorityRequestTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<PriorityRequestType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreatePriorityRequestTypeCommandValidator validatorMock = Substitute.For<CreatePriorityRequestTypeCommandValidator>();
        ILogger<CreatePriorityRequestTypeHandler> loggerMock = Substitute.For<ILogger<CreatePriorityRequestTypeHandler>>();
        IPriorityRequestTypeWriteRepository repositoryMock = Substitute.For<IPriorityRequestTypeWriteRepository>();
        ICreatePriorityRequestTypeMapper mapperMock = Substitute.For<ICreatePriorityRequestTypeMapper>();
        CancellationToken cancellationToken = default;

        CreatePriorityRequestTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreatePriorityRequestTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreatePriorityRequestTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreatePriorityRequestTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreatePriorityRequestTypeCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<PriorityRequestType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreatePriorityRequestTypeCommandValidator validatorMock = Substitute.For<CreatePriorityRequestTypeCommandValidator>();
        ILogger<CreatePriorityRequestTypeHandler> loggerMock = Substitute.For<ILogger<CreatePriorityRequestTypeHandler>>();
        IPriorityRequestTypeWriteRepository repositoryMock = Substitute.For<IPriorityRequestTypeWriteRepository>();
        ICreatePriorityRequestTypeMapper mapperMock = Substitute.For<ICreatePriorityRequestTypeMapper>();
        CancellationToken cancellationToken = default;

        CreatePriorityRequestTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreatePriorityRequestTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        PriorityRequestType priorityrequesttype = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(priorityrequesttype);

        // TODO: change error type
        repositoryMock
            .Create(priorityrequesttype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<PriorityRequestType, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreatePriorityRequestTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreatePriorityRequestTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreatePriorityRequestTypeCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<PriorityRequestType>(), cancellationToken).Received(1);
        });
    }
}