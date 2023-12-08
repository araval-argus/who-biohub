using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.CreateDocument;

public interface ICreateDocumentHandler
{
    Task<Either<CreateDocumentCommandResponse, Errors>> Handle(CreateDocumentCommand command, CancellationToken cancellationToken);
}

public class CreateDocumentHandler : ICreateDocumentHandler
{
    private readonly ILogger<CreateDocumentHandler> _logger;
    private readonly CreateDocumentCommandValidator _validator;
    private readonly ICreateDocumentMapper _mapper;
    private readonly IDocumentWriteRepository _writeRepository;

    public CreateDocumentHandler(
        ILogger<CreateDocumentHandler> logger,
        CreateDocumentCommandValidator validator,
        ICreateDocumentMapper mapper,
        IDocumentWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateDocumentCommandResponse, Errors>> Handle(
        CreateDocumentCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        Document document = _mapper.Map(command);

        try
        {
            Either<Document, Errors> response = await _writeRepository.Create(document, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            Document createdDocument =
                response.Left ?? throw new Exception("This is a bug: document value must be defined");
            return new(new CreateDocumentCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Document");
            throw;
        }
    }
}