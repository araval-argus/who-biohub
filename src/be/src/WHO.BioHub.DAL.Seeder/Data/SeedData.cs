using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace WHO.BioHub.DAL.Seeder.Data;

internal interface ISeedData
{
    Task SeedAll(CancellationToken cancellationToken);
}

internal partial class SeedData : ISeedData
{
    private readonly ILogger<SeedData> _logger;
    private readonly BioHubDbContext _db;

    public SeedData(
        ILogger<SeedData> logger,
        BioHubDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task SeedAll(CancellationToken cancellationToken)
    {
        //if (!await _db.BioHubFacilities.AnyAsync(cancellationToken))
        //{
        //    await AddBioHubFacilities(cancellationToken);
        //}
       
        await AddOrUpdateBSLLevels(cancellationToken);
       
      
        await AddOrUpdateCountries(cancellationToken);
       

        await AddOrUpdateCultivabilityTypes(cancellationToken);

        await AddOrUpdateGeneticSequenceDatas(cancellationToken);


        await AddOrUpdateInternationalTaxonomyClassifications(cancellationToken);


        await AddOrUpdateIsolationHostTypes(cancellationToken);

        await AddOrUpdateIsolationTechniqueTypes(cancellationToken);

        //if (!await _db.Laboratories.AnyAsync(cancellationToken))
        //{
        //    await AddLaboratories(cancellationToken);
        //}

        await AddOrUpdateMaterialProducts(cancellationToken);


        await AddOrUpdateMaterialTypes(cancellationToken);


        await AddOrUpdateMaterialUsagePermissions(cancellationToken);

       
        await AddOrUpdatePriorityRequestTypes(cancellationToken);
       

       
        await AddOrUpdateTemperatureUnitOfMeasures(cancellationToken);
       

        await AddOrUpdateTransportCategories(cancellationToken);


        await AddOrUpdateTransportModes(cancellationToken);


        await AddOrUpdateRoles(cancellationToken);



        await AddOrUpdateUserRequestStatuses(cancellationToken);



        await AddOrUpdatePermissions(cancellationToken);


        await AddOrUpdateRolePermissions(cancellationToken);


        //if (!await _db.DocumentTemplates.AnyAsync(cancellationToken))
        //{
        //    await AddDocumentTemplates(cancellationToken);
        //}

        //if (!await _db.Resources.AnyAsync(cancellationToken))
        //{
        //    await AddResources(cancellationToken);
        //}

        await AddOrUpdateSMTA1WorkflowEmails(cancellationToken);
        await AddOrUpdateSMTA2WorkflowEmails(cancellationToken);
        await AddOrUpdateWorklistToBioHubEmails(cancellationToken);
        await AddOrUpdateWorklistFromBioHubEmails(cancellationToken);
        await AddOrUpdateAnnex2OfSMTA2Conditions(cancellationToken);
        await AddOrUpdateBiosafetyChecklistOfSMTA2s(cancellationToken);

        await AddOrUpdateSpecimenTypes(cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);
    }
}