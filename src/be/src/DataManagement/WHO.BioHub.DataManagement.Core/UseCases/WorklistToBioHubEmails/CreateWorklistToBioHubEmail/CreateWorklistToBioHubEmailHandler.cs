using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.CreateWorklistToBioHubEmail;

public interface ICreateWorklistToBioHubEmailHandler
{
    Task<Either<CreateWorklistToBioHubEmailCommandResponse, Errors>> Handle(CreateWorklistToBioHubEmailCommand command, CancellationToken cancellationToken);
}

public class CreateWorklistToBioHubEmailHandler : ICreateWorklistToBioHubEmailHandler
{
    private readonly ILogger<CreateWorklistToBioHubEmailHandler> _logger;
    private readonly CreateWorklistToBioHubEmailCommandValidator _validator;
    private readonly ICreateWorklistToBioHubEmailMapper _mapper;
    private readonly IWorklistToBioHubEmailWriteRepository _writeRepository;

    public CreateWorklistToBioHubEmailHandler(
        ILogger<CreateWorklistToBioHubEmailHandler> logger,
        CreateWorklistToBioHubEmailCommandValidator validator,
        ICreateWorklistToBioHubEmailMapper mapper,
        IWorklistToBioHubEmailWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateWorklistToBioHubEmailCommandResponse, Errors>> Handle(
        CreateWorklistToBioHubEmailCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        WorklistToBioHubEmail worklisttobiohubemail = _mapper.Map(command);

        try
        {
            Either<WorklistToBioHubEmail, Errors> response = await _writeRepository.Create(worklisttobiohubemail, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            WorklistToBioHubEmail createdWorklistToBioHubEmail =
                response.Left ?? throw new Exception("This is a bug: worklisttobiohubemail value must be defined");
            return new(new CreateWorklistToBioHubEmailCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new WorklistToBioHubEmail");
            throw;
        }
    }
}