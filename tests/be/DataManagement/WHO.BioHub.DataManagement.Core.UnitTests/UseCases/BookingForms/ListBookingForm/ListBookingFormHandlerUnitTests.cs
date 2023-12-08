using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForms;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForm;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BookingForms.ListBookingForms;

public class ListBookingFormsHandlerUnitTests
{
    [Fact]
    public async Task If_no_bookingforms_exists_then_empty_array_is_returned()
    {
        // Arrange
        ListBookingFormsQueryValidator validatorMock = Substitute.For<ListBookingFormsQueryValidator>();
        ILogger<ListBookingFormsHandler> loggerMock = Substitute.For<ILogger<ListBookingFormsHandler>>();
        IBookingFormReadRepository repositoryMock = Substitute.For<IBookingFormReadRepository>();
        CancellationToken cancellationToken = default;

        ListBookingFormsMapper mapperMock = Substitute.For<ListBookingFormsMapper>();

        ListBookingFormsHandler handler = new(loggerMock, mapperMock, validatorMock, repositoryMock);
        ListBookingFormsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<BookingForm> bookingforms = Array.Empty<BookingForm>();
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(bookingforms));

        // Act
        Either<ListBookingFormsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        //response.Left.BookingForms.Should()
        //    .BeEquivalentTo(bookingforms, because: "Expected returned bookingforms to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListBookingFormsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_at_least_on_bookingform_exists_then_it_is_returned()
    {
        // Arrange
        ListBookingFormsQueryValidator validatorMock = Substitute.For<ListBookingFormsQueryValidator>();
        ILogger<ListBookingFormsHandler> loggerMock = Substitute.For<ILogger<ListBookingFormsHandler>>();
        IBookingFormReadRepository repositoryMock = Substitute.For<IBookingFormReadRepository>();
        CancellationToken cancellationToken = default;
        ListBookingFormsMapper mapperMock = Substitute.For<ListBookingFormsMapper>();

        ListBookingFormsHandler handler = new(loggerMock, mapperMock, validatorMock, repositoryMock);
        ListBookingFormsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid assignedId = Guid.NewGuid();
        IEnumerable<BookingForm> bookingforms = new BookingForm[1] { new() };
        repositoryMock
            .List(cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(bookingforms));

        // Act
        Either<ListBookingFormsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        //response.Left.BookingForms.Should()
        //    .BeEquivalentTo(bookingforms, because: "Expected returned bookingforms to be equivalent to ones returned from repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ListBookingFormsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(1);
        });
    }


    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ListBookingFormsQueryValidator validatorMock = Substitute.For<ListBookingFormsQueryValidator>();
        ILogger<ListBookingFormsHandler> loggerMock = Substitute.For<ILogger<ListBookingFormsHandler>>();
        IBookingFormReadRepository repositoryMock = Substitute.For<IBookingFormReadRepository>();
        CancellationToken cancellationToken = default;

        ListBookingFormsMapper mapperMock = Substitute.For<ListBookingFormsMapper>();

        ListBookingFormsHandler handler = new(loggerMock, mapperMock, validatorMock, repositoryMock);
        ListBookingFormsQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ListBookingFormsQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ListBookingFormsQuery>(), cancellationToken).Received(1);
            await repositoryMock.List(cancellationToken).Received(0);
        });
    }
}