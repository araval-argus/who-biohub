using FluentValidation.Results;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ReadBookingForm;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForm;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UnitTests.UseCases.BookingForms.ReadBookingForm;

public class ReadBookingFormHandlerUnitTests
{
    [Fact]
    public async Task If_resource_is_read_then_a_valid_response_is_returned()
    {
        // Arrange
        ReadBookingFormQueryValidator validatorMock = Substitute.For<ReadBookingFormQueryValidator>();
        ILogger<ReadBookingFormHandler> loggerMock = Substitute.For<ILogger<ReadBookingFormHandler>>();
        IBookingFormReadRepository repositoryMock = Substitute.For<IBookingFormReadRepository>();
        CancellationToken cancellationToken = default;

        IReadBookingFormMapper mapperMock = Substitute.For<IReadBookingFormMapper>();

        ReadBookingFormHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ReadBookingFormQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult());

        Guid id = Guid.NewGuid();
        BookingForm bookingform = new() { Id = id };

        CourierBookingFormDto courierBookingFormDto = new() { Id = id };

        Guid assignedId = Guid.NewGuid();
        repositoryMock
            .ReadByIdWithExtraInfo(id, cancellationToken)
            .ReturnsForAnyArgs(Task.FromResult(bookingform));

        mapperMock.Map(bookingform).ReturnsForAnyArgs(courierBookingFormDto);

        // Act
        Either<ReadBookingFormQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

        // Assert
        response.Should()
            .NotBeNull(because: "A response is expected in this scenario");
        response.IsLeft.Should()
            .BeTrue(because: "Response is expected in this scenario");
        response.Left.Should()
            .NotBeNull(because: "Response should NOT be null");
        response.Left.CourierBookingForm.Should()
            .Be(courierBookingFormDto, because: "Expected id to be the requested one");

        Received.InOrder(async () =>
        {
            await validatorMock.ValidateAsync(Arg.Any<ReadBookingFormQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(id, cancellationToken).Received(1);
        });
    }

    [Fact]
    public async Task If_validation_fails_then_errors_must_be_returned()
    {
        // Arrange
        ReadBookingFormQueryValidator validatorMock = Substitute.For<ReadBookingFormQueryValidator>();
        ILogger<ReadBookingFormHandler> loggerMock = Substitute.For<ILogger<ReadBookingFormHandler>>();
        IBookingFormReadRepository repositoryMock = Substitute.For<IBookingFormReadRepository>();
        CancellationToken cancellationToken = default;

        ReadBookingFormMapper mapperMock = Substitute.For<ReadBookingFormMapper>();

        ReadBookingFormHandler handler = new(loggerMock, validatorMock, repositoryMock, mapperMock);
        ReadBookingFormQuery cmd = new();

        validatorMock
            .ValidateAsync(cmd, cancellationToken)
            .ReturnsForAnyArgs(new ValidationResult(new ValidationFailure[1] { new("test", "error message") }));

        // Act
        Either<ReadBookingFormQueryResponse, Errors> response = await handler.Handle(cmd, cancellationToken);

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
            await validatorMock.ValidateAsync(Arg.Any<ReadBookingFormQuery>(), cancellationToken).Received(1);
            await repositoryMock.Read(Arg.Any<Guid>(), cancellationToken).Received(0);
        });
    }
}