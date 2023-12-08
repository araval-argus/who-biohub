using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.Core.UseCases.Resources.ListResources;

public interface IListResourcesHandler
{
    Task<Either<ListResourcesQueryResponse, Errors>> Handle(ListResourcesQuery query, CancellationToken cancellationToken);
}

public class ListResourcesHandler : IListResourcesHandler
{
    private readonly ILogger<ListResourcesHandler> _logger;
    private readonly ListResourcesQueryValidator _validator;
    private readonly IResourcePublicReadRepository _readRepository;

    public ListResourcesHandler(
        ILogger<ListResourcesHandler> logger,
        ListResourcesQueryValidator validator,
        IResourcePublicReadRepository readRepository)
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
            List<ResourcePublicItem> resourcesItem = new List<ResourcePublicItem>();
            foreach (Resource resource in resources)
            {
                ResourcePublicItem resourceItem = new ResourcePublicItem();
                resourceItem.Id = resource.Id;
                resourceItem.Name = resource.Name;
                resourceItem.ParentId = resource.ParentId;                
                resourceItem.Type = resource.Type;
                resourceItem.FileType = resource.FileType;               
                resourceItem.Extension = resource.Extension;
                resourceItem.FileCompleteName = resource.Type == ResourceType.Folder ? resource.Name : $"{resource.Name}.{resource.Extension}";
                resourcesItem.Add(resourceItem);
            }
            
            return new(new ListResourcesQueryResponse(resourcesItem));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Resources");
            throw;
        }
    }
}