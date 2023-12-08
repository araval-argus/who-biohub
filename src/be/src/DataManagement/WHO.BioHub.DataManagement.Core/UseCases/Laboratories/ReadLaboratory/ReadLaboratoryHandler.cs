using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ReadLaboratory;

public interface IReadLaboratoryHandler
{
    Task<Either<ReadLaboratoryQueryResponse, Errors>> Handle(ReadLaboratoryQuery query, CancellationToken cancellationToken);
}

public class ReadLaboratoryHandler : IReadLaboratoryHandler
{
    private readonly ILogger<ReadLaboratoryHandler> _logger;
    private readonly ReadLaboratoryQueryValidator _validator;
    private readonly ILaboratoryReadRepository _readRepository;
    private readonly IReadLaboratoryMapper _mapper;

    public ReadLaboratoryHandler(
        ILogger<ReadLaboratoryHandler> logger,
        ReadLaboratoryQueryValidator validator,
        ILaboratoryReadRepository readRepository,
        IReadLaboratoryMapper mapper)
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
            Laboratory laboratory;
            LaboratoryViewModel laboratoryViewModel;

            switch (query.RoleType)
            {
                case RoleType.WHO:
                case RoleType.BioHubFacility:
                    laboratory = await _readRepository.Read(query.Id, cancellationToken);
                    if (laboratory == null)
                        return new(new Errors(ErrorType.NotFound, $"Laboratory with Id {query.Id} not found"));
                    laboratoryViewModel = _mapper.Map(laboratory);
                    break;
                case RoleType.Laboratory:
                    laboratory = await _readRepository.ReadForLaboratoryUser(query.Id, query.UserLaboratoryId.GetValueOrDefault(), cancellationToken);
                    if (laboratory == null)
                        return new(new Errors(ErrorType.NotFound, $"Laboratory with Id {query.Id} not found"));
                    laboratoryViewModel = _mapper.MapForLaboratoryUser(laboratory);
                    break;
                default:
                    throw new InvalidOperationException();
                    break;
            }         
                        

            return new(new ReadLaboratoryQueryResponse(laboratoryViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Laboratory with Id {id}", query.Id);
            throw;
        }
    }
}