using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.UpdateBookingFormHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BookingFormsHistory.UpdateBookingFormHistory;

public class UpdateBookingFormHistoryHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateBookingFormHistoryCommandValidator validatorMock = Substitute.For<UpdateBookingFormHistoryCommandValidator>();
        ILogger<UpdateBookingFormHistoryHandler> loggerMock = Substitute.For<ILogger<UpdateBookingFormHistoryHandler>>();
        IBookingFormHistoryWriteRepository repositoryMock = Substitute.For<IBookingFormHistoryWriteRepository>();
        IUpdateBookingFormHistoryMapper mapperMock = Substitute.For<IUpdateBookingFormHistoryMapper>();
        CancellationToken cancellationToken = default;

        UpdateBookingFormHistoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBookingFormHistoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BookingFormHistory bookingformhistory = new() { Id = Guid.NewGuid() };
        BookingFormHistory bookingformhistoryMapped = new() { Id = bookingformhistory.Id };

        mapperMock.Map(bookingformhistory, cmd).ReturnsForAnyArgs(bookingformhistoryMapped);

        repositoryMock
            .Update(bookingformhistory, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateBookingFormHistoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BookingFormHistory.Should()
            .NotBeNull(because: "BookingFormHistory should NOT be null");
        response.Left.BookingFormHistory.Should()
            .BeEquivalentTo(bookingformhistoryMapped, because: "Returned bookingformhistory must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateBookingFormHistoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(bookingformhistory, Arg.Any<UpdateBookingFormHistoryCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<BookingFormHistory>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateBookingFormHistoryCommandValidator validatorMock = Substitute.For<UpdateBookingFormHistoryCommandValidator>();
        ILogger<UpdateBookingFormHistoryHandler> loggerMock = Substitute.For<ILogger<UpdateBookingFormHistoryHandler>>();
        IBookingFormHistoryWriteRepository repositoryMock = Substitute.For<IBookingFormHistoryWriteRepository>();
        IUpdateBookingFormHistoryMapper mapperMock = Substitute.For<IUpdateBookingFormHistoryMapper>();
        CancellationToken cancellationToken = default;

        UpdateBookingFormHistoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBookingFormHistoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateBookingFormHistoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateBookingFormHistoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<BookingFormHistory>(), Arg.Any<UpdateBookingFormHistoryCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<BookingFormHistory>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateBookingFormHistoryCommandValidator validatorMock = Substitute.For<UpdateBookingFormHistoryCommandValidator>();
        ILogger<UpdateBookingFormHistoryHandler> loggerMock = Substitute.For<ILogger<UpdateBookingFormHistoryHandler>>();
        IBookingFormHistoryWriteRepository repositoryMock = Substitute.For<IBookingFormHistoryWriteRepository>();
        IUpdateBookingFormHistoryMapper mapperMock = Substitute.For<IUpdateBookingFormHistoryMapper>();
        CancellationToken cancellationToken = default;

        UpdateBookingFormHistoryHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBookingFormHistoryCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BookingFormHistory bookingformhistory = new();
        BookingFormHistory bookingformhistoryMapped = new();
        mapperMock.Map(bookingformhistory, cmd).ReturnsForAnyArgs(bookingformhistoryMapped);

        // TODO: change error type
        repositoryMock
            .Update(bookingformhistory, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateBookingFormHistoryCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateBookingFormHistoryCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<BookingFormHistory>(), Arg.Any<UpdateBookingFormHistoryCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<BookingFormHistory>(), cancellationToken).Received(1);
        });
    }
}