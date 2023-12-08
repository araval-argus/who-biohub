using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.CreateBookingForm;

public interface ICreateBookingFormHandler
{
    Task<Either<CreateBookingFormCommandResponse, Errors>> Handle(CreateBookingFormCommand command, CancellationToken cancellationToken);
}

public class CreateBookingFormHandler : ICreateBookingFormHandler
{
    private readonly ILogger<CreateBookingFormHandler> _logger;
    private readonly CreateBookingFormCommandValidator _validator;
    private readonly ICreateBookingFormMapper _mapper;
    private readonly IBookingFormWriteRepository _writeRepository;

    public CreateBookingFormHandler(
        ILogger<CreateBookingFormHandler> logger,
        CreateBookingFormCommandValidator validator,
        ICreateBookingFormMapper mapper,
        IBookingFormWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateBookingFormCommandResponse, Errors>> Handle(
        CreateBookingFormCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        BookingForm bookingform = _mapper.Map(command);

        try
        {
            Either<BookingForm, Errors> response = await _writeRepository.Create(bookingform, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            BookingForm createdBookingForm =
                response.Left ?? throw new Exception("This is a bug: bookingform value must be defined");
            return new(new CreateBookingFormCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new BookingForm");
            throw;
        }
    }
}