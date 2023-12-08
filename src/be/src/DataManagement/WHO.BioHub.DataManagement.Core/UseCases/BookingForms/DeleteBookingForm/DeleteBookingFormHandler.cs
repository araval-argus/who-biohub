using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.DeleteBookingForm;

public interface IDeleteBookingFormHandler
{
    Task<Either<DeleteBookingFormCommandResponse, Errors>> Handle(DeleteBookingFormCommand command, CancellationToken cancellationToken);
}

public class DeleteBookingFormHandler : IDeleteBookingFormHandler
{
    private readonly ILogger<DeleteBookingFormHandler> _logger;
    private readonly DeleteBookingFormCommandValidator _validator;
    private readonly IBookingFormWriteRepository _writeRepository;

    public DeleteBookingFormHandler(
        ILogger<DeleteBookingFormHandler> logger,
        DeleteBookingFormCommandValidator validator,
        IBookingFormWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteBookingFormCommandResponse, Errors>> Handle(
        DeleteBookingFormCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Errors? errors = await _writeRepository.Delete(command.Id, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new DeleteBookingFormCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the BookingForm with {id}", command.Id);
            throw;
        }
    }
}