using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.ListMaterialTypes;

public interface IListMaterialTypesHandler
{
    Task<Either<ListMaterialTypesQueryResponse, Errors>> Handle(ListMaterialTypesQuery query, CancellationToken cancellationToken);
}

public class ListMaterialTypesHandler : IListMaterialTypesHandler
{
    private readonly ILogger<ListMaterialTypesHandler> _logger;
    private readonly ListMaterialTypesQueryValidator _validator;
    private readonly IMaterialTypeReadRepository _readRepository;

    public ListMaterialTypesHandler(
        ILogger<ListMaterialTypesHandler> logger,
        ListMaterialTypesQueryValidator validator,
        IMaterialTypeReadRepository readRepository)
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
            var materialTypeDtos = new List<MaterialTypeDto>();
            foreach (var materialType in materialtypes)
            {
                MaterialTypeDto materialTypeDto = new()
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