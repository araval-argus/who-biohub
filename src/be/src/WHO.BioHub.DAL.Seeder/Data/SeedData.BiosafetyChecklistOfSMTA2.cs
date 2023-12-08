using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static BiosafetyChecklistOfSMTA2[] BiosafetyChecklistOfSMTA2s => new BiosafetyChecklistOfSMTA2[]
        {

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id1,
                    Order = 1,
                    Condition = "<p>Biosafety and biosecurity risk assessment(s) are completed for the work to be carried out with the BMEPP to be received</p>",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },
                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id2,
                    Order = 2,
                    Condition = "<p>Facility for storing and working with the BMEPP meets core requirements as outlined in the Laboratory biosafety manual 4th edition (LBM4)</p>",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id16,
                    Order = 3,
                    Condition = "<p>Will heightened control measures, as outlined in the LBM4, be required for working with the BMEPP?</p>",
                    Mandatory = false,
                    Current = false,
                    Selectable = true,
                    IsParentCondition = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id3,
                    Order = 4,
                    Condition = "<p>Facility for working with the BMEPP meets heightened control measures as outlined in the LBM4, where required</p>",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                    ParentConditionId = BiosafetyChecklistOfSMTA2Id16,
                    ShowOnParentValue = true
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id17,
                    Order = 5,
                    Condition = "<p>Will maximum contained measures, as outlined in the LBM4, be required for working with the BMEPP?</p>",
                    Mandatory = false,
                    Current = false,
                    Selectable = true,
                    IsParentCondition = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id4,
                    Order = 6,
                    Condition = "<p>Facility for working with the BMEPP meets maximum containment measures as outlined in the LBM4, where required</p>",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                    ParentConditionId = BiosafetyChecklistOfSMTA2Id17,
                    ShowOnParentValue = true
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id5,
                    Order = 7,
                    Condition = "<p>Biosafety manual, containing institutional policies, programmes and plans as well as responsibilities of biosafety personnel, is in place</p>",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },


                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id6,
                    Order = 8,
                    Condition = "<p>Biosecurity plan, outlining the security measures including personnel reliability, is in place</p>",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id7,
                    Order = 9,
                    Condition = "<p>Commitment and a transparent regime are in place to address dual-use research of concern (DURC)</p>",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id8,
                    Order = 10,
                    Condition = "<p>Training programme, competency assessments and records are in place</p>",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id9,
                    Order = 11,
                    Condition = "<p>Medical surveillance programme(s) is in place</p>",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id10,
                    Order = 12,
                    Condition = "<p>Emergency response plan is in place</p>",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id11,
                    Order = 13,
                    Condition = "<p>Incident reporting system and records are in place",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id12,
                    Order = 14,
                    Condition = "<p>Inventory system is in place",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id13,
                    Order = 15,
                    Condition = "<p>Have records of audits and inspections",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id14,
                    Order = 16,
                    Condition = "<p>Have a maintenance and servicing programme for equipment and technical installations",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id15,
                    Order = 17,
                    Condition = "<p>Have export and import permits",
                    Mandatory = true,
                    Current = false,
                    Selectable = true,
                },



                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id18,
                    Order = 1,
                    Condition = "<p>Biosafety and biosecurity risk assessment(s) have been completed for the work to be carried out with the BMEPP to be received</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },
                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id19,
                    Order = 2,
                    Condition = "<p>Core requirements, as outlined in the WHO Laboratory Biosafety Manual, 4th edition (<span style='color: rgb(0, 154, 222)'><a style='color: rgb(0, 154, 222)' href='https://www.who.int/publications/i/item/9789240011311?sequence=1&isAllowed=y' target='_blank'>LBM4<sup>2</sup></a></span>), are in place for storing and working with the BMEPP</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },



                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id20,
                    Order = 3,
                    Condition = "<p>Biosafety manual, containing institutional policies, programmes and plans as well as responsibilities of biosafety personnel, is in place</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },


                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id21,
                    Order = 4,
                    Condition = "<p>Biosecurity plan, outlining the security measures including personnel reliability, is in place</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id22,
                    Order = 5,
                    Condition = "<p>Commitment and a transparent regime are in place to address dual-use research of concern (DURC)</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id23,
                    Order = 6,
                    Condition = "<p>Training programme, competency assessments and records are in place</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id24,
                    Order = 7,
                    Condition = "<p>Medical surveillance programme(s) is in place</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id25,
                    Order = 8,
                    Condition = "<p>Emergency response plan is in place</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id26,
                    Order = 9,
                    Condition = "<p>Incident reporting system and records are in place",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id27,
                    Order = 10,
                    Condition = "<p>Inventory system is in place",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id28,
                    Order = 11,
                    Condition = "<p>Have records of audits and inspections",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id29,
                    Order = 12,
                    Condition = "<p>Have a maintenance and servicing programme for equipment and technical installations",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id30,
                    Order = 13,
                    Condition = "<p>Have export and import permits",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },
                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id31,
                    Order = 14,
                    Condition = "<p>Will heightened control measures, as determined by local risk assessment and guided by the <span style='color: rgb(0, 154, 222)'><a style='color: rgb(0, 154, 222)' href='https://www.who.int/publications/i/item/9789240011311?sequence=1&isAllowed=y' target='_blank'>LBM4<sup>2</sup></a></span> be required for working with the BMEPP?</p>",
                    Mandatory = false,
                    Current = true,
                    Selectable = true,
                    IsParentCondition = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id32,
                    Order = 15,
                    Condition = "<p>Heightened control measures, as determined by local risk assessment and guided by the <span style='color: rgb(0, 154, 222)'><a style='color: rgb(0, 154, 222)' href='https://www.who.int/publications/i/item/9789240011311?sequence=1&isAllowed=y' target='_blank'>LBM4<sup>2</sup></a></span>, are in place</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                    ParentConditionId = BiosafetyChecklistOfSMTA2Id31,
                    ShowOnParentValue = true
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id33,
                    Order = 16,
                    Condition = "<p>Will maximum containment measures, as determined by local risk assessment and guided by the <span style='color: rgb(0, 154, 222)'><a style='color: rgb(0, 154, 222)' href='https://www.who.int/publications/i/item/9789240011311?sequence=1&isAllowed=y' target='_blank'>LBM4<sup>2</sup></a></span>, be required for working with the BMEPP?</p>",
                    Mandatory = false,
                    Current = true,
                    Selectable = true,
                    IsParentCondition = true,
                },

                new()
                {
                    Id = BiosafetyChecklistOfSMTA2Id34,
                    Order = 17,
                    Condition = "<p>Maximum containment measures, as determined by local risk assessment and guided by the <span style='color: rgb(0, 154, 222)'><a style='color: rgb(0, 154, 222)' href='https://www.who.int/publications/i/item/9789240011311?sequence=1&isAllowed=y' target='_blank'>LBM4<sup>2</sup></a></span>, are in place</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                    ParentConditionId = BiosafetyChecklistOfSMTA2Id33,
                    ShowOnParentValue = true
                },


        };

    internal static Guid BiosafetyChecklistOfSMTA2Id1 => Guid.Parse("217e4ab9-922f-407e-8d8e-330ca6f392f8");
    internal static Guid BiosafetyChecklistOfSMTA2Id2 => Guid.Parse("df785dfe-2188-4618-8508-5e501a8674ee");
    internal static Guid BiosafetyChecklistOfSMTA2Id3 => Guid.Parse("c9c037b4-53d5-4132-91fb-f70f8af72504");
    internal static Guid BiosafetyChecklistOfSMTA2Id4 => Guid.Parse("b9b22dbc-ca0f-450c-bb36-90c7adc19f5d");
    internal static Guid BiosafetyChecklistOfSMTA2Id5 => Guid.Parse("cef0cc3f-0942-4589-8ff9-6d22ae33cf7e");
    internal static Guid BiosafetyChecklistOfSMTA2Id6 => Guid.Parse("a9340324-9f9a-4b2f-a27b-e76d92b589f5");
    internal static Guid BiosafetyChecklistOfSMTA2Id7 => Guid.Parse("c2683a28-fbbb-4c7c-810e-c5b6210db94a");
    internal static Guid BiosafetyChecklistOfSMTA2Id8 => Guid.Parse("07f0c41f-e2ed-4199-a2ba-5d0f87d6869b");
    internal static Guid BiosafetyChecklistOfSMTA2Id9 => Guid.Parse("3d621f02-6588-4d48-9980-fda6d9ca9b41");
    internal static Guid BiosafetyChecklistOfSMTA2Id10 => Guid.Parse("cee8de53-c6de-4f0e-9070-b1a09b7dad03");
    internal static Guid BiosafetyChecklistOfSMTA2Id11 => Guid.Parse("105c3251-2e4a-4822-8e6d-bf9cde77dea7");
    internal static Guid BiosafetyChecklistOfSMTA2Id12 => Guid.Parse("fc80c82a-c087-42d5-9c14-79fd112552bd");
    internal static Guid BiosafetyChecklistOfSMTA2Id13 => Guid.Parse("26c3f8fe-b703-4780-bb4e-979a416fa78d");
    internal static Guid BiosafetyChecklistOfSMTA2Id14 => Guid.Parse("05c3f404-5903-41da-8147-f3e09b996b65");
    internal static Guid BiosafetyChecklistOfSMTA2Id15 => Guid.Parse("ebffc8e0-01d4-4a7c-9c0e-16b25e8c48f7");

    internal static Guid BiosafetyChecklistOfSMTA2Id16 => Guid.Parse("acff96f2-7147-4f4c-9a0f-391f4329d76d");
    internal static Guid BiosafetyChecklistOfSMTA2Id17 => Guid.Parse("a96bcb90-f242-4f9d-a072-8ecc161dc93e");

    internal static Guid BiosafetyChecklistOfSMTA2Id18 => Guid.Parse("4097047d-26f7-47fd-a32e-8a2d9ae1ca9b");
    internal static Guid BiosafetyChecklistOfSMTA2Id19 => Guid.Parse("e735a22a-4476-410a-9eda-fdcaa02c1963");
    internal static Guid BiosafetyChecklistOfSMTA2Id20 => Guid.Parse("73f4edd0-f9c5-4b3b-9066-7b6d2df4e81b");
    internal static Guid BiosafetyChecklistOfSMTA2Id21 => Guid.Parse("c7a3cb79-63a3-43f6-aafa-92ee4b6d1c57");
    internal static Guid BiosafetyChecklistOfSMTA2Id22 => Guid.Parse("14c91f0f-88f1-45fa-b7bd-4e2b8e523073");
    internal static Guid BiosafetyChecklistOfSMTA2Id23 => Guid.Parse("4487c726-0d40-44cd-adfd-ef23e128683b");
    internal static Guid BiosafetyChecklistOfSMTA2Id24 => Guid.Parse("c9559973-6610-4b0f-ba89-295ab067421a");
    internal static Guid BiosafetyChecklistOfSMTA2Id25 => Guid.Parse("a899a10d-42dc-4ec7-9c70-1b2128ef9695");
    internal static Guid BiosafetyChecklistOfSMTA2Id26 => Guid.Parse("d4be32c2-8287-4681-8b3b-a8e56e38e0f1");
    internal static Guid BiosafetyChecklistOfSMTA2Id27 => Guid.Parse("5c939d62-829e-4510-a395-beee4c441fd8");
    internal static Guid BiosafetyChecklistOfSMTA2Id28 => Guid.Parse("7ad8c514-9763-426d-b3be-39ceab9420c1");
    internal static Guid BiosafetyChecklistOfSMTA2Id29 => Guid.Parse("30652d8e-0d3c-48fe-bcae-c8c9984fd21e");
    internal static Guid BiosafetyChecklistOfSMTA2Id30 => Guid.Parse("672f5d96-5800-4e9e-91ef-870cb15915df");
    internal static Guid BiosafetyChecklistOfSMTA2Id31 => Guid.Parse("c3a4de4c-6f9c-45b7-a7a7-c87604005ae4");
    internal static Guid BiosafetyChecklistOfSMTA2Id32 => Guid.Parse("4b292f1f-10c6-4d72-b2f7-6c1f2f133a1b");
    internal static Guid BiosafetyChecklistOfSMTA2Id33 => Guid.Parse("f3d10478-777c-438e-a480-1811e39452a4");
    internal static Guid BiosafetyChecklistOfSMTA2Id34 => Guid.Parse("a4e3b5a8-cbe1-42b3-aaf3-d63fa7d9e3f6");


    private async Task AddOrUpdateBiosafetyChecklistOfSMTA2s(CancellationToken cancellationToken)
    {

        foreach (var BiosafetyChecklistOfSMTA2 in BiosafetyChecklistOfSMTA2s)
        {
            if (await _db.BiosafetyChecklistOfSMTA2s.Where(x => x.Id == BiosafetyChecklistOfSMTA2.Id).AnyAsync(cancellationToken))
            {
                _db.Update(BiosafetyChecklistOfSMTA2);
            }
            else
            {
                await _db.AddAsync(BiosafetyChecklistOfSMTA2);
            }
        }

        var exludedList = _db.BiosafetyChecklistOfSMTA2s.Where(x => !(BiosafetyChecklistOfSMTA2s.Select(x => x.Id).Contains(x.Id)));

        foreach (var BiosafetyChecklistOfSMTA2 in exludedList)
        {
            BiosafetyChecklistOfSMTA2.Current = false;
            _db.Update(BiosafetyChecklistOfSMTA2);
        }
    }
}
