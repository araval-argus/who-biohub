using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.CreateWorklistFromBioHubItem;

public interface ICreateWorklistFromBioHubItemHandler
{
    Task<Either<CreateWorklistFromBioHubItemCommandResponse, Errors>> Handle(CreateWorklistFromBioHubItemCommand command, CancellationToken cancellationToken);
}

public class CreateWorklistFromBioHubItemHandler : ICreateWorklistFromBioHubItemHandler
{
    private readonly ILogger<CreateWorklistFromBioHubItemHandler> _logger;
    private readonly CreateWorklistFromBioHubItemCommandValidator _validator;
    private readonly ICreateWorklistFromBioHubItemMapper _mapper;
    private readonly IWorklistFromBioHubItemWriteRepository _writeRepository;
    private readonly IWorklistFromBioHubEngine _worklistFromBioHubEngine;
    private readonly IWorklistFromBioHubHistoryItemWriteRepository _worklistFromBioHubHistoryItemWriteRepository;
    private readonly IWorklistFromBioHubItemReadRepository _readRepository;
    private readonly IDocumentTemplateReadRepository _readDocumentTemplateRepository;
    private readonly IWorklistItemUsedReferenceNumberReadRepository _readUsedReferenceNumberRepository;

    public CreateWorklistFromBioHubItemHandler(
        ILogger<CreateWorklistFromBioHubItemHandler> logger,
        CreateWorklistFromBioHubItemCommandValidator validator,
        ICreateWorklistFromBioHubItemMapper mapper,
        IWorklistFromBioHubItemWriteRepository writeRepository,
        IWorklistFromBioHubItemReadRepository readRepository,
        IDocumentTemplateReadRepository readDocumentTemplateRepository,
        IWorklistFromBioHubEngine worklistFromBioHubEngine,
        IWorklistFromBioHubHistoryItemWriteRepository worklistFromBioHubHistoryItemWriteRepository,
        IWorklistItemUsedReferenceNumberReadRepository readUsedReferenceNumberRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _worklistFromBioHubEngine = worklistFromBioHubEngine;
        _worklistFromBioHubHistoryItemWriteRepository = worklistFromBioHubHistoryItemWriteRepository;
        _readDocumentTemplateRepository = readDocumentTemplateRepository;
        _readUsedReferenceNumberRepository = readUsedReferenceNumberRepository;
    }

    public async Task<Either<CreateWorklistFromBioHubItemCommandResponse, Errors>> Handle(
        CreateWorklistFromBioHubItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        if (command.IsPast != true)
        {
            List<DocumentFileType?> neededTemplates = new List<DocumentFileType?>();
            neededTemplates.Add(DocumentFileType.SMTA2);
            neededTemplates.Add(DocumentFileType.Annex2OfSMTA2);
            neededTemplates.Add(DocumentFileType.BiosafetyChecklist);
            neededTemplates.Add(DocumentFileType.BookingFormOfSMTA2);
            neededTemplates.Add(DocumentFileType.PackagingList);
            neededTemplates.Add(DocumentFileType.NonCommercialInvoiceCatA);
            neededTemplates.Add(DocumentFileType.NonCommercialInvoiceCatB);

            if (!await _readDocumentTemplateRepository.TemplatesPresent(neededTemplates, cancellationToken))
            {
                return new(new Errors(ErrorType.NotFound, $"One or more needed template documets are missing in the system. Please contact the WHO secretariat"));
            }
        }

        WorklistFromBioHubItem worklisttobiohubitem = _mapper.Map(command);        

        try
        {
            var moveToNextStatusCommand = PrepareMoveToNextStatusCommand(command);


            worklisttobiohubitem = await _worklistFromBioHubEngine.MoveToNextStatusUponApproveOrSaveDraft(worklisttobiohubitem, moveToNextStatusCommand, cancellationToken);

            return new(new CreateWorklistFromBioHubItemCommandResponse(worklisttobiohubitem.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new(new Errors(ErrorType.Internal, $"Internal Server Error"));
        }
    }

    private MoveToNextStatusFromBioHubEngineCommand PrepareMoveToNextStatusCommand(CreateWorklistFromBioHubItemCommand command)
    {
        MoveToNextStatusFromBioHubEngineCommand moveToNextStatusCommand = new MoveToNextStatusFromBioHubEngineCommand();
        moveToNextStatusCommand.UserId = command.UserId;
        return moveToNextStatusCommand;
    }
}