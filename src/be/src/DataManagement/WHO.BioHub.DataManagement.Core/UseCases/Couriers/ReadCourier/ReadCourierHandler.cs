using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.ReadCourier;

public interface IReadCourierHandler
{
    Task<Either<ReadCourierQueryResponse, Errors>> Handle(ReadCourierQuery query, CancellationToken cancellationToken);
}

public class ReadCourierHandler : IReadCourierHandler
{
    private readonly ILogger<ReadCourierHandler> _logger;
    private readonly ReadCourierQueryValidator _validator;
    private readonly ICourierReadRepository _readRepository;
    private readonly IReadCourierMapper _mapper;

    public ReadCourierHandler(
        ILogger<ReadCourierHandler> logger,
        ReadCourierQueryValidator validator,
        ICourierReadRepository readRepository,
        IReadCourierMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadCourierQueryResponse, Errors>> Handle(
        ReadCourierQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Courier courier = await _readRepository.Read(query.Id, cancellationToken);
            if (courier == null)
                return new(new Errors(ErrorType.NotFound, $"Courier with Id {query.Id} not found"));

            var courierViewModel = _mapper.Map(courier);

            return new(new ReadCourierQueryResponse(courierViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Courier with Id {id}", query.Id);
            throw;
        }
    }
}