using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SpecimenTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.CreateSpecimenType;

public interface ICreateSpecimenTypeHandler
{
    Task<Either<CreateSpecimenTypeCommandResponse, Errors>> Handle(CreateSpecimenTypeCommand command, CancellationToken cancellationToken);
}

public class CreateSpecimenTypeHandler : ICreateSpecimenTypeHandler
{
    private readonly ILogger<CreateSpecimenTypeHandler> _logger;
    private readonly CreateSpecimenTypeCommandValidator _validator;
    private readonly ICreateSpecimenTypeMapper _mapper;
    private readonly ISpecimenTypeWriteRepository _writeRepository;

    public CreateSpecimenTypeHandler(
        ILogger<CreateSpecimenTypeHandler> logger,
        CreateSpecimenTypeCommandValidator validator,
        ICreateSpecimenTypeMapper mapper,
        ISpecimenTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateSpecimenTypeCommandResponse, Errors>> Handle(
        CreateSpecimenTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        SpecimenType specimenttype = _mapper.Map(command);

        try
        {
            Either<SpecimenType, Errors> response = await _writeRepository.Create(specimenttype, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            SpecimenType createdSpecimenType =
                response.Left ?? throw new Exception("This is a bug: specimenttype value must be defined");
            return new(new CreateSpecimenTypeCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new SpecimenType");
            throw;
        }
    }
}