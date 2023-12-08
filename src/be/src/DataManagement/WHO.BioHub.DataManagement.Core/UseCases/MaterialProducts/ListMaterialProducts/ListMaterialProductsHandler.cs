using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.ListMaterialProducts;

public interface IListMaterialProductsHandler
{
    Task<Either<ListMaterialProductsQueryResponse, Errors>> Handle(ListMaterialProductsQuery query, CancellationToken cancellationToken);
}

public class ListMaterialProductsHandler : IListMaterialProductsHandler
{
    private readonly ILogger<ListMaterialProductsHandler> _logger;
    private readonly ListMaterialProductsQueryValidator _validator;
    private readonly IMaterialProductReadRepository _readRepository;

    public ListMaterialProductsHandler(
        ILogger<ListMaterialProductsHandler> logger,
        ListMaterialProductsQueryValidator validator,
        IMaterialProductReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListMaterialProductsQueryResponse, Errors>> Handle(
        ListMaterialProductsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<MaterialProduct> materialproducts = await _readRepository.List(cancellationToken);
            var materialProductDtos = new List<MaterialProductDto>();
            foreach (var materialProduct in materialproducts)
            {
                MaterialProductDto materialProductDto = new()
                {
                    Id = materialProduct.Id,
                    Name = materialProduct.Name,
                    Description = materialProduct.Description,
                    IsActive = materialProduct.IsActive
                };

                materialProductDtos.Add(materialProductDto);
            }
            return new(new ListMaterialProductsQueryResponse(materialProductDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all MaterialProducts");
            throw;
        }
    }
}