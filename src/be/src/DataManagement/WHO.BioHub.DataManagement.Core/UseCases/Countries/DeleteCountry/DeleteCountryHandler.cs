using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.DeleteCountry;

public interface IDeleteCountryHandler
{
    Task<Either<DeleteCountryCommandResponse, Errors>> Handle(DeleteCountryCommand command, CancellationToken cancellationToken);
}

public class DeleteCountryHandler : IDeleteCountryHandler
{
    private readonly ILogger<DeleteCountryHandler> _logger;
    private readonly DeleteCountryCommandValidator _validator;
    private readonly ICountryWriteRepository _writeRepository;

    public DeleteCountryHandler(
        ILogger<DeleteCountryHandler> logger,
        DeleteCountryCommandValidator validator,
        ICountryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteCountryCommandResponse, Errors>> Handle(
        DeleteCountryCommand command,
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

            return new(new DeleteCountryCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the Country with {id}", command.Id);
            throw;
        }
    }
}