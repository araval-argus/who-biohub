using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.UpdateCultivabilityType;

public interface IUpdateCultivabilityTypeHandler
{
    Task<Either<UpdateCultivabilityTypeCommandResponse, Errors>> Handle(UpdateCultivabilityTypeCommand command, CancellationToken cancellationToken);
}

public class UpdateCultivabilityTypeHandler : IUpdateCultivabilityTypeHandler
{
    private readonly ILogger<UpdateCultivabilityTypeHandler> _logger;
    private readonly UpdateCultivabilityTypeCommandValidator _validator;
    private readonly IUpdateCultivabilityTypeMapper _mapper;
    private readonly ICultivabilityTypeWriteRepository _writeRepository;

    public UpdateCultivabilityTypeHandler(
        ILogger<UpdateCultivabilityTypeHandler> logger,
        UpdateCultivabilityTypeCommandValidator validator,
        IUpdateCultivabilityTypeMapper mapper,
        ICultivabilityTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateCultivabilityTypeCommandResponse, Errors>> Handle(
        UpdateCultivabilityTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            CultivabilityType cultivabilitytype = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            cultivabilitytype = _mapper.Map(cultivabilitytype, command);

            Errors? errors = await _writeRepository.Update(cultivabilitytype, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateCultivabilityTypeCommandResponse(cultivabilitytype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new CultivabilityType");
            throw;
        }
    }
}