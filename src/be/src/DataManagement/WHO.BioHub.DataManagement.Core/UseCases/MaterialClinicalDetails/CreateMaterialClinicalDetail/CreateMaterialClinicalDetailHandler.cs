using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.CreateMaterialClinicalDetail;

public interface ICreateMaterialClinicalDetailHandler
{
    Task<Either<CreateMaterialClinicalDetailCommandResponse, Errors>> Handle(CreateMaterialClinicalDetailCommand command, CancellationToken cancellationToken);
}

public class CreateMaterialClinicalDetailHandler : ICreateMaterialClinicalDetailHandler
{
    private readonly ILogger<CreateMaterialClinicalDetailHandler> _logger;
    private readonly CreateMaterialClinicalDetailCommandValidator _validator;
    private readonly ICreateMaterialClinicalDetailMapper _mapper;
    private readonly IMaterialClinicalDetailWriteRepository _writeRepository;

    public CreateMaterialClinicalDetailHandler(
        ILogger<CreateMaterialClinicalDetailHandler> logger,
        CreateMaterialClinicalDetailCommandValidator validator,
        ICreateMaterialClinicalDetailMapper mapper,
        IMaterialClinicalDetailWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateMaterialClinicalDetailCommandResponse, Errors>> Handle(
        CreateMaterialClinicalDetailCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        MaterialClinicalDetail materialclinicaldetail = _mapper.Map(command);

        try
        {
            Either<MaterialClinicalDetail, Errors> response = await _writeRepository.Create(materialclinicaldetail, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            MaterialClinicalDetail createdMaterialClinicalDetail =
                response.Left ?? throw new Exception("This is a bug: materialclinicaldetail value must be defined");
            return new(new CreateMaterialClinicalDetailCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialClinicalDetail");
            throw;
        }
    }
}