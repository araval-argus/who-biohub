using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.CreateIsolationHostType;

public interface ICreateIsolationHostTypeHandler
{
    Task<Either<CreateIsolationHostTypeCommandResponse, Errors>> Handle(CreateIsolationHostTypeCommand command, CancellationToken cancellationToken);
}

public class CreateIsolationHostTypeHandler : ICreateIsolationHostTypeHandler
{
    private readonly ILogger<CreateIsolationHostTypeHandler> _logger;
    private readonly CreateIsolationHostTypeCommandValidator _validator;
    private readonly ICreateIsolationHostTypeMapper _mapper;
    private readonly IIsolationHostTypeWriteRepository _writeRepository;

    public CreateIsolationHostTypeHandler(
        ILogger<CreateIsolationHostTypeHandler> logger,
        CreateIsolationHostTypeCommandValidator validator,
        ICreateIsolationHostTypeMapper mapper,
        IIsolationHostTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateIsolationHostTypeCommandResponse, Errors>> Handle(
        CreateIsolationHostTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IsolationHostType isolationhosttype = _mapper.Map(command);

        try
        {
            Either<IsolationHostType, Errors> response = await _writeRepository.Create(isolationhosttype, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            IsolationHostType createdIsolationHostType =
                response.Left ?? throw new Exception("This is a bug: isolationhosttype value must be defined");
            return new(new CreateIsolationHostTypeCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new IsolationHostType");
            throw;
        }
    }
}