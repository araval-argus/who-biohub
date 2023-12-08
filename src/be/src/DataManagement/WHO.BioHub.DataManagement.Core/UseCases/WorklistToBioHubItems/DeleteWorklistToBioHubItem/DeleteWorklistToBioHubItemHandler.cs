using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.DeleteWorklistToBioHubItem;

public interface IDeleteWorklistToBioHubItemHandler
{
    Task<Either<DeleteWorklistToBioHubItemCommandResponse, Errors>> Handle(DeleteWorklistToBioHubItemCommand command, CancellationToken cancellationToken);
}

public class DeleteWorklistToBioHubItemHandler : IDeleteWorklistToBioHubItemHandler
{
    private readonly ILogger<DeleteWorklistToBioHubItemHandler> _logger;
    private readonly DeleteWorklistToBioHubItemCommandValidator _validator;
    private readonly IWorklistToBioHubItemWriteRepository _writeRepository;

    public DeleteWorklistToBioHubItemHandler(
        ILogger<DeleteWorklistToBioHubItemHandler> logger,
        DeleteWorklistToBioHubItemCommandValidator validator,
        IWorklistToBioHubItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteWorklistToBioHubItemCommandResponse, Errors>> Handle(
        DeleteWorklistToBioHubItemCommand command,
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

            return new(new DeleteWorklistToBioHubItemCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the WorklistToBioHubItem with {id}", command.Id);
            throw;
        }
    }
}