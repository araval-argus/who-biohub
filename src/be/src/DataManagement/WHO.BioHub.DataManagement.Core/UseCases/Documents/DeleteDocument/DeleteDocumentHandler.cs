using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.DeleteDocument;

public interface IDeleteDocumentHandler
{
    Task<Either<DeleteDocumentCommandResponse, Errors>> Handle(DeleteDocumentCommand command, CancellationToken cancellationToken);
}

public class DeleteDocumentHandler : IDeleteDocumentHandler
{
    private readonly ILogger<DeleteDocumentHandler> _logger;
    private readonly DeleteDocumentCommandValidator _validator;
    private readonly IDocumentWriteRepository _writeRepository;

    public DeleteDocumentHandler(
        ILogger<DeleteDocumentHandler> logger,
        DeleteDocumentCommandValidator validator,
        IDocumentWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteDocumentCommandResponse, Errors>> Handle(
        DeleteDocumentCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Errors? errors = await _writeRepository.Delete(command.Id, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new DeleteDocumentCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the Document with {id}", command.Id);
            throw;
        }
    }
}