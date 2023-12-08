using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.DeleteWorklistToBioHubEmail;

public interface IDeleteWorklistToBioHubEmailHandler
{
    Task<Either<DeleteWorklistToBioHubEmailCommandResponse, Errors>> Handle(DeleteWorklistToBioHubEmailCommand command, CancellationToken cancellationToken);
}

public class DeleteWorklistToBioHubEmailHandler : IDeleteWorklistToBioHubEmailHandler
{
    private readonly ILogger<DeleteWorklistToBioHubEmailHandler> _logger;
    private readonly DeleteWorklistToBioHubEmailCommandValidator _validator;
    private readonly IWorklistToBioHubEmailWriteRepository _writeRepository;

    public DeleteWorklistToBioHubEmailHandler(
        ILogger<DeleteWorklistToBioHubEmailHandler> logger,
        DeleteWorklistToBioHubEmailCommandValidator validator,
        IWorklistToBioHubEmailWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteWorklistToBioHubEmailCommandResponse, Errors>> Handle(
        DeleteWorklistToBioHubEmailCommand command,
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

            return new(new DeleteWorklistToBioHubEmailCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the WorklistToBioHubEmail with {id}", command.Id);
            throw;
        }
    }
}