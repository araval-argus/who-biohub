using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Shared.Utils;


namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.UpdateBioHubFacility;
public interface IUpdateBioHubFacilityHandler
{
    Task<Either<UpdateBioHubFacilityCommandResponse, Errors>> Handle(UpdateBioHubFacilityCommand command, CancellationToken cancellationToken);
}

public class UpdateBioHubFacilityHandler : IUpdateBioHubFacilityHandler
{
    private readonly ILogger<UpdateBioHubFacilityHandler> _logger;
    private readonly UpdateBioHubFacilityCommandValidator _validator;
    private readonly IUpdateBioHubFacilityMapper _mapper;
    private readonly IBioHubFacilityWriteRepository _writeRepository;

    public UpdateBioHubFacilityHandler(
        ILogger<UpdateBioHubFacilityHandler> logger,
        UpdateBioHubFacilityCommandValidator validator,
        IUpdateBioHubFacilityMapper mapper,
        IBioHubFacilityWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateBioHubFacilityCommandResponse, Errors>> Handle(
        UpdateBioHubFacilityCommand command,
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

            biohubfacility = _mapper.Map(biohubfacility, command);
            Errors? errors = await _writeRepository.Update(biohubfacility, cancellationToken, transaction);
            if (errors.HasValue)
            {
                await Rollback(transaction);
                return new(errors.Value);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            return new(new UpdateBioHubFacilityCommandResponse(biohubfacility.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new BioHubFacility");
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