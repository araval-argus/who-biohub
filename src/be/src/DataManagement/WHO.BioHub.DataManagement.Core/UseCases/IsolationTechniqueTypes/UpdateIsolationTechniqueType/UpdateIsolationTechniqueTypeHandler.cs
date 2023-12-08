using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.UpdateIsolationTechniqueType;

public interface IUpdateIsolationTechniqueTypeHandler
{
    Task<Either<UpdateIsolationTechniqueTypeCommandResponse, Errors>> Handle(UpdateIsolationTechniqueTypeCommand command, CancellationToken cancellationToken);
}

public class UpdateIsolationTechniqueTypeHandler : IUpdateIsolationTechniqueTypeHandler
{
    private readonly ILogger<UpdateIsolationTechniqueTypeHandler> _logger;
    private readonly UpdateIsolationTechniqueTypeCommandValidator _validator;
    private readonly IUpdateIsolationTechniqueTypeMapper _mapper;
    private readonly IIsolationTechniqueTypeWriteRepository _writeRepository;

    public UpdateIsolationTechniqueTypeHandler(
        ILogger<UpdateIsolationTechniqueTypeHandler> logger,
        UpdateIsolationTechniqueTypeCommandValidator validator,
        IUpdateIsolationTechniqueTypeMapper mapper,
        IIsolationTechniqueTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateIsolationTechniqueTypeCommandResponse, Errors>> Handle(
        UpdateIsolationTechniqueTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IsolationTechniqueType isolationtechniquetype = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            isolationtechniquetype = _mapper.Map(isolationtechniquetype, command);

            Errors? errors = await _writeRepository.Update(isolationtechniquetype, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateIsolationTechniqueTypeCommandResponse(isolationtechniquetype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new IsolationTechniqueType");
            throw;
        }
    }
}