using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.ReadBookingFormHistory;

public interface IReadBookingFormHistoryHandler
{
    Task<Either<ReadBookingFormHistoryQueryResponse, Errors>> Handle(ReadBookingFormHistoryQuery query, CancellationToken cancellationToken);
}

public class ReadBookingFormHistoryHandler : IReadBookingFormHistoryHandler
{
    private readonly ILogger<ReadBookingFormHistoryHandler> _logger;
    private readonly ReadBookingFormHistoryQueryValidator _validator;
    private readonly IBookingFormHistoryReadRepository _readRepository;

    public ReadBookingFormHistoryHandler(
        ILogger<ReadBookingFormHistoryHandler> logger,
        ReadBookingFormHistoryQueryValidator validator,
        IBookingFormHistoryReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadBookingFormHistoryQueryResponse, Errors>> Handle(
        ReadBookingFormHistoryQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            BookingFormHistory bookingformhistory = await _readRepository.Read(query.Id, cancellationToken);
            if (bookingformhistory == null)
                return new(new Errors(ErrorType.NotFound, $"BookingFormHistory with Id {query.Id} not found"));

            return new(new ReadBookingFormHistoryQueryResponse(bookingformhistory));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading BookingFormHistory with Id {id}", query.Id);
            throw;
        }
    }
}