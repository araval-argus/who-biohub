using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.DeleteResourceFileToken;

public interface IDeleteResourceFileTokenHandler
{
    Task<Either<DeleteResourceFileTokenQueryResponse, Errors>> Handle(DeleteResourceFileTokenQuery query, CancellationToken cancellationToken);
}

public class DeleteResourceFileTokenHandler : IDeleteResourceFileTokenHandler
{
    private readonly ILogger<DeleteResourceFileTokenHandler> _logger;
    private readonly DeleteResourceFileTokenQueryValidator _validator;
    private readonly IResourceReadRepository _readRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;

    public DeleteResourceFileTokenHandler(
        ILogger<DeleteResourceFileTokenHandler> logger,
        DeleteResourceFileTokenQueryValidator validator,
        IResourceReadRepository readRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _storageAccountUtility = storageAccountUtility;
    }

    public async Task<Either<DeleteResourceFileTokenQueryResponse, Errors>> Handle(
        DeleteResourceFileTokenQuery query,
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

            return new(new DeleteResourceFileTokenQueryResponse(fileToken));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Resource with Id {id}", query.Id);
            throw;
        }
    }
}