using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.DeleteWorklistFromBioHubItem;

public interface IDeleteWorklistFromBioHubItemHandler
{
    Task<Either<DeleteWorklistFromBioHubItemCommandResponse, Errors>> Handle(DeleteWorklistFromBioHubItemCommand command, CancellationToken cancellationToken);
}

public class DeleteWorklistFromBioHubItemHandler : IDeleteWorklistFromBioHubItemHandler
{
    private readonly ILogger<DeleteWorklistFromBioHubItemHandler> _logger;
    private readonly DeleteWorklistFromBioHubItemCommandValidator _validator;
    private readonly IWorklistFromBioHubItemWriteRepository _writeRepository;

    public DeleteWorklistFromBioHubItemHandler(
        ILogger<DeleteWorklistFromBioHubItemHandler> logger,
        DeleteWorklistFromBioHubItemCommandValidator validator,
        IWorklistFromBioHubItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteWorklistFromBioHubItemCommandResponse, Errors>> Handle(
        DeleteWorklistFromBioHubItemCommand command,
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

            return new(new DeleteWorklistFromBioHubItemCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the WorklistFromBioHubItem with {id}", command.Id);
            throw;
        }
    }
}