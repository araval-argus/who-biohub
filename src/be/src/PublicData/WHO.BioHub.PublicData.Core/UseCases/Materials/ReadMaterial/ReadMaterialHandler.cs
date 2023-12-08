using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Materials.ReadMaterial;

public interface IReadMaterialHandler
{
    Task<Either<ReadMaterialQueryResponse, Errors>> Handle(ReadMaterialQuery query, CancellationToken cancellationToken);
}

public class ReadMaterialHandler : IReadMaterialHandler
{
    private readonly ILogger<ReadMaterialHandler> _logger;
    private readonly ReadMaterialQueryValidator _validator;
    private readonly IMaterialPublicReadRepository _readRepository;
    private readonly IReadMaterialMapper _mapper;

    public ReadMaterialHandler(
        ILogger<ReadMaterialHandler> logger,
        ReadMaterialQueryValidator validator,
        IReadMaterialMapper mapper,
        IMaterialPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadMaterialQueryResponse, Errors>> Handle(
        ReadMaterialQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Material material = await _readRepository.Read(query.Id, cancellationToken);
            if (material == null)
                return new(new Errors(ErrorType.NotFound, $"Material with Id {query.Id} not found"));

            MaterialPublicViewModel materialPublicViewModel = _mapper.Map(material);

            return new(new ReadMaterialQueryResponse(materialPublicViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Material with Id {id}", query.Id);
            throw;
        }
    }
}