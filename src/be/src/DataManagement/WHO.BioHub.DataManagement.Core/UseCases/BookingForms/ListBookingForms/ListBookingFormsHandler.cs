using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForm;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForms;

public interface IListBookingFormsHandler
{
    Task<Either<ListBookingFormsQueryResponse, Errors>> Handle(ListBookingFormsQuery query, CancellationToken cancellationToken);
}

public class ListBookingFormsHandler : IListBookingFormsHandler
{
    private readonly ILogger<ListBookingFormsHandler> _logger;
    private readonly ListBookingFormsQueryValidator _validator;
    private readonly IBookingFormReadRepository _readRepository;
    private readonly IListBookingFormsMapper _mapper;

    public ListBookingFormsHandler(
        ILogger<ListBookingFormsHandler> logger,
        IListBookingFormsMapper mapper,
        ListBookingFormsQueryValidator validator,
        IBookingFormReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListBookingFormsQueryResponse, Errors>> Handle(
        ListBookingFormsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<BookingForm> bookingforms = await _readRepository.ListByCourierIdWithExtraInformation(query.CourierId, cancellationToken);

            IEnumerable<CourierBookingFormDto> bookingFormsDto = _mapper.Map(bookingforms);


            return new(new ListBookingFormsQueryResponse(bookingFormsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all BookingForms");
            throw;
        }
    }
}