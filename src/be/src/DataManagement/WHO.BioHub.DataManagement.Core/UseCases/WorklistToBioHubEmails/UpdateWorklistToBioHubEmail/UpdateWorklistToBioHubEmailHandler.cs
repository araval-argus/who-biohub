using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.UpdateWorklistToBioHubEmail;

public interface IUpdateWorklistToBioHubEmailHandler
{
    Task<Either<UpdateWorklistToBioHubEmailCommandResponse, Errors>> Handle(UpdateWorklistToBioHubEmailCommand command, CancellationToken cancellationToken);
}

public class UpdateWorklistToBioHubEmailHandler : IUpdateWorklistToBioHubEmailHandler
{
    private readonly ILogger<UpdateWorklistToBioHubEmailHandler> _logger;
    private readonly UpdateWorklistToBioHubEmailCommandValidator _validator;
    private readonly IUpdateWorklistToBioHubEmailMapper _mapper;
    private readonly IWorklistToBioHubEmailWriteRepository _writeRepository;

    public UpdateWorklistToBioHubEmailHandler(
        ILogger<UpdateWorklistToBioHubEmailHandler> logger,
        UpdateWorklistToBioHubEmailCommandValidator validator,
        IUpdateWorklistToBioHubEmailMapper mapper,
        IWorklistToBioHubEmailWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateWorklistToBioHubEmailCommandResponse, Errors>> Handle(
        UpdateWorklistToBioHubEmailCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            WorklistToBioHubEmail worklisttobiohubemail = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            worklisttobiohubemail = _mapper.Map(worklisttobiohubemail, command);

            Errors? errors = await _writeRepository.Update(worklisttobiohubemail, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateWorklistToBioHubEmailCommandResponse(worklisttobiohubemail));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new WorklistToBioHubEmail");
            throw;
        }
    }
}