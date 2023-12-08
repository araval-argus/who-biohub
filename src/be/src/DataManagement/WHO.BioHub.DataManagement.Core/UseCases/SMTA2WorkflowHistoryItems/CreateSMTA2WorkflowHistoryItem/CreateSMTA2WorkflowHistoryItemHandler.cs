using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.CreateSMTA2WorkflowHistoryItem;

public interface ICreateSMTA2WorkflowHistoryItemHandler
{
    Task<Either<CreateSMTA2WorkflowHistoryItemCommandResponse, Errors>> Handle(CreateSMTA2WorkflowHistoryItemCommand command, CancellationToken cancellationToken);
}

public class CreateSMTA2WorkflowHistoryItemHandler : ICreateSMTA2WorkflowHistoryItemHandler
{
    private readonly ILogger<CreateSMTA2WorkflowHistoryItemHandler> _logger;
    private readonly CreateSMTA2WorkflowHistoryItemCommandValidator _validator;
    private readonly ICreateSMTA2WorkflowHistoryItemMapper _mapper;
    private readonly ISMTA2WorkflowHistoryItemWriteRepository _writeRepository;

    public CreateSMTA2WorkflowHistoryItemHandler(
        ILogger<CreateSMTA2WorkflowHistoryItemHandler> logger,
        CreateSMTA2WorkflowHistoryItemCommandValidator validator,
        ICreateSMTA2WorkflowHistoryItemMapper mapper,
        ISMTA2WorkflowHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateSMTA2WorkflowHistoryItemCommandResponse, Errors>> Handle(
        CreateSMTA2WorkflowHistoryItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        SMTA2WorkflowHistoryItem smta2workflowhistoryitem = _mapper.Map(command);

        try
        {
            Either<SMTA2WorkflowHistoryItem, Errors> response = await _writeRepository.Create(smta2workflowhistoryitem, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            SMTA2WorkflowHistoryItem createdSMTA2WorkflowHistoryItem =
                response.Left ?? throw new Exception("This is a bug: smta2workflowhistoryitem value must be defined");
            return new(new CreateSMTA2WorkflowHistoryItemCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new SMTA2WorkflowHistoryItem");
            throw;
        }
    }
}