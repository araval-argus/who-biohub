using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetailsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.CreateMaterialClinicalDetailHistory;

public interface ICreateMaterialClinicalDetailHistoryHandler
{
    Task<Either<CreateMaterialClinicalDetailHistoryCommandResponse, Errors>> Handle(CreateMaterialClinicalDetailHistoryCommand command, CancellationToken cancellationToken);
}

public class CreateMaterialClinicalDetailHistoryHandler : ICreateMaterialClinicalDetailHistoryHandler
{
    private readonly ILogger<CreateMaterialClinicalDetailHistoryHandler> _logger;
    private readonly CreateMaterialClinicalDetailHistoryCommandValidator _validator;
    private readonly ICreateMaterialClinicalDetailHistoryMapper _mapper;
    private readonly IMaterialClinicalDetailHistoryWriteRepository _writeRepository;

    public CreateMaterialClinicalDetailHistoryHandler(
        ILogger<CreateMaterialClinicalDetailHistoryHandler> logger,
        CreateMaterialClinicalDetailHistoryCommandValidator validator,
        ICreateMaterialClinicalDetailHistoryMapper mapper,
        IMaterialClinicalDetailHistoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateMaterialClinicalDetailHistoryCommandResponse, Errors>> Handle(
        CreateMaterialClinicalDetailHistoryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        MaterialClinicalDetailHistory materialclinicaldetailhistory = _mapper.Map(command);

        try
        {
            Either<MaterialClinicalDetailHistory, Errors> response = await _writeRepository.Create(materialclinicaldetailhistory, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            MaterialClinicalDetailHistory createdMaterialClinicalDetailHistory =
                response.Left ?? throw new Exception("This is a bug: materialclinicaldetailhistory value must be defined");
            return new(new CreateMaterialClinicalDetailHistoryCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialClinicalDetailHistory");
            throw;
        }
    }
}