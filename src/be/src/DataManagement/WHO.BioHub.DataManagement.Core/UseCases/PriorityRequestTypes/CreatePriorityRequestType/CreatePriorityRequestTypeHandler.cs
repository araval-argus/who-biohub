using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.CreatePriorityRequestType;

public interface ICreatePriorityRequestTypeHandler
{
    Task<Either<CreatePriorityRequestTypeCommandResponse, Errors>> Handle(CreatePriorityRequestTypeCommand command, CancellationToken cancellationToken);
}

public class CreatePriorityRequestTypeHandler : ICreatePriorityRequestTypeHandler
{
    private readonly ILogger<CreatePriorityRequestTypeHandler> _logger;
    private readonly CreatePriorityRequestTypeCommandValidator _validator;
    private readonly ICreatePriorityRequestTypeMapper _mapper;
    private readonly IPriorityRequestTypeWriteRepository _writeRepository;

    public CreatePriorityRequestTypeHandler(
        ILogger<CreatePriorityRequestTypeHandler> logger,
        CreatePriorityRequestTypeCommandValidator validator,
        ICreatePriorityRequestTypeMapper mapper,
        IPriorityRequestTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreatePriorityRequestTypeCommandResponse, Errors>> Handle(
        CreatePriorityRequestTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        PriorityRequestType priorityrequesttype = _mapper.Map(command);

        try
        {
            Either<PriorityRequestType, Errors> response = await _writeRepository.Create(priorityrequesttype, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            PriorityRequestType createdPriorityRequestType =
                response.Left ?? throw new Exception("This is a bug: priorityrequesttype value must be defined");
            return new(new CreatePriorityRequestTypeCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new PriorityRequestType");
            throw;
        }
    }
}