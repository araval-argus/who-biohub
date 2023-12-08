using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.CreateMaterialType;

public interface ICreateMaterialTypeHandler
{
    Task<Either<CreateMaterialTypeCommandResponse, Errors>> Handle(CreateMaterialTypeCommand command, CancellationToken cancellationToken);
}

public class CreateMaterialTypeHandler : ICreateMaterialTypeHandler
{
    private readonly ILogger<CreateMaterialTypeHandler> _logger;
    private readonly CreateMaterialTypeCommandValidator _validator;
    private readonly ICreateMaterialTypeMapper _mapper;
    private readonly IMaterialTypeWriteRepository _writeRepository;

    public CreateMaterialTypeHandler(
        ILogger<CreateMaterialTypeHandler> logger,
        CreateMaterialTypeCommandValidator validator,
        ICreateMaterialTypeMapper mapper,
        IMaterialTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateMaterialTypeCommandResponse, Errors>> Handle(
        CreateMaterialTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        MaterialType materialtype = _mapper.Map(command);

        try
        {
            Either<MaterialType, Errors> response = await _writeRepository.Create(materialtype, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            MaterialType createdMaterialType =
                response.Left ?? throw new Exception("This is a bug: materialtype value must be defined");
            return new(new CreateMaterialTypeCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialType");
            throw;
        }
    }
}