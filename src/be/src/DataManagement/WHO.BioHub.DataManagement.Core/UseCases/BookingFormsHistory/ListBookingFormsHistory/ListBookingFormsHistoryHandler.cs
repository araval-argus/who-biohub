using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.ListBookingFormsHistory;

public interface IListBookingFormsHistoryHandler
{
    Task<Either<ListBookingFormsHistoryQueryResponse, Errors>> Handle(ListBookingFormsHistoryQuery query, CancellationToken cancellationToken);
}

public class ListBookingFormsHistoryHandler : IListBookingFormsHistoryHandler
{
    private readonly ILogger<ListBookingFormsHistoryHandler> _logger;
    private readonly ListBookingFormsHistoryQueryValidator _validator;
    private readonly IBookingFormHistoryReadRepository _readRepository;

    public ListBookingFormsHistoryHandler(
        ILogger<ListBookingFormsHistoryHandler> logger,
        ListBookingFormsHistoryQueryValidator validator,
        IBookingFormHistoryReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListBookingFormsHistoryQueryResponse, Errors>> Handle(
        ListBookingFormsHistoryQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<BookingFormHistory> bookingformshistory = await _readRepository.List(cancellationToken);
            return new(new ListBookingFormsHistoryQueryResponse(bookingformshistory));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all BookingFormsHistory");
            throw;
        }
    }
}