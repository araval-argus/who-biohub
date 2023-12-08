using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.UpdateMaterialType;

public interface IUpdateMaterialTypeHandler
{
    Task<Either<UpdateMaterialTypeCommandResponse, Errors>> Handle(UpdateMaterialTypeCommand command, CancellationToken cancellationToken);
}

public class UpdateMaterialTypeHandler : IUpdateMaterialTypeHandler
{
    private readonly ILogger<UpdateMaterialTypeHandler> _logger;
    private readonly UpdateMaterialTypeCommandValidator _validator;
    private readonly IUpdateMaterialTypeMapper _mapper;
    private readonly IMaterialTypeWriteRepository _writeRepository;

    public UpdateMaterialTypeHandler(
        ILogger<UpdateMaterialTypeHandler> logger,
        UpdateMaterialTypeCommandValidator validator,
        IUpdateMaterialTypeMapper mapper,
        IMaterialTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateMaterialTypeCommandResponse, Errors>> Handle(
        UpdateMaterialTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialType materialtype = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            materialtype = _mapper.Map(materialtype, command);

            Errors? errors = await _writeRepository.Update(materialtype, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateMaterialTypeCommandResponse(materialtype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialType");
            throw;
        }
    }
}