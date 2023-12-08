using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.DeleteMaterialProduct;

public interface IDeleteMaterialProductHandler
{
    Task<Either<DeleteMaterialProductCommandResponse, Errors>> Handle(DeleteMaterialProductCommand command, CancellationToken cancellationToken);
}

public class DeleteMaterialProductHandler : IDeleteMaterialProductHandler
{
    private readonly ILogger<DeleteMaterialProductHandler> _logger;
    private readonly DeleteMaterialProductCommandValidator _validator;
    private readonly IMaterialProductWriteRepository _writeRepository;

    public DeleteMaterialProductHandler(
        ILogger<DeleteMaterialProductHandler> logger,
        DeleteMaterialProductCommandValidator validator,
        IMaterialProductWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteMaterialProductCommandResponse, Errors>> Handle(
        DeleteMaterialProductCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Errors? errors = await _writeRepository.Delete(command.Id, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new DeleteMaterialProductCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the MaterialProduct with {id}", command.Id);
            throw;
        }
    }
}