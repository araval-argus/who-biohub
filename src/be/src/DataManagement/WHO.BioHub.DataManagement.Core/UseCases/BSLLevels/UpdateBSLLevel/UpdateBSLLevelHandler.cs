using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.UpdateBSLLevel;

public interface IUpdateBSLLevelHandler
{
    Task<Either<UpdateBSLLevelCommandResponse, Errors>> Handle(UpdateBSLLevelCommand command, CancellationToken cancellationToken);
}

public class UpdateBSLLevelHandler : IUpdateBSLLevelHandler
{
    private readonly ILogger<UpdateBSLLevelHandler> _logger;
    private readonly UpdateBSLLevelCommandValidator _validator;
    private readonly IUpdateBSLLevelMapper _mapper;
    private readonly IBSLLevelWriteRepository _writeRepository;

    public UpdateBSLLevelHandler(
        ILogger<UpdateBSLLevelHandler> logger,
        UpdateBSLLevelCommandValidator validator,
        IUpdateBSLLevelMapper mapper,
        IBSLLevelWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateBSLLevelCommandResponse, Errors>> Handle(
        UpdateBSLLevelCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            BSLLevel bsllevel = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            bsllevel = _mapper.Map(bsllevel, command);

            Errors? errors = await _writeRepository.Update(bsllevel, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateBSLLevelCommandResponse(bsllevel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new BSLLevel");
            throw;
        }
    }
}