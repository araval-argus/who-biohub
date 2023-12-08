using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.CreateCultivabilityType;

public interface ICreateCultivabilityTypeHandler
{
    Task<Either<CreateCultivabilityTypeCommandResponse, Errors>> Handle(CreateCultivabilityTypeCommand command, CancellationToken cancellationToken);
}

public class CreateCultivabilityTypeHandler : ICreateCultivabilityTypeHandler
{
    private readonly ILogger<CreateCultivabilityTypeHandler> _logger;
    private readonly CreateCultivabilityTypeCommandValidator _validator;
    private readonly ICreateCultivabilityTypeMapper _mapper;
    private readonly ICultivabilityTypeWriteRepository _writeRepository;

    public CreateCultivabilityTypeHandler(
        ILogger<CreateCultivabilityTypeHandler> logger,
        CreateCultivabilityTypeCommandValidator validator,
        ICreateCultivabilityTypeMapper mapper,
        ICultivabilityTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateCultivabilityTypeCommandResponse, Errors>> Handle(
        CreateCultivabilityTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        CultivabilityType cultivabilitytype = _mapper.Map(command);

        try
        {
            Either<CultivabilityType, Errors> response = await _writeRepository.Create(cultivabilitytype, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            CultivabilityType createdCultivabilityType =
                response.Left ?? throw new Exception("This is a bug: cultivabilitytype value must be defined");
            return new(new CreateCultivabilityTypeCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new CultivabilityType");
            throw;
        }
    }
}