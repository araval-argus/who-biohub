using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.EForms.ListEForm;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.EForms.ListEForms;

public interface IListEFormsHandler
{
    Task<Either<ListEFormsQueryResponse, Errors>> Handle(ListEFormsQuery query, CancellationToken cancellationToken);
}

public class ListEFormsHandler : IListEFormsHandler
{
    private readonly ILogger<ListEFormsHandler> _logger;
    private readonly ListEFormsQueryValidator _validator;
    private readonly IWorklistToBioHubItemReadRepository _readToBioHubRepository;
    private readonly IWorklistFromBioHubItemReadRepository _readFromBioHubRepository;
    private readonly IListEFormsMapper _listEFormsMapper;

    public ListEFormsHandler(
        ILogger<ListEFormsHandler> logger,
        ListEFormsQueryValidator validator,
        IWorklistToBioHubItemReadRepository readToBioHubRepository,
        IWorklistFromBioHubItemReadRepository readFromBioHubRepository,
        IListEFormsMapper listEFormsMapper)
    {
        _logger = logger;
        _validator = validator;
        _readToBioHubRepository = readToBioHubRepository;
        _readFromBioHubRepository = readFromBioHubRepository;
        _listEFormsMapper = listEFormsMapper;
    }

    public async Task<Either<ListEFormsQueryResponse, Errors>> Handle(
        ListEFormsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IEnumerable<WorklistToBioHubItem> worklistToBioHubItems;

        IEnumerable<WorklistFromBioHubItem> worklistFromBioHubItems;

        try
        {
            switch (query.RoleType)
            {
                case RoleType.WHO:
                    worklistToBioHubItems = await _readToBioHubRepository.ReadForEformList(cancellationToken);
                    worklistFromBioHubItems = await _readFromBioHubRepository.ReadForEformList(cancellationToken);
                    break;

                case RoleType.Laboratory:
                    worklistToBioHubItems = await _readToBioHubRepository.ReadForEformListForLaboratory(query.LaboratoryId.GetValueOrDefault(), cancellationToken);
                    worklistFromBioHubItems = await _readFromBioHubRepository.ReadForEformListForLaboratory(query.LaboratoryId.GetValueOrDefault(), cancellationToken);
                    break;

                case RoleType.BioHubFacility:
                    worklistToBioHubItems = await _readToBioHubRepository.ReadForEformListForBioHubFacility(query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);
                    worklistFromBioHubItems = await _readFromBioHubRepository.ReadForEformListForBioHubFacility(query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);

                    break;

                default:
                    throw new InvalidOperationException();
                    break;
            }           
                       
            var eFormItems = _listEFormsMapper.Map(worklistToBioHubItems.ToList(), worklistFromBioHubItems.ToList(), query.RoleType.GetValueOrDefault());
            return new(new ListEFormsQueryResponse(eFormItems));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all EForms");
            throw;
        }
    }
}