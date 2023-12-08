using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialProducts.ReadMaterialProduct;

public interface IReadMaterialProductHandler
{
    Task<Either<ReadMaterialProductQueryResponse, Errors>> Handle(ReadMaterialProductQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialProductHandler : IReadMaterialProductHandler
{
    private readonly ILogger<ReadMaterialProductHandler> _logger;
    private readonly ReadMaterialProductQueryValidator _validator;
    private readonly IMaterialProductPublicReadRepository _readRepository;

    public ReadMaterialProductHandler(
        ILogger<ReadMaterialProductHandler> logger,
        ReadMaterialProductQueryValidator validator,
        IMaterialProductPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadMaterialProductQueryResponse, Errors>> Handle(
        ReadMaterialProductQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialProduct materialproduct = await _readRepository.Read(query.Id, cancellationToken);
            if (materialproduct == null)
                return new(new Errors(ErrorType.NotFound, $"MaterialProduct with Id {query.Id} not found"));

            return new(new ReadMaterialProductQueryResponse(materialproduct));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading MaterialProduct with Id {id}", query.Id);
            throw;
        }
    }
}