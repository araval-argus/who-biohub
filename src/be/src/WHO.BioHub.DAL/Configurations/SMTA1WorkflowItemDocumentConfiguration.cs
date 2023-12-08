using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class SMTA1WorkflowItemDocumentConfiguration : IEntityTypeConfiguration<SMTA1WorkflowItemDocument>
{
    public void Configure(EntityTypeBuilder<SMTA1WorkflowItemDocument> entity)
    {
        entity.HasKey(wd => new { wd.SMTA1WorkflowItemId, wd.DocumentId });

        entity.Property(wd => wd.IsDocumentFile)
            .IsRequired(false);

        entity.HasOne<SMTA1WorkflowItem>(wd => wd.SMTA1WorkflowItem)
            .WithMany(r => r.SMTA1WorkflowItemDocuments)
            .HasForeignKey(wd => wd.SMTA1WorkflowItemId);


        entity.HasOne<Document>(wd => wd.Document)
            .WithMany(d => d.SMTA1WorkflowItemDocuments)
            .HasForeignKey(wd => wd.DocumentId);
    }

    public new static SMTA1WorkflowItemDocumentConfiguration Default => new SMTA1WorkflowItemDocumentConfiguration();
}
