using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.UpdatePriorityRequestType;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.PriorityRequestTypes.UpdatePriorityRequestType;

public class UpdatePriorityRequestTypeHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdatePriorityRequestTypeCommandValidator validatorMock = Substitute.For<UpdatePriorityRequestTypeCommandValidator>();
        ILogger<UpdatePriorityRequestTypeHandler> loggerMock = Substitute.For<ILogger<UpdatePriorityRequestTypeHandler>>();
        IPriorityRequestTypeWriteRepository repositoryMock = Substitute.For<IPriorityRequestTypeWriteRepository>();
        IUpdatePriorityRequestTypeMapper mapperMock = Substitute.For<IUpdatePriorityRequestTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdatePriorityRequestTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdatePriorityRequestTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        PriorityRequestType priorityrequesttype = new() { Id = Guid.NewGuid() };
        PriorityRequestType priorityrequesttypeMapped = new() { Id = priorityrequesttype.Id };

        mapperMock.Map(priorityrequesttype, cmd).ReturnsForAnyArgs(priorityrequesttypeMapped);

        repositoryMock
            .Update(priorityrequesttype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdatePriorityRequestTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.PriorityRequestType.Should()
            .NotBeNull(because: "PriorityRequestType should NOT be null");
        response.Left.PriorityRequestType.Should()
            .BeEquivalentTo(priorityrequesttypeMapped, because: "Returned priorityrequesttype must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdatePriorityRequestTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(priorityrequesttype, Arg.Any<UpdatePriorityRequestTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<PriorityRequestType>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdatePriorityRequestTypeCommandValidator validatorMock = Substitute.For<UpdatePriorityRequestTypeCommandValidator>();
        ILogger<UpdatePriorityRequestTypeHandler> loggerMock = Substitute.For<ILogger<UpdatePriorityRequestTypeHandler>>();
        IPriorityRequestTypeWriteRepository repositoryMock = Substitute.For<IPriorityRequestTypeWriteRepository>();
        IUpdatePriorityRequestTypeMapper mapperMock = Substitute.For<IUpdatePriorityRequestTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdatePriorityRequestTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdatePriorityRequestTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdatePriorityRequestTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdatePriorityRequestTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<PriorityRequestType>(), Arg.Any<UpdatePriorityRequestTypeCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<PriorityRequestType>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdatePriorityRequestTypeCommandValidator validatorMock = Substitute.For<UpdatePriorityRequestTypeCommandValidator>();
        ILogger<UpdatePriorityRequestTypeHandler> loggerMock = Substitute.For<ILogger<UpdatePriorityRequestTypeHandler>>();
        IPriorityRequestTypeWriteRepository repositoryMock = Substitute.For<IPriorityRequestTypeWriteRepository>();
        IUpdatePriorityRequestTypeMapper mapperMock = Substitute.For<IUpdatePriorityRequestTypeMapper>();
        CancellationToken cancellationToken = default;

        UpdatePriorityRequestTypeHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdatePriorityRequestTypeCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        PriorityRequestType priorityrequesttype = new();
        PriorityRequestType priorityrequesttypeMapped = new();
        mapperMock.Map(priorityrequesttype, cmd).ReturnsForAnyArgs(priorityrequesttypeMapped);

        // TODO: change error type
        repositoryMock
            .Update(priorityrequesttype, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdatePriorityRequestTypeCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdatePriorityRequestTypeCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<PriorityRequestType>(), Arg.Any<UpdatePriorityRequestTypeCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<PriorityRequestType>(), cancellationToken).Received(1);
        });
    }
}