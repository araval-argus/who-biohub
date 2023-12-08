using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.UpdateSMTA1WorkflowHistoryItem;

public interface IUpdateSMTA1WorkflowHistoryItemHandler
{
    Task<Either<UpdateSMTA1WorkflowHistoryItemCommandResponse, Errors>> Handle(UpdateSMTA1WorkflowHistoryItemCommand command, CancellationToken cancellationToken);
}

public class UpdateSMTA1WorkflowHistoryItemHandler : IUpdateSMTA1WorkflowHistoryItemHandler
{
    private readonly ILogger<UpdateSMTA1WorkflowHistoryItemHandler> _logger;
    private readonly UpdateSMTA1WorkflowHistoryItemCommandValidator _validator;
    private readonly IUpdateSMTA1WorkflowHistoryItemMapper _mapper;
    private readonly ISMTA1WorkflowHistoryItemWriteRepository _writeRepository;

    public UpdateSMTA1WorkflowHistoryItemHandler(
        ILogger<UpdateSMTA1WorkflowHistoryItemHandler> logger,
        UpdateSMTA1WorkflowHistoryItemCommandValidator validator,
        IUpdateSMTA1WorkflowHistoryItemMapper mapper,
        ISMTA1WorkflowHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateSMTA1WorkflowHistoryItemCommandResponse, Errors>> Handle(
        UpdateSMTA1WorkflowHistoryItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            SMTA1WorkflowHistoryItem SMTA1WorkflowHistoryItem = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            SMTA1WorkflowHistoryItem = _mapper.Map(SMTA1WorkflowHistoryItem, command);

            Errors? errors = await _writeRepository.Update(SMTA1WorkflowHistoryItem, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateSMTA1WorkflowHistoryItemCommandResponse(SMTA1WorkflowHistoryItem));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new SMTA1WorkflowHistoryItem");
            throw;
        }
    }
}