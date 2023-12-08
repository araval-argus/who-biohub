using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.CreateBookingFormHistory;

public interface ICreateBookingFormHistoryHandler
{
    Task<Either<CreateBookingFormHistoryCommandResponse, Errors>> Handle(CreateBookingFormHistoryCommand command, CancellationToken cancellationToken);
}

public class CreateBookingFormHistoryHandler : ICreateBookingFormHistoryHandler
{
    private readonly ILogger<CreateBookingFormHistoryHandler> _logger;
    private readonly CreateBookingFormHistoryCommandValidator _validator;
    private readonly ICreateBookingFormHistoryMapper _mapper;
    private readonly IBookingFormHistoryWriteRepository _writeRepository;

    public CreateBookingFormHistoryHandler(
        ILogger<CreateBookingFormHistoryHandler> logger,
        CreateBookingFormHistoryCommandValidator validator,
        ICreateBookingFormHistoryMapper mapper,
        IBookingFormHistoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateBookingFormHistoryCommandResponse, Errors>> Handle(
        CreateBookingFormHistoryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        BookingFormHistory bookingformhistory = _mapper.Map(command);

        try
        {
            Either<BookingFormHistory, Errors> response = await _writeRepository.Create(bookingformhistory, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            BookingFormHistory createdBookingFormHistory =
                response.Left ?? throw new Exception("This is a bug: bookingformhistory value must be defined");
            return new(new CreateBookingFormHistoryCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new BookingFormHistory");
            throw;
        }
    }
}