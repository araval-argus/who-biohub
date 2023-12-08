using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratoryFromUserRequest;

public interface ICreateLaboratoryFromUserRequestHandler
{
    Task<Either<CreateLaboratoryFromUserRequestCommandResponse, Errors>> Handle(CreateLaboratoryFromUserRequestCommand command, CancellationToken cancellationToken);
}

public class CreateLaboratoryFromUserRequestHandler : ICreateLaboratoryFromUserRequestHandler
{
    private readonly ILogger<CreateLaboratoryFromUserRequestHandler> _logger;
    private readonly CreateLaboratoryFromUserRequestCommandValidator _validator;
    private readonly ICreateLaboratoryFromUserRequestMapper _mapper;
    private readonly ILaboratoryWriteRepository _writeRepository;
    private readonly ICountryReadRepository _countryReadRepository;

    public CreateLaboratoryFromUserRequestHandler(
        ILogger<CreateLaboratoryFromUserRequestHandler> logger,
        CreateLaboratoryFromUserRequestCommandValidator validator,
        ICreateLaboratoryFromUserRequestMapper mapper,
        ILaboratoryWriteRepository writeRepository,
        ICountryReadRepository countryReadRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _countryReadRepository = countryReadRepository;
    }

    public async Task<Either<CreateLaboratoryFromUserRequestCommandResponse, Errors>> Handle(
        CreateLaboratoryFromUserRequestCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        Country country = await _countryReadRepository.Read(command.CountryId.GetValueOrDefault(), cancellationToken);

        Laboratory laboratory = _mapper.Map(command);
        laboratory.Latitude = country.Latitude;
        laboratory.Longitude = country.Longitude;

        try
        {
            Either<Laboratory, Errors> response = await _writeRepository.Create(laboratory, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            Laboratory createdLaboratory =
                response.Left ?? throw new Exception("This is a bug: laboratory value must be defined");
            return new(new CreateLaboratoryFromUserRequestCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Laboratory");
            throw;
        }
    }
}