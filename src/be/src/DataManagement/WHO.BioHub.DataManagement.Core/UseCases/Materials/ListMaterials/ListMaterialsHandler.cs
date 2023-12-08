using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

public interface IListMaterialsHandler
{
    Task<Either<ListMaterialsQueryResponse, Errors>> Handle(ListMaterialsQuery query, CancellationToken cancellationToken);
}

public class ListMaterialsHandler : IListMaterialsHandler
{
    private readonly ILogger<ListMaterialsHandler> _logger;
    private readonly ListMaterialsQueryValidator _validator;
    private readonly IMaterialReadRepository _readRepository;
    private readonly IListMaterialsMapper _mapper;

    public ListMaterialsHandler(
        ILogger<ListMaterialsHandler> logger,
        ListMaterialsQueryValidator validator,
        IMaterialReadRepository readRepository,
        IListMaterialsMapper mapper)
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
        IEnumerable<Material> materials;
        IEnumerable<MaterialViewModel> materialsViewModel;

        try
        {
            switch (query.RoleType)
            {
                case RoleType.WHO:                
                    materials = await _readRepository.List(cancellationToken);
                    materialsViewModel = _mapper.Map(materials);
                    return new(new ListMaterialsQueryResponse(materialsViewModel));

                case RoleType.BioHubFacility:
                    materials = await _readRepository.ListForBioHubFacilityUser(query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);
                    materialsViewModel = _mapper.Map(materials);
                    return new(new ListMaterialsQueryResponse(materialsViewModel));

                case RoleType.Laboratory:
                    materials = await _readRepository.ListForLaboratoryUser(query.LaboratoryId.GetValueOrDefault(), cancellationToken);
                    materialsViewModel = _mapper.Map(materials);
                    return new(new ListMaterialsQueryResponse(materialsViewModel));

                default:
                    throw new InvalidOperationException();

            }

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Materials");
            throw;
        }
    }
}