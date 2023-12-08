using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static DocumentTemplate[] DocumentTemplates = new DocumentTemplate[]
    {
        new()
        {
            Id = DocumentTemplateId1,
            Type = DocumentTemplateType.Folder,
            Name = "Root"
        },

    };

    internal static Guid DocumentTemplateId1 => Guid.Parse("807ea670-33c7-48e5-995b-43b0583c0ee9");


    private async Task AddDocumentTemplates(CancellationToken cancellationToken)
    {

        await _db.AddRangeAsync(DocumentTemplates, cancellationToken: cancellationToken);
    }
}