using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.UpdateMaterialShippingInformation;

public interface IUpdateMaterialShippingInformationHandler
{
    Task<Either<UpdateMaterialShippingInformationCommandResponse, Errors>> Handle(UpdateMaterialShippingInformationCommand command, CancellationToken cancellationToken);
}

public class UpdateMaterialShippingInformationHandler : IUpdateMaterialShippingInformationHandler
{
    private readonly ILogger<UpdateMaterialShippingInformationHandler> _logger;
    private readonly UpdateMaterialShippingInformationCommandValidator _validator;
    private readonly IUpdateMaterialShippingInformationMapper _mapper;
    private readonly IMaterialShippingInformationWriteRepository _writeRepository;

    public UpdateMaterialShippingInformationHandler(
        ILogger<UpdateMaterialShippingInformationHandler> logger,
        UpdateMaterialShippingInformationCommandValidator validator,
        IUpdateMaterialShippingInformationMapper mapper,
        IMaterialShippingInformationWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateMaterialShippingInformationCommandResponse, Errors>> Handle(
        UpdateMaterialShippingInformationCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialShippingInformation materialshippinginformation = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            materialshippinginformation = _mapper.Map(materialshippinginformation, command);

            Errors? errors = await _writeRepository.Update(materialshippinginformation, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateMaterialShippingInformationCommandResponse(materialshippinginformation));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialShippingInformation");
            throw;
        }
    }
}