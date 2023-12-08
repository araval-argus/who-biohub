using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.DeleteCourier;

public interface IDeleteCourierHandler
{
    Task<Either<DeleteCourierCommandResponse, Errors>> Handle(DeleteCourierCommand command, CancellationToken cancellationToken);
}

public class DeleteCourierHandler : IDeleteCourierHandler
{
    private readonly ILogger<DeleteCourierHandler> _logger;
    private readonly DeleteCourierCommandValidator _validator;
    private readonly ICourierWriteRepository _writeRepository;

    public DeleteCourierHandler(
        ILogger<DeleteCourierHandler> logger,
        DeleteCourierCommandValidator validator,
        ICourierWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteCourierCommandResponse, Errors>> Handle(
        DeleteCourierCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IDbContextTransaction transaction = null;

        try
        {
            Courier courier = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (courier == null)
            {
                return new(new Errors(ErrorType.NotFound, $"BioHub Facility with Id {command.Id} not found"));
            }

            transaction = await _writeRepository.BeginTransactionAsync();

            await _writeRepository.CreateCourierHistoryItem(courier, cancellationToken, transaction);
            courier.DeletedOn = DateTime.UtcNow;
            courier.LastOperationDate = courier.DeletedOn;
            courier.LastOperationByUserId = command.OperationById;

            Errors? errors = await _writeRepository.Update(courier, cancellationToken, transaction);
            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            return new(new DeleteCourierCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the Courier with {id}", command.Id);
            await Rollback(transaction);
            throw;
        }
    }

    private async Task Rollback(IDbContextTransaction transaction)
    {
        if (transaction != null)
        {
            await transaction.RollbackAsync();
            await transaction.DisposeAsync();
        }
    }
}