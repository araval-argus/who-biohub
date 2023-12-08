using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.UpdateCourier;

public interface IUpdateCourierHandler
{
    Task<Either<UpdateCourierCommandResponse, Errors>> Handle(UpdateCourierCommand command, CancellationToken cancellationToken);
}

public class UpdateCourierHandler : IUpdateCourierHandler
{
    private readonly ILogger<UpdateCourierHandler> _logger;
    private readonly UpdateCourierCommandValidator _validator;
    private readonly IUpdateCourierMapper _mapper;
    private readonly ICourierWriteRepository _writeRepository;

    public UpdateCourierHandler(
        ILogger<UpdateCourierHandler> logger,
        UpdateCourierCommandValidator validator,
        IUpdateCourierMapper mapper,
        ICourierWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateCourierCommandResponse, Errors>> Handle(
        UpdateCourierCommand command,
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
                return new(new Errors(ErrorType.NotFound, $"Courier with Id {command.Id} not found"));
            }

            transaction = await _writeRepository.BeginTransactionAsync();

            await _writeRepository.CreateCourierHistoryItem(courier, cancellationToken, transaction);

            courier = _mapper.Map(courier, command);

            Errors? errors = await _writeRepository.Update(courier, cancellationToken, transaction);
            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            return new(new UpdateCourierCommandResponse(courier.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Courier");
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