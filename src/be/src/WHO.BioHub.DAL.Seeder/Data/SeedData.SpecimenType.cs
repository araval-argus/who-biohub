using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static SpecimenType[] SpecimenTypes = new SpecimenType[]
    {
        new()
        {
            Id = SpecimenTypeId1,
            Name = "Nasal swab",
            Description = "Nasal swab",
            IsActive = true
        },


        new()
        {
            Id = SpecimenTypeId2,
            Name = "Throat swab",
            Description = "Throat swab",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId3,
            Name = "Nasopharyngeal swab",
            Description = "Nasopharyngeal swab",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId4,
            Name = "Sputum",
            Description = "Sputum",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId5,
            Name = "Nasopharyngeal aspirate",
            Description = "Nasopharyngeal aspirate",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId6,
            Name = "Tracheal aspirate",
            Description = "Tracheal aspirate",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId7,
            Name = "Bronchoalveolar lavage",
            Description = "Bronchoalveolar lavage",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId8,
            Name = "Tissue biopsy",
            Description = "Tissue biopsy",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId9,
            Name = "Serum (first sample)",
            Description = "Serum (first sample)",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId10,
            Name = "Serum (second sample)",
            Description = "Serum (second sample)",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId11,
            Name = "Whole blood",
            Description = "Whole blood",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId12,
            Name = "Urine",
            Description = "Urine",
            IsActive = true
        },

        new()
        {
            Id = SpecimenTypeId13,
            Name = "Other",
            Description = "Other",
            IsActive = true
        },

    };

    internal static Guid SpecimenTypeId1 => Guid.Parse("f4253e71-254e-4cbc-9dcc-3615f64991b8");
    internal static Guid SpecimenTypeId2 => Guid.Parse("7959a286-5222-425b-a09c-367cf1d74019");
    internal static Guid SpecimenTypeId3 => Guid.Parse("04705d07-daaf-4d1e-97ed-4bf18560ccb7");
    internal static Guid SpecimenTypeId4 => Guid.Parse("9280b2eb-69c0-43be-98a6-784ccb63fb64");
    internal static Guid SpecimenTypeId5 => Guid.Parse("072daf8f-d212-4082-8ca4-e7ae5c8f0bcd");
    internal static Guid SpecimenTypeId6 => Guid.Parse("23e4e639-3b40-4075-9d71-ece1dda6f71f");
    internal static Guid SpecimenTypeId7 => Guid.Parse("1254a771-3616-4586-ab7e-250c482784f2");
    internal static Guid SpecimenTypeId8 => Guid.Parse("5db2836c-3a1f-406d-baa5-f18a3776a9e8");
    internal static Guid SpecimenTypeId9 => Guid.Parse("a18e2190-f97f-4d39-aa48-dad6128ec719");
    internal static Guid SpecimenTypeId10 => Guid.Parse("b7bcf5dc-8190-4203-b6f6-7828ed2f4914");
    internal static Guid SpecimenTypeId11 => Guid.Parse("a5ad5baf-7f42-48b5-ae0a-9d67863d171b");
    internal static Guid SpecimenTypeId12 => Guid.Parse("007c4c02-422f-4648-83ea-947369c342f2");
    internal static Guid SpecimenTypeId13 => Guid.Parse("90dc6be3-6c39-4690-9def-962491d538fe");

    private async Task AddOrUpdateSpecimenTypes(CancellationToken cancellationToken)
    {
        foreach (var specimenType in SpecimenTypes)
        {
            if (await _db.SpecimenTypes.Where(x => x.Id == specimenType.Id).AnyAsync(cancellationToken))
            {
                _db.Update(specimenType);
            }
            else
            {
                await _db.AddAsync(specimenType);
            }
        }
    }
}
