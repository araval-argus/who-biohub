using FluentValidation;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.SeedDataConstants;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterialForLaboratoryCompletion;

public class UpdateMaterialForLaboratoryCompletionCommandValidator : AbstractValidator<UpdateMaterialForLaboratoryCompletionCommand>
{
    private readonly IMaterialReadRepository _materialReadRepository;
    public UpdateMaterialForLaboratoryCompletionCommandValidator(IMaterialReadRepository materialReadRepository)
    {
        _materialReadRepository = materialReadRepository;

        RuleFor(cmd => cmd.ReferenceNumber)
             .NotEmpty().WithMessage("'WHO BMEPP Reference Number' is required")
             .MustAsync(async (command, referenceNumber, token) => await ReferenceNumberNotPresent(command.Id, referenceNumber, token)).WithMessage("'WHO BMEPP Reference Number' already present"); ;

        RuleFor(cmd => cmd.ReferenceNumber)
    .NotEmpty().WithMessage("'WHO BMEPP Reference Number' is required")
    .MustAsync(async (command, referenceNumber, token) => await ReferenceNumberNotPresent(command.Id, referenceNumber, token)).WithMessage("'WHO BMEPP Reference Number' already present"); ;

        When(cmd => cmd.CulturingResult != null, () =>
        {
            RuleFor(cmd => cmd.CulturingResultDate)
             .NotNull().WithMessage("'Culturing Result Date' is required");
        });

        When(cmd => cmd.QualityControlResult != null, () =>
        {
            RuleFor(cmd => cmd.QualityControlResultDate)           
             .NotNull().WithMessage("'Quality Control Result Date' is required");
        });

        When(cmd => cmd.GSDAnalysisResult != null, () =>
        {
            RuleFor(cmd => cmd.GSDAnalysisResultDate)
             .NotNull().WithMessage("'GSD Analysis Result Date' is required");
        });

        When(cmd => cmd.GSDUploadingStatus == GSDUploadingStatus.Uploaded, () =>
        {
            RuleFor(cmd => cmd.GSDUploadingDate)
             .NotNull().WithMessage("'GSD Uploading Date' is required");
        });

        When(cmd => cmd.NumberOfVialsToAdd != null, () =>
        {
            RuleFor(cmd => cmd.LastAliquotsAdditionDate)
             .NotNull().WithMessage("'Date' is required");
        });


        When(cmd => cmd.Approve == true, () =>
        {
            //   RuleFor(cmd => cmd.Name)
            //   .NotEmpty().WithMessage("'Name' is required");          

            //   RuleFor(cmd => cmd.Temperature)
            //       .NotEmpty().WithMessage("'Temperature' is required");

            //   RuleFor(cmd => cmd.Age)
            //       .NotEmpty().WithMessage("'Age' is required");

            //   When(cmd => cmd.Age != null, () =>
            //   {
            //       RuleFor(cmd => cmd.Age)
            //           .GreaterThanOrEqualTo(0).WithMessage("Invalid 'Age'");
            //   });


            //   RuleFor(cmd => cmd.Description)
            //   .NotEmpty()
            //   .WithMessage("'Description' is required");


            //   RuleFor(cmd => cmd.SampleId)
            //   .NotEmpty()
            //   .WithMessage("'Sample Id' is required");

            //   RuleFor(cmd => cmd.Lineage)
            //   .NotEmpty()
            //   .WithMessage("'Lineage' is required");

            //   RuleFor(cmd => cmd.Variant)
            //   .NotEmpty()
            //   .WithMessage("'Variant' is required");

            //   RuleFor(cmd => cmd.VariantAssessment)
            //   .NotEmpty()
            //   .WithMessage("'Variant Assessment' is required");

            //   RuleFor(cmd => cmd.StrainDesignation)
            //   .NotEmpty()
            //   .WithMessage("'Strain Designation at External Registered Database for GSD' is required");

            //   RuleFor(cmd => cmd.Genotype)
            //   .NotEmpty()
            //   .WithMessage("'Genotype' is required");

            //   RuleFor(cmd => cmd.Serotype)
            //   .NotEmpty()
            //   .WithMessage("'Serotype' is required");

            //   RuleFor(cmd => cmd.DatabaseAccessionId)
            //   .NotEmpty()
            //   .WithMessage("'Accession ID in External Registered Database for GSD' is required");

            //   RuleFor(cmd => cmd.OriginalGeneticSequence)
            //   .NotEmpty()
            //   .WithMessage("'Original Genetic Sequence' is required");

            //   RuleFor(cmd => cmd.GSDCulturedMaterialCellLine1)
            //   .NotEmpty()
            //   .WithMessage("'GSD of Cultured Material in Cell Line 1' is required");

            //   RuleFor(cmd => cmd.GSDCulturedMaterialCellLine2)
            //   .NotEmpty()
            //   .WithMessage("'GSD of Cultured Material in Cell Line 2' is required");

            //   RuleFor(cmd => cmd.FacilityGSD)
            //   .NotEmpty()
            //   .WithMessage("'Facility that Generated GSD' is required");

            //   //RuleFor(cmd => cmd.ProductionCellLine)
            //   //.NotEmpty()
            //   //.WithMessage("'Production Cell Line' is required");

            //   RuleFor(cmd => cmd.Infectivity)
            //   .NotEmpty()
            //   .WithMessage("'Infectivity' is required");

            //   RuleFor(cmd => cmd.ViralTiter)
            //   .NotEmpty()
            //   .WithMessage("'Viral Titer' is required");

            //   RuleFor(cmd => cmd.TypeId)
            //   .NotEmpty()
            //   .WithMessage("'Type' is required");

            //   RuleFor(cmd => cmd.SuspectedEpidemiologicalOriginId)
            //   .NotEmpty()
            //   .WithMessage("'Suspected Epidemiological Origin' is required");

            ////   RuleFor(cmd => cmd.OriginalProductTypeId)
            ////.NotEmpty()
            ////.WithMessage("'Original Product Type' is required");

            //   RuleFor(cmd => cmd.ProductTypeId)
            //   .NotEmpty()
            //   .WithMessage("'Product Type' is required");

            //   RuleFor(cmd => cmd.CulturingCellLine1)
            //   .NotEmpty()
            //   .WithMessage("'Culturing Cell Line 1' is required");

            //   RuleFor(cmd => cmd.CulturingCellLine2)
            //  .NotEmpty()
            //  .WithMessage("'Culturing Cell Line 2' is required");

            //  // RuleFor(cmd => cmd.CulturingCellLine3)
            //  //.NotEmpty()
            //  //.WithMessage("'Culturing Cell Line 3' is required");

            //   RuleFor(cmd => cmd.TransportCategoryId)
            //   .NotEmpty()
            //   .WithMessage("'Transport Category' is required");

            //   RuleFor(cmd => cmd.UnitOfMeasureId)
            //   .NotEmpty()
            //   .WithMessage("'Unit Of Measure' is required");

            //   RuleFor(cmd => cmd.UsagePermissionId)
            //   .NotEmpty()
            //   .WithMessage("'Usage Permission' is required");

            //   RuleFor(cmd => cmd.GeneticSequenceDataId)
            //   .NotEmpty()
            //   .WithMessage("'External Registered Database for Genetic Sequence Data (GSD)' is required");

            //   RuleFor(cmd => cmd.InternationalTaxonomyClassificationId)
            //   .NotEmpty()
            //   .WithMessage("'International Taxonomy Classification' is required");

            //   RuleFor(cmd => cmd.IsolationHostTypeId)
            //   .NotEmpty()
            //   .WithMessage("'Isolation Host Type' is required");

            //   RuleFor(cmd => cmd.CultivabilityTypeId)
            //   .NotEmpty()
            //   .WithMessage("'Cultivability Type' is required");

            //   RuleFor(cmd => cmd.IsolationTechniqueTypeId)
            //   .NotEmpty()
            //   .WithMessage("'IsolationTechniqueType' is required");

            //   RuleFor(cmd => cmd.CollectionDate)
            //   .NotEmpty()
            //   .WithMessage("'Collection Date' is required");

            //   RuleFor(cmd => cmd.Location)
            //   .NotEmpty()
            //   .WithMessage("'Location' is required");

            //   RuleFor(cmd => cmd.PatientStatus)
            //   .NotEmpty()
            //   .WithMessage("'Patient Status' is required");

            //   RuleFor(cmd => cmd.Gender)
            //   .NotEmpty()
            //   .WithMessage("'Gender' is required");

            ///**********************////
            //RuleFor(cmd => cmd.ReferenceNumberValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'ReferenceNumber' must be verified");

            //RuleFor(cmd => cmd.NameValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Name' must be verified");

            //RuleFor(cmd => cmd.DescriptionValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Description' must be verified");

            //RuleFor(cmd => cmd.TemperatureValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Temperature' must be verified");

            ////RuleFor(cmd => cmd.SampleIdValidation)
            ////.Must((e) => IsSelectionFieldCompleted(e))
            ////.WithMessage("'Sample Id' must be verified");

            //RuleFor(cmd => cmd.LineageValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Lineage' must be verified");

            //RuleFor(cmd => cmd.VariantValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Variant' must be verified");

            //RuleFor(cmd => cmd.VariantAssessmentValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Variant Assessment' must be verified");

            //RuleFor(cmd => cmd.StrainDesignationValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Strain Designation' must be verified");


            //RuleFor(cmd => cmd.DatabaseAccessionIdValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Gsd Registration Id' must be verified");

            ////RuleFor(cmd => cmd.OriginalGeneticSequenceValidation)
            ////.Must((e) => IsSelectionFieldCompleted(e))
            ////.WithMessage("'Original Genetic Sequence' must be verified");

            ////RuleFor(cmd => cmd.FacilityGSDValidation)
            ////.Must((e) => IsSelectionFieldCompleted(e))
            ////.WithMessage("'Facility that Generated GSD' must be verified");

            //RuleFor(cmd => cmd.GMOValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'GMO' must be verified");

            ////RuleFor(cmd => cmd.ProductionCellLineValidation)
            ////.Must((e) => IsSelectionFieldCompleted(e))
            ////.WithMessage("'Production Cell Line' must be verified");

            //RuleFor(cmd => cmd.InfectivityValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Infectivity' must be verified");

            //RuleFor(cmd => cmd.ViralTiterValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Viral Titer' must be verified");

            //RuleFor(cmd => cmd.TypeValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Pathogen Type' must be verified");

            //RuleFor(cmd => cmd.SuspectedEpidemiologicalOriginValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Suspected Epidemiological Origin' must be verified");

            //RuleFor(cmd => cmd.OriginalProductTypeValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Original Product Type' must be verified");


            //RuleFor(cmd => cmd.ProductTypeValidation)
            //.NotEmpty()
            //.WithMessage("'Product Type' must be verified");




            //RuleFor(cmd => cmd.TransportCategoryValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Transport Category' must be verified");

            //RuleFor(cmd => cmd.UnitOfMeasureValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Unit Of Measure' must be verified");

            //RuleFor(cmd => cmd.UsagePermissionValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Usage Permission' must be verified");

            //RuleFor(cmd => cmd.GeneticSequenceDataValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Genetic Sequence Data' must be verified");

            //RuleFor(cmd => cmd.InternationalTaxonomyClassificationValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'International Taxonomy Classification' must be verified");

            //RuleFor(cmd => cmd.IsolationHostTypeValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Isolation Host Type' must be verified");



            //RuleFor(cmd => cmd.IsolationTechniqueTypeValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'IsolationTechniqueType' must be verified");

            //RuleFor(cmd => cmd.CollectionDateValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Collection Date' must be verified");

            //RuleFor(cmd => cmd.LocationValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Location' must be verified");

            //RuleFor(cmd => cmd.PatientStatusValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Patient Status' must be verified");

            //RuleFor(cmd => cmd.AgeValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Age' must be verified");

            //RuleFor(cmd => cmd.GenderValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Gender' must be verified");


            //RuleFor(cmd => cmd.PathogenValidation)
            //.Must((e) => IsSelectionFieldCompleted(e))
            //.WithMessage("'Pathogen' must be verified");

            //RuleFor(cmd => cmd.ShipmentNumberOfVialsValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'ShipmentNumberOfVials' must be verified");
            //RuleFor(cmd => cmd.ShipmentAmountValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'ShipmentAmount' must be verified");
            //RuleFor(cmd => cmd.ShipmentTemperatureValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'ShipmentTemperature' must be verified");
            //RuleFor(cmd => cmd.ShipmentUnitOfMeasureValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Shipment Unit Of Measure' must be verified");

            //When(cmd => cmd.OriginalProductTypeId == Guid.Parse(SeedDataConstants.CulturedIsolateProductId), () =>
            //{
            //    RuleFor(cmd => cmd.CulturingCellLineValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Culturing Cell Line' must be verified");
            //    RuleFor(cmd => cmd.CulturingPassagesNumberValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Culturing Passages Number' must be verified");
            //});

            //When(cmd => cmd.OriginalProductTypeId == Guid.Parse(SeedDataConstants.ClinicalSpecimenProductId), () =>
            //{
            //    RuleFor(cmd => cmd.TypeOfTransportMediumValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Type Of Transport Medium' must be verified");
            //    RuleFor(cmd => cmd.BrandOfTransportMediumValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Brand Of Transport Medium' must be verified");
            //    RuleFor(cmd => cmd.MaterialCollectedSpecimenTypesValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Collected Specimen Type' must be verified");
            //});


            //RuleFor(cmd => cmd.DatabaseUploadedByValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Database Uploaded By' must be verified");
            //RuleFor(cmd => cmd.CulturingResultValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Culturing Result' must be verified");
            //RuleFor(cmd => cmd.CulturingResultDateValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Culturing Result Date' must be verified");
            //RuleFor(cmd => cmd.QualityControlResultValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Quality Control Result' must be verified");
            //RuleFor(cmd => cmd.QualityControlResultDateValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Quality Control Result Date' must be verified");
            //RuleFor(cmd => cmd.GSDAnalysisResultValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'GSD Analysis Result' must be verified");
            //RuleFor(cmd => cmd.GSDAnalysisResultDateValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'GSD Analysis Result Date' must be verified");
            //RuleFor(cmd => cmd.GSDUploadingStatusValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'GSD Uploading Status' must be verified");
            //RuleFor(cmd => cmd.GSDUploadingDateValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'GSD Uploading Date' must be verified");
            //RuleFor(cmd => cmd.MaterialGSDInfoValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Material GSD Info' must be verified");
            //RuleFor(cmd => cmd.DateOfBMEPPReceiptValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Arrival Date' must be verified");


            RuleFor(cmd => cmd.ReferenceNumberValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'ReferenceNumber' must be verified");


            RuleFor(cmd => cmd.NameValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Name' must be verified");


            RuleFor(cmd => cmd.TypeValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Pathogen Type' must be verified");


            RuleFor(cmd => cmd.PathogenValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Pathogen' must be verified");


            RuleFor(cmd => cmd.VariantValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Variant' must be verified");


            RuleFor(cmd => cmd.VariantAssessmentValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Variant Assessment' must be verified");


            RuleFor(cmd => cmd.GMOValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'GMO' must be verified");


            RuleFor(cmd => cmd.LineageValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Lineage (Pango)' must be verified");


            RuleFor(cmd => cmd.SuspectedEpidemiologicalOriginValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Suspected Epidemiological Origin' must be verified");


            RuleFor(cmd => cmd.UsagePermissionValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Usage Permission' must be verified");


            RuleFor(cmd => cmd.OwnerBioHubFacilityValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("Owner BioHub Facility Validation must be verified");


            RuleFor(cmd => cmd.DateOfBMEPPReceiptValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Arrival Date at BioHub Facility' must be verified");


            RuleFor(cmd => cmd.ProductTypeValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Product Type' must be verified");


            RuleFor(cmd => cmd.TemperatureValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Temperature in Storage' must be verified");


            RuleFor(cmd => cmd.UnitOfMeasureValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Unit (Temperature)' must be verified");


            RuleFor(cmd => cmd.OriginalProductTypeValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Material Type in Shipment to BioHub Facility' must be verified");


            RuleFor(cmd => cmd.TransportCategoryValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Material Category for Transport to BioHub Facility' must be verified");


            RuleFor(cmd => cmd.ShipmentNumberOfVialsValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Number of Vials Shipped to BioHub Facility' must be verified");


            RuleFor(cmd => cmd.ShipmentAmountValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Shipment Amount per Vial' must be verified");


            RuleFor(cmd => cmd.FreezingDateValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Sample Freezing Date' must be verified");


            RuleFor(cmd => cmd.ShipmentTemperatureValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Temperature in Storage' must be verified");


            RuleFor(cmd => cmd.ShipmentUnitOfMeasureValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Unit (Temperature)' must be verified");


            RuleFor(cmd => cmd.VirusConcentrationValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Virus Concentration' must be verified");


            When(cmd => cmd.OriginalProductTypeId == Guid.Parse(SeedDataConstants.CulturedIsolateProductId), () =>
            {
                RuleFor(cmd => cmd.CulturingCellLineValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Culturing Cell Line' must be verified");
                RuleFor(cmd => cmd.CulturingPassagesNumberValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Culturing Passages Number' must be verified");
            });

            When(cmd => cmd.OriginalProductTypeId == Guid.Parse(SeedDataConstants.ClinicalSpecimenProductId), () =>
            {
                RuleFor(cmd => cmd.TypeOfTransportMediumValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Type Of Transport Medium' must be verified");
                RuleFor(cmd => cmd.BrandOfTransportMediumValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Brand Of Transport Medium' must be verified");
                RuleFor(cmd => cmd.MaterialCollectedSpecimenTypesValidation).Must((e) => IsSelectionFieldCompleted(e)).WithMessage("'Collected Specimen Type' must be verified");
            });


            RuleFor(cmd => cmd.CollectionDateValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Sample Collection Date' must be verified");


            RuleFor(cmd => cmd.LocationValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Sampling Location' must be verified");


            RuleFor(cmd => cmd.IsolationHostTypeValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Host' must be verified");


            RuleFor(cmd => cmd.GenderValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Gender' must be verified");


            RuleFor(cmd => cmd.AgeValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Age' must be verified");


            RuleFor(cmd => cmd.PatientStatusValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Patient Status in Sampling' must be verified");


            RuleFor(cmd => cmd.CulturingResultValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Culturing Result' must be verified");


            RuleFor(cmd => cmd.CulturingResultDateValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Culturing Result Date' must be verified");


            RuleFor(cmd => cmd.IsolationTechniqueTypeValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Isolation Technique' must be verified");


            RuleFor(cmd => cmd.QualityControlResultValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Quality Control Result' must be verified");


            RuleFor(cmd => cmd.QualityControlResultDateValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Quality Control Result Date' must be verified");


            RuleFor(cmd => cmd.InfectivityValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Infectivity of Cultured Material' must be verified");


            RuleFor(cmd => cmd.ViralTiterValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Viral Titer of Cultured Material' must be verified");


            RuleFor(cmd => cmd.GSDAnalysisResultValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'GSD Analysis Result' must be verified");


            RuleFor(cmd => cmd.GSDAnalysisResultDateValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'GSD Analysis Result Date' must be verified");


            RuleFor(cmd => cmd.GSDUploadingStatusValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'GSD Uploading Status' must be verified");


            When(cmd => cmd.GSDUploadingStatus == GSDUploadingStatus.Uploaded, () =>
            {
                RuleFor(cmd => cmd.GSDUploadingDateValidation)
                .Must((e) => IsSelectionFieldCompleted(e))
                .WithMessage("'GSD Uploading Date' must be verified");
            });


            RuleFor(cmd => cmd.GeneticSequenceDataValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Externally Registered Database for GSD' must be verified");


            RuleFor(cmd => cmd.DatabaseUploadedByValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Uploaded by' must be verified");


            RuleFor(cmd => cmd.DatabaseAccessionIdValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Accession ID in Externally Registered Database for GSD' must be verified");


            RuleFor(cmd => cmd.MaterialGSDInfoValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Material GSD Info' must be verified");


            RuleFor(cmd => cmd.StrainDesignationValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Strain Designation at Externally Registered Database for GSD' must be verified");


            RuleFor(cmd => cmd.DescriptionValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Description' must be verified");


            RuleFor(cmd => cmd.ShipmentMaterialConditionValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("'Shipment Material Condition' must be verified");

            RuleFor(cmd => cmd.SampleIdValidation)
            .Must((e) => IsSelectionFieldCompleted(e))
            .WithMessage("SampleIdValidation must be verified");




        });
    }

    protected bool IsSelectionFieldCompleted(MaterialValidationSelection? selection)
    {
        return selection != null && selection != MaterialValidationSelection.Waiting;
    }

    protected async Task<bool> ReferenceNumberNotPresent(Guid id, string referenceNumber, CancellationToken token)
    {
        return !(await _materialReadRepository.ReferenceNumberAlreadyPresent(id, referenceNumber, token));
    }

}