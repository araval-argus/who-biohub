using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.UpdatePriorityRequestType;

public interface IUpdatePriorityRequestTypeHandler
{
    Task<Either<UpdatePriorityRequestTypeCommandResponse, Errors>> Handle(UpdatePriorityRequestTypeCommand command, CancellationToken cancellationToken);
}

public class UpdatePriorityRequestTypeHandler : IUpdatePriorityRequestTypeHandler
{
    private readonly ILogger<UpdatePriorityRequestTypeHandler> _logger;
    private readonly UpdatePriorityRequestTypeCommandValidator _validator;
    private readonly IUpdatePriorityRequestTypeMapper _mapper;
    private readonly IPriorityRequestTypeWriteRepository _writeRepository;

    public UpdatePriorityRequestTypeHandler(
        ILogger<UpdatePriorityRequestTypeHandler> logger,
        UpdatePriorityRequestTypeCommandValidator validator,
        IUpdatePriorityRequestTypeMapper mapper,
        IPriorityRequestTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdatePriorityRequestTypeCommandResponse, Errors>> Handle(
        UpdatePriorityRequestTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            PriorityRequestType priorityrequesttype = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            priorityrequesttype = _mapper.Map(priorityrequesttype, command);

            Errors? errors = await _writeRepository.Update(priorityrequesttype, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdatePriorityRequestTypeCommandResponse(priorityrequesttype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new PriorityRequestType");
            throw;
        }
    }
}