using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.UpdateMaterialProduct;

public interface IUpdateMaterialProductHandler
{
    Task<Either<UpdateMaterialProductCommandResponse, Errors>> Handle(UpdateMaterialProductCommand command, CancellationToken cancellationToken);
}

public class UpdateMaterialProductHandler : IUpdateMaterialProductHandler
{
    private readonly ILogger<UpdateMaterialProductHandler> _logger;
    private readonly UpdateMaterialProductCommandValidator _validator;
    private readonly IUpdateMaterialProductMapper _mapper;
    private readonly IMaterialProductWriteRepository _writeRepository;

    public UpdateMaterialProductHandler(
        ILogger<UpdateMaterialProductHandler> logger,
        UpdateMaterialProductCommandValidator validator,
        IUpdateMaterialProductMapper mapper,
        IMaterialProductWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateMaterialProductCommandResponse, Errors>> Handle(
        UpdateMaterialProductCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialProduct materialproduct = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            materialproduct = _mapper.Map(materialproduct, command);

            Errors? errors = await _writeRepository.Update(materialproduct, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateMaterialProductCommandResponse(materialproduct));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialProduct");
            throw;
        }
    }
}