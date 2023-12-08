using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.UpdateMaterialClinicalDetail;

public interface IUpdateMaterialClinicalDetailHandler
{
    Task<Either<UpdateMaterialClinicalDetailCommandResponse, Errors>> Handle(UpdateMaterialClinicalDetailCommand command, CancellationToken cancellationToken);
}

public class UpdateMaterialClinicalDetailHandler : IUpdateMaterialClinicalDetailHandler
{
    private readonly ILogger<UpdateMaterialClinicalDetailHandler> _logger;
    private readonly UpdateMaterialClinicalDetailCommandValidator _validator;
    private readonly IUpdateMaterialClinicalDetailMapper _mapper;
    private readonly IMaterialClinicalDetailWriteRepository _writeRepository;

    public UpdateMaterialClinicalDetailHandler(
        ILogger<UpdateMaterialClinicalDetailHandler> logger,
        UpdateMaterialClinicalDetailCommandValidator validator,
        IUpdateMaterialClinicalDetailMapper mapper,
        IMaterialClinicalDetailWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateMaterialClinicalDetailCommandResponse, Errors>> Handle(
        UpdateMaterialClinicalDetailCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialClinicalDetail materialclinicaldetail = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            materialclinicaldetail = _mapper.Map(materialclinicaldetail, command);

            Errors? errors = await _writeRepository.Update(materialclinicaldetail, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateMaterialClinicalDetailCommandResponse(materialclinicaldetail));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialClinicalDetail");
            throw;
        }
    }
}