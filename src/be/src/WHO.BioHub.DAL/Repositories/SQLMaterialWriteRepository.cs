using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialWriteRepository : IMaterialWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<Material> Materials => _dbContext.Materials;
    private DbSet<MaterialHistory> MaterialsHistory => _dbContext.MaterialsHistory;

    private DbSet<MaterialCollectedSpecimenType> MaterialCollectedSpecimenTypes => _dbContext.MaterialCollectedSpecimenTypes;
    private DbSet<MaterialCollectedSpecimenTypeHistory> MaterialCollectedSpecimenTypesHistory => _dbContext.MaterialCollectedSpecimenTypesHistory;

    private DbSet<MaterialGSDInfo> MaterialGSDInfo => _dbContext.MaterialGSDInfo;
    private DbSet<MaterialGSDInfoHistory> MaterialGSDInfoHistory => _dbContext.MaterialGSDInfoHistory;


    public SQLMaterialWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<Material, Errors>> Create(Material material, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(material, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(material);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        Material lab = await Materials.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        Materials.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<Material> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<MaterialCollectedSpecimenType>> ReadMaterialCollectedSpecimenTypes(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialCollectedSpecimenTypes
            .Where(l => l.MaterialId == id)
            .ToArrayAsync(cancellationToken);
    }


    public async Task<IEnumerable<MaterialGSDInfo>> ReadMaterialGSDInfo(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialGSDInfo
            .Where(l => l.MaterialId == id)
            .ToArrayAsync(cancellationToken);
    }


    public async Task<Material> ReadForUpdateForLaboratoryUser(Guid id, Guid userLaboratoryId, CancellationToken cancellationToken)
    {
        return await _dbContext.Materials
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.Id == userLaboratoryId, cancellationToken);
    }

    public async Task<Errors?> Update(Material material, List<MaterialGSDInfoDto> materialGSDInfoDto, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        var materialGSDInfoToRemove = MaterialGSDInfo.Where(x => x.MaterialId == material.Id);

        if (materialGSDInfoToRemove.Any())
        {
            MaterialGSDInfo.RemoveRange(materialGSDInfoToRemove);
        }


        List<MaterialGSDInfo> materialGSDInfo = new List<MaterialGSDInfo>();

        if (materialGSDInfoDto != null && materialGSDInfoDto.Any())
        {
            foreach (var materialGSDInfoDtoElement in materialGSDInfoDto)
            {
                MaterialGSDInfo materialGSDInfoElement = new MaterialGSDInfo();
                materialGSDInfoElement.Id = Guid.NewGuid();
                materialGSDInfoElement.MaterialId = material.Id;
                materialGSDInfoElement.CellLine = materialGSDInfoDtoElement.CellLine;
                materialGSDInfoElement.GSDFasta = materialGSDInfoDtoElement.GSDFasta;
                materialGSDInfoElement.GSDType = materialGSDInfoDtoElement.GSDType;
                materialGSDInfoElement.PassageNumber = materialGSDInfoDtoElement.PassageNumber;
                materialGSDInfoElement.CreationDate = DateTime.UtcNow;

                materialGSDInfo.Add(materialGSDInfoElement);
            }

            await MaterialGSDInfo.AddRangeAsync(materialGSDInfo, cancellationToken);
        }

        Materials.Update(material);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }


    public async Task<Errors?> Update(Material material, List<MaterialGSDInfoDto> materialGSDInfoDto, List<Guid?> specimenTypeIds, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }


        var materialGSDInfoToRemove = MaterialGSDInfo.Where(x => x.MaterialId == material.Id);

        if (materialGSDInfoToRemove.Any())
        {
            MaterialGSDInfo.RemoveRange(materialGSDInfoToRemove);
        }



        List<MaterialGSDInfo> materialGSDInfo = new List<MaterialGSDInfo>();

        if (materialGSDInfoDto != null && materialGSDInfoDto.Any())
        {
            foreach (var materialGSDInfoDtoElement in materialGSDInfoDto)
            {
                MaterialGSDInfo materialGSDInfoElement = new MaterialGSDInfo();
                materialGSDInfoElement.Id = Guid.NewGuid();
                materialGSDInfoElement.MaterialId = material.Id;
                materialGSDInfoElement.CellLine = materialGSDInfoDtoElement.CellLine;
                materialGSDInfoElement.GSDFasta = materialGSDInfoDtoElement.GSDFasta;
                materialGSDInfoElement.GSDType = materialGSDInfoDtoElement.GSDType;
                materialGSDInfoElement.PassageNumber = materialGSDInfoDtoElement.PassageNumber;
                materialGSDInfoElement.CreationDate = DateTime.UtcNow;

                materialGSDInfo.Add(materialGSDInfoElement);
            }

            await MaterialGSDInfo.AddRangeAsync(materialGSDInfo, cancellationToken);
        }

        var specimentTypesToRemove = MaterialCollectedSpecimenTypes.Where(x => x.MaterialId == material.Id);

        if (specimentTypesToRemove.Any())
        {
            MaterialCollectedSpecimenTypes.RemoveRange(specimentTypesToRemove);
        }

        List<MaterialCollectedSpecimenType> materialCollectedSpecimenTypes = new List<MaterialCollectedSpecimenType>();

        if (specimenTypeIds != null && specimenTypeIds.Any())
        {
            foreach (var specimenTypeId in specimenTypeIds)
            {
                MaterialCollectedSpecimenType materialCollectedSpecimenType = new MaterialCollectedSpecimenType();
                materialCollectedSpecimenType.Id = Guid.NewGuid();
                materialCollectedSpecimenType.SpecimenTypeId = specimenTypeId;
                materialCollectedSpecimenType.MaterialId = material.Id;
                materialCollectedSpecimenType.CreationDate = DateTime.UtcNow;
                materialCollectedSpecimenTypes.Add(materialCollectedSpecimenType);
            }

            await MaterialCollectedSpecimenTypes.AddRangeAsync(materialCollectedSpecimenTypes, cancellationToken);
        }
        Materials.Update(material);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }


    public async Task<Errors?> Update(Material material, CancellationToken cancellationToken, IDbContextTransaction? transaction = null)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }
               
        Materials.Update(material);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }


    public async Task<Errors?> CreateMaterialHistoryItem(Material material, CancellationToken cancellationToken, IDbContextTransaction transaction)
    {
        if (transaction != null)
        {
            _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        }

        MaterialHistory materialHistory = new MaterialHistory();

        materialHistory.Id = Guid.NewGuid();
        materialHistory.MaterialId = material.Id;

        materialHistory.ReferenceNumber = material.ReferenceNumber;
        materialHistory.Name = material.Name;
        materialHistory.Description = material.Description;
        materialHistory.Temperature = material.Temperature;
        materialHistory.SampleId = material.SampleId;
        materialHistory.Lineage = material.Lineage;
        materialHistory.Variant = material.Variant;
        materialHistory.VariantAssessment = material.VariantAssessment;
        materialHistory.StrainDesignation = material.StrainDesignation;
        materialHistory.Genotype = material.Genotype;
        materialHistory.Serotype = material.Serotype;
        materialHistory.DatabaseAccessionId = material.DatabaseAccessionId;
        materialHistory.OriginalGeneticSequence = material.OriginalGeneticSequence;

        materialHistory.FacilityGSD = material.FacilityGSD;
        materialHistory.GMO = material.GMO;
        //materialHistory.ProductionCellLine = material.ProductionCellLine;
        materialHistory.Infectivity = material.Infectivity;
        materialHistory.ViralTiter = material.ViralTiter;
        materialHistory.IsNew = material.IsNew;
        materialHistory.TypeId = material.TypeId;
        materialHistory.SuspectedEpidemiologicalOriginId = material.SuspectedEpidemiologicalOriginId;
        materialHistory.OriginalProductTypeId = material.OriginalProductTypeId;
        materialHistory.TransportCategoryId = material.TransportCategoryId;
        materialHistory.UnitOfMeasureId = material.UnitOfMeasureId;
        materialHistory.UsagePermissionId = material.UsagePermissionId;
        materialHistory.GeneticSequenceDataId = material.GeneticSequenceDataId;
        materialHistory.InternationalTaxonomyClassificationId = material.InternationalTaxonomyClassificationId;
        materialHistory.IsolationHostTypeId = material.IsolationHostTypeId;
        materialHistory.CultivabilityTypeId = material.CultivabilityTypeId;
        materialHistory.IsolationTechniqueTypeId = material.IsolationTechniqueTypeId;
        materialHistory.DeletedOn = null;
        materialHistory.ProviderBioHubFacilityId = material.ProviderBioHubFacilityId;
        materialHistory.ProviderLaboratoryId = material.ProviderLaboratoryId;
        materialHistory.Age = material.Age;
        materialHistory.PatientStatus = material.PatientStatus;
        materialHistory.CollectionDate = material.CollectionDate;
        materialHistory.Location = material.Location;
        materialHistory.Gender = material.Gender;
        materialHistory.LastOperationById = material.LastOperationById;
        materialHistory.LastOperationDate = material.LastOperationDate;
        materialHistory.ReferenceId = material.ReferenceId;
        materialHistory.Status = material.Status;
        materialHistory.OwnerBioHubFacilityId = material.OwnerBioHubFacilityId;

        materialHistory.CurrentNumberOfVials = material.CurrentNumberOfVials;
        materialHistory.ShipmentNumberOfVials = material.ShipmentNumberOfVials;
        materialHistory.WarningEmailCurrentNumberOfVialsThreshold = material.WarningEmailCurrentNumberOfVialsThreshold;
        materialHistory.ManualCreation = material.ManualCreation;

        materialHistory.ReferenceNumberValidation = material.ReferenceNumberValidation;
        materialHistory.NameValidation = material.NameValidation;
        materialHistory.DescriptionValidation = material.DescriptionValidation;
        materialHistory.TypeValidation = material.TypeValidation;
        materialHistory.SuspectedEpidemiologicalOriginValidation = material.SuspectedEpidemiologicalOriginValidation;
        materialHistory.ProductTypeValidation = material.OriginalProductTypeValidation;
        materialHistory.TransportCategoryValidation = material.TransportCategoryValidation;
        materialHistory.TemperatureValidation = material.TemperatureValidation;
        materialHistory.UnitOfMeasureValidation = material.UnitOfMeasureValidation;
        materialHistory.UsagePermissionValidation = material.UsagePermissionValidation;
        materialHistory.SampleIdValidation = material.SampleIdValidation;
        materialHistory.LineageValidation = material.LineageValidation;
        materialHistory.VariantValidation = material.VariantValidation;
        materialHistory.VariantAssessmentValidation = material.VariantAssessmentValidation;
        materialHistory.StrainDesignationValidation = material.StrainDesignationValidation;
        materialHistory.GenotypeValidation = material.GenotypeValidation;
        materialHistory.SerotypeValidation = material.SerotypeValidation;
        materialHistory.GeneticSequenceDataValidation = material.GeneticSequenceDataValidation;
        materialHistory.DatabaseAccessionIdValidation = material.DatabaseAccessionIdValidation;
        materialHistory.OriginalGeneticSequenceValidation = material.OriginalGeneticSequenceValidation;

        materialHistory.FacilityGSDValidation = material.FacilityGSDValidation;
        materialHistory.InternationalTaxonomyClassificationValidation = material.InternationalTaxonomyClassificationValidation;
        materialHistory.GMOValidation = material.GMOValidation;
        materialHistory.IsolationHostTypeValidation = material.IsolationHostTypeValidation;
        materialHistory.CultivabilityTypeValidation = material.CultivabilityTypeValidation;
        materialHistory.OriginalProductTypeValidation = material.OriginalProductTypeValidation;
        materialHistory.IsolationTechniqueTypeValidation = material.IsolationTechniqueTypeValidation;
        materialHistory.InfectivityValidation = material.InfectivityValidation;
        materialHistory.ViralTiterValidation = material.ViralTiterValidation;
        materialHistory.CollectionDateValidation = material.CollectionDateValidation;
        materialHistory.LocationValidation = material.LocationValidation;
        materialHistory.GenderValidation = material.GenderValidation;
        materialHistory.AgeValidation = material.AgeValidation;
        materialHistory.PatientStatusValidation = material.PatientStatusValidation;
        materialHistory.ReferenceNumberComment = material.ReferenceNumberComment;
        materialHistory.NameComment = material.NameComment;
        materialHistory.DescriptionComment = material.DescriptionComment;
        materialHistory.TypeComment = material.TypeComment;
        materialHistory.SuspectedEpidemiologicalOriginComment = material.SuspectedEpidemiologicalOriginComment;
        materialHistory.OriginalProductTypeComment = material.OriginalProductTypeComment;
        materialHistory.TransportCategoryComment = material.TransportCategoryComment;
        materialHistory.TemperatureComment = material.TemperatureComment;
        materialHistory.UnitOfMeasureComment = material.UnitOfMeasureComment;
        materialHistory.UsagePermissionComment = material.UsagePermissionComment;
        materialHistory.SampleIdComment = material.SampleIdComment;
        materialHistory.LineageComment = material.LineageComment;
        materialHistory.VariantComment = material.VariantComment;
        materialHistory.VariantAssessmentComment = material.VariantAssessmentComment;
        materialHistory.StrainDesignationComment = material.StrainDesignationComment;
        materialHistory.GenotypeComment = material.GenotypeComment;
        materialHistory.SerotypeComment = material.SerotypeComment;
        materialHistory.GeneticSequenceDataComment = material.GeneticSequenceDataComment;
        materialHistory.DatabaseAccessionIdComment = material.DatabaseAccessionIdComment;
        materialHistory.OriginalGeneticSequenceComment = material.OriginalGeneticSequenceComment;

        materialHistory.FacilityGSDComment = material.FacilityGSDComment;
        materialHistory.InternationalTaxonomyClassificationComment = material.InternationalTaxonomyClassificationComment;
        materialHistory.GMOComment = material.GMOComment;
        materialHistory.IsolationHostTypeComment = material.IsolationHostTypeComment;
        materialHistory.CultivabilityTypeComment = material.CultivabilityTypeComment;
        //materialHistory.ProductionCellLineComment = material.ProductionCellLineComment;
        materialHistory.IsolationTechniqueTypeComment = material.IsolationTechniqueTypeComment;
        materialHistory.InfectivityComment = material.InfectivityComment;
        materialHistory.ViralTiterComment = material.ViralTiterComment;
        materialHistory.CollectionDateComment = material.CollectionDateComment;
        materialHistory.LocationComment = material.LocationComment;
        materialHistory.GenderComment = material.GenderComment;
        materialHistory.AgeComment = material.AgeComment;
        materialHistory.PatientStatusComment = material.PatientStatusComment;

        materialHistory.ProductTypeId = material.ProductTypeId;

        materialHistory.BHFShareReadiness = material.BHFShareReadiness;
        materialHistory.PublicShare = material.PublicShare;

        materialHistory.ProductTypeValidation = material.ProductTypeValidation;
        materialHistory.ProductTypeComment = material.ProductTypeComment;

        materialHistory.Pathogen = material.Pathogen;
        materialHistory.FreezingDate = material.FreezingDate;
        materialHistory.VirusConcentration = material.VirusConcentration;
        materialHistory.ShipmentNumberOfVials = material.ShipmentNumberOfVials;
        materialHistory.ShipmentAmount = material.ShipmentAmount;
        materialHistory.ShipmentTemperature = material.ShipmentTemperature;
        materialHistory.ShipmentUnitOfMeasureId = material.ShipmentUnitOfMeasureId;
        materialHistory.CulturingCellLine = material.CulturingCellLine;
        materialHistory.CulturingPassagesNumber = material.CulturingPassagesNumber;
        materialHistory.TypeOfTransportMedium = material.TypeOfTransportMedium;
        materialHistory.BrandOfTransportMedium = material.BrandOfTransportMedium;
        materialHistory.DatabaseUploadedBy = material.DatabaseUploadedBy;

        materialHistory.PathogenValidation = material.PathogenValidation;
        materialHistory.FreezingDateValidation = material.FreezingDateValidation;
        materialHistory.VirusConcentrationValidation = material.VirusConcentrationValidation;
        materialHistory.ShipmentNumberOfVialsValidation = material.ShipmentNumberOfVialsValidation;
        materialHistory.ShipmentAmountValidation = material.ShipmentAmountValidation;
        materialHistory.ShipmentTemperatureValidation = material.ShipmentTemperatureValidation;
        materialHistory.ShipmentUnitOfMeasureValidation = material.ShipmentUnitOfMeasureValidation;
        materialHistory.CulturingCellLineValidation = material.CulturingCellLineValidation;
        materialHistory.CulturingPassagesNumberValidation = material.CulturingPassagesNumberValidation;
        materialHistory.TypeOfTransportMediumValidation = material.TypeOfTransportMediumValidation;
        materialHistory.BrandOfTransportMediumValidation = material.BrandOfTransportMediumValidation;
        materialHistory.DatabaseUploadedByValidation = material.DatabaseUploadedByValidation;



        materialHistory.PathogenComment = material.PathogenComment;
        materialHistory.FreezingDateComment = material.FreezingDateComment;
        materialHistory.VirusConcentrationComment = material.VirusConcentrationComment;
        materialHistory.ShipmentNumberOfVialsComment = material.ShipmentNumberOfVialsComment;
        materialHistory.ShipmentAmountComment = material.ShipmentAmountComment;
        materialHistory.ShipmentTemperatureComment = material.ShipmentTemperatureComment;
        materialHistory.ShipmentUnitOfMeasureComment = material.ShipmentUnitOfMeasureComment;
        materialHistory.CulturingCellLineComment = material.CulturingCellLineComment;
        materialHistory.CulturingPassagesNumberComment = material.CulturingPassagesNumberComment;
        materialHistory.TypeOfTransportMediumComment = material.TypeOfTransportMediumComment;
        materialHistory.BrandOfTransportMediumComment = material.BrandOfTransportMediumComment;
        materialHistory.DatabaseUploadedByComment = material.DatabaseUploadedByComment;

        materialHistory.CollectionDate = material.CollectionDate;
        materialHistory.CreationDate = DateTime.UtcNow;

        materialHistory.CulturingResult = material.CulturingResult;
        materialHistory.CulturingResultDate = material.CulturingResultDate;
        materialHistory.QualityControlResult = material.QualityControlResult;
        materialHistory.QualityControlResultDate = material.QualityControlResultDate;
        materialHistory.GSDAnalysisResult = material.GSDAnalysisResult;
        materialHistory.GSDAnalysisResultDate = material.GSDAnalysisResultDate;
        materialHistory.GSDUploadingStatus = material.GSDUploadingStatus;
        materialHistory.GSDUploadingDate = material.GSDUploadingDate;
        materialHistory.LastAliquotsAdditionDate = material.LastAliquotsAdditionDate;
        materialHistory.AddedAliquots = material.AddedAliquots;

        materialHistory.CulturingResultValidation = material.CulturingResultValidation;
        materialHistory.CulturingResultDateValidation = material.CulturingResultDateValidation;
        materialHistory.QualityControlResultValidation = material.QualityControlResultValidation;
        materialHistory.QualityControlResultDateValidation = material.QualityControlResultDateValidation;
        materialHistory.GSDAnalysisResultValidation = material.GSDAnalysisResultValidation;
        materialHistory.GSDAnalysisResultDateValidation = material.GSDAnalysisResultDateValidation;
        materialHistory.GSDUploadingStatusValidation = material.GSDUploadingStatusValidation;
        materialHistory.GSDUploadingDateValidation = material.GSDUploadingDateValidation;

        materialHistory.CulturingResultComment = material.CulturingResultComment;
        materialHistory.CulturingResultDateComment = material.CulturingResultDateComment;
        materialHistory.QualityControlResultComment = material.QualityControlResultComment;
        materialHistory.QualityControlResultDateComment = material.QualityControlResultDateComment;
        materialHistory.GSDAnalysisResultComment = material.GSDAnalysisResultComment;
        materialHistory.GSDAnalysisResultDateComment = material.GSDAnalysisResultDateComment;
        materialHistory.GSDUploadingStatusComment = material.GSDUploadingStatusComment;
        materialHistory.GSDUploadingDateComment = material.GSDUploadingDateComment;


        materialHistory.OwnerBioHubFacilityValidation = material.OwnerBioHubFacilityValidation;
        materialHistory.OwnerBioHubFacilityComment = material.OwnerBioHubFacilityComment;

        materialHistory.FreezingDateValidation = material.FreezingDateValidation;
        materialHistory.FreezingDateComment = material.FreezingDateComment;

        materialHistory.VirusConcentrationValidation = material.VirusConcentrationValidation;
        materialHistory.VirusConcentrationComment = material.VirusConcentrationComment;

        materialHistory.DateOfBMEPPReceiptValidation = material.DateOfBMEPPReceiptValidation;
        materialHistory.DateOfBMEPPReceiptComment = material.DateOfBMEPPReceiptComment;

        materialHistory.ShipmentNumberOfVialsValidation = material.ShipmentNumberOfVialsValidation;
        materialHistory.ShipmentNumberOfVialsComment = material.ShipmentNumberOfVialsComment;

        materialHistory.StartingDate = material.StartingDate;
        materialHistory.IsPast = material.IsPast;

        materialHistory.ShipmentMaterialCondition = material.ShipmentMaterialCondition;
        materialHistory.ShipmentMaterialConditionNote = material.ShipmentMaterialConditionNote;
        materialHistory.ShipmentMaterialConditionValidation = material.ShipmentMaterialConditionValidation;
        materialHistory.ShipmentMaterialConditionComment = material.ShipmentMaterialConditionComment;


        materialHistory.DateOfBMEPPReceipt = material.DateOfBMEPPReceipt;       

        materialHistory.MaterialCollectedSpecimenTypesValidation = material.MaterialCollectedSpecimenTypesValidation;

        materialHistory.MaterialCollectedSpecimenTypesComment = material.MaterialCollectedSpecimenTypesComment;

        materialHistory.MaterialGSDInfoValidation = material.MaterialGSDInfoValidation;

        materialHistory.MaterialGSDInfoComment = material.MaterialGSDInfoComment;

        var specimenTypes = await ReadMaterialCollectedSpecimenTypes(material.Id, cancellationToken);
        var specimenTypeIds = specimenTypes.Select(x => x.SpecimenTypeId).ToList();

        var materialGSDInfo = (await ReadMaterialGSDInfo(material.Id, cancellationToken)).ToList();


        await MaterialsHistory.AddAsync(materialHistory, cancellationToken);


        List<MaterialCollectedSpecimenTypeHistory> materialCollectedSpecimenTypes = new List<MaterialCollectedSpecimenTypeHistory>();

        if (specimenTypeIds != null && specimenTypeIds.Any())
        {
            foreach (var specimenTypeId in specimenTypeIds)
            {
                MaterialCollectedSpecimenTypeHistory materialCollectedSpecimenType = new MaterialCollectedSpecimenTypeHistory();
                materialCollectedSpecimenType.Id = Guid.NewGuid();
                materialCollectedSpecimenType.SpecimenTypeId = specimenTypeId;
                materialCollectedSpecimenType.MaterialHistoryId = materialHistory.Id;
                materialCollectedSpecimenType.CreationDate = DateTime.UtcNow;
                materialCollectedSpecimenTypes.Add(materialCollectedSpecimenType);
            }
            await MaterialCollectedSpecimenTypesHistory.AddRangeAsync(materialCollectedSpecimenTypes, cancellationToken);
        }

        if (materialGSDInfo != null && materialGSDInfo.Any())
        {
            List<MaterialGSDInfoHistory> materialGSDInfoHistory = new List<MaterialGSDInfoHistory>();
            foreach (var elem in materialGSDInfo)
            {
                MaterialGSDInfoHistory historyElem = new MaterialGSDInfoHistory();
                historyElem.Id = Guid.NewGuid();
                historyElem.CellLine = elem.CellLine;
                historyElem.GSDFasta = elem.GSDFasta;
                historyElem.GSDType = elem.GSDType;
                historyElem.PassageNumber = elem.PassageNumber;
                historyElem.MaterialHistoryId = materialHistory.Id;
                historyElem.CreationDate = DateTime.UtcNow;
                materialGSDInfoHistory.Add(historyElem);
            }
            await MaterialGSDInfoHistory.AddRangeAsync(materialGSDInfoHistory, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }


    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }
}