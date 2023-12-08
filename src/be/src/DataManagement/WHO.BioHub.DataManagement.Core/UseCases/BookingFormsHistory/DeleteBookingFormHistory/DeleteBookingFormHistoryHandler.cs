using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.DeleteBookingFormHistory;

public interface IDeleteBookingFormHistoryHandler
{
    Task<Either<DeleteBookingFormHistoryCommandResponse, Errors>> Handle(DeleteBookingFormHistoryCommand command, CancellationToken cancellationToken);
}

public class DeleteBookingFormHistoryHandler : IDeleteBookingFormHistoryHandler
{
    private readonly ILogger<DeleteBookingFormHistoryHandler> _logger;
    private readonly DeleteBookingFormHistoryCommandValidator _validator;
    private readonly IBookingFormHistoryWriteRepository _writeRepository;

    public DeleteBookingFormHistoryHandler(
        ILogger<DeleteBookingFormHistoryHandler> logger,
        DeleteBookingFormHistoryCommandValidator validator,
        IBookingFormHistoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteBookingFormHistoryCommandResponse, Errors>> Handle(
        DeleteBookingFormHistoryCommand command,
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

            return new(new DeleteBookingFormHistoryCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the BookingFormHistory with {id}", command.Id);
            throw;
        }
    }
}