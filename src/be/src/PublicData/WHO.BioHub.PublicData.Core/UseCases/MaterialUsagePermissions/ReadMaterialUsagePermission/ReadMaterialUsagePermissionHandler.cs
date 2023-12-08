using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialUsagePermissions.ReadMaterialUsagePermission;

public interface IReadMaterialUsagePermissionHandler
{
    Task<Either<ReadMaterialUsagePermissionQueryResponse, Errors>> Handle(ReadMaterialUsagePermissionQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialUsagePermissionHandler : IReadMaterialUsagePermissionHandler
{
    private readonly ILogger<ReadMaterialUsagePermissionHandler> _logger;
    private readonly ReadMaterialUsagePermissionQueryValidator _validator;
    private readonly IMaterialUsagePermissionPublicReadRepository _readRepository;

    public ReadMaterialUsagePermissionHandler(
        ILogger<ReadMaterialUsagePermissionHandler> logger,
        ReadMaterialUsagePermissionQueryValidator validator,
        IMaterialUsagePermissionPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadMaterialUsagePermissionQueryResponse, Errors>> Handle(
        ReadMaterialUsagePermissionQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialUsagePermission materialusagepermission = await _readRepository.Read(query.Id, cancellationToken);
            if (materialusagepermission == null)
                return new(new Errors(ErrorType.NotFound, $"MaterialUsagePermission with Id {query.Id} not found"));

            return new(new ReadMaterialUsagePermissionQueryResponse(materialusagepermission));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading MaterialUsagePermission with Id {id}", query.Id);
            throw;
        }
    }
}