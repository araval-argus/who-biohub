using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;


namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UploadFile;

public interface IUploadFileHandler
{
    Task<Either<UploadFileCommandResponse, Errors>> Handle(UploadFileCommand command, CancellationToken cancellationToken);
}

public class UploadFileHandler : IUploadFileHandler
{
    private readonly ILogger<UploadFileHandler> _logger;
    private readonly UploadFileCommandValidator _validator;
    private readonly IUploadFileMapper _mapper;
    private readonly IDocumentTemplateWriteRepository _writeRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;

    public UploadFileHandler(
        ILogger<UploadFileHandler> logger,
        UploadFileCommandValidator validator,
        IUploadFileMapper mapper,
        IDocumentTemplateWriteRepository writeRepository,
    IStorageAccountUtility storageAccountUtility
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _storageAccountUtility = storageAccountUtility;
    }

    public async Task<Either<UploadFileCommandResponse, Errors>> Handle(
        UploadFileCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return GetValidationMessages(validationResult);
        }


        DocumentTemplate documenttemplate = _mapper.Map(command);

        try
        {
            documenttemplate.Current = await _writeRepository.IsCurrentForUpload(command.DocumentTemplateFileType.GetValueOrDefault(), cancellationToken);

            var fileId = documenttemplate.Id.ToString() + "." + documenttemplate.Extension;

            var uploadedFile = await _storageAccountUtility.UploadDocumentTemplate(command.File, fileId);

            if (!uploadedFile)
            {
                return new(new Errors(ErrorType.Internal, "Error Uploading file"));
            }

            Either<DocumentTemplate, Errors> response = await _writeRepository.Create(documenttemplate, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            DocumentTemplate createdDocumentTemplate =
                response.Left ?? throw new Exception("This is a bug: documenttemplate value must be defined");
            return new(new UploadFileCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new DocumentTemplate");
            throw;
        }
    }

    private Either<UploadFileCommandResponse, Errors> GetValidationMessages(ValidationResult validationResult)
    {
        List<string> errors = new List<string>();
        foreach (ValidationFailure? error in validationResult.Errors)
        {
            errors.Add(error.ErrorMessage);
        }
        return new(new Errors(ErrorType.RequestParsing, errors.ToArray()));
    }
}