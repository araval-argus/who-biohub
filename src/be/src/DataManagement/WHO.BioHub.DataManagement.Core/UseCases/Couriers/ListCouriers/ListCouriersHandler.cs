using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.ListCouriers;

public interface IListCouriersHandler
{
    Task<Either<ListCouriersQueryResponse, Errors>> Handle(ListCouriersQuery query, CancellationToken cancellationToken);
}

public class ListCouriersHandler : IListCouriersHandler
{
    private readonly ILogger<ListCouriersHandler> _logger;
    private readonly ListCouriersQueryValidator _validator;
    private readonly ICourierReadRepository _readRepository;
    private readonly IListCouriersMapper _mapper;

    public ListCouriersHandler(
        ILogger<ListCouriersHandler> logger,
        ListCouriersQueryValidator validator,
        ICourierReadRepository readRepository,
        IListCouriersMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListCouriersQueryResponse, Errors>> Handle(
        ListCouriersQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<Courier> couriers = await _readRepository.List(cancellationToken);

            var couriersViewModel = _mapper.Map(couriers);
            return new(new ListCouriersQueryResponse(couriersViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Couriers");
            throw;
        }
    }
}