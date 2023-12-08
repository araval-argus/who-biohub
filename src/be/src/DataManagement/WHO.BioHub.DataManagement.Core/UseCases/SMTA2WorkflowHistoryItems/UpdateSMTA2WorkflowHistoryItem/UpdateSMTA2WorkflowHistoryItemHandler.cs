using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.UpdateSMTA2WorkflowHistoryItem;

public interface IUpdateSMTA2WorkflowHistoryItemHandler
{
    Task<Either<UpdateSMTA2WorkflowHistoryItemCommandResponse, Errors>> Handle(UpdateSMTA2WorkflowHistoryItemCommand command, CancellationToken cancellationToken);
}

public class UpdateSMTA2WorkflowHistoryItemHandler : IUpdateSMTA2WorkflowHistoryItemHandler
{
    private readonly ILogger<UpdateSMTA2WorkflowHistoryItemHandler> _logger;
    private readonly UpdateSMTA2WorkflowHistoryItemCommandValidator _validator;
    private readonly IUpdateSMTA2WorkflowHistoryItemMapper _mapper;
    private readonly ISMTA2WorkflowHistoryItemWriteRepository _writeRepository;

    public UpdateSMTA2WorkflowHistoryItemHandler(
        ILogger<UpdateSMTA2WorkflowHistoryItemHandler> logger,
        UpdateSMTA2WorkflowHistoryItemCommandValidator validator,
        IUpdateSMTA2WorkflowHistoryItemMapper mapper,
        ISMTA2WorkflowHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateSMTA2WorkflowHistoryItemCommandResponse, Errors>> Handle(
        UpdateSMTA2WorkflowHistoryItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            SMTA2WorkflowHistoryItem smta2workflowhistoryitem = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            smta2workflowhistoryitem = _mapper.Map(smta2workflowhistoryitem, command);

            Errors? errors = await _writeRepository.Update(smta2workflowhistoryitem, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateSMTA2WorkflowHistoryItemCommandResponse(smta2workflowhistoryitem));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new SMTA2WorkflowHistoryItem");
            throw;
        }
    }
}