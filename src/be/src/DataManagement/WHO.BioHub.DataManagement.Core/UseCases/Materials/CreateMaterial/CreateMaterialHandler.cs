using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.CreateMaterial;

public interface ICreateMaterialHandler
{
    Task<Either<CreateMaterialCommandResponse, Errors>> Handle(CreateMaterialCommand command, CancellationToken cancellationToken);
}

public class CreateMaterialHandler : ICreateMaterialHandler
{
    private readonly ILogger<CreateMaterialHandler> _logger;
    private readonly CreateMaterialCommandValidator _validator;
    private readonly ICreateMaterialMapper _mapper;
    private readonly IMaterialWriteRepository _writeRepository;

    public CreateMaterialHandler(
        ILogger<CreateMaterialHandler> logger,
        CreateMaterialCommandValidator validator,
        ICreateMaterialMapper mapper,
        IMaterialWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateMaterialCommandResponse, Errors>> Handle(
        CreateMaterialCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        Material material = _mapper.Map(command);

        try
        {
            Either<Material, Errors> response = await _writeRepository.Create(material, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            Material createdMaterial =
                response.Left ?? throw new Exception("This is a bug: material value must be defined");
            return new(new CreateMaterialCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Material");
            throw;
        }
    }
}