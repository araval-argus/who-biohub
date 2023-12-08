using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.CreateIsolationTechniqueType;

public interface ICreateIsolationTechniqueTypeHandler
{
    Task<Either<CreateIsolationTechniqueTypeCommandResponse, Errors>> Handle(CreateIsolationTechniqueTypeCommand command, CancellationToken cancellationToken);
}

public class CreateIsolationTechniqueTypeHandler : ICreateIsolationTechniqueTypeHandler
{
    private readonly ILogger<CreateIsolationTechniqueTypeHandler> _logger;
    private readonly CreateIsolationTechniqueTypeCommandValidator _validator;
    private readonly ICreateIsolationTechniqueTypeMapper _mapper;
    private readonly IIsolationTechniqueTypeWriteRepository _writeRepository;

    public CreateIsolationTechniqueTypeHandler(
        ILogger<CreateIsolationTechniqueTypeHandler> logger,
        CreateIsolationTechniqueTypeCommandValidator validator,
        ICreateIsolationTechniqueTypeMapper mapper,
        IIsolationTechniqueTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateIsolationTechniqueTypeCommandResponse, Errors>> Handle(
        CreateIsolationTechniqueTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IsolationTechniqueType isolationtechniquetype = _mapper.Map(command);

        try
        {
            Either<IsolationTechniqueType, Errors> response = await _writeRepository.Create(isolationtechniquetype, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            IsolationTechniqueType createdIsolationTechniqueType =
                response.Left ?? throw new Exception("This is a bug: isolationtechniquetype value must be defined");
            return new(new CreateIsolationTechniqueTypeCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new IsolationTechniqueType");
            throw;
        }
    }
}