using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.CreateSMTA2WorkflowItem;

public interface ICreateSMTA2WorkflowItemHandler
{
    Task<Either<CreateSMTA2WorkflowItemCommandResponse, Errors>> Handle(CreateSMTA2WorkflowItemCommand command, CancellationToken cancellationToken);
}

public class CreateSMTA2WorkflowItemHandler : ICreateSMTA2WorkflowItemHandler
{
    private readonly ILogger<CreateSMTA2WorkflowItemHandler> _logger;
    private readonly CreateSMTA2WorkflowItemCommandValidator _validator;
    private readonly ICreateSMTA2WorkflowItemMapper _mapper;
    private readonly ISMTA2WorkflowEngine _SMTA2workflowEngine;
    private readonly IDocumentTemplateReadRepository _readDocumentTemplateRepository;

    public CreateSMTA2WorkflowItemHandler(
        ILogger<CreateSMTA2WorkflowItemHandler> logger,
        CreateSMTA2WorkflowItemCommandValidator validator,
        ICreateSMTA2WorkflowItemMapper mapper,
        IDocumentTemplateReadRepository readDocumentTemplateRepository,
        ISMTA2WorkflowEngine SMTA2workflowEngine

    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _SMTA2workflowEngine = SMTA2workflowEngine;
        _readDocumentTemplateRepository = readDocumentTemplateRepository;
    }

    public async Task<Either<CreateSMTA2WorkflowItemCommandResponse, Errors>> Handle(
        CreateSMTA2WorkflowItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        if (command.IsPast != true)
        {
            List<DocumentFileType?> neededTemplates = new List<DocumentFileType?>();
            neededTemplates.Add(DocumentFileType.SMTA2);

            if (!await _readDocumentTemplateRepository.TemplatesPresent(neededTemplates, cancellationToken))
            {
                return new(new Errors(ErrorType.NotFound, $"One or more needed template documets are missing in the system. Please contact the WHO secretariat"));
            }
        }

        SMTA2WorkflowItem SMTA2WorkflowItem = _mapper.Map(command);

        try
        {
            var moveToNextStatusCommand = PrepareMoveToNextStatusCommand(command);


            SMTA2WorkflowItem = await _SMTA2workflowEngine.MoveToNextStatusUponApproveOrSaveDraft(SMTA2WorkflowItem, moveToNextStatusCommand, cancellationToken);

            return new(new CreateSMTA2WorkflowItemCommandResponse(SMTA2WorkflowItem.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new(new Errors(ErrorType.Internal, $"Internal Server Error"));
        }
    }

    private MoveToNextStatusSMTA2WorkflowEngineCommand PrepareMoveToNextStatusCommand(CreateSMTA2WorkflowItemCommand command)
    {
        MoveToNextStatusSMTA2WorkflowEngineCommand moveToNextStatusCommand = new MoveToNextStatusSMTA2WorkflowEngineCommand();
        moveToNextStatusCommand.UserId = command.UserId;
        return moveToNextStatusCommand;
    }
}