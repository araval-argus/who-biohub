using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;

public interface IListMaterialUsagePermissionsHandler
{
    Task<Either<ListMaterialUsagePermissionsQueryResponse, Errors>> Handle(ListMaterialUsagePermissionsQuery query, CancellationToken cancellationToken);
}

public class ListMaterialUsagePermissionsHandler : IListMaterialUsagePermissionsHandler
{
    private readonly ILogger<ListMaterialUsagePermissionsHandler> _logger;
    private readonly ListMaterialUsagePermissionsQueryValidator _validator;
    private readonly IMaterialUsagePermissionPublicReadRepository _readRepository;

    public ListMaterialUsagePermissionsHandler(
        ILogger<ListMaterialUsagePermissionsHandler> logger,
        ListMaterialUsagePermissionsQueryValidator validator,
        IMaterialUsagePermissionPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListMaterialUsagePermissionsQueryResponse, Errors>> Handle(
        ListMaterialUsagePermissionsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<MaterialUsagePermission> materialusagepermissions = await _readRepository.List(cancellationToken);
            var materialUsagePermissionDtos = new List<MaterialUsagePermissionPublicDto>();
            foreach (var materialUsagePermission in materialusagepermissions)
            {
                MaterialUsagePermissionPublicDto materialUsagePermissionDto = new()
                {
                    Id = materialUsagePermission.Id,
                    Name = materialUsagePermission.Name,
                    Description = materialUsagePermission.Description,
                    IsActive = materialUsagePermission.IsActive
                };

                materialUsagePermissionDtos.Add(materialUsagePermissionDto);
            }
            return new(new ListMaterialUsagePermissionsQueryResponse(materialUsagePermissionDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all MaterialUsagePermissions");
            throw;
        }
    }
}