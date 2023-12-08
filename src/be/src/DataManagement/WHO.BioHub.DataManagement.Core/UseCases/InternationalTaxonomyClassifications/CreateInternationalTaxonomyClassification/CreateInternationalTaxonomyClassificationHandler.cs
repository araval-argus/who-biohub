using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.CreateInternationalTaxonomyClassification;

public interface ICreateInternationalTaxonomyClassificationHandler
{
    Task<Either<CreateInternationalTaxonomyClassificationCommandResponse, Errors>> Handle(CreateInternationalTaxonomyClassificationCommand command, CancellationToken cancellationToken);
}

public class CreateInternationalTaxonomyClassificationHandler : ICreateInternationalTaxonomyClassificationHandler
{
    private readonly ILogger<CreateInternationalTaxonomyClassificationHandler> _logger;
    private readonly CreateInternationalTaxonomyClassificationCommandValidator _validator;
    private readonly ICreateInternationalTaxonomyClassificationMapper _mapper;
    private readonly IInternationalTaxonomyClassificationWriteRepository _writeRepository;

    public CreateInternationalTaxonomyClassificationHandler(
        ILogger<CreateInternationalTaxonomyClassificationHandler> logger,
        CreateInternationalTaxonomyClassificationCommandValidator validator,
        ICreateInternationalTaxonomyClassificationMapper mapper,
        IInternationalTaxonomyClassificationWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateInternationalTaxonomyClassificationCommandResponse, Errors>> Handle(
        CreateInternationalTaxonomyClassificationCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        InternationalTaxonomyClassification internationaltaxonomyclassification = _mapper.Map(command);

        try
        {
            Either<InternationalTaxonomyClassification, Errors> response = await _writeRepository.Create(internationaltaxonomyclassification, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            InternationalTaxonomyClassification createdInternationalTaxonomyClassification =
                response.Left ?? throw new Exception("This is a bug: internationaltaxonomyclassification value must be defined");
            return new(new CreateInternationalTaxonomyClassificationCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new InternationalTaxonomyClassification");
            throw;
        }
    }
}