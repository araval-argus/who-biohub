using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.DeleteLaboratory;

public interface IDeleteLaboratoryHandler
{
    Task<Either<DeleteLaboratoryCommandResponse, Errors>> Handle(DeleteLaboratoryCommand command, CancellationToken cancellationToken);
}

public class DeleteLaboratoryHandler : IDeleteLaboratoryHandler
{
    private readonly ILogger<DeleteLaboratoryHandler> _logger;
    private readonly DeleteLaboratoryCommandValidator _validator;
    private readonly ILaboratoryWriteRepository _writeRepository;
    private readonly IUserWriteRepository _userWriteRepository;

    public DeleteLaboratoryHandler(
        ILogger<DeleteLaboratoryHandler> logger,
        DeleteLaboratoryCommandValidator validator,
        ILaboratoryWriteRepository writeRepository,
        IUserWriteRepository userWriteRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
        _userWriteRepository = userWriteRepository;
    }

    public async Task<Either<DeleteLaboratoryCommandResponse, Errors>> Handle(
        DeleteLaboratoryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IDbContextTransaction transaction = null;

        try
        {
            Laboratory laboratory = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (laboratory == null)
            {
                return new(new Errors(ErrorType.NotFound, $"BioHub Facility with Id {command.Id} not found"));
            }

            transaction = await _writeRepository.BeginTransactionAsync();

            await _writeRepository.CreateLaboratoryHistoryItem(laboratory, cancellationToken, transaction);

            laboratory.DeletedOn = DateTime.UtcNow;
            laboratory.LastOperationDate = laboratory.DeletedOn;
            laboratory.LastOperationByUserId = command.OperationById;

            Errors? errors = await _writeRepository.Update(laboratory, cancellationToken, transaction);
            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            errors = await _userWriteRepository.DeleteUsersByLaboratory(laboratory.Id, command.OperationById, laboratory.DeletedOn.GetValueOrDefault(), cancellationToken, transaction);
            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            return new(new DeleteLaboratoryCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the Laboratory with {id}", command.Id);
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