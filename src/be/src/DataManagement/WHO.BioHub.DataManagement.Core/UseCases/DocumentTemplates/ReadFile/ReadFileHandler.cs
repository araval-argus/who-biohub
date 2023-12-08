using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;


namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadFile;

public interface IReadFileHandler
{
    Task<Either<ReadFileQueryResponse, Errors>> Handle(ReadFileQuery query, CancellationToken cancellationToken);
}

public class ReadFileHandler : IReadFileHandler
{
    private readonly ILogger<ReadFileHandler> _logger;
    private readonly ReadFileQueryValidator _validator;
    private readonly IDocumentTemplateReadRepository _readRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;

    public ReadFileHandler(
        ILogger<ReadFileHandler> logger,
        ReadFileQueryValidator validator,
        IDocumentTemplateReadRepository readRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _storageAccountUtility = storageAccountUtility;
    }

    public async Task<Either<ReadFileQueryResponse, Errors>> Handle(
        ReadFileQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            DocumentTemplate documenttemplate = await _readRepository.Read(query.Id, cancellationToken);
            if (documenttemplate == null)
                return new(new Errors(ErrorType.NotFound, $"DocumentTemplate with Id {query.Id} not found"));
            var fileId = documenttemplate.Id.ToString() + "." + documenttemplate.Extension;
            var downloadedFile = await _storageAccountUtility.DownloadDocumentTemplate(fileId, documenttemplate.Name);

            if (downloadedFile.IsRight)
            {
                return new(new Errors(ErrorType.NotFound, $"File for DocumentTemplate with Id {query.Id} not found"));
            }

            return new(new ReadFileQueryResponse(DownloadedFile: downloadedFile?.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading DocumentTemplate with Id {id}", query.Id);
            throw;
        }
    }
}