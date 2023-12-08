using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.ListBookingFormsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BookingFormsHistory.ListBookingFormsHistory;

public class ListBookingFormsHistoryHandlerUnitTests
{
    [Fact]
    public async Task If_no_bookingformshistory_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListBookingFormsHistoryQueryValidator validatorMock = Substitute.For<ListBookingFormsHistoryQueryValidator>();
        ILogger<ListBookingFormsHistoryHandler> loggerMock = Substitute.For<ILogger<ListBookingFormsHistoryHandler>>();
        IBookingFormHistoryReadRepository repositoryMock = Substitute.For<IBookingFormHistoryReadRepository>();
        CancellationToken cancellationToken = default;

        ListBookingFormsHistoryHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListBookingFormsHistoryQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<BookingFormHistory> bookingformshistory = Array.Empty<BookingFormHistory>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(bookingformshistory));

        // Act
        Either<ListBookingFormsHistoryQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BookingFormsHistory.Should()
            .BeEquivalentTo(bookingformshistory, because: "Expected returned bookingformshistory to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListBookingFormsHistoryQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_bookingformhistory_exists_then_it_is_returned()
    {
        // Arrange
        ListBookingFormsHistoryQueryValidator validatorMock = Substitute.For<ListBookingFormsHistoryQueryValidator>();
        ILogger<ListBookingFormsHistoryHandler> loggerMock = Substitute.For<ILogger<ListBookingFormsHistoryHandler>>();
        IBookingFormHistoryReadRepository repositoryMock = Substitute.For<IBookingFormHistoryReadRepository>();
        CancellationToken cancellationToken = default;

        ListBookingFormsHistoryHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListBookingFormsHistoryQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<BookingFormHistory> bookingformshistory = new BookingFormHistory[1] { new() };
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(bookingformshistory));

        // Act
        Either<ListBookingFormsHistoryQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BookingFormsHistory.Should()
            .BeEquivalentTo(bookingformshistory, because: "Expected returned bookingformshistory to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListBookingFormsHistoryQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListBookingFormsHistoryQueryValidator validatorMock = Substitute.For<ListBookingFormsHistoryQueryValidator>();
        ILogger<ListBookingFormsHistoryHandler> loggerMock = Substitute.For<ILogger<ListBookingFormsHistoryHandler>>();
        IBookingFormHistoryReadRepository repositoryMock = Substitute.For<IBookingFormHistoryReadRepository>();
        CancellationToken cancellationToken = default;

        ListBookingFormsHistoryHandler handler = new(loggerMock, validatorMock, repositoryMock);
        ListBookingFormsHistoryQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListBookingFormsHistoryQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListBookingFormsHistoryQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}