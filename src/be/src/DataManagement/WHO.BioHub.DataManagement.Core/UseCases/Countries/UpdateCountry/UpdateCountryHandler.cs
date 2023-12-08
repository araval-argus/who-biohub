using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.UpdateCountry;

public interface IUpdateCountryHandler
{
    Task<Either<UpdateCountryCommandResponse, Errors>> Handle(UpdateCountryCommand command, CancellationToken cancellationToken);
}

public class UpdateCountryHandler : IUpdateCountryHandler
{
    private readonly ILogger<UpdateCountryHandler> _logger;
    private readonly UpdateCountryCommandValidator _validator;
    private readonly IUpdateCountryMapper _mapper;
    private readonly ICountryWriteRepository _writeRepository;

    public UpdateCountryHandler(
        ILogger<UpdateCountryHandler> logger,
        UpdateCountryCommandValidator validator,
        IUpdateCountryMapper mapper,
        ICountryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateCountryCommandResponse, Errors>> Handle(
        UpdateCountryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Country country = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            country = _mapper.Map(country, command);

            Errors? errors = await _writeRepository.Update(country, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateCountryCommandResponse(country));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Country");
            throw;
        }
    }
}