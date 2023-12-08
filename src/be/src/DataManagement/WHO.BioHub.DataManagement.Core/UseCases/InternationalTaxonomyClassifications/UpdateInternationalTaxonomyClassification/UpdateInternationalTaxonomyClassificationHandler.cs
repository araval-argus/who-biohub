using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.UpdateInternationalTaxonomyClassification;

public interface IUpdateInternationalTaxonomyClassificationHandler
{
    Task<Either<UpdateInternationalTaxonomyClassificationCommandResponse, Errors>> Handle(UpdateInternationalTaxonomyClassificationCommand command, CancellationToken cancellationToken);
}

public class UpdateInternationalTaxonomyClassificationHandler : IUpdateInternationalTaxonomyClassificationHandler
{
    private readonly ILogger<UpdateInternationalTaxonomyClassificationHandler> _logger;
    private readonly UpdateInternationalTaxonomyClassificationCommandValidator _validator;
    private readonly IUpdateInternationalTaxonomyClassificationMapper _mapper;
    private readonly IInternationalTaxonomyClassificationWriteRepository _writeRepository;

    public UpdateInternationalTaxonomyClassificationHandler(
        ILogger<UpdateInternationalTaxonomyClassificationHandler> logger,
        UpdateInternationalTaxonomyClassificationCommandValidator validator,
        IUpdateInternationalTaxonomyClassificationMapper mapper,
        IInternationalTaxonomyClassificationWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateInternationalTaxonomyClassificationCommandResponse, Errors>> Handle(
        UpdateInternationalTaxonomyClassificationCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            InternationalTaxonomyClassification internationaltaxonomyclassification = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            internationaltaxonomyclassification = _mapper.Map(internationaltaxonomyclassification, command);

            Errors? errors = await _writeRepository.Update(internationaltaxonomyclassification, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateInternationalTaxonomyClassificationCommandResponse(internationaltaxonomyclassification));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new InternationalTaxonomyClassification");
            throw;
        }
    }
}