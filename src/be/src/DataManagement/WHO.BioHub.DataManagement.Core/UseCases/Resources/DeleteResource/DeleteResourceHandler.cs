using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.DeleteResource;

public interface IDeleteResourceHandler
{
    Task<Either<DeleteResourceCommandResponse, Errors>> Handle(DeleteResourceCommand command, CancellationToken cancellationToken);
}

public class DeleteResourceHandler : IDeleteResourceHandler
{
    private readonly ILogger<DeleteResourceHandler> _logger;
    private readonly DeleteResourceCommandValidator _validator;
    private readonly IResourceWriteRepository _writeRepository;
    private readonly IResourceReadRepository _readRepository;

    public DeleteResourceHandler(
        ILogger<DeleteResourceHandler> logger,
        DeleteResourceCommandValidator validator,
        IResourceWriteRepository writeRepository,
        IResourceReadRepository readRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<Either<DeleteResourceCommandResponse, Errors>> Handle(
        DeleteResourceCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return GetValidationMessages(validationResult);
        }

        try
        {
            Resource resource = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (resource == null)
                return new(new Errors(ErrorType.NotFound, $"Resource with Id {command.Id} not found"));

            Errors? errors;
            if (resource.Type == ResourceType.Folder)
            {
                List<Guid> totalIdsToDelete = new List<Guid>() { command.Id };

                List<Guid> currentLevelIdsToDelete = await _writeRepository.GetIdsForDelete(totalIdsToDelete, cancellationToken);

                while (currentLevelIdsToDelete.Any())
                {
                    totalIdsToDelete.AddRange(currentLevelIdsToDelete);
                    currentLevelIdsToDelete = await _writeRepository.GetIdsForDelete(currentLevelIdsToDelete, cancellationToken);
                }                

                errors = await _writeRepository.DeleteRange(totalIdsToDelete, cancellationToken);
            }
            else
            {               
                errors = await _writeRepository.Delete(command.Id, cancellationToken);
            }


            if (errors.HasValue)
                return new(errors.Value);

            return new(new DeleteResourceCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the Resource with {id}", command.Id);
            throw;
        }
    }

    private Either<DeleteResourceCommandResponse, Errors> GetValidationMessages(ValidationResult validationResult)
    {
        List<string> errors = new List<string>();
        foreach (ValidationFailure? error in validationResult.Errors)
        {
            errors.Add(error.ErrorMessage);
        }
        return new(new Errors(ErrorType.RequestParsing, errors.ToArray()));
    }
}