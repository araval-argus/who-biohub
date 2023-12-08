using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.CreateSMTA1WorkflowHistoryItem;

public interface ICreateSMTA1WorkflowHistoryItemHandler
{
    Task<Either<CreateSMTA1WorkflowHistoryItemCommandResponse, Errors>> Handle(CreateSMTA1WorkflowHistoryItemCommand command, CancellationToken cancellationToken);
}

public class CreateSMTA1WorkflowHistoryItemHandler : ICreateSMTA1WorkflowHistoryItemHandler
{
    private readonly ILogger<CreateSMTA1WorkflowHistoryItemHandler> _logger;
    private readonly CreateSMTA1WorkflowHistoryItemCommandValidator _validator;
    private readonly ICreateSMTA1WorkflowHistoryItemMapper _mapper;
    private readonly ISMTA1WorkflowHistoryItemWriteRepository _writeRepository;

    public CreateSMTA1WorkflowHistoryItemHandler(
        ILogger<CreateSMTA1WorkflowHistoryItemHandler> logger,
        CreateSMTA1WorkflowHistoryItemCommandValidator validator,
        ICreateSMTA1WorkflowHistoryItemMapper mapper,
        ISMTA1WorkflowHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateSMTA1WorkflowHistoryItemCommandResponse, Errors>> Handle(
        CreateSMTA1WorkflowHistoryItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        SMTA1WorkflowHistoryItem SMTA1WorkflowHistoryItem = _mapper.Map(command);

        try
        {
            Either<SMTA1WorkflowHistoryItem, Errors> response = await _writeRepository.Create(SMTA1WorkflowHistoryItem, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            SMTA1WorkflowHistoryItem createdSMTA1WorkflowHistoryItem =
                response.Left ?? throw new Exception("This is a bug: SMTA1WorkflowHistoryItem value must be defined");
            return new(new CreateSMTA1WorkflowHistoryItemCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new SMTA1WorkflowHistoryItem");
            throw;
        }
    }
}