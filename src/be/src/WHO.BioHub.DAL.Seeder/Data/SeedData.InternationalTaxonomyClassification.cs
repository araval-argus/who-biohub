using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static InternationalTaxonomyClassification[] InternationalTaxonomyClassifications => new InternationalTaxonomyClassification[]
    {
        new()
        {
            Id = InternationalTaxonomyClassificationId1,
            Name = "Severe acute respiratory syndrome-related coronavirus",
            Description = "Severe acute respiratory syndrome-related coronavirus",
            IsActive = true
        },
    };

    internal static Guid InternationalTaxonomyClassificationId1 => Guid.Parse("55495001-8b5a-46e8-bbf6-7adaf3503632");


    private async Task AddOrUpdateInternationalTaxonomyClassifications(CancellationToken cancellationToken)
    {

        foreach (var internationalTaxonomyClassification in InternationalTaxonomyClassifications)
        {
            if (await _db.InternationalTaxonomyClassifications.Where(x => x.Id == internationalTaxonomyClassification.Id).AnyAsync(cancellationToken))
            {
                _db.Update(internationalTaxonomyClassification);
            }
            else
            {
                await _db.AddAsync(internationalTaxonomyClassification);
            }
        }
    }
}
