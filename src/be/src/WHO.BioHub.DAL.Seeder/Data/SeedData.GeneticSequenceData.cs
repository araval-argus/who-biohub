using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static GeneticSequenceData[] GeneticSequenceDatas = new GeneticSequenceData[]
    {
        new()
        {
            Id = GeneticSequenceDataId1,
            Code = "GISAID",
            Name = "GISAID",
            Description = "GISAID",
            IsActive = true
        },
        new()
        {
            Id = GeneticSequenceDataId2,
            Code = "Other",
            Name = "Other",
            Description = "Other",
            IsActive = true
        },
        new()
        {
            Id = GeneticSequenceDataId3,
            Code = "GenBank",
            Name = "GenBank",
            Description = "GenBank",
            IsActive = true
        },
        new()
        {
            Id = GeneticSequenceDataId4,
            Code = "INSDC",
            Name = "INSDC",
            Description = "INSDC",
            IsActive = true
        },

        
    };

    internal static Guid GeneticSequenceDataId1 => Guid.Parse("8dd0f1e6-52dd-42f5-bf3a-757c547f41b5");
    internal static Guid GeneticSequenceDataId2 => Guid.Parse("6365d1c8-2809-4fc9-8e1f-0360a2a43d50");
    internal static Guid GeneticSequenceDataId3 => Guid.Parse("d9194088-92d1-4d21-8d43-b3bb5deb69d4");
    internal static Guid GeneticSequenceDataId4 => Guid.Parse("cea37a4b-9cf3-4330-83e4-e80607e6cf30");

    private async Task AddOrUpdateGeneticSequenceDatas(CancellationToken cancellationToken)
    {
        foreach (var geneticSequenceData in GeneticSequenceDatas)
        {
            if (await _db.GeneticSequenceDatas.Where(x => x.Id == geneticSequenceData.Id).AnyAsync(cancellationToken))
            {
                _db.Update(geneticSequenceData);
            }
            else
            {
                await _db.AddAsync(geneticSequenceData);
            }
        }        
    }
}