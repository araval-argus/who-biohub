using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.ListResources;

public interface IListResourcesHandler
{
    Task<Either<ListResourcesQueryResponse, Errors>> Handle(ListResourcesQuery query, CancellationToken cancellationToken);
}

public class ListResourcesHandler : IListResourcesHandler
{
    private readonly ILogger<ListResourcesHandler> _logger;
    private readonly ListResourcesQueryValidator _validator;
    private readonly IResourceReadRepository _readRepository;

    public ListResourcesHandler(
        ILogger<ListResourcesHandler> logger,
        ListResourcesQueryValidator validator,
        IResourceReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListResourcesQueryResponse, Errors>> Handle(
        ListResourcesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<Resource> resources = await _readRepository.List(query.Id, cancellationToken);
            List<ResourceItem> resourcesItem = new List<ResourceItem>();
            foreach (Resource resource in resources)
            {
                ResourceItem documentTemplateItem = new ResourceItem();
                documentTemplateItem.Id = resource.Id;
                documentTemplateItem.Name = resource.Name;
                documentTemplateItem.ParentId = resource.ParentId;
                documentTemplateItem.UploadTime = resource.UploadTime;
                documentTemplateItem.UploadedBy = resource.Type == ResourceType.File ? resource.UploadedBy.FirstName + " " + resource.UploadedBy.LastName : String.Empty;
                documentTemplateItem.UploadedById = resource.UploadedById;
                documentTemplateItem.Type = resource.Type;
                documentTemplateItem.FileType = resource.FileType;
                documentTemplateItem.Current = resource.Current;
                documentTemplateItem.Extension = resource.Extension;
                resourcesItem.Add(documentTemplateItem);
            }
            resourcesItem = resourcesItem.OrderByDescending(x => x.UploadTime).ToList();
            return new(new ListResourcesQueryResponse(resourcesItem));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Resources");
            throw;
        }
    }
}