using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.UpdateBookingFormHistory;

public interface IUpdateBookingFormHistoryHandler
{
    Task<Either<UpdateBookingFormHistoryCommandResponse, Errors>> Handle(UpdateBookingFormHistoryCommand command, CancellationToken cancellationToken);
}

public class UpdateBookingFormHistoryHandler : IUpdateBookingFormHistoryHandler
{
    private readonly ILogger<UpdateBookingFormHistoryHandler> _logger;
    private readonly UpdateBookingFormHistoryCommandValidator _validator;
    private readonly IUpdateBookingFormHistoryMapper _mapper;
    private readonly IBookingFormHistoryWriteRepository _writeRepository;

    public UpdateBookingFormHistoryHandler(
        ILogger<UpdateBookingFormHistoryHandler> logger,
        UpdateBookingFormHistoryCommandValidator validator,
        IUpdateBookingFormHistoryMapper mapper,
        IBookingFormHistoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateBookingFormHistoryCommandResponse, Errors>> Handle(
        UpdateBookingFormHistoryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            BookingFormHistory bookingformhistory = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            bookingformhistory = _mapper.Map(bookingformhistory, command);

            Errors? errors = await _writeRepository.Update(bookingformhistory, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateBookingFormHistoryCommandResponse(bookingformhistory));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new BookingFormHistory");
            throw;
        }
    }
}