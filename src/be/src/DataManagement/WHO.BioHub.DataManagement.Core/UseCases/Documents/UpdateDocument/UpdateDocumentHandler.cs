using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.UpdateDocument;

public interface IUpdateDocumentHandler
{
    Task<Either<UpdateDocumentCommandResponse, Errors>> Handle(UpdateDocumentCommand command, CancellationToken cancellationToken);
}

public class UpdateDocumentHandler : IUpdateDocumentHandler
{
    private readonly ILogger<UpdateDocumentHandler> _logger;
    private readonly UpdateDocumentCommandValidator _validator;
    private readonly IUpdateDocumentMapper _mapper;
    private readonly IDocumentWriteRepository _writeRepository;

    public UpdateDocumentHandler(
        ILogger<UpdateDocumentHandler> logger,
        UpdateDocumentCommandValidator validator,
        IUpdateDocumentMapper mapper,
        IDocumentWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateDocumentCommandResponse, Errors>> Handle(
        UpdateDocumentCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Document document = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            document = _mapper.Map(document, command);

            Errors? errors = await _writeRepository.Update(document, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateDocumentCommandResponse(document.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Document");
            throw;
        }
    }
}