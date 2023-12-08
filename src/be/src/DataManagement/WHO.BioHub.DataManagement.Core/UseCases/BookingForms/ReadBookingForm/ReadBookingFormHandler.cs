using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForm;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ReadBookingForm;

public interface IReadBookingFormHandler
{
    Task<Either<ReadBookingFormQueryResponse, Errors>> Handle(ReadBookingFormQuery query, CancellationToken cancellationToken);
}

public class ReadBookingFormHandler : IReadBookingFormHandler
{
    private readonly ILogger<ReadBookingFormHandler> _logger;
    private readonly ReadBookingFormQueryValidator _validator;
    private readonly IBookingFormReadRepository _readRepository;
    private readonly IReadBookingFormMapper _mapper;

    public ReadBookingFormHandler(
        ILogger<ReadBookingFormHandler> logger,
        ReadBookingFormQueryValidator validator,
        IBookingFormReadRepository readRepository,
        IReadBookingFormMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadBookingFormQueryResponse, Errors>> Handle(
        ReadBookingFormQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            BookingForm bookingform = await _readRepository.ReadByIdWithExtraInfo(query.Id, cancellationToken);
            if (bookingform == null)
                return new(new Errors(ErrorType.NotFound, $"BookingForm with Id {query.Id} not found"));

            var bookingFormDto = _mapper.Map(bookingform);

            return new(new ReadBookingFormQueryResponse(bookingFormDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading BookingForm with Id {id}", query.Id);
            throw;
        }
    }
}