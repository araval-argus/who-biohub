using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistToBioHubItemDocumentConfiguration : IEntityTypeConfiguration<WorklistToBioHubItemDocument>
{
    public void Configure(EntityTypeBuilder<WorklistToBioHubItemDocument> entity)
    {
        entity.HasKey(wd => new { wd.WorklistToBioHubItemId, wd.DocumentId });

        entity.Property(wd => wd.IsDocumentFile)
            .IsRequired(false);

        entity.HasOne<WorklistToBioHubItem>(wd => wd.WorklistToBioHubItem)
            .WithMany(r => r.WorklistToBioHubItemDocuments)
            .HasForeignKey(wd => wd.WorklistToBioHubItemId);


        entity.HasOne<Document>(wd => wd.Document)
            .WithMany(d => d.WorklistToBioHubItemDocuments)
            .HasForeignKey(wd => wd.DocumentId);
    }

    public new static WorklistToBioHubItemDocumentConfiguration Default => new WorklistToBioHubItemDocumentConfiguration();
}
