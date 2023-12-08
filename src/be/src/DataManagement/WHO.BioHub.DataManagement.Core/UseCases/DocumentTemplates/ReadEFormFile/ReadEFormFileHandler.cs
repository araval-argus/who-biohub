using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadEFormFile;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;


namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadEFormFile;

public interface IReadEFormFileHandler
{
    Task<Either<ReadEFormFileQueryResponse, Errors>> Handle(ReadEFormFileQuery query, CancellationToken cancellationToken);
}

public class ReadEFormFileHandler : IReadEFormFileHandler
{
    private readonly ILogger<ReadEFormFileHandler> _logger;
    private readonly ReadEFormFileQueryValidator _validator;
    private readonly IDocumentTemplateReadRepository _readRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;

    public ReadEFormFileHandler(
        ILogger<ReadEFormFileHandler> logger,
        ReadEFormFileQueryValidator validator,
        IDocumentTemplateReadRepository readRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _storageAccountUtility = storageAccountUtility;
    }

    public async Task<Either<ReadEFormFileQueryResponse, Errors>> Handle(
        ReadEFormFileQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            DocumentTemplate documenttemplate = await _readRepository.ReadEFormTemplate(query.Id, cancellationToken);
            if (documenttemplate == null)
                return new(new Errors(ErrorType.NotFound, $"DocumentTemplate with Id {query.Id} not found"));
            var fileId = documenttemplate.Id.ToString() + "." + documenttemplate.Extension;
            var downloadedEFormFile = await _storageAccountUtility.DownloadDocumentTemplate(fileId, documenttemplate.Name);

            if (downloadedEFormFile.IsRight)
            {
                return new(new Errors(ErrorType.NotFound, $"EFormFile for DocumentTemplate with Id {query.Id} not found"));
            }

            return new(new ReadEFormFileQueryResponse(DownloadedFile: downloadedEFormFile?.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading DocumentTemplate with Id {id}", query.Id);
            throw;
        }
    }
}