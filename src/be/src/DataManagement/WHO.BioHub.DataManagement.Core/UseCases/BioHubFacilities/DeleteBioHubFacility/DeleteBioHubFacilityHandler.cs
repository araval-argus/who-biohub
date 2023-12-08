using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.DeleteBioHubFacility;

public interface IDeleteBioHubFacilityHandler
{
    Task<Either<DeleteBioHubFacilityCommandResponse, Errors>> Handle(DeleteBioHubFacilityCommand command, CancellationToken cancellationToken);
}

public class DeleteBioHubFacilityHandler : IDeleteBioHubFacilityHandler
{
    private readonly ILogger<DeleteBioHubFacilityHandler> _logger;
    private readonly DeleteBioHubFacilityCommandValidator _validator;
    private readonly IBioHubFacilityWriteRepository _writeRepository;
    private readonly IUserWriteRepository _userWriteRepository;

    public DeleteBioHubFacilityHandler(
        ILogger<DeleteBioHubFacilityHandler> logger,
        DeleteBioHubFacilityCommandValidator validator,
        IBioHubFacilityWriteRepository writeRepository,
        IUserWriteRepository userWriteRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
        _userWriteRepository = userWriteRepository;
    }

    public async Task<Either<DeleteBioHubFacilityCommandResponse, Errors>> Handle(
        DeleteBioHubFacilityCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IDbContextTransaction transaction = null;
        

        try
        {
            BioHubFacility biohubfacility = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (biohubfacility == null)
            {
                return new(new Errors(ErrorType.NotFound, $"BioHub Facility with Id {command.Id} not found"));
            }

            transaction = await _writeRepository.BeginTransactionAsync();

            await _writeRepository.CreateBioHubFacilityHistoryItem(biohubfacility, cancellationToken, transaction);

            biohubfacility.DeletedOn = DateTime.UtcNow;
            biohubfacility.LastOperationDate = biohubfacility.DeletedOn;
            biohubfacility.LastOperationByUserId = command.OperationById;

            Errors? errors = await _writeRepository.Update(biohubfacility, cancellationToken, transaction);
            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            errors = await _userWriteRepository.DeleteUsersByBioHubFacility(biohubfacility.Id, command.OperationById, biohubfacility.DeletedOn.GetValueOrDefault(), cancellationToken, transaction);
            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            return new(new DeleteBioHubFacilityCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the BioHubFacility with {id}", command.Id);
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