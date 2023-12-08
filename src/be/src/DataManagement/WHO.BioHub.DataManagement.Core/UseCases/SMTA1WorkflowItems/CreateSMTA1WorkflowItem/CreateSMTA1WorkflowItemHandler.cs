using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.CreateSMTA1WorkflowItem;

public interface ICreateSMTA1WorkflowItemHandler
{
    Task<Either<CreateSMTA1WorkflowItemCommandResponse, Errors>> Handle(CreateSMTA1WorkflowItemCommand command, CancellationToken cancellationToken);
}

public class CreateSMTA1WorkflowItemHandler : ICreateSMTA1WorkflowItemHandler
{
    private readonly ILogger<CreateSMTA1WorkflowItemHandler> _logger;
    private readonly CreateSMTA1WorkflowItemCommandValidator _validator;
    private readonly ICreateSMTA1WorkflowItemMapper _mapper;
    private readonly ISMTA1WorkflowEngine _SMTA1workflowEngine;
    private readonly IDocumentTemplateReadRepository _readDocumentTemplateRepository;

    public CreateSMTA1WorkflowItemHandler(
        ILogger<CreateSMTA1WorkflowItemHandler> logger,
        CreateSMTA1WorkflowItemCommandValidator validator,
        ICreateSMTA1WorkflowItemMapper mapper,
        IDocumentTemplateReadRepository readDocumentTemplateRepository,
        ISMTA1WorkflowEngine SMTA1workflowEngine

    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _SMTA1workflowEngine = SMTA1workflowEngine;
        _readDocumentTemplateRepository = readDocumentTemplateRepository;
    }

    public async Task<Either<CreateSMTA1WorkflowItemCommandResponse, Errors>> Handle(
        CreateSMTA1WorkflowItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        if (command.IsPast != true)
        {
            List<DocumentFileType?> neededTemplates = new List<DocumentFileType?>();
            neededTemplates.Add(DocumentFileType.SMTA1);

            if (!await _readDocumentTemplateRepository.TemplatesPresent(neededTemplates, cancellationToken))
            {
                return new(new Errors(ErrorType.NotFound, $"One or more needed template documets are missing in the system. Please contact the WHO secretariat"));
            }
        }

        SMTA1WorkflowItem SMTA1WorkflowItem = _mapper.Map(command);

        try
        {
            var moveToNextStatusCommand = PrepareMoveToNextStatusCommand(command);


            SMTA1WorkflowItem = await _SMTA1workflowEngine.MoveToNextStatusUponApproveOrSaveDraft(SMTA1WorkflowItem, moveToNextStatusCommand, cancellationToken);
                       

            return new(new CreateSMTA1WorkflowItemCommandResponse(SMTA1WorkflowItem.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new(new Errors(ErrorType.Internal, $"Internal Server Error"));           
        }
    }

    private MoveToNextStatusSMTA1WorkflowEngineCommand PrepareMoveToNextStatusCommand(CreateSMTA1WorkflowItemCommand command)
    {
        MoveToNextStatusSMTA1WorkflowEngineCommand moveToNextStatusCommand = new MoveToNextStatusSMTA1WorkflowEngineCommand();
        moveToNextStatusCommand.UserId = command.UserId;
        return moveToNextStatusCommand;
    }
}