using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.CreateBookingFormHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BookingFormsHistory.CreateBookingFormHistory;

public class CreateBookingFormHistoryHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateBookingFormHistoryCommandValidator validatorMock = Substitute.For<CreateBookingFormHistoryCommandValidator>();
        ILogger<CreateBookingFormHistoryHandler> loggerMock = Substitute.For<ILogger<CreateBookingFormHistoryHandler>>();
        IBookingFormHistoryWriteRepository repositoryMock = Substitute.For<IBookingFormHistoryWriteRepository>();
        ICreateBookingFormHistoryMapper mapperMock = Substitute.For<ICreateBookingFormHistoryMapper>();
        CancellationToken cancellationToken = default;

        CreateBookingFormHistoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBookingFormHistoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BookingFormHistory bookingformhistory = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(bookingformhistory);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(bookingformhistory, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<BookingFormHistory, Errors>>(() =>
                {
                    bookingformhistory.Id = assignedId;
                    return new(bookingformhistory);
                }));

        // Act
        Either<CreateBookingFormHistoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BookingFormHistory.Should()
            .NotBeNull(because: "BookingFormHistory should NOT be null");
        response.Left.BookingFormHistory.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the BookingFormHistory")
            .And.Be(assignedId, because: "Returned bookingformhistory Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateBookingFormHistoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBookingFormHistoryCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<BookingFormHistory>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateBookingFormHistoryCommandValidator validatorMock = Substitute.For<CreateBookingFormHistoryCommandValidator>();
        ILogger<CreateBookingFormHistoryHandler> loggerMock = Substitute.For<ILogger<CreateBookingFormHistoryHandler>>();
        IBookingFormHistoryWriteRepository repositoryMock = Substitute.For<IBookingFormHistoryWriteRepository>();
        ICreateBookingFormHistoryMapper mapperMock = Substitute.For<ICreateBookingFormHistoryMapper>();
        CancellationToken cancellationToken = default;

        CreateBookingFormHistoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBookingFormHistoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateBookingFormHistoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateBookingFormHistoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBookingFormHistoryCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<BookingFormHistory>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateBookingFormHistoryCommandValidator validatorMock = Substitute.For<CreateBookingFormHistoryCommandValidator>();
        ILogger<CreateBookingFormHistoryHandler> loggerMock = Substitute.For<ILogger<CreateBookingFormHistoryHandler>>();
        IBookingFormHistoryWriteRepository repositoryMock = Substitute.For<IBookingFormHistoryWriteRepository>();
        ICreateBookingFormHistoryMapper mapperMock = Substitute.For<ICreateBookingFormHistoryMapper>();
        CancellationToken cancellationToken = default;

        CreateBookingFormHistoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBookingFormHistoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BookingFormHistory bookingformhistory = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(bookingformhistory);

        // TODO: change error type
        repositoryMock
            .Create(bookingformhistory, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<BookingFormHistory, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateBookingFormHistoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateBookingFormHistoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBookingFormHistoryCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<BookingFormHistory>(), cancellationToken).Received(1);
        });
    }
}