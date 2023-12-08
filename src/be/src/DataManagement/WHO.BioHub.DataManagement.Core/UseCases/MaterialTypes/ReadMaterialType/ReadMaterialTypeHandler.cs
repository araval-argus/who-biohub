using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.ReadMaterialType;

public interface IReadMaterialTypeHandler
{
    Task<Either<ReadMaterialTypeQueryResponse, Errors>> Handle(ReadMaterialTypeQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialTypeHandler : IReadMaterialTypeHandler
{
    private readonly ILogger<ReadMaterialTypeHandler> _logger;
    private readonly ReadMaterialTypeQueryValidator _validator;
    private readonly IMaterialTypeReadRepository _readRepository;

    public ReadMaterialTypeHandler(
        ILogger<ReadMaterialTypeHandler> logger,
        ReadMaterialTypeQueryValidator validator,
        IMaterialTypeReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadMaterialTypeQueryResponse, Errors>> Handle(
        ReadMaterialTypeQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialType materialtype = await _readRepository.Read(query.Id, cancellationToken);
            if (materialtype == null)
                return new(new Errors(ErrorType.NotFound, $"MaterialType with Id {query.Id} not found"));

            return new(new ReadMaterialTypeQueryResponse(materialtype));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading MaterialType with Id {id}", query.Id);
            throw;
        }
    }
}