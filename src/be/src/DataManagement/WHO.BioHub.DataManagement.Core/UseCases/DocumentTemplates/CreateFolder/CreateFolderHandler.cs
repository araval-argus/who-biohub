using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CreateFolder;

public interface ICreateFolderHandler
{
    Task<Either<CreateFolderCommandResponse, Errors>> Handle(CreateFolderCommand command, CancellationToken cancellationToken);
}

public class CreateFolderHandler : ICreateFolderHandler
{
    private readonly ILogger<CreateFolderHandler> _logger;
    private readonly CreateFolderCommandValidator _validator;
    private readonly ICreateFolderMapper _mapper;
    private readonly IDocumentTemplateWriteRepository _writeRepository;
    private readonly IUserReadRepository _userRepository;

    public CreateFolderHandler(
        ILogger<CreateFolderHandler> logger,
        CreateFolderCommandValidator validator,
        ICreateFolderMapper mapper,
        IDocumentTemplateWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateFolderCommandResponse, Errors>> Handle(
        CreateFolderCommand command,
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

            Either<DocumentTemplate, Errors> response = await _writeRepository.Create(documenttemplate, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            DocumentTemplate createdDocumentTemplate =
                response.Left ?? throw new Exception("This is a bug: documenttemplate value must be defined");
            return new(new CreateFolderCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new DocumentTemplate");
            throw;
        }
    }

    private Either<CreateFolderCommandResponse, Errors> GetValidationMessages(ValidationResult validationResult)
    {
        List<string> errors = new List<string>();
        foreach (ValidationFailure? error in validationResult.Errors)
        {
            errors.Add(error.ErrorMessage);
        }
        return new(new Errors(ErrorType.RequestParsing, errors.ToArray()));
    }
}