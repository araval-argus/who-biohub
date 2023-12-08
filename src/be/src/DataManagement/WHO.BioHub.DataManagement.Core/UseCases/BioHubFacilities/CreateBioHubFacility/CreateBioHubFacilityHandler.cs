using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.CreateBioHubFacility;

public interface ICreateBioHubFacilityHandler
{
    Task<Either<CreateBioHubFacilityCommandResponse, Errors>> Handle(CreateBioHubFacilityCommand command, CancellationToken cancellationToken);
}

public class CreateBioHubFacilityHandler : ICreateBioHubFacilityHandler
{
    private readonly ILogger<CreateBioHubFacilityHandler> _logger;
    private readonly CreateBioHubFacilityCommandValidator _validator;
    private readonly ICreateBioHubFacilityMapper _mapper;
    private readonly IBioHubFacilityWriteRepository _writeRepository;

    public CreateBioHubFacilityHandler(
        ILogger<CreateBioHubFacilityHandler> logger,
        CreateBioHubFacilityCommandValidator validator,
        ICreateBioHubFacilityMapper mapper,
        IBioHubFacilityWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateBioHubFacilityCommandResponse, Errors>> Handle(
        CreateBioHubFacilityCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        BioHubFacility biohubfacility = _mapper.Map(command);

        try
        {
            Either<BioHubFacility, Errors> response = await _writeRepository.Create(biohubfacility, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            BioHubFacility createdBioHubFacility =
                response.Left ?? throw new Exception("This is a bug: biohubfacility value must be defined");
            return new(new CreateBioHubFacilityCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new BioHubFacility");
            throw;
        }
    }
}