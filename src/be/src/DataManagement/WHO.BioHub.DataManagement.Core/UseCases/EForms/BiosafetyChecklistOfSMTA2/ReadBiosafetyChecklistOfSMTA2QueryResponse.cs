using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
namespace WHO.BioHub.Data.Core.UseCases.EForms.BiosafetyChecklistOfSMTA2;

public record struct ReadBiosafetyChecklistOfSMTA2QueryResponse(BiosafetyChecklistOfSMTA2DataViewModel BiosafetyChecklistOfSMTA2Data) { }