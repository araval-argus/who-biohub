using FluentValidation;
using WHO.BioHub.Models.Repositories.Materials;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.CreateMaterial;

public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
{
    private readonly IMaterialReadRepository _materialReadRepository;
    public CreateMaterialCommandValidator(IMaterialReadRepository materialReadRepository)
    {
        _materialReadRepository = materialReadRepository;

        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("'Name' is required");

        RuleFor(cmd => cmd.ReferenceNumber)
            .NotEmpty().WithMessage("'WHO BMEPP Reference Number' is required")
            .MustAsync(async (command, referenceNumber, token) => await ReferenceNumberNotPresent(referenceNumber, token)).WithMessage("'WHO BMEPP Reference Number' already present"); ;


        RuleFor(cmd => cmd.Temperature)
            .NotNull().WithMessage("'Temperature' is required");

        When(cmd => cmd.Age != null, () =>
        {
            RuleFor(cmd => cmd.Age)
                .GreaterThanOrEqualTo(0).WithMessage("Invalid 'Age'");
        });

        RuleFor(cmd => cmd.Description)
        .NotEmpty()
        .WithMessage("'Description' is required");


        RuleFor(cmd => cmd.SampleId)
        .NotEmpty()
        .WithMessage("'Sample Id' is required");

        RuleFor(cmd => cmd.Lineage)
        .NotEmpty()
        .WithMessage("'Lineage' is required");

        RuleFor(cmd => cmd.Variant)
        .NotEmpty()
        .WithMessage("'Variant' is required");

        RuleFor(cmd => cmd.VariantAssessment)
        .NotEmpty()
        .WithMessage("'Variant Assessment' is required");

        RuleFor(cmd => cmd.StrainDesignation)
        .NotEmpty()
        .WithMessage("'Strain Designation' is required");

        RuleFor(cmd => cmd.Genotype)
        .NotEmpty()
        .WithMessage("'Genotype' is required");

        RuleFor(cmd => cmd.Serotype)
        .NotEmpty()
        .WithMessage("'Serotype' is required");

        RuleFor(cmd => cmd.DatabaseAccessionId)
        .NotEmpty()
        .WithMessage("'Accession ID in External Registered Database for GSD' is required");

        RuleFor(cmd => cmd.OriginalGeneticSequence)
        .NotEmpty()
        .WithMessage("'Original Genetic Sequence' is required");

        RuleFor(cmd => cmd.GSDCulturedMaterialCellLine1)
        .NotEmpty()
        .WithMessage("'GSD of Cultured Material in Cell Line 1' is required");

        RuleFor(cmd => cmd.GSDCulturedMaterialCellLine2)
        .NotEmpty()
        .WithMessage("'GSD of Cultured Material in Cell Line 2' is required");

        RuleFor(cmd => cmd.FacilityGSD)
        .NotEmpty()
        .WithMessage("'Facility that Generated GSD' is required");


        //RuleFor(cmd => cmd.ProductionCellLine)
        //.NotEmpty()
        //.WithMessage("'Production Cell Line' is required");

        RuleFor(cmd => cmd.Infectivity)
        .NotEmpty()
        .WithMessage("'Infectivity' is required");

        RuleFor(cmd => cmd.ViralTiter)
        .NotEmpty()
        .WithMessage("'Viral Titer' is required");

        RuleFor(cmd => cmd.TypeId)
        .NotEmpty()
        .WithMessage("'Type' is required");

        RuleFor(cmd => cmd.SuspectedEpidemiologicalOriginId)
        .NotEmpty()
        .WithMessage("'Suspected Epidemiological Origin' is required");

        //RuleFor(cmd => cmd.OriginalProductTypeId)
        //.NotEmpty()
        //.WithMessage("'Original Product Type' is required");

        RuleFor(cmd => cmd.ProductTypeId)
        .NotEmpty()
        .WithMessage("'Product Type' is required");

        RuleFor(cmd => cmd.CulturingCellLine1)
        .NotEmpty()
        .WithMessage("'Culturing Cell Line 1' is required");

        RuleFor(cmd => cmd.CulturingCellLine2)
       .NotEmpty()
       .WithMessage("'Culturing Cell Line 2' is required");

        // RuleFor(cmd => cmd.CulturingCellLine3)
        //.NotEmpty()
        //.WithMessage("'Culturing Cell Line 3' is required");



        RuleFor(cmd => cmd.TransportCategoryId)
        .NotEmpty()
        .WithMessage("'Transport Category' is required");

        RuleFor(cmd => cmd.UnitOfMeasureId)
        .NotEmpty()
        .WithMessage("'Unit Of Measure' is required");

        RuleFor(cmd => cmd.UsagePermissionId)
        .NotEmpty()
        .WithMessage("'Usage Permission' is required");

        RuleFor(cmd => cmd.GeneticSequenceDataId)
        .NotEmpty()
        .WithMessage("'Genetic Sequence Data' is required");

        RuleFor(cmd => cmd.InternationalTaxonomyClassificationId)
        .NotEmpty()
        .WithMessage("'International Taxonomy Classification' is required");

        RuleFor(cmd => cmd.IsolationHostTypeId)
        .NotEmpty()
        .WithMessage("'Isolation Host Type' is required");

        RuleFor(cmd => cmd.CultivabilityTypeId)
        .NotEmpty()
        .WithMessage("'Cultivability Type' is required");

        RuleFor(cmd => cmd.IsolationTechniqueTypeId)
        .NotEmpty()
        .WithMessage("'IsolationTechniqueType' is required");

        RuleFor(cmd => cmd.CollectionDate)
        .NotEmpty()
        .WithMessage("'Collection Date' is required");

        RuleFor(cmd => cmd.Location)
        .NotEmpty()
        .WithMessage("'Location' is required");

        RuleFor(cmd => cmd.PatientStatus)
        .NotEmpty()
        .WithMessage("'Patient Status' is required");

        RuleFor(cmd => cmd.Gender)
        .NotEmpty()
        .WithMessage("'Gender' is required");
    }

    protected async Task<bool> ReferenceNumberNotPresent(string referenceNumber, CancellationToken token)
    {
        return !(await _materialReadRepository.ReferenceNumberAlreadyPresentForCreation(referenceNumber, token));
    }
}