using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class SMTA2WorkflowItemDocumentConfiguration : IEntityTypeConfiguration<SMTA2WorkflowItemDocument>
{
    public void Configure(EntityTypeBuilder<SMTA2WorkflowItemDocument> entity)
    {
        entity.HasKey(wd => new { wd.SMTA2WorkflowItemId, wd.DocumentId });

        entity.Property(wd => wd.IsDocumentFile)
            .IsRequired(false);

        entity.HasOne<SMTA2WorkflowItem>(wd => wd.SMTA2WorkflowItem)
            .WithMany(r => r.SMTA2WorkflowItemDocuments)
            .HasForeignKey(wd => wd.SMTA2WorkflowItemId);


        entity.HasOne<Document>(wd => wd.Document)
            .WithMany(d => d.SMTA2WorkflowItemDocuments)
            .HasForeignKey(wd => wd.DocumentId);
    }

    public new static SMTA2WorkflowItemDocumentConfiguration Default => new SMTA2WorkflowItemDocumentConfiguration();
}
