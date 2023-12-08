using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    public static RolePermission[] RolePermissions = new RolePermission[]
    {
        new()
        {
            PermissionId = CanReadBioHubFacilityPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadBioHubFacilityPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBioHubFacilityPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBioHubFacilityPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBioHubFacilityPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBioHubFacilityPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadBioHubFacilityPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBioHubFacilityPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadBSLLevelPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadBSLLevelPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBSLLevelPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBSLLevelPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadBSLLevelPermissionId,
        //    RoleId = LaboratoryITToolFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadBSLLevelPermissionId,
        //    RoleId = LaboratoryUserRoleId,
        //},

        new()
        {
            PermissionId = CanReadBSLLevelPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBSLLevelPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadCountryPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadCountryPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCountryPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCountryPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCountryPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCountryPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadCountryPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCountryPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadCultivabilityTypePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadCultivabilityTypePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCultivabilityTypePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCultivabilityTypePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCultivabilityTypePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCultivabilityTypePermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadCultivabilityTypePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCultivabilityTypePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadGeneticSequenceDataPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadGeneticSequenceDataPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadGeneticSequenceDataPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadGeneticSequenceDataPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadGeneticSequenceDataPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadGeneticSequenceDataPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadGeneticSequenceDataPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadGeneticSequenceDataPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadInternationalTaxonomyClassificationPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadInternationalTaxonomyClassificationPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadInternationalTaxonomyClassificationPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadInternationalTaxonomyClassificationPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadInternationalTaxonomyClassificationPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadInternationalTaxonomyClassificationPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadInternationalTaxonomyClassificationPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadInternationalTaxonomyClassificationPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationHostTypePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationHostTypePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationHostTypePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationHostTypePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationHostTypePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationHostTypePermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationHostTypePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationHostTypePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationTechniqueTypePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationTechniqueTypePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationTechniqueTypePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationTechniqueTypePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationTechniqueTypePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationTechniqueTypePermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationTechniqueTypePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationTechniqueTypePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialProductPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialProductPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialProductPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialProductPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialProductPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialProductPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialProductPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialProductPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialTypePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialTypePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialTypePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialTypePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialTypePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialTypePermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialTypePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialTypePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialUsagePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialUsagePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialUsagePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialUsagePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialUsagePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialUsagePermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialUsagePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialUsagePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadPriorityRequestTypePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadPriorityRequestTypePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadPriorityRequestTypePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadPriorityRequestTypePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadPriorityRequestTypePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadPriorityRequestTypePermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadPriorityRequestTypePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadPriorityRequestTypePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadRolePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadRolePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadRolePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadRolePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadRolePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadRolePermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadRolePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadRolePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanEditShipmentPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        

        new()
        {
            PermissionId = CanReadTemperatureUnitOfMeasurePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadTemperatureUnitOfMeasurePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTemperatureUnitOfMeasurePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTemperatureUnitOfMeasurePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTemperatureUnitOfMeasurePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTemperatureUnitOfMeasurePermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadTemperatureUnitOfMeasurePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTemperatureUnitOfMeasurePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportCategoryPermissionId,
            RoleId = WHOSecretariatRoleId,
        },




        new()
        {
            PermissionId = CanReadTransportCategoryPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportCategoryPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportCategoryPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportCategoryPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportCategoryPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportCategoryPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportCategoryPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportModePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportModePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportModePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportModePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportModePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportModePermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportModePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportModePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReadUserRequestStatusPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestStatusPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestStatusPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestStatusPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestStatusPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestStatusPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestStatusPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestStatusPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadUserPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadUserPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadUserPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanEditBioHubFacilityPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanEditBioHubFacilityPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditBioHubFacilityPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditBioHubFacilityPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditBioHubFacilityPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditBioHubFacilityPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanEditBioHubFacilityPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditBioHubFacilityPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },




        new()
        {
            PermissionId = CanEditLaboratoryPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanEditMaterialPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanEditMaterialPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditMaterialPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditMaterialPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanEditMaterialPermissionId,
        //    RoleId = LaboratoryITToolFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanEditMaterialPermissionId,
        //    RoleId = LaboratoryUserRoleId,
        //},

        new()
        {
            PermissionId = CanEditMaterialPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditMaterialPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },


        new()
        {
            PermissionId = CanEditUserRequestPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanEditUserPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanEditUserPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditUserPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditUserPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditUserPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteUserPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditUserPermissionId,
            RoleId = LaboratoryUserRoleId,
        },


        new()
        {
            PermissionId = CanEditUserPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditUserPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanDeleteBioHubFacilityPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDeleteBioHubFacilityPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteBioHubFacilityPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteBioHubFacilityPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteBioHubFacilityPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteBioHubFacilityPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanDeleteBioHubFacilityPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteBioHubFacilityPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanDeleteLaboratoryPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDeleteLaboratoryPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteLaboratoryPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteLaboratoryPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanDeleteMaterialPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDeleteMaterialPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteMaterialPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteMaterialPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanDeleteMaterialPermissionId,
        //    RoleId = LaboratoryITToolFocalPointRoleId,
        //},




        //new()
        //{
        //    PermissionId = CanDeleteMaterialPermissionId,
        //    RoleId = LaboratoryUserRoleId,
        //},


        new()
        {
            PermissionId = CanDeleteUserRequestPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanDeleteUserPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDeleteUserPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteUserPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanDeleteUserPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanCreateBioHubFacilityPermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        new()
        {
            PermissionId = CanCreateBioHubFacilityPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },



        new()
        {
            PermissionId = CanCreateBioHubFacilityPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanCreateBioHubFacilityPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanCreateLaboratoryPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanCreateLaboratoryPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanCreateLaboratoryPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanCreateLaboratoryPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanCreateMaterialPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        //new()
        //{
        //    PermissionId = CanCreateMaterialPermissionId,
        //    RoleId = WHOOperationalFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanCreateMaterialPermissionId,
        //    RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanCreateMaterialPermissionId,
        //    RoleId = WHOLaboratoryFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanCreateMaterialPermissionId,
        //    RoleId = LaboratoryITToolFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanCreateMaterialPermissionId,
        //    RoleId = LaboratoryUserRoleId,
        //},


        new()
        {
            PermissionId = CanCreateUserRequestPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanCreateUserRequestPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanCreateUserPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanAccessWHODashboardPermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOBMEPPPermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOBioHubFacilitiesPermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOLaboratoriesPermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOTemplatesPermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOShipmentsPermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOPendingRequestPermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOUsersPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanAccessWHODashboardPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOBMEPPPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOBioHubFacilitiesPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOLaboratoriesPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOTemplatesPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOShipmentsPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessWHODashboardPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOBMEPPPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOBioHubFacilitiesPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOLaboratoriesPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOTemplatesPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOShipmentsPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessWHODashboardPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOBMEPPPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOBioHubFacilitiesPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOLaboratoriesPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOTemplatesPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessWHOShipmentsPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessLaboratoryDashboardPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryUserProfilePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryStaffPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryFacilityInstituteProfilePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryBMEPPPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryBMEPPCataloguePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryTemplatesPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessLaboratoryDashboardPermissionId,
            RoleId = LaboratoryUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryUserProfilePermissionId,
            RoleId = LaboratoryUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryStaffPermissionId,
            RoleId = LaboratoryUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryFacilityInstituteProfilePermissionId,
            RoleId = LaboratoryUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryBMEPPPermissionId,
            RoleId = LaboratoryUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryBMEPPCataloguePermissionId,
            RoleId = LaboratoryUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessLaboratoryTemplatesPermissionId,
            RoleId = LaboratoryUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessBioHubFacilityDashboardPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessBioHubFacilityUserProfilePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessBioHubFacilityLaboratoriesPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessBioHubFacilityBMEPPPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessBioHubFacilityTemplatesPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },
        new()
        {
            PermissionId = CanAccessBioHubFacilityDashboardPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessBioHubFacilityUserProfilePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessBioHubFacilityLaboratoriesPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessBioHubFacilityBMEPPPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },
        new()
        {
            PermissionId = CanAccessBioHubFacilityTemplatesPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryStaffPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryStaffPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryStaffPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteLaboratoryStaffPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryStaffPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanApproveOrRejectUserRequestPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanCreateDocumentTemplatePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanEditDocumentTemplatePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDeleteDocumentTemplatePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentTemplatePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentTemplatePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentTemplatePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentTemplatePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentTemplatePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentTemplatePermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentTemplatePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentTemplatePermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },


        new()
        {
            PermissionId = CanApproveBioHubFacilityCompletionPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanApproveLaboratoryCompletionPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanApproveLaboratoryCompletionPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanVerifyMaterialPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanVerifyMaterialPermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        new()
        {
            PermissionId = CanSetMaterialReadyToSharePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanSetMaterialPublicPermissionId,
            RoleId = WHOSecretariatRoleId,
        },



        new()
        {
            PermissionId = CanReadKpiDataPermissionId,
            RoleId = WHOSecretariatRoleId,
        },



        new()
        {
            PermissionId = CanReadKpiDataPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReadKpiDataPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadKpiDataPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadKpiDataPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadKpiDataPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanAddMaterialNewVialsPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditMaterialOwnerBioHubFacilityPermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        new()
        {
            PermissionId = CanEditMaterialWarningEmailCurrentNumberOfVialsThresholdPermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        new()
        {
            PermissionId = CanEditMaterialWarningEmailCurrentNumberOfVialsThresholdPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditMaterialWarningEmailCurrentNumberOfVialsThresholdPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadResourcePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanCreateResourcePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanEditResourcePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDeleteResourcePermissionId,
            RoleId = WHOSecretariatRoleId,
        },




        new()
        {
            PermissionId = CanReadSpecimenTypePermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSpecimenTypePermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSpecimenTypePermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSpecimenTypePermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSpecimenTypePermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSpecimenTypePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
       

        //new()
        //{
        //    PermissionId = CanEditMaterialShipmentNumberOfVialsPermissionId,
        //    RoleId = WHOSecretariatRoleId,
        //},


        //Request Iniziation Worklist Permissions
        new()
        {
            PermissionId = CanAccessRequestIniziationPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
		
		//Access Worklist Permissions
		new()
        {
            PermissionId = CanAccessWorklistPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanAccessWorklistPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessWorklistPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessWorklistPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessWorklistPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessWorklistPermissionId,
            RoleId = LaboratoryUserRoleId,
        },


        new()
        {
            PermissionId = CanAccessWorklistPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessWorklistPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },



        //Access SMTA Workflow Permissions
		new()
        {
            PermissionId = CanAccessSMTAWorkflowPermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        new()
        {
            PermissionId = CanAccessSMTAWorkflowPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessSMTAWorkflowPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanAccessSMTAWorkflowPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },


        //Submit SMTA1 state Permissions

        new()
        {
            PermissionId = CanReadSubmitSMTA1PermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadSubmitSMTA1PermissionId,
        //    RoleId = WHOOperationalFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSubmitSMTA1PermissionId,
        //    RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSubmitSMTA1PermissionId,
        //    RoleId = WHOLaboratoryFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReadSubmitSMTA1PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitSMTA1PermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadSubmitSMTA1PermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSubmitSMTA1PermissionId,
        //    RoleId = BioHubFacilityUserRoleId,
        //},

        new()
        {
            PermissionId = CanDownloadFileSubmitSMTA1PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitSMTA1PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },                            

        
        //Waiting For SMTA1 SEC's Approval state Permissions		
		new()
        {
            PermissionId = CanReadWaitingForSMTA1SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadWaitingForSMTA1SECsApprovalPermissionId,
        //    RoleId = WHOOperationalFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadWaitingForSMTA1SECsApprovalPermissionId,
        //    RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadWaitingForSMTA1SECsApprovalPermissionId,
        //    RoleId = WHOLaboratoryFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReadWaitingForSMTA1SECsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForSMTA1SECsApprovalPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadWaitingForSMTA1SECsApprovalPermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadWaitingForSMTA1SECsApprovalPermissionId,
        //    RoleId = BioHubFacilityUserRoleId,
        //},

        new()
        {
            PermissionId = CanSubmitWaitingForSMTA1SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForSMTA1SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForSMTA1SECsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanDownloadFileWaitingForSMTA1SECsApprovalPermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},


        //SMTA 1 Workflow Complete state Permissions
        new()
        {
            PermissionId = CanReadSMTA1WorkflowCompletePermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanReadSMTA1WorkflowCompletePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
        //new()
        //{
        //    PermissionId = CanReadSMTA1WorkflowCompletePermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},
        new()
        {
            PermissionId = CanDownloadFileSMTA1WorkflowCompletePermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanDownloadFileSMTA1WorkflowCompletePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
        //new()
        //{
        //    PermissionId = CanDownloadFileSMTA1WorkflowCompletePermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},


        //Submit Annex 2 Of SMTA 1 state Permissions	

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanSubmitAnnex2OfSMTA1PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitAnnex2OfSMTA1PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
             

                

        //Waiting for Annex 2 of SMTA 1 SEC's approval state permissions	

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

                new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

                

                
        //Submit Booking Form Of SMTA1 state permissions	

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanSubmitBookingFormOfSMTA1PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBookingFormOfSMTA1PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
        


        //Wait For Booking Form SMTA1 OPS Approval state permissions
        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },


        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForBookingFormSMTA1OPSApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        

        //Submit SMTA1 Shipment Documents state permissions
        //new()
        //{
        //    PermissionId = CanReadSMTA1ShipmentDocumentsPermissionId,
        //    RoleId = WHOSecretariatRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSMTA1ShipmentDocumentsPermissionId,
        //    RoleId = WHOOperationalFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSMTA1ShipmentDocumentsPermissionId,
        //    RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSMTA1ShipmentDocumentsPermissionId,
        //    RoleId = WHOLaboratoryFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReadSMTA1ShipmentDocumentsPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadSMTA1ShipmentDocumentsPermissionId,
        //    RoleId = LaboratoryUserRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSMTA1ShipmentDocumentsPermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSMTA1ShipmentDocumentsPermissionId,
        //    RoleId = BioHubFacilityUserRoleId,
        //},



        new()
        {
            PermissionId = CanDownloadSMTA1ShipmentDocumentsPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanDownloadSMTA1ShipmentDocumentsPermissionId,
        //    RoleId = WHOOperationalFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanSubmitSMTA1ShipmentDocumentsPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanCreateCourierPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditCourierPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCourierPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteCourierPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCourierPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanCreateCourierStaffPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditCourierStaffPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCourierStaffPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteCourierStaffPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCourierStaffPermissionId,
            RoleId = WHOSecretariatRoleId,
        },



        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },



        new()
        {
            PermissionId = CanSubmitWaitForPickUpCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForPickUpCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanSubmitWaitForDeliveryCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForDeliveryCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanSubmitWaitForArrivalConditionCheckPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForArrivalConditionCheckPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanSubmitWaitForCommentBHFSendFeedbackPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForCommentBHFSendFeedbackPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanSubmitWaitForFinalApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForFinalApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },



        new()
        {
            PermissionId = CanReadSubmitSMTA2PermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadSubmitSMTA2PermissionId,
        //    RoleId = WHOOperationalFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSubmitSMTA2PermissionId,
        //    RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSubmitSMTA2PermissionId,
        //    RoleId = WHOLaboratoryFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReadSubmitSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitSMTA2PermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadSubmitSMTA2PermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadSubmitSMTA2PermissionId,
        //    RoleId = BioHubFacilityUserRoleId,
        //},

        new()
        {
            PermissionId = CanReadWaitingForSMTA2SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadWaitingForSMTA2SECsApprovalPermissionId,
        //    RoleId = WHOOperationalFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadWaitingForSMTA2SECsApprovalPermissionId,
        //    RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadWaitingForSMTA2SECsApprovalPermissionId,
        //    RoleId = WHOLaboratoryFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReadWaitingForSMTA2SECsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForSMTA2SECsApprovalPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadWaitingForSMTA2SECsApprovalPermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadWaitingForSMTA2SECsApprovalPermissionId,
        //    RoleId = BioHubFacilityUserRoleId,
        //},

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadBHFSMTA2ShipmentDocumentsPermissionId,
        //    RoleId = WHOSecretariatRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadBHFSMTA2ShipmentDocumentsPermissionId,
        //    RoleId = WHOOperationalFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadBHFSMTA2ShipmentDocumentsPermissionId,
        //    RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadBHFSMTA2ShipmentDocumentsPermissionId,
        //    RoleId = WHOLaboratoryFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadBHFSMTA2ShipmentDocumentsPermissionId,
        //    RoleId = LaboratoryITToolFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadBHFSMTA2ShipmentDocumentsPermissionId,
        //    RoleId = LaboratoryUserRoleId,
        //},

        new()
        {
            PermissionId = CanReadBHFSMTA2ShipmentDocumentsPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadBHFSMTA2ShipmentDocumentsPermissionId,
        //    RoleId = BioHubFacilityUserRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadQESMTA2ShipmentDocumentsPermissionId,
        //    RoleId = WHOSecretariatRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadQESMTA2ShipmentDocumentsPermissionId,
        //    RoleId = WHOOperationalFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadQESMTA2ShipmentDocumentsPermissionId,
        //    RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadQESMTA2ShipmentDocumentsPermissionId,
        //    RoleId = WHOLaboratoryFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReadQESMTA2ShipmentDocumentsPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReadQESMTA2ShipmentDocumentsPermissionId,
        //    RoleId = LaboratoryUserRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadQESMTA2ShipmentDocumentsPermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        //new()
        //{
        //    PermissionId = CanReadQESMTA2ShipmentDocumentsPermissionId,
        //    RoleId = BioHubFacilityUserRoleId,
        //},

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForFinalApprovalFromBioHubPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPermissionId,
            RoleId = LaboratoryUserRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },


        //////
        ///

        new()
        {
            PermissionId = CanSubmitSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitAnnex2OfSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitQESMTA2ShipmentDocumentsPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForArrivalConditionFromBioHubCheckPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        /////
        ///

        new()
        {
            PermissionId = CanSubmitWaitingForSMTA2SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        ////
        ///


        new()
        {
            PermissionId = CanSubmitBookingFormOfSMTA2PermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitBHFSMTA2ShipmentDocumentsPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForCommentQESendFeedbackPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        /////
        ///

        new()
        {
            PermissionId = CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        /////
        ///

        new()
        {
            PermissionId = CanSubmitWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForPickUpFromBioHubCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForDeliveryFromBioHubCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        ////
        ///

        new()
        {
            PermissionId = CanDownloadFileSubmitSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        ////
        ///

        new()
        {
            PermissionId = CanDownloadFileWaitingForSMTA2SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForSMTA2SECsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitAnnex2OfSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

                new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBookingFormOfSMTA2PermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

                new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },

        new()
        {
            PermissionId = CanDownloadBHFSMTA2ShipmentDocumentsPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadQESMTA2ShipmentDocumentsPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForPickUpFromBioHubCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForDeliveryFromBioHubCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForArrivalConditionFromBioHubCheckPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForCommentQESendFeedbackPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForFinalApprovalFromBioHubPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


                new()
        {
            PermissionId = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            RoleId = BioHubFacilityUserRoleId,
        },


        new()
        {
            PermissionId = CanSubmitShipmentCompletedPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanSubmitShipmentCompletedPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitShipmentCompletedPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitShipmentCompletedPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitShipmentCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanSubmitShipmentFromBioHubCompletedPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanSubmitShipmentFromBioHubCompletedPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitShipmentFromBioHubCompletedPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitShipmentFromBioHubCompletedPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReadDocumentPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnRequestAccessPermissionId,
            RoleId = WHOSecretariatRoleId,
        },




        //SMTA 2 Workflow Complete state Permissions
        new()
        {
            PermissionId = CanReadSMTA2WorkflowCompletePermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanReadSMTA2WorkflowCompletePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSMTA2WorkflowCompletePermissionId,
            RoleId = WHOSecretariatRoleId,
        },
        new()
        {
            PermissionId = CanDownloadFileSMTA2WorkflowCompletePermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },
       

       
        /////////// SMTA 1 Request Email permissions ///////////////////
        //Status: S1 => S2
		new()
        {
            PermissionId = CanReceiveEmailsOnSubmitSMTA1PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReceiveEmailsOnSubmitSMTA1PermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitSMTA1PermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitSMTA1PermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        //Status: S2 => S3 (Approved)
        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApprovalPermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        //Status: S2 => S1 (Rejected)

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalRejectPermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalRejectPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		

        /////////// Shipment Request Into BioHub Email permissions ///////////////////
		//Status: S4 => S5
		new()
        {
            PermissionId = CanReceiveEmailsOnSubmitAnnex2OfSMTA1ApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitAnnex2OfSMTA1ApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitAnnex2OfSMTA1ApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		//Status: S5 => S6 (Approved)	
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		//Status: S5 => S4 (Rejected)
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalRejectPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		//Status: S6 => S7		
		new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBookingFormOfSMTA1ApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBookingFormOfSMTA1ApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBookingFormOfSMTA1ApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		//Status: S7 => S8 (Approved)		
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		//Status: S7 => S6 (Rejected)	
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalRejectPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		//Status: S8 => S9		
		new()
        {
            PermissionId = CanReceiveEmailsOnSMTA1ShipmentDocumentsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnSMTA1ShipmentDocumentsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		
		//Status: S9 => S10	
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForPickUpCompletedApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForPickUpCompletedApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForPickUpCompletedApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

                
		
		//Status: S10 => S11
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitForDeliveryCompletedApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForDeliveryCompletedApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForDeliveryCompletedApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
                        


        //Status: S11 => S14 (Approved)
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForArrivalConditionCheckApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForArrivalConditionCheckApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForArrivalConditionCheckApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForArrivalConditionCheckApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		//Status: S11 => S12 (Rejected)
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitForArrivalConditionCheckRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForArrivalConditionCheckRejectPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForArrivalConditionCheckRejectPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForArrivalConditionCheckRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		//Status: S12 => S13 (Comment)
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		//Status: S13 => S12 (Rejected)
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitForFinalApprovalRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForFinalApprovalRejectPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForFinalApprovalRejectPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForFinalApprovalRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },
		
		
		//Status: S13 => S14 (Completed)
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitForFinalApprovalApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForFinalApprovalApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForFinalApprovalApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForFinalApprovalApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },



         /////////// SMTA 2 Request Email permissions ///////////////////
         ///
         //Status: S1 => S2
		new()
        {
            PermissionId = CanReceiveEmailsOnSubmitSMTA2PermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReceiveEmailsOnSubmitSMTA2PermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitSMTA2PermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitSMTA2PermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        //Status: S2 => S3 (Approved)
        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApprovalPermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        //Status: S2 => S1 (Rejected)

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        //new()
        //{
        //    PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalRejectPermissionId,
        //    RoleId = BioHubFacilityITToolFocalPointRoleId,
        //},

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalRejectPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        /////////// Shipment Request From BioHub Email permissions ///////////////////
        //Status S3 => S4

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitAnnex2OfSMTA2ApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitAnnex2OfSMTA2ApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitAnnex2OfSMTA2ApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        //Status: S4 => S5 (Approved)	
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },


        //Status: S4 => S3 (Rejected)
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalRejectPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },



        //Status: S5 => S6
        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2ApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2ApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2ApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2ApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },


        //Status: S6 => S7 (Approved)
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovalPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },


        //Status: S6 => S5 (Reject)
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },


        //Status: S7 => S8      
        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBookingFormOfSMTA2ApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBookingFormOfSMTA2ApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBookingFormOfSMTA2ApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },


        //Status: S8 => S9 (Approved)
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        //Status: S8 => S7 (Rejected)	
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalRejectPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        //Status: S9 => S11		
		new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },


        //Status: S11 => S12 (Approved)		
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2PickUpCompletedApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2PickUpCompletedApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2PickUpCompletedApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailOnNumberOfVialsWarningPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },


        //Status: S11 => S10 (Rejected)		
		new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2PickUpCompletedRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2PickUpCompletedRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2PickUpCompletedRejectPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        //Status: S10 => S11		
		new()
        {
            PermissionId = CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },



        //Status: S12 => S13 [Delivery completed]
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },



        //Status: S13 => S16 (Approved)
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApprovalPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },


        //Status: S13 => S14 (Rejected)
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckRejectPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckRejectPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckRejectPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        //Status: S14 => S15 (Comment) 
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForCommentQESendFeedbackApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForCommentQESendFeedbackApprovalPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        //Status: S15 => S14 (Rejected)
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2FinalApprovalRejectPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2FinalApprovalRejectPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        //Status: S15 => S16 (Completed)
        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2FinalApprovalApprovalPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReceiveEmailsOnWaitForSMTA2FinalApprovalApprovalPermissionId,
            RoleId = WHOSecretariatRoleId,
        },




        ///////////////////////////////
        ///
        new()
        {
            PermissionId = CanReadSubmitSMTA1PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitSMTA1PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitSMTA1PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForSMTA1SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForSMTA1SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForSMTA1SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSMTA1WorkflowCompletePastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSMTA1WorkflowCompletePastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSMTA1WorkflowCompletePastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitSMTA1PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitSMTA1PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitSMTA1PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForSMTA1SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForSMTA1SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForSMTA1SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSMTA1WorkflowCompletePastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSMTA1WorkflowCompletePastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSMTA1WorkflowCompletePastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA1PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA1PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA1OPSApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSMTA1ShipmentDocumentsPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSMTA1ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSMTA1ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionCheckPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentBHFSendFeedbackPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitAnnex2OfSMTA1PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitAnnex2OfSMTA1PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitAnnex2OfSMTA1PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBookingFormOfSMTA1PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBookingFormOfSMTA1PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBookingFormOfSMTA1PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadSMTA1ShipmentDocumentsPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadSMTA1ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadSMTA1ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForPickUpCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForPickUpCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForPickUpCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForDeliveryCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForDeliveryCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForDeliveryCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForArrivalConditionCheckPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForArrivalConditionCheckPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForArrivalConditionCheckPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForCommentBHFSendFeedbackPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForCommentBHFSendFeedbackPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForCommentBHFSendFeedbackPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForFinalApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForFinalApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForFinalApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileShipmentCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileShipmentCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileShipmentCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitSMTA2PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitSMTA2PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForSMTA2SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForSMTA2SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForSMTA2SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSMTA2WorkflowCompletePastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSMTA2WorkflowCompletePastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSMTA2WorkflowCompletePastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitSMTA2PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitSMTA2PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForSMTA2SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForSMTA2SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForSMTA2SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSMTA2WorkflowCompletePastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSMTA2WorkflowCompletePastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSMTA2WorkflowCompletePastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitAnnex2OfSMTA2PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSubmitBookingFormOfSMTA2PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForBookingFormSMTA2OPSsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBHFSMTA2ShipmentDocumentsPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadBHFSMTA2ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBHFSMTA2ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadQESMTA2ShipmentDocumentsPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadQESMTA2ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadQESMTA2ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForPickUpFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForDeliveryFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForArrivalConditionFromBioHubCheckPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForCommentQESendFeedbackPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadWaitForFinalApprovalFromBioHubPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitAnnex2OfSMTA2PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitAnnex2OfSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitAnnex2OfSMTA2PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBookingFormOfSMTA2PastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBookingFormOfSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileSubmitBookingFormOfSMTA2PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadBHFSMTA2ShipmentDocumentsPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadBHFSMTA2ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadBHFSMTA2ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadQESMTA2ShipmentDocumentsPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadQESMTA2ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadQESMTA2ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForPickUpFromBioHubCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForPickUpFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForPickUpFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForDeliveryFromBioHubCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForDeliveryFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForDeliveryFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForArrivalConditionFromBioHubCheckPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForArrivalConditionFromBioHubCheckPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForArrivalConditionFromBioHubCheckPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForCommentQESendFeedbackPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForCommentQESendFeedbackPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForCommentQESendFeedbackPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForFinalApprovalFromBioHubPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForFinalApprovalFromBioHubPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileWaitForFinalApprovalFromBioHubPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileShipmentFromBioHubCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileShipmentFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDownloadFileShipmentFromBioHubCompletedPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

                new()
        {
            PermissionId = CanSubmitSMTA1PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitingForSMTA1SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanSubmitAnnex2OfSMTA1PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanSubmitBookingFormOfSMTA1PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForBookingFormSMTA1OPSApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForPickUpCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForDeliveryCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForArrivalConditionCheckPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForCommentBHFSendFeedbackPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForFinalApprovalPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },




        new()
        {
            PermissionId = CanSubmitSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitingForSMTA2SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        new()
        {
            PermissionId = CanSubmitAnnex2OfSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },



        new()
        {
            PermissionId = CanSubmitBookingFormOfSMTA2PastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForBookingFormSMTA2OPSsApprovalPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        new()
        {
            PermissionId = CanSubmitWaitForPickUpFromBioHubCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        new()
        {
            PermissionId = CanSubmitWaitForDeliveryFromBioHubCompletedPastPermissionId,
            RoleId = WHOSecretariatRoleId,
        },



        new()
        {
            PermissionId = CanSubmitWaitForArrivalConditionFromBioHubCheckPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitWaitForCommentQESendFeedbackPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanSubmitWaitForFinalApprovalFromBioHubPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanAccessPastRequestIniziationPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessPastWorklistPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessPastWorklistPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanAccessPastWorklistPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessPastSMTAWorkflowPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessPastSMTAWorkflowPermissionId,
            RoleId = WHOSecretariatRoleId,
        },


        ///////
        ///

        		new()
        {
            PermissionId = CanReadBioHubFacilityPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBSLLevelPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCountryPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCultivabilityTypePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadGeneticSequenceDataPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadInternationalTaxonomyClassificationPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationHostTypePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationTechniqueTypePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialProductPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialTypePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialUsagePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadPriorityRequestTypePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadRolePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTemperatureUnitOfMeasurePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportCategoryPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportModePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestStatusPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditBioHubFacilityPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanEditUserRequestPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditUserPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteUserPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteBioHubFacilityPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanDeleteUserRequestPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanCreateUserRequestPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessLaboratoryDashboardPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessLaboratoryUserProfilePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessLaboratoryStaffPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessLaboratoryFacilityInstituteProfilePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessLaboratoryBMEPPPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessLaboratoryBMEPPCataloguePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessLaboratoryTemplatesPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryStaffPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryStaffPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteLaboratoryStaffPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentTemplatePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadSpecimenTypePermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {

            PermissionId = CanReadDocumentPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },


        new()
        {
            PermissionId = CanReadBioHubFacilityPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadBSLLevelPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCountryPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadCultivabilityTypePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadGeneticSequenceDataPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadInternationalTaxonomyClassificationPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationHostTypePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadIsolationTechniqueTypePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadLaboratoryPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialProductPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialTypePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadMaterialUsagePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadPriorityRequestTypePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadRolePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadShipmentPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTemperatureUnitOfMeasurePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportCategoryPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadTransportModePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserRequestStatusPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadUserPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditBioHubFacilityPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditLaboratoryPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditMaterialPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditUserPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanDeleteBioHubFacilityPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessBioHubFacilityDashboardPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessBioHubFacilityUserProfilePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessBioHubFacilityLaboratoriesPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessBioHubFacilityBMEPPPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAccessBioHubFacilityTemplatesPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentTemplatePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanApproveBioHubFacilityCompletionPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSetMaterialReadyToSharePermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadKpiDataPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanAddMaterialNewVialsPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanEditMaterialWarningEmailCurrentNumberOfVialsThresholdPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadDocumentPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitSMTA1ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitBHFSMTA2ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanSubmitQESMTA2ShipmentDocumentsPastPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadOnBehalfOfRolesPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadOnBehalfOfRolesPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadOnBehalfOfRolesPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanEditMaterialShipmentInformationPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadEFormPermissionId,
            RoleId = WHOSecretariatRoleId,
        },

        new()
        {
            PermissionId = CanReadEFormPermissionId,
            RoleId = WHOBiosafetyBiosecurityFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadEFormPermissionId,
            RoleId = WHOLaboratoryFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadEFormPermissionId,
            RoleId = WHOOperationalFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadEFormPermissionId,
            RoleId = LaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadEFormPermissionId,
            RoleId = BioHubFacilityITToolFocalPointRoleId,
        },

        new()
        {

            PermissionId = CanReadEFormPermissionId,
            RoleId = OnBehalfOfLaboratoryITToolFocalPointRoleId,
        },

        new()
        {
            PermissionId = CanReadEFormPermissionId,
            RoleId = OnBehalfOfBioHubFacilityITToolFocalPointRoleId,
        },

    };

     
    private async Task AddOrUpdateRolePermissions(CancellationToken cancellationToken)
    {
        var rows = from o in _db.RolePermissions
                   select o;

        foreach (var row in rows)
        {
            _db.RolePermissions.Remove(row);
        }

        foreach (var rolePermission in RolePermissions)
        {
            await _db.AddAsync(rolePermission);
        }
    }
}