using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.UpdateBookingForm;

public interface IUpdateBookingFormHandler
{
    Task<Either<UpdateBookingFormCommandResponse, Errors>> Handle(UpdateBookingFormCommand command, CancellationToken cancellationToken);
}

public class UpdateBookingFormHandler : IUpdateBookingFormHandler
{
    private readonly ILogger<UpdateBookingFormHandler> _logger;
    private readonly UpdateBookingFormCommandValidator _validator;
    private readonly IUpdateBookingFormMapper _mapper;
    private readonly IBookingFormWriteRepository _writeRepository;

    public UpdateBookingFormHandler(
        ILogger<UpdateBookingFormHandler> logger,
        UpdateBookingFormCommandValidator validator,
        IUpdateBookingFormMapper mapper,
        IBookingFormWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateBookingFormCommandResponse, Errors>> Handle(
        UpdateBookingFormCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            BookingForm bookingform = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            bookingform = _mapper.Map(bookingform, command);

            Errors? errors = await _writeRepository.Update(bookingform, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateBookingFormCommandResponse(bookingform));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new BookingForm");
            throw;
        }
    }
}