using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UpdateDocumentTemplate;

public interface IUpdateDocumentTemplateHandler
{
    Task<Either<UpdateDocumentTemplateCommandResponse, Errors>> Handle(UpdateDocumentTemplateCommand command, CancellationToken cancellationToken);
}

public class UpdateDocumentTemplateHandler : IUpdateDocumentTemplateHandler
{
    private readonly ILogger<UpdateDocumentTemplateHandler> _logger;
    private readonly UpdateDocumentTemplateCommandValidator _validator;
    private readonly IUpdateDocumentTemplateMapper _mapper;
    private readonly IDocumentTemplateWriteRepository _writeRepository;

    public UpdateDocumentTemplateHandler(
        ILogger<UpdateDocumentTemplateHandler> logger,
        UpdateDocumentTemplateCommandValidator validator,
        IUpdateDocumentTemplateMapper mapper,
        IDocumentTemplateWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateDocumentTemplateCommandResponse, Errors>> Handle(
        UpdateDocumentTemplateCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return GetValidationMessages(validationResult);
        }

        try
        {
            DocumentTemplate documenttemplate = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            if (documenttemplate == null)
                return new(new Errors(ErrorType.NotFound, $"DocumentTemplate with Id {command.Id} not found"));

            documenttemplate.Name = command.Name;
            documenttemplate.Current = command.Current;

            Errors? errors = await _writeRepository.Update(documenttemplate, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            if (documenttemplate.Type == DocumentTemplateType.File && documenttemplate.Current == true)
            {
                errors = await _writeRepository.SetOffOtherCurrents(documenttemplate.Id, documenttemplate.FileType.GetValueOrDefault(), cancellationToken);
                if (errors.HasValue)
                    return new(errors.Value);
            }

            return new(new UpdateDocumentTemplateCommandResponse(documenttemplate.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new DocumentTemplate");
            throw;
        }
    }

    private Either<UpdateDocumentTemplateCommandResponse, Errors> GetValidationMessages(ValidationResult validationResult)
    {
        List<string> errors = new List<string>();
        foreach (ValidationFailure? error in validationResult.Errors)
        {
            errors.Add(error.ErrorMessage);
        }
        return new(new Errors(ErrorType.RequestParsing, errors.ToArray()));
    }
}