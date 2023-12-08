using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformationsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.UpdateMaterialShippingInformationHistory;

public interface IUpdateMaterialShippingInformationHistoryHandler
{
    Task<Either<UpdateMaterialShippingInformationHistoryCommandResponse, Errors>> Handle(UpdateMaterialShippingInformationHistoryCommand command, CancellationToken cancellationToken);
}

public class UpdateMaterialShippingInformationHistoryHandler : IUpdateMaterialShippingInformationHistoryHandler
{
    private readonly ILogger<UpdateMaterialShippingInformationHistoryHandler> _logger;
    private readonly UpdateMaterialShippingInformationHistoryCommandValidator _validator;
    private readonly IUpdateMaterialShippingInformationHistoryMapper _mapper;
    private readonly IMaterialShippingInformationHistoryWriteRepository _writeRepository;

    public UpdateMaterialShippingInformationHistoryHandler(
        ILogger<UpdateMaterialShippingInformationHistoryHandler> logger,
        UpdateMaterialShippingInformationHistoryCommandValidator validator,
        IUpdateMaterialShippingInformationHistoryMapper mapper,
        IMaterialShippingInformationHistoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateMaterialShippingInformationHistoryCommandResponse, Errors>> Handle(
        UpdateMaterialShippingInformationHistoryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialShippingInformationHistory materialshippinginformation = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            materialshippinginformation = _mapper.Map(materialshippinginformation, command);

            Errors? errors = await _writeRepository.Update(materialshippinginformation, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateMaterialShippingInformationHistoryCommandResponse(materialshippinginformation));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialShippingInformationHistory");
            throw;
        }
    }
}