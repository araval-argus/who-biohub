using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.UpdateIsolationHostType;

public interface IUpdateIsolationHostTypeHandler
{
    Task<Either<UpdateIsolationHostTypeCommandResponse, Errors>> Handle(UpdateIsolationHostTypeCommand command, CancellationToken cancellationToken);
}

public class UpdateIsolationHostTypeHandler : IUpdateIsolationHostTypeHandler
{
    private readonly ILogger<UpdateIsolationHostTypeHandler> _logger;
    private readonly UpdateIsolationHostTypeCommandValidator _validator;
    private readonly IUpdateIsolationHostTypeMapper _mapper;
    private readonly IIsolationHostTypeWriteRepository _writeRepository;

    public UpdateIsolationHostTypeHandler(
        ILogger<UpdateIsolationHostTypeHandler> logger,
        UpdateIsolationHostTypeCommandValidator validator,
        IUpdateIsolationHostTypeMapper mapper,
        IIsolationHostTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateIsolationHostTypeCommandResponse, Errors>> Handle(
        UpdateIsolationHostTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IsolationHostType isolationhosttype = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            isolationhosttype = _mapper.Map(isolationhosttype, command);

            Errors? errors = await _writeRepository.Update(isolationhosttype, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateIsolationHostTypeCommandResponse(isolationhosttype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new IsolationHostType");
            throw;
        }
    }
}