using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.ReadResourceFileToken;

public interface IReadResourceFileTokenHandler
{
    Task<Either<ReadResourceFileTokenQueryResponse, Errors>> Handle(ReadResourceFileTokenQuery query, CancellationToken cancellationToken);
}

public class ReadResourceFileTokenHandler : IReadResourceFileTokenHandler
{
    private readonly ILogger<ReadResourceFileTokenHandler> _logger;
    private readonly ReadResourceFileTokenQueryValidator _validator;
    private readonly IResourceReadRepository _readRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;

    public ReadResourceFileTokenHandler(
        ILogger<ReadResourceFileTokenHandler> logger,
        ReadResourceFileTokenQueryValidator validator,
        IResourceReadRepository readRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _storageAccountUtility = storageAccountUtility;
    }

    public async Task<Either<ReadResourceFileTokenQueryResponse, Errors>> Handle(
        ReadResourceFileTokenQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Resource resource = await _readRepository.Read(query.Id, cancellationToken);
            if (resource == null)
                return new(new Errors(ErrorType.NotFound, $"Resource with Id {query.Id} not found"));

            string fileId = $"{query.Id}.{resource.Extension}"; 

            string fileToken = _storageAccountUtility.GetPublicAccountStorageDownloadFileToken(fileId);

            return new(new ReadResourceFileTokenQueryResponse(FileToken: fileToken, FileCompleteName: $"{resource.Name}.{resource.Extension}"));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Resource with Id {id}", query.Id);
            throw;
        }
    }
}