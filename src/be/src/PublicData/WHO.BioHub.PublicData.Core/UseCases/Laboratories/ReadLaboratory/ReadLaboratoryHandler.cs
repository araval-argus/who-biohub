using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ReadLaboratory;

public interface IReadLaboratoryHandler
{
    Task<Either<ReadLaboratoryQueryResponse, Errors>> Handle(ReadLaboratoryQuery query, CancellationToken cancellationToken);
}

public class ReadLaboratoryHandler : IReadLaboratoryHandler
{
    private readonly ILogger<ReadLaboratoryHandler> _logger;
    private readonly ReadLaboratoryQueryValidator _validator;
    private readonly ILaboratoryPublicReadRepository _readRepository;
    private readonly IReadLaboratoryMapper _mapper;

    public ReadLaboratoryHandler(
        ILogger<ReadLaboratoryHandler> logger,
        ReadLaboratoryQueryValidator validator,
        IReadLaboratoryMapper mapper,
        ILaboratoryPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadLaboratoryQueryResponse, Errors>> Handle(
        ReadLaboratoryQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Laboratory laboratory = await _readRepository.Read(query.Id, cancellationToken);
            if (laboratory == null)
                return new(new Errors(ErrorType.NotFound, $"Laboratory with Id {query.Id} not found"));

            LaboratoryPublicViewModel laboratoryPublicViewModel = _mapper.Map(laboratory);

            return new(new ReadLaboratoryQueryResponse(laboratoryPublicViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Laboratory with Id {id}", query.Id);
            throw;
        }
    }
}