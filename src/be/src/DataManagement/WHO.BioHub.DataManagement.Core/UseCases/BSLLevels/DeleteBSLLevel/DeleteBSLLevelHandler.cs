using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.DeleteBSLLevel;

public interface IDeleteBSLLevelHandler
{
    Task<Either<DeleteBSLLevelCommandResponse, Errors>> Handle(DeleteBSLLevelCommand command, CancellationToken cancellationToken);
}

public class DeleteBSLLevelHandler : IDeleteBSLLevelHandler
{
    private readonly ILogger<DeleteBSLLevelHandler> _logger;
    private readonly DeleteBSLLevelCommandValidator _validator;
    private readonly IBSLLevelWriteRepository _writeRepository;

    public DeleteBSLLevelHandler(
        ILogger<DeleteBSLLevelHandler> logger,
        DeleteBSLLevelCommandValidator validator,
        IBSLLevelWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteBSLLevelCommandResponse, Errors>> Handle(
        DeleteBSLLevelCommand command,
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

            return new(new DeleteBSLLevelCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the BSLLevel with {id}", command.Id);
            throw;
        }
    }
}