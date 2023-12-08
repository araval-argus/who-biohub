﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class DocumentTemplateConfiguration : IEntityTypeConfiguration<DocumentTemplate>
{
    public static DocumentTemplateConfiguration Default => new();

    public void Configure(EntityTypeBuilder<DocumentTemplate> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.ParentId)
            .HasDatabaseName("IX_ParentId");


        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsRequired(true);

        builder.Property(e => e.Extension)
            .HasMaxLength(4)
            .IsRequired(false);


        builder
            .HasOne(e => e.UploadedBy)
            .WithMany(b => b.DocumentTemplates)
            .HasForeignKey(e => e.UploadedById)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
