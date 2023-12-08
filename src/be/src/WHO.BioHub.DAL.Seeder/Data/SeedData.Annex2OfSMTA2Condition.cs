using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static Annex2OfSMTA2Condition[] Annex2OfSMTA2Conditions => new Annex2OfSMTA2Condition[]
        {
                new()
                {
                    Id = Annex2OfSMTA2ConditionId1,
                    Order = 1,
                    PointNumber = "<p>1.</p>",
                    Condition = "<p>1. The recipient Qualified Entity warrants that it meets applicable biosafety & biosecurity standards for the respective BMEPP as per the WHO guidance in this area.</p><br /><p><strong>Note:</strong> By filling in this Annex, the Qualified Entity commits to a dynamic self re-evaluation of the biosecurity and biosafety risks, based on their (self-assessed) risk level, and consult WHO specifically in case of doubt or if Dual Use Research of Concern is contemplated.</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },
                new()
                {
                    Id = Annex2OfSMTA2ConditionId2,
                    Order = 2,
                    PointNumber = "<p>2.</p>",
                    Condition = "<p>2. The Qualified Entity warrants to use the BMEPP solely <u>for <strong>non-commercial public health</strong></u> purposes</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },
                new()
                {
                    Id = Annex2OfSMTA2ConditionId3,
                    Order = 3,
                    PointNumber = "<p>2a.</p>",
                    Condition = "<p>2a. The Qualified Entity has signed an SMTA 2</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = false,
                    FlagPresetValue = true,
                },
                new()
                {
                    Id = Annex2OfSMTA2ConditionId4,
                    Order = 4,
                    PointNumber = "<p>3.</p>",
                    Condition = "<p>3. The Qualified Entity agrees with the Guiding Principles of the WHO BioHub System (See Annex1 of SMTA 1)</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },
                new()
                {
                    Id = Annex2OfSMTA2ConditionId5,
                    Order = 5,
                    PointNumber = "<p>3a.</p>",
                    Condition = "<p>3a. The Qualified Entity will endeavour to ensure participation of Provider scientists, especially those from developing countries, to the fullest extent possible, in scientific projects associated with research on BMEPP from their countries and active engagement of such scientists in the preparation of manuscripts for presentation and publication.</p><br /><p>The Qualified Entity agrees to ensure appropriate acknowledgement in presentations and publications, of the contributions from BMEPP Provider scientists using existing scientific guidelines.</p>",
                    Mandatory = true,
                    Current = true,
                    Selectable = true,
                },

        };

    internal static Guid Annex2OfSMTA2ConditionId1 => Guid.Parse("36b1d417-2161-4573-ad43-eaa92f2db296");
    internal static Guid Annex2OfSMTA2ConditionId2 => Guid.Parse("d68bc61f-666b-488a-a7d4-54a5ddcc04f4");

    internal static Guid Annex2OfSMTA2ConditionId3 => Guid.Parse("450dd3fa-bbcc-4b17-abf7-04fea438aafb");
    internal static Guid Annex2OfSMTA2ConditionId4 => Guid.Parse("6f923404-af67-43a1-9de8-caeac2af6416");
    internal static Guid Annex2OfSMTA2ConditionId5 => Guid.Parse("e75060b0-c1e0-4d17-b197-7bfc22bf6e94");



    private async Task AddOrUpdateAnnex2OfSMTA2Conditions(CancellationToken cancellationToken)
    {
        foreach (var Annex2OfSMTA2Condition in Annex2OfSMTA2Conditions)
        {
            if (await _db.Annex2OfSMTA2Conditions.Where(x => x.Id == Annex2OfSMTA2Condition.Id).AnyAsync(cancellationToken))
            {
                _db.Update(Annex2OfSMTA2Condition);
            }
            else
            {
                await _db.AddAsync(Annex2OfSMTA2Condition);
            }
        }
    }
}
