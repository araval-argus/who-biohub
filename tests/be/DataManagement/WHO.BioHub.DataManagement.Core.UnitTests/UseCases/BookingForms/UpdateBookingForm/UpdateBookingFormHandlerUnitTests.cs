using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.UpdateBookingForm;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BookingForms.UpdateBookingForm;

public class UpdateBookingFormHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_updated_then_a_valid_response_is_returned()
    {
        // Arrange
        UpdateBookingFormCommandValidator validatorMock = Substitute.For<UpdateBookingFormCommandValidator>();
        ILogger<UpdateBookingFormHandler> loggerMock = Substitute.For<ILogger<UpdateBookingFormHandler>>();
        IBookingFormWriteRepository repositoryMock = Substitute.For<IBookingFormWriteRepository>();
        IUpdateBookingFormMapper mapperMock = Substitute.For<IUpdateBookingFormMapper>();
        CancellationToken cancellationToken = default;

        UpdateBookingFormHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBookingFormCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BookingForm bookingform = new() { Id = Guid.NewGuid() };
        BookingForm bookingformMapped = new() { Id = bookingform.Id };

        mapperMock.Map(bookingform, cmd).ReturnsForAnyArgs(bookingformMapped);

        repositoryMock
            .Update(bookingform, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(null));

        // Act
        Either<UpdateBookingFormCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BookingForm.Should()
            .NotBeNull(because: "BookingForm should NOT be null");
        response.Left.BookingForm.Should()
            .BeEquivalentTo(bookingformMapped, because: "Returned bookingform must mach the one provided in request");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<UpdateBookingFormCommand>(), cancellationToken).Received(1);
            mapperMock.Map(bookingform, Arg.Any<UpdateBookingFormCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<BookingForm>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        UpdateBookingFormCommandValidator validatorMock = Substitute.For<UpdateBookingFormCommandValidator>();
        ILogger<UpdateBookingFormHandler> loggerMock = Substitute.For<ILogger<UpdateBookingFormHandler>>();
        IBookingFormWriteRepository repositoryMock = Substitute.For<IBookingFormWriteRepository>();
        IUpdateBookingFormMapper mapperMock = Substitute.For<IUpdateBookingFormMapper>();
        CancellationToken cancellationToken = default;

        UpdateBookingFormHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBookingFormCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<UpdateBookingFormCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateBookingFormCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<BookingForm>(), Arg.Any<UpdateBookingFormCommand>()).Received(0);
            await repositoryMock.Update(Arg.Any<BookingForm>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        UpdateBookingFormCommandValidator validatorMock = Substitute.For<UpdateBookingFormCommandValidator>();
        ILogger<UpdateBookingFormHandler> loggerMock = Substitute.For<ILogger<UpdateBookingFormHandler>>();
        IBookingFormWriteRepository repositoryMock = Substitute.For<IBookingFormWriteRepository>();
        IUpdateBookingFormMapper mapperMock = Substitute.For<IUpdateBookingFormMapper>();
        CancellationToken cancellationToken = default;

        UpdateBookingFormHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        UpdateBookingFormCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BookingForm bookingform = new();
        BookingForm bookingformMapped = new();
        mapperMock.Map(bookingform, cmd).ReturnsForAnyArgs(bookingformMapped);

        // TODO: change error type
        repositoryMock
            .Update(bookingform, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Errors?>(
                new Errors(ErrorType.Validation, "I should be changed to a persistence error type")));

        // Act
        Either<UpdateBookingFormCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<UpdateBookingFormCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<BookingForm>(), Arg.Any<UpdateBookingFormCommand>()).Received(1);
            await repositoryMock.Update(Arg.Any<BookingForm>(), cancellationToken).Received(1);
        });
    }
}