using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.UpdateLaboratory;

public interface IUpdateLaboratoryHandler
{
    Task<Either<UpdateLaboratoryCommandResponse, Errors>> Handle(UpdateLaboratoryCommand command, CancellationToken cancellationToken);
}

public class UpdateLaboratoryHandler : IUpdateLaboratoryHandler
{
    private readonly ILogger<UpdateLaboratoryHandler> _logger;
    private readonly UpdateLaboratoryCommandValidator _validator;
    private readonly IUpdateLaboratoryMapper _mapper;
    private readonly ILaboratoryWriteRepository _writeRepository;

    public UpdateLaboratoryHandler(
        ILogger<UpdateLaboratoryHandler> logger,
        UpdateLaboratoryCommandValidator validator,
        IUpdateLaboratoryMapper mapper,
        ILaboratoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateLaboratoryCommandResponse, Errors>> Handle(
        UpdateLaboratoryCommand command,
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
                return new(new Errors(ErrorType.NotFound, $"Laboratory with Id {command.Id} not found"));
            }

            transaction = await _writeRepository.BeginTransactionAsync();

            await _writeRepository.CreateLaboratoryHistoryItem(laboratory, cancellationToken, transaction);


            laboratory = _mapper.Map(laboratory, command);

            Errors? errors = await _writeRepository.Update(laboratory, cancellationToken, transaction);
            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            return new(new UpdateLaboratoryCommandResponse(laboratory.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Laboratory");
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