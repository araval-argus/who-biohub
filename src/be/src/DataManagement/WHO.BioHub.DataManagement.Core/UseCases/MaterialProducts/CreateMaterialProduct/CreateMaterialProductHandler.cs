using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.CreateMaterialProduct;

public interface ICreateMaterialProductHandler
{
    Task<Either<CreateMaterialProductCommandResponse, Errors>> Handle(CreateMaterialProductCommand command, CancellationToken cancellationToken);
}

public class CreateMaterialProductHandler : ICreateMaterialProductHandler
{
    private readonly ILogger<CreateMaterialProductHandler> _logger;
    private readonly CreateMaterialProductCommandValidator _validator;
    private readonly ICreateMaterialProductMapper _mapper;
    private readonly IMaterialProductWriteRepository _writeRepository;

    public CreateMaterialProductHandler(
        ILogger<CreateMaterialProductHandler> logger,
        CreateMaterialProductCommandValidator validator,
        ICreateMaterialProductMapper mapper,
        IMaterialProductWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateMaterialProductCommandResponse, Errors>> Handle(
        CreateMaterialProductCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        MaterialProduct materialproduct = _mapper.Map(command);

        try
        {
            Either<MaterialProduct, Errors> response = await _writeRepository.Create(materialproduct, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            MaterialProduct createdMaterialProduct =
                response.Left ?? throw new Exception("This is a bug: materialproduct value must be defined");
            return new(new CreateMaterialProductCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialProduct");
            throw;
        }
    }
}