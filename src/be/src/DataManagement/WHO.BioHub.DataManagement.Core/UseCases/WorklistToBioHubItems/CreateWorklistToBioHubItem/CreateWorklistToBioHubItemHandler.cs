using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.CreateWorklistToBioHubItem;

public interface ICreateWorklistToBioHubItemHandler
{
    Task<Either<CreateWorklistToBioHubItemCommandResponse, Errors>> Handle(CreateWorklistToBioHubItemCommand command, CancellationToken cancellationToken);
}

public class CreateWorklistToBioHubItemHandler : ICreateWorklistToBioHubItemHandler
{
    private readonly ILogger<CreateWorklistToBioHubItemHandler> _logger;
    private readonly CreateWorklistToBioHubItemCommandValidator _validator;
    private readonly ICreateWorklistToBioHubItemMapper _mapper;
    private readonly IWorklistToBioHubItemWriteRepository _writeRepository;
    private readonly IWorklistToBioHubEngine _worklistToBioHubEngine;
    private readonly IWorklistToBioHubHistoryItemWriteRepository _worklistToBioHubHistoryItemWriteRepository;
    private readonly IWorklistToBioHubItemReadRepository _readRepository;
    private readonly IDocumentTemplateReadRepository _readDocumentTemplateRepository;
    private readonly IWorklistItemUsedReferenceNumberReadRepository _readUsedReferenceNumberRepository;

    public CreateWorklistToBioHubItemHandler(
        ILogger<CreateWorklistToBioHubItemHandler> logger,
        CreateWorklistToBioHubItemCommandValidator validator,
        ICreateWorklistToBioHubItemMapper mapper,
        IWorklistToBioHubItemWriteRepository writeRepository,
        IWorklistToBioHubItemReadRepository readRepository,
        IDocumentTemplateReadRepository readDocumentTemplateRepository,
        IWorklistToBioHubEngine worklistToBioHubEngine,
        IWorklistToBioHubHistoryItemWriteRepository worklistToBioHubHistoryItemWriteRepository,
        IWorklistItemUsedReferenceNumberReadRepository readUsedReferenceNumberRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _worklistToBioHubEngine = worklistToBioHubEngine;
        _worklistToBioHubHistoryItemWriteRepository = worklistToBioHubHistoryItemWriteRepository;
        _readDocumentTemplateRepository = readDocumentTemplateRepository;
        _readUsedReferenceNumberRepository = readUsedReferenceNumberRepository;
    }

    public async Task<Either<CreateWorklistToBioHubItemCommandResponse, Errors>> Handle(
        CreateWorklistToBioHubItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        if (command.IsPast != true)
        {
            List<DocumentFileType?> neededTemplates = new List<DocumentFileType?>();
            neededTemplates.Add(DocumentFileType.Annex2OfSMTA1);
            neededTemplates.Add(DocumentFileType.BookingFormOfSMTA1);
            neededTemplates.Add(DocumentFileType.PackagingList);
            neededTemplates.Add(DocumentFileType.NonCommercialInvoiceCatA);
            neededTemplates.Add(DocumentFileType.NonCommercialInvoiceCatB);

            if (!await _readDocumentTemplateRepository.TemplatesPresent(neededTemplates, cancellationToken))
            {
                return new(new Errors(ErrorType.NotFound, $"One or more needed template documets are missing in the system. Please contact the WHO secretariat"));
            }
        }

        WorklistToBioHubItem worklisttobiohubitem = _mapper.Map(command);
        
        try
        {
            var moveToNextStatusCommand = PrepareMoveToNextStatusCommand(command);

            worklisttobiohubitem = await _worklistToBioHubEngine.MoveToNextStatusUponApproveOrSaveDraft(worklisttobiohubitem, moveToNextStatusCommand, cancellationToken);
            
            return new(new CreateWorklistToBioHubItemCommandResponse(worklisttobiohubitem.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new(new Errors(ErrorType.Internal, $"Internal Server Error"));            
        }
    }

    private MoveToNextStatusToBioHubEngineCommand PrepareMoveToNextStatusCommand(CreateWorklistToBioHubItemCommand command)
    {
        MoveToNextStatusToBioHubEngineCommand moveToNextStatusCommand = new MoveToNextStatusToBioHubEngineCommand();
        moveToNextStatusCommand.UserId = command.UserId;
        return moveToNextStatusCommand;
    }
}