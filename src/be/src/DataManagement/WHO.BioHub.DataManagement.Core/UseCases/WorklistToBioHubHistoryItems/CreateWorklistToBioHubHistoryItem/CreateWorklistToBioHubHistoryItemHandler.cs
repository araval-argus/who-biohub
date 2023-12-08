using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.CreateWorklistToBioHubHistoryItem;

public interface ICreateWorklistToBioHubHistoryItemHandler
{
    Task<Either<CreateWorklistToBioHubHistoryItemCommandResponse, Errors>> Handle(CreateWorklistToBioHubHistoryItemCommand command, CancellationToken cancellationToken);
}

public class CreateWorklistToBioHubHistoryItemHandler : ICreateWorklistToBioHubHistoryItemHandler
{
    private readonly ILogger<CreateWorklistToBioHubHistoryItemHandler> _logger;
    private readonly CreateWorklistToBioHubHistoryItemCommandValidator _validator;
    private readonly ICreateWorklistToBioHubHistoryItemMapper _mapper;
    private readonly IWorklistToBioHubHistoryItemWriteRepository _writeRepository;

    public CreateWorklistToBioHubHistoryItemHandler(
        ILogger<CreateWorklistToBioHubHistoryItemHandler> logger,
        CreateWorklistToBioHubHistoryItemCommandValidator validator,
        ICreateWorklistToBioHubHistoryItemMapper mapper,
        IWorklistToBioHubHistoryItemWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateWorklistToBioHubHistoryItemCommandResponse, Errors>> Handle(
        CreateWorklistToBioHubHistoryItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        WorklistToBioHubHistoryItem worklisttobiohubhistoryitem = _mapper.Map(command);

        try
        {
            Either<WorklistToBioHubHistoryItem, Errors> response = await _writeRepository.Create(worklisttobiohubhistoryitem, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            WorklistToBioHubHistoryItem createdWorklistToBioHubHistoryItem =
                response.Left ?? throw new Exception("This is a bug: worklisttobiohubhistoryitem value must be defined");
            return new(new CreateWorklistToBioHubHistoryItemCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new WorklistToBioHubHistoryItem");
            throw;
        }
    }
}