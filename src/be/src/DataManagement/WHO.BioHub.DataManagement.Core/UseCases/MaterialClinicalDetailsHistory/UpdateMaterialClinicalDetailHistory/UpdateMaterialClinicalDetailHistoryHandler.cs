using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetailsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.UpdateMaterialClinicalDetailHistory;

public interface IUpdateMaterialClinicalDetailHistoryHandler
{
    Task<Either<UpdateMaterialClinicalDetailHistoryCommandResponse, Errors>> Handle(UpdateMaterialClinicalDetailHistoryCommand command, CancellationToken cancellationToken);
}

public class UpdateMaterialClinicalDetailHistoryHandler : IUpdateMaterialClinicalDetailHistoryHandler
{
    private readonly ILogger<UpdateMaterialClinicalDetailHistoryHandler> _logger;
    private readonly UpdateMaterialClinicalDetailHistoryCommandValidator _validator;
    private readonly IUpdateMaterialClinicalDetailHistoryMapper _mapper;
    private readonly IMaterialClinicalDetailHistoryWriteRepository _writeRepository;

    public UpdateMaterialClinicalDetailHistoryHandler(
        ILogger<UpdateMaterialClinicalDetailHistoryHandler> logger,
        UpdateMaterialClinicalDetailHistoryCommandValidator validator,
        IUpdateMaterialClinicalDetailHistoryMapper mapper,
        IMaterialClinicalDetailHistoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateMaterialClinicalDetailHistoryCommandResponse, Errors>> Handle(
        UpdateMaterialClinicalDetailHistoryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialClinicalDetailHistory materialclinicaldetailhistory = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            materialclinicaldetailhistory = _mapper.Map(materialclinicaldetailhistory, command);

            Errors? errors = await _writeRepository.Update(materialclinicaldetailhistory, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateMaterialClinicalDetailHistoryCommandResponse(materialclinicaldetailhistory));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialClinicalDetailHistory");
            throw;
        }
    }
}