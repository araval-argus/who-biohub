using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.CreateWorklistFromBioHubHistoryItem;

public interface ICreateWorklistFromBioHubHistoryItemHandler
{
    Task<Either<CreateWorklistFromBioHubHistoryItemCommandResponse, Errors>> Handle(CreateWorklistFromBioHubHistoryItemCommand command, CancellationToken cancellationToken);
}

public class CreateWorklistFromBioHubHistoryItemHandler : ICreateWorklistFromBioHubHistoryItemHandler
{
    private readonly ILogger<CreateWorklistFromBioHubHistoryItemHandler> _logger;
    private readonly CreateWorklistFromBioHubHistoryItemCommandValidator _validator;
    private readonly ICreateWorklistFromBioHubHistoryItemMapper _mapper;
    private readonly IWorklistFromBioHubHistoryItemWriteRepository _writeRepository;

    public CreateWorklistFromBioHubHistoryItemHandler(
        ILogger<CreateWorklistFromBioHubHistoryItemHandler> logger,
        CreateWorklistFromBioHubHistoryItemCommandValidator validator,
        ICreateWorklistFromBioHubHistoryItemMapper mapper,
        IWorklistFromBioHubHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateWorklistFromBioHubHistoryItemCommandResponse, Errors>> Handle(
        CreateWorklistFromBioHubHistoryItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        WorklistFromBioHubHistoryItem worklistfrombiohubhistoryitem = _mapper.Map(command);

        try
        {
            Either<WorklistFromBioHubHistoryItem, Errors> response = await _writeRepository.Create(worklistfrombiohubhistoryitem, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            WorklistFromBioHubHistoryItem createdWorklistFromBioHubHistoryItem =
                response.Left ?? throw new Exception("This is a bug: worklistfrombiohubhistoryitem value must be defined");
            return new(new CreateWorklistFromBioHubHistoryItemCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new WorklistFromBioHubHistoryItem");
            throw;
        }
    }
}