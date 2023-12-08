using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

public interface IListMaterialsForWorklistFromBioHubItemHandler
{
    Task<Either<ListMaterialsForWorklistFromBioHubItemQueryResponse, Errors>> Handle(ListMaterialsForWorklistFromBioHubItemQuery query, CancellationToken cancellationToken);
}

public class ListMaterialsForWorklistFromBioHubItemHandler : IListMaterialsForWorklistFromBioHubItemHandler
{
    private readonly ILogger<ListMaterialsForWorklistFromBioHubItemHandler> _logger;
    private readonly ListMaterialsForWorklistFromBioHubItemQueryValidator _validator;
    private readonly IMaterialReadRepository _readRepository;

    public ListMaterialsForWorklistFromBioHubItemHandler(
        ILogger<ListMaterialsForWorklistFromBioHubItemHandler> logger,
        ListMaterialsForWorklistFromBioHubItemQueryValidator validator,
        IMaterialReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListMaterialsForWorklistFromBioHubItemQueryResponse, Errors>> Handle(
        ListMaterialsForWorklistFromBioHubItemQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<Material> materials = await _readRepository.ListForShipmentRequestFromBioHub(query.BioHubFacilityId, cancellationToken);

            List<WorklistFromBioHubItemMaterialDto> worklistFromBioHubItemBioHubFacilityMaterials = new List<WorklistFromBioHubItemMaterialDto>();

            foreach (var material in materials)
            {
                WorklistFromBioHubItemMaterialDto item = new WorklistFromBioHubItemMaterialDto();
                item.Id = Guid.NewGuid();
                item.MaterialId = material.Id;
                item.MaterialNumber = material.ReferenceNumber;
                item.MaterialProductId = material.OriginalProductTypeId;
                item.MaterialName = material.Name;
                item.CollectionDate = material.CollectionDate;
                item.Location = material.Location;
                item.IsolationHostTypeId = material.IsolationHostTypeId;
                item.Gender = material.Gender;
                item.Age = material.Age;
                item.TransportCategoryId = material.TransportCategoryId;
                item.AvailableQuantity = material.CurrentNumberOfVials ?? 0;


                item.WorklistFromBioHubItemId = query.WorklistFromBioHubItemId;
                worklistFromBioHubItemBioHubFacilityMaterials.Add(item);
            }


            return new(new ListMaterialsForWorklistFromBioHubItemQueryResponse(worklistFromBioHubItemBioHubFacilityMaterials));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading ListMaterialsForWorklistFromBioHubItem with WorklistFromBioHubItemId {id}", query.WorklistFromBioHubItemId);
            throw;
        }
    }
}