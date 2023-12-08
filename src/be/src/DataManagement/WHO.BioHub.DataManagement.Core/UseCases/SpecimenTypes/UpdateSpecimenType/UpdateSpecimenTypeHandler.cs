using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SpecimenTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.UpdateSpecimenType;

public interface IUpdateSpecimenTypeHandler
{
    Task<Either<UpdateSpecimenTypeCommandResponse, Errors>> Handle(UpdateSpecimenTypeCommand command, CancellationToken cancellationToken);
}

public class UpdateSpecimenTypeHandler : IUpdateSpecimenTypeHandler
{
    private readonly ILogger<UpdateSpecimenTypeHandler> _logger;
    private readonly UpdateSpecimenTypeCommandValidator _validator;
    private readonly IUpdateSpecimenTypeMapper _mapper;
    private readonly ISpecimenTypeWriteRepository _writeRepository;

    public UpdateSpecimenTypeHandler(
        ILogger<UpdateSpecimenTypeHandler> logger,
        UpdateSpecimenTypeCommandValidator validator,
        IUpdateSpecimenTypeMapper mapper,
        ISpecimenTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateSpecimenTypeCommandResponse, Errors>> Handle(
        UpdateSpecimenTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            SpecimenType specimenttype = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            specimenttype = _mapper.Map(specimenttype, command);

            Errors? errors = await _writeRepository.Update(specimenttype, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateSpecimenTypeCommandResponse(specimenttype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new SpecimenType");
            throw;
        }
    }
}