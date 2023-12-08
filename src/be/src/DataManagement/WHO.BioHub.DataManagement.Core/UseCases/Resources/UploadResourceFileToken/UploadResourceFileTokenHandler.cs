using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.UploadResourceFileToken;

public interface IUploadResourceFileTokenHandler
{
    Task<Either<UploadResourceFileTokenQueryResponse, Errors>> Handle(UploadResourceFileTokenQuery query, CancellationToken cancellationToken);
}

public class UploadResourceFileTokenHandler : IUploadResourceFileTokenHandler
{
    private readonly ILogger<UploadResourceFileTokenHandler> _logger;
    private readonly UploadResourceFileTokenQueryValidator _validator;
    private readonly IResourceReadRepository _readRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;

    public UploadResourceFileTokenHandler(
        ILogger<UploadResourceFileTokenHandler> logger,
        UploadResourceFileTokenQueryValidator validator,
        IResourceReadRepository readRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _storageAccountUtility = storageAccountUtility;
    }

    public async Task<Either<UploadResourceFileTokenQueryResponse, Errors>> Handle(
        UploadResourceFileTokenQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Guid id = Guid.NewGuid();            

            string extension = GetFileExtension(query.FileCompleteName);
           
            string fileId = $"{id}.{extension}"; 

            string fileToken = _storageAccountUtility.GetPublicAccountStorageUploadFileToken(fileId);

            return new(new UploadResourceFileTokenQueryResponse(Id: id, FileToken: fileToken, FileCompleteName: query.FileCompleteName));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Resource with name {name}", query.FileCompleteName);
            throw;
        }
    }
    private string GetFileExtension(string fileCompleteName)
    {
        var nameParts = fileCompleteName.Split(".");

        return nameParts.LastOrDefault() ?? string.Empty;
    }
}