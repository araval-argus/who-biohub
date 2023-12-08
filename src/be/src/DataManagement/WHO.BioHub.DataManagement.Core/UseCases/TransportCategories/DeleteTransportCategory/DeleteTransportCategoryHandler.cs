using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.DeleteTransportCategory;

public interface IDeleteTransportCategoryHandler
{
    Task<Either<DeleteTransportCategoryCommandResponse, Errors>> Handle(DeleteTransportCategoryCommand command, CancellationToken cancellationToken);
}

public class DeleteTransportCategoryHandler : IDeleteTransportCategoryHandler
{
    private readonly ILogger<DeleteTransportCategoryHandler> _logger;
    private readonly DeleteTransportCategoryCommandValidator _validator;
    private readonly ITransportCategoryWriteRepository _writeRepository;

    public DeleteTransportCategoryHandler(
        ILogger<DeleteTransportCategoryHandler> logger,
        DeleteTransportCategoryCommandValidator validator,
        ITransportCategoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteTransportCategoryCommandResponse, Errors>> Handle(
        DeleteTransportCategoryCommand command,
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

            return new(new DeleteTransportCategoryCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the TransportCategory with {id}", command.Id);
            throw;
        }
    }
}