using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.CreateBSLLevel;

public interface ICreateBSLLevelHandler
{
    Task<Either<CreateBSLLevelCommandResponse, Errors>> Handle(CreateBSLLevelCommand command, CancellationToken cancellationToken);
}

public class CreateBSLLevelHandler : ICreateBSLLevelHandler
{
    private readonly ILogger<CreateBSLLevelHandler> _logger;
    private readonly CreateBSLLevelCommandValidator _validator;
    private readonly ICreateBSLLevelMapper _mapper;
    private readonly IBSLLevelWriteRepository _writeRepository;

    public CreateBSLLevelHandler(
        ILogger<CreateBSLLevelHandler> logger,
        CreateBSLLevelCommandValidator validator,
        ICreateBSLLevelMapper mapper,
        IBSLLevelWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateBSLLevelCommandResponse, Errors>> Handle(
        CreateBSLLevelCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        BSLLevel bsllevel = _mapper.Map(command);

        try
        {
            Either<BSLLevel, Errors> response = await _writeRepository.Create(bsllevel, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            BSLLevel createdBSLLevel =
                response.Left ?? throw new Exception("This is a bug: bsllevel value must be defined");
            return new(new CreateBSLLevelCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new BSLLevel");
            throw;
        }
    }
}