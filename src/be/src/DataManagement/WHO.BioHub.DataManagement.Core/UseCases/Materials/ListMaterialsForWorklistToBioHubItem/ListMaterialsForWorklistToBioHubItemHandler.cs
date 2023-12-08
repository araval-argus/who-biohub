using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

public interface IListMaterialsForWorklistToBioHubItemHandler
{
    Task<Either<ListMaterialsForWorklistToBioHubItemQueryResponse, Errors>> Handle(ListMaterialsForWorklistToBioHubItemQuery query, CancellationToken cancellationToken);
}

public class ListMaterialsForWorklistToBioHubItemHandler : IListMaterialsForWorklistToBioHubItemHandler
{
    private readonly ILogger<ListMaterialsForWorklistToBioHubItemHandler> _logger;
    private readonly ListMaterialsForWorklistToBioHubItemQueryValidator _validator;
    private readonly IMaterialReadRepository _readRepository;

    public ListMaterialsForWorklistToBioHubItemHandler(
        ILogger<ListMaterialsForWorklistToBioHubItemHandler> logger,
        ListMaterialsForWorklistToBioHubItemQueryValidator validator,
        IMaterialReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListMaterialsForWorklistToBioHubItemQueryResponse, Errors>> Handle(
        ListMaterialsForWorklistToBioHubItemQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<Material> materials = await _readRepository.ListForShipmentRequest(cancellationToken);

            List<WorklistToBioHubItemMaterialDto> worklistToBioHubItemBioHubFacilityMaterials = new List<WorklistToBioHubItemMaterialDto>();

            foreach (var material in materials)
            {
                WorklistToBioHubItemMaterialDto item = new WorklistToBioHubItemMaterialDto();
                item.Id = Guid.NewGuid();
                item.MaterialId = material.Id;
                item.MaterialNumber = material.ReferenceNumber;
                item.MaterialProductId = material.OriginalProductTypeId;
                item.TransportCategoryId = material.TransportCategoryId;
                item.MaterialName = material.Name;
                item.Age = material.Age;
                item.CollectionDate = material.CollectionDate;
                item.Location = material.Location;
                item.Gender = material.Gender;
                item.IsolationHostTypeId = material.IsolationHostTypeId;
                item.WorklistToBioHubItemId = query.WorklistToBioHubItemId;
                worklistToBioHubItemBioHubFacilityMaterials.Add(item);
            }


            return new(new ListMaterialsForWorklistToBioHubItemQueryResponse(worklistToBioHubItemBioHubFacilityMaterials));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading ListMaterialsForWorklistToBioHubItem with WorklistToBioHubItemId {id}", query.WorklistToBioHubItemId);
            throw;
        }
    }
}