using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Materials.ListMaterials;

public interface IListMaterialsHandler
{
    Task<Either<ListMaterialsQueryResponse, Errors>> Handle(ListMaterialsQuery query, CancellationToken cancellationToken);
}

public class ListMaterialsHandler : IListMaterialsHandler
{
    private readonly ILogger<ListMaterialsHandler> _logger;
    private readonly ListMaterialsQueryValidator _validator;
    private readonly IMaterialPublicReadRepository _readRepository;
    private readonly IListMaterialMapper _mapper;

    public ListMaterialsHandler(
        ILogger<ListMaterialsHandler> logger,
        ListMaterialsQueryValidator validator,
        IMaterialPublicReadRepository readRepository,
        IListMaterialMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListMaterialsQueryResponse, Errors>> Handle(
        ListMaterialsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<Material> materials = await _readRepository.List(cancellationToken);

            IEnumerable<MaterialPublicViewModel> materialPublicViewModels = _mapper.Map(materials);
            return new(new ListMaterialsQueryResponse(materialPublicViewModels));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Materials");
            throw;
        }
    }
}