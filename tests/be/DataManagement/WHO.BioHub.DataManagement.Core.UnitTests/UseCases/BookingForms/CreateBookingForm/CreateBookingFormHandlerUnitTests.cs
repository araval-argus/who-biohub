using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.CreateBookingForm;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BookingForms.CreateBookingForm;

public class CreateBookingFormHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_created_then_a_valid_response_is_returned()
    {
        // Arrange
        CreateBookingFormCommandValidator validatorMock = Substitute.For<CreateBookingFormCommandValidator>();
        ILogger<CreateBookingFormHandler> loggerMock = Substitute.For<ILogger<CreateBookingFormHandler>>();
        IBookingFormWriteRepository repositoryMock = Substitute.For<IBookingFormWriteRepository>();
        ICreateBookingFormMapper mapperMock = Substitute.For<ICreateBookingFormMapper>();
        CancellationToken cancellationToken = default;

        CreateBookingFormHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBookingFormCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BookingForm bookingform = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(bookingform);

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .Create(bookingform, cancellationToken)
            .ReturnsForAnyArgs(
                Task.Run<Either<BookingForm, Errors>>(() =>
                {
                    bookingform.Id = assignedId;
                    return new(bookingform);
                }));

        // Act
        Either<CreateBookingFormCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.BookingForm.Should()
            .NotBeNull(because: "BookingForm should NOT be null");
        response.Left.BookingForm.Id.Should()
            .NotBeEmpty(because: "An Id should be assigned to the BookingForm")
            .And.Be(assignedId, because: "Returned bookingform Id must mach the one assigned by repository");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<CreateBookingFormCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBookingFormCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<BookingForm>(), cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        CreateBookingFormCommandValidator validatorMock = Substitute.For<CreateBookingFormCommandValidator>();
        ILogger<CreateBookingFormHandler> loggerMock = Substitute.For<ILogger<CreateBookingFormHandler>>();
        IBookingFormWriteRepository repositoryMock = Substitute.For<IBookingFormWriteRepository>();
        ICreateBookingFormMapper mapperMock = Substitute.For<ICreateBookingFormMapper>();
        CancellationToken cancellationToken = default;

        CreateBookingFormHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBookingFormCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<CreateBookingFormCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateBookingFormCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBookingFormCommand>()).Received(0);
            await repositoryMock.Create(Arg.Any<BookingForm>(), cancellationToken).Received(0);
        });
    }

    [Fact]
    public async Task If_errors_are_returned_from_repository_then_they_must_be_forwarded_from_handler()
    {
        // Arrange
        CreateBookingFormCommandValidator validatorMock = Substitute.For<CreateBookingFormCommandValidator>();
        ILogger<CreateBookingFormHandler> loggerMock = Substitute.For<ILogger<CreateBookingFormHandler>>();
        IBookingFormWriteRepository repositoryMock = Substitute.For<IBookingFormWriteRepository>();
        ICreateBookingFormMapper mapperMock = Substitute.For<ICreateBookingFormMapper>();
        CancellationToken cancellationToken = default;

        CreateBookingFormHandler handler = new(loggerMock, validatorMock, mapperMock, repositoryMock);
        CreateBookingFormCommand cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        BookingForm bookingform = new();
        mapperMock.Map(cmd).ReturnsForAnyArgs(bookingform);

        // TODO: change error type
        repositoryMock
            .Create(bookingform, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult<Either<BookingForm, Errors>>(
                new(new Errors(ErrorType.Validation, "I should be changed to a persistence error type"))));

        // Act
        Either<CreateBookingFormCommandResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<CreateBookingFormCommand>(), cancellationToken).Received(1);
            mapperMock.Map(Arg.Any<CreateBookingFormCommand>()).Received(1);
            await repositoryMock.Create(Arg.Any<BookingForm>(), cancellationToken).Received(1);
        });
    }
}