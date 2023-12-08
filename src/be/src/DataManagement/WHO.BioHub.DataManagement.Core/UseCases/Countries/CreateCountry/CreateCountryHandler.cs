using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.CreateCountry;

public interface ICreateCountryHandler
{
    Task<Either<CreateCountryCommandResponse, Errors>> Handle(CreateCountryCommand command, CancellationToken cancellationToken);
}

public class CreateCountryHandler : ICreateCountryHandler
{
    private readonly ILogger<CreateCountryHandler> _logger;
    private readonly CreateCountryCommandValidator _validator;
    private readonly ICreateCountryMapper _mapper;
    private readonly ICountryWriteRepository _writeRepository;

    public CreateCountryHandler(
        ILogger<CreateCountryHandler> logger,
        CreateCountryCommandValidator validator,
        ICreateCountryMapper mapper,
        ICountryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateCountryCommandResponse, Errors>> Handle(
        CreateCountryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        Country country = _mapper.Map(command);

        try
        {
            Either<Country, Errors> response = await _writeRepository.Create(country, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            Country createdCountry =
                response.Left ?? throw new Exception("This is a bug: country value must be defined");
            return new(new CreateCountryCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Country");
            throw;
        }
    }
}