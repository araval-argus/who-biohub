using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialTypes.ListMaterialTypes;

public interface IListMaterialTypesHandler
{
    Task<Either<ListMaterialTypesQueryResponse, Errors>> Handle(ListMaterialTypesQuery query, CancellationToken cancellationToken);
}

public class ListMaterialTypesHandler : IListMaterialTypesHandler
{
    private readonly ILogger<ListMaterialTypesHandler> _logger;
    private readonly ListMaterialTypesQueryValidator _validator;
    private readonly IMaterialTypePublicReadRepository _readRepository;

    public ListMaterialTypesHandler(
        ILogger<ListMaterialTypesHandler> logger,
        ListMaterialTypesQueryValidator validator,
        IMaterialTypePublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListMaterialTypesQueryResponse, Errors>> Handle(
        ListMaterialTypesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<MaterialType> materialtypes = await _readRepository.List(cancellationToken);
            var materialTypeDtos = new List<MaterialTypePublicDto>();
            foreach (var materialType in materialtypes)
            {
                MaterialTypePublicDto materialTypeDto = new()
                {
                    Id = materialType.Id,
                    Name = materialType.Name,
                    Description = materialType.Description,
                    IsActive = materialType.IsActive
                };

                materialTypeDtos.Add(materialTypeDto);
            }
            return new(new ListMaterialTypesQueryResponse(materialTypeDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all MaterialTypes");
            throw;
        }
    }
}