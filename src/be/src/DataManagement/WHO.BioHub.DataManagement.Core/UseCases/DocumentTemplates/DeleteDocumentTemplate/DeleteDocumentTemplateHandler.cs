using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.DeleteDocumentTemplate;

public interface IDeleteDocumentTemplateHandler
{
    Task<Either<DeleteDocumentTemplateCommandResponse, Errors>> Handle(DeleteDocumentTemplateCommand command, CancellationToken cancellationToken);
}

public class DeleteDocumentTemplateHandler : IDeleteDocumentTemplateHandler
{
    private readonly ILogger<DeleteDocumentTemplateHandler> _logger;
    private readonly DeleteDocumentTemplateCommandValidator _validator;
    private readonly IDocumentTemplateWriteRepository _writeRepository;
    private readonly IDocumentTemplateReadRepository _readRepository;

    public DeleteDocumentTemplateHandler(
        ILogger<DeleteDocumentTemplateHandler> logger,
        DeleteDocumentTemplateCommandValidator validator,
        IDocumentTemplateWriteRepository writeRepository,
        IDocumentTemplateReadRepository readRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<Either<DeleteDocumentTemplateCommandResponse, Errors>> Handle(
        DeleteDocumentTemplateCommand command,
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

            Errors? errors;
            if (documenttemplate.Type == DocumentTemplateType.Folder)
            {
                List<Guid> totalIdsToDelete = new List<Guid>() { command.Id };

                List<Guid> currentLevelIdsToDelete = await _writeRepository.GetIdsForDelete(totalIdsToDelete, cancellationToken);

                while (currentLevelIdsToDelete.Any())
                {
                    totalIdsToDelete.AddRange(currentLevelIdsToDelete);
                    currentLevelIdsToDelete = await _writeRepository.GetIdsForDelete(currentLevelIdsToDelete, cancellationToken);
                }

                var folderContainsCurrent = await _readRepository.ContainsCurrent(totalIdsToDelete, cancellationToken);

                if (folderContainsCurrent)
                {
                    return new(new Errors(ErrorType.RequestParsing, $"DocumentTemplate folder contains currently used files"));
                }

                errors = await _writeRepository.DeleteRange(totalIdsToDelete, cancellationToken);
            }
            else
            {
                if (documenttemplate.Current == true)
                {
                    return new(new Errors(ErrorType.RequestParsing, $"DocumentTemplate file is currently used"));
                }
                errors = await _writeRepository.Delete(command.Id, cancellationToken);
            }


            if (errors.HasValue)
                return new(errors.Value);

            return new(new DeleteDocumentTemplateCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the DocumentTemplate with {id}", command.Id);
            throw;
        }
    }

    private Either<DeleteDocumentTemplateCommandResponse, Errors> GetValidationMessages(ValidationResult validationResult)
    {
        List<string> errors = new List<string>();
        foreach (ValidationFailure? error in validationResult.Errors)
        {
            errors.Add(error.ErrorMessage);
        }
        return new(new Errors(ErrorType.RequestParsing, errors.ToArray()));
    }
}