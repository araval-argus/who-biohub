using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    public static Permission[] Permissions = new Permission[]
    {
        new()
        {
            Id = CanReadBioHubFacilityPermissionId,
            Name = PermissionNames.CanReadBioHubFacility,
            Description = PermissionNames.CanReadBioHubFacility,


        },
        new()
        {
            Id = CanReadBSLLevelPermissionId,
            Name = PermissionNames.CanReadBSLLevel,
            Description = PermissionNames.CanReadBSLLevel,

        },
        new()
        {
            Id = CanReadCountryPermissionId,
            Name = PermissionNames.CanReadCountry,
            Description = PermissionNames.CanReadCountry,

        },
        new()
        {
            Id = CanReadCultivabilityTypePermissionId,
            Name = PermissionNames.CanReadCultivabilityType,
            Description = PermissionNames.CanReadCultivabilityType,

        },
        new()
        {
            Id = CanReadGeneticSequenceDataPermissionId,
            Name = PermissionNames.CanReadGeneticSequenceData,
            Description = PermissionNames.CanReadGeneticSequenceData,
        },

        new()
        {
            Id = CanReadInternationalTaxonomyClassificationPermissionId,
            Name = PermissionNames.CanReadInternationalTaxonomyClassification,
            Description = PermissionNames.CanReadInternationalTaxonomyClassification,
        },

        new()
        {
            Id = CanReadIsolationHostTypePermissionId,
            Name = PermissionNames.CanReadIsolationHostType,
            Description = PermissionNames.CanReadIsolationHostType,
        },

        new()
        {
            Id = CanReadIsolationTechniqueTypePermissionId,
            Name = PermissionNames.CanReadIsolationTechniqueType,
            Description = PermissionNames.CanReadIsolationTechniqueType,
        },

        new()
        {
            Id = CanReadLaboratoryPermissionId,
            Name = PermissionNames.CanReadLaboratory,
            Description = PermissionNames.CanReadLaboratory,
        },

        new()
        {
            Id = CanReadMaterialProductPermissionId,
            Name = PermissionNames.CanReadMaterialProduct,
            Description = PermissionNames.CanReadMaterialProduct,
        },

        new()
        {
            Id = CanReadMaterialPermissionId,
            Name = PermissionNames.CanReadMaterial,
            Description = PermissionNames.CanReadMaterial,
        },

        new()
        {
            Id = CanReadMaterialTypePermissionId,
            Name = PermissionNames.CanReadMaterialType,
            Description = PermissionNames.CanReadMaterialType,
        },

        new()
        {
            Id = CanReadMaterialUsagePermissionId,
            Name = PermissionNames.CanReadMaterialUsage,
            Description = PermissionNames.CanReadMaterialUsage,
        },

        new()
        {
            Id = CanReadPriorityRequestTypePermissionId,
            Name = PermissionNames.CanReadPriorityRequestType,
            Description = PermissionNames.CanReadPriorityRequestType,
        },

        new()
        {
            Id = CanReadRolePermissionId,
            Name = PermissionNames.CanReadRole,
            Description = PermissionNames.CanReadRole,
        },

        new()
        {
            Id = CanReadShipmentPermissionId,
            Name = PermissionNames.CanReadShipment,
            Description = PermissionNames.CanReadShipment,
        },

        new()
        {
            Id = CanReadTemperatureUnitOfMeasurePermissionId,
            Name = PermissionNames.CanReadTemperatureUnitOfMeasure,
            Description = PermissionNames.CanReadTemperatureUnitOfMeasure,
        },

        new()
        {
            Id = CanReadTransportCategoryPermissionId,
            Name = PermissionNames.CanReadTransportCategory,
            Description = PermissionNames.CanReadTransportCategory,
        },

        new()
        {
            Id = CanReadTransportModePermissionId,
            Name = PermissionNames.CanReadTransportMode,
            Description = PermissionNames.CanReadTransportMode,
        },

        new()
        {
            Id = CanReadUserRequestPermissionId,
            Name = PermissionNames.CanReadUserRequest,
            Description = PermissionNames.CanReadUserRequest,
        },

        new()
        {
            Id = CanReadUserRequestStatusPermissionId,
            Name = PermissionNames.CanReadUserRequestStatus,
            Description = PermissionNames.CanReadUserRequestStatus,
        },

        new()
        {
            Id = CanReadUserPermissionId,
            Name = PermissionNames.CanReadUser,
            Description = PermissionNames.CanReadUser,
        },

        new()
        {
            Id = CanEditBioHubFacilityPermissionId,
            Name = PermissionNames.CanEditBioHubFacility,
            Description = PermissionNames.CanEditBioHubFacility,


        },
        new()
        {
            Id = CanEditBSLLevelPermissionId,
            Name = PermissionNames.CanEditBSLLevel,
            Description = PermissionNames.CanEditBSLLevel,

        },
        new()
        {
            Id = CanEditCountryPermissionId,
            Name = PermissionNames.CanEditCountry,
            Description = PermissionNames.CanEditCountry,

        },
        new()
        {
            Id = CanEditCultivabilityTypePermissionId,
            Name = PermissionNames.CanEditCultivabilityType,
            Description = PermissionNames.CanEditCultivabilityType,

        },
        new()
        {
            Id = CanEditGeneticSequenceDataPermissionId,
            Name = PermissionNames.CanEditGeneticSequenceData,
            Description = PermissionNames.CanEditGeneticSequenceData,
        },

        new()
        {
            Id = CanEditInternationalTaxonomyClassificationPermissionId,
            Name = PermissionNames.CanEditInternationalTaxonomyClassification,
            Description = PermissionNames.CanEditInternationalTaxonomyClassification,
        },

        new()
        {
            Id = CanEditIsolationHostTypePermissionId,
            Name = PermissionNames.CanEditIsolationHostType,
            Description = PermissionNames.CanEditIsolationHostType,
        },

        new()
        {
            Id = CanEditIsolationTechniqueTypePermissionId,
            Name = PermissionNames.CanEditIsolationTechniqueType,
            Description = PermissionNames.CanEditIsolationTechniqueType,
        },

        new()
        {
            Id = CanEditLaboratoryPermissionId,
            Name = PermissionNames.CanEditLaboratory,
            Description = PermissionNames.CanEditLaboratory,
        },

        new()
        {
            Id = CanEditMaterialProductPermissionId,
            Name = PermissionNames.CanEditMaterialProduct,
            Description = PermissionNames.CanEditMaterialProduct,
        },

        new()
        {
            Id = CanEditMaterialPermissionId,
            Name = PermissionNames.CanEditMaterial,
            Description = PermissionNames.CanEditMaterial,
        },

        new()
        {
            Id = CanEditMaterialTypePermissionId,
            Name = PermissionNames.CanEditMaterialType,
            Description = PermissionNames.CanEditMaterialType,
        },

        new()
        {
            Id = CanEditMaterialUsagePermissionId,
            Name = PermissionNames.CanEditMaterialUsage,
            Description = PermissionNames.CanEditMaterialUsage,
        },

        new()
        {
            Id = CanEditPriorityRequestTypePermissionId,
            Name = PermissionNames.CanEditPriorityRequestType,
            Description = PermissionNames.CanEditPriorityRequestType,
        },

        new()
        {
            Id = CanEditRolePermissionId,
            Name = PermissionNames.CanEditRole,
            Description = PermissionNames.CanEditRole,
        },

        new()
        {
            Id = CanEditShipmentPermissionId,
            Name = PermissionNames.CanEditShipment,
            Description = PermissionNames.CanEditShipment,
        },

        new()
        {
            Id = CanEditTemperatureUnitOfMeasurePermissionId,
            Name = PermissionNames.CanEditTemperatureUnitOfMeasure,
            Description = PermissionNames.CanEditTemperatureUnitOfMeasure,
        },

        new()
        {
            Id = CanEditTransportCategoryPermissionId,
            Name = PermissionNames.CanEditTransportCategory,
            Description = PermissionNames.CanEditTransportCategory,
        },

        new()
        {
            Id = CanEditTransportModePermissionId,
            Name = PermissionNames.CanEditTransportMode,
            Description = PermissionNames.CanEditTransportMode,
        },

        new()
        {
            Id = CanEditUserRequestPermissionId,
            Name = PermissionNames.CanEditUserRequest,
            Description = PermissionNames.CanEditUserRequest,
        },

        new()
        {
            Id = CanEditUserRequestStatusPermissionId,
            Name = PermissionNames.CanEditUserRequestStatus,
            Description = PermissionNames.CanEditUserRequestStatus,
        },

        new()
        {
            Id = CanEditUserPermissionId,
            Name = PermissionNames.CanEditUser,
            Description = PermissionNames.CanEditUser,
        },

        new()
        {
            Id = CanDeleteBioHubFacilityPermissionId,
            Name = PermissionNames.CanDeleteBioHubFacility,
            Description = PermissionNames.CanDeleteBioHubFacility,


        },
        new()
        {
            Id = CanDeleteBSLLevelPermissionId,
            Name = PermissionNames.CanDeleteBSLLevel,
            Description = PermissionNames.CanDeleteBSLLevel,

        },
        new()
        {
            Id = CanDeleteCountryPermissionId,
            Name = PermissionNames.CanDeleteCountry,
            Description = PermissionNames.CanDeleteCountry,

        },
        new()
        {
            Id = CanDeleteCultivabilityTypePermissionId,
            Name = PermissionNames.CanDeleteCultivabilityType,
            Description = PermissionNames.CanDeleteCultivabilityType,

        },
        new()
        {
            Id = CanDeleteGeneticSequenceDataPermissionId,
            Name = PermissionNames.CanDeleteGeneticSequenceData,
            Description = PermissionNames.CanDeleteGeneticSequenceData,
        },

        new()
        {
            Id = CanDeleteInternationalTaxonomyClassificationPermissionId,
            Name = PermissionNames.CanDeleteInternationalTaxonomyClassification,
            Description = PermissionNames.CanDeleteInternationalTaxonomyClassification,
        },

        new()
        {
            Id = CanDeleteIsolationHostTypePermissionId,
            Name = PermissionNames.CanDeleteIsolationHostType,
            Description = PermissionNames.CanDeleteIsolationHostType,
        },

        new()
        {
            Id = CanDeleteIsolationTechniqueTypePermissionId,
            Name = PermissionNames.CanDeleteIsolationTechniqueType,
            Description = PermissionNames.CanDeleteIsolationTechniqueType,
        },

        new()
        {
            Id = CanDeleteLaboratoryPermissionId,
            Name = PermissionNames.CanDeleteLaboratory,
            Description = PermissionNames.CanDeleteLaboratory,
        },

        new()
        {
            Id = CanDeleteMaterialProductPermissionId,
            Name = PermissionNames.CanDeleteMaterialProduct,
            Description = PermissionNames.CanDeleteMaterialProduct,
        },

        new()
        {
            Id = CanDeleteMaterialPermissionId,
            Name = PermissionNames.CanDeleteMaterial,
            Description = PermissionNames.CanDeleteMaterial,
        },

        new()
        {
            Id = CanDeleteMaterialTypePermissionId,
            Name = PermissionNames.CanDeleteMaterialType,
            Description = PermissionNames.CanDeleteMaterialType,
        },

        new()
        {
            Id = CanDeleteMaterialUsagePermissionId,
            Name = PermissionNames.CanDeleteMaterialUsage,
            Description = PermissionNames.CanDeleteMaterialUsage,
        },

        new()
        {
            Id = CanDeletePriorityRequestTypePermissionId,
            Name = PermissionNames.CanDeletePriorityRequestType,
            Description = PermissionNames.CanDeletePriorityRequestType,
        },

        new()
        {
            Id = CanDeleteRolePermissionId,
            Name = PermissionNames.CanDeleteRole,
            Description = PermissionNames.CanDeleteRole,
        },

        new()
        {
            Id = CanDeleteShipmentPermissionId,
            Name = PermissionNames.CanDeleteShipment,
            Description = PermissionNames.CanDeleteShipment,
        },

        new()
        {
            Id = CanDeleteTemperatureUnitOfMeasurePermissionId,
            Name = PermissionNames.CanDeleteTemperatureUnitOfMeasure,
            Description = PermissionNames.CanDeleteTemperatureUnitOfMeasure,
        },

        new()
        {
            Id = CanDeleteTransportCategoryPermissionId,
            Name = PermissionNames.CanDeleteTransportCategory,
            Description = PermissionNames.CanDeleteTransportCategory,
        },

        new()
        {
            Id = CanDeleteTransportModePermissionId,
            Name = PermissionNames.CanDeleteTransportMode,
            Description = PermissionNames.CanDeleteTransportMode,
        },

        new()
        {
            Id = CanDeleteUserRequestPermissionId,
            Name = PermissionNames.CanDeleteUserRequest,
            Description = PermissionNames.CanDeleteUserRequest,
        },

        new()
        {
            Id = CanDeleteUserRequestStatusPermissionId,
            Name = PermissionNames.CanDeleteUserRequestStatus,
            Description = PermissionNames.CanDeleteUserRequestStatus,
        },

        new()
        {
            Id = CanDeleteUserPermissionId,
            Name = PermissionNames.CanDeleteUser,
            Description = PermissionNames.CanDeleteUser,
        },

        new()
        {
            Id = CanCreateBioHubFacilityPermissionId,
            Name = PermissionNames.CanCreateBioHubFacility,
            Description = PermissionNames.CanCreateBioHubFacility,
        },

        new()
        {
            Id = CanCreateBSLLevelPermissionId,
            Name = PermissionNames.CanCreateBSLLevel,
            Description = PermissionNames.CanCreateBSLLevel,
        },
        new()
        {
            Id = CanCreateCountryPermissionId,
            Name = PermissionNames.CanCreateCountry,
            Description = PermissionNames.CanCreateCountry,
        },
        new()
        {
            Id = CanCreateCultivabilityTypePermissionId,
            Name = PermissionNames.CanCreateCultivabilityType,
            Description = PermissionNames.CanCreateCultivabilityType,
        },
        new()
        {
            Id = CanCreateGeneticSequenceDataPermissionId,
            Name = PermissionNames.CanCreateGeneticSequenceData,
            Description = PermissionNames.CanCreateGeneticSequenceData,
        },

        new()
        {
            Id = CanCreateInternationalTaxonomyClassificationPermissionId,
            Name = PermissionNames.CanCreateInternationalTaxonomyClassification,
            Description = PermissionNames.CanCreateInternationalTaxonomyClassification,
        },

        new()
        {
            Id = CanCreateIsolationHostTypePermissionId,
            Name = PermissionNames.CanCreateIsolationHostType,
            Description = PermissionNames.CanCreateIsolationHostType,
        },

        new()
        {
            Id = CanCreateIsolationTechniqueTypePermissionId,
            Name = PermissionNames.CanCreateIsolationTechniqueType,
            Description = PermissionNames.CanCreateIsolationTechniqueType,
        },

        new()
        {
            Id = CanCreateLaboratoryPermissionId,
            Name = PermissionNames.CanCreateLaboratory,
            Description = PermissionNames.CanCreateLaboratory,
        },

        new()
        {
            Id = CanCreateMaterialProductPermissionId,
            Name = PermissionNames.CanCreateMaterialProduct,
            Description = PermissionNames.CanCreateMaterialProduct,
        },

        new()
        {
            Id = CanCreateMaterialPermissionId,
            Name = PermissionNames.CanCreateMaterial,
            Description = PermissionNames.CanCreateMaterial,
        },

        new()
        {
            Id = CanCreateMaterialTypePermissionId,
            Name = PermissionNames.CanCreateMaterialType,
            Description = PermissionNames.CanCreateMaterialType,
        },

        new()
        {
            Id = CanCreateMaterialUsagePermissionId,
            Name = PermissionNames.CanCreateMaterialUsage,
            Description = PermissionNames.CanCreateMaterialUsage,
        },

        new()
        {
            Id = CanCreatePriorityRequestTypePermissionId,
            Name = PermissionNames.CanCreatePriorityRequestType,
            Description = PermissionNames.CanCreatePriorityRequestType,
        },

        new()
        {
            Id = CanCreateRolePermissionId,
            Name = PermissionNames.CanCreateRole,
            Description = PermissionNames.CanCreateRole,
        },

        new()
        {
            Id = CanCreateShipmentPermissionId,
            Name = PermissionNames.CanCreateShipment,
            Description = PermissionNames.CanCreateShipment,
        },

        new()
        {
            Id = CanCreateTemperatureUnitOfMeasurePermissionId,
            Name = PermissionNames.CanCreateTemperatureUnitOfMeasure,
            Description = PermissionNames.CanCreateTemperatureUnitOfMeasure,
        },

        new()
        {
            Id = CanCreateTransportCategoryPermissionId,
            Name = PermissionNames.CanCreateTransportCategory,
            Description = PermissionNames.CanCreateTransportCategory,
        },

        new()
        {
            Id = CanCreateTransportModePermissionId,
            Name = PermissionNames.CanCreateTransportMode,
            Description = PermissionNames.CanCreateTransportMode,
        },

        new()
        {
            Id = CanCreateUserRequestPermissionId,
            Name = PermissionNames.CanCreateUserRequest,
            Description = PermissionNames.CanCreateUserRequest,
        },

        new()
        {
            Id = CanCreateUserRequestStatusPermissionId,
            Name = PermissionNames.CanCreateUserRequestStatus,
            Description = PermissionNames.CanCreateUserRequestStatus,
        },

        new()
        {
            Id = CanCreateUserPermissionId,
            Name = PermissionNames.CanCreateUser,
            Description = PermissionNames.CanCreateUser,
        },


        new()
        {
            Id = CanAccessWHODashboardPermissionId,
            Name = PermissionNames.CanAccessWHODashboard,
            Description = PermissionNames.CanAccessWHODashboard,
        },
        new()
        {
            Id = CanAccessWHOBMEPPPermissionId,
            Name = PermissionNames.CanAccessWHOBMEPP,
            Description = PermissionNames.CanAccessWHOBMEPP,
        },
        new()
        {
            Id = CanAccessWHOBioHubFacilitiesPermissionId,
            Name = PermissionNames.CanAccessWHOBioHubFacilities,
            Description = PermissionNames.CanAccessWHOBioHubFacilities,
        },
        new()
        {
            Id = CanAccessWHOLaboratoriesPermissionId,
            Name = PermissionNames.CanAccessWHOLaboratories,
            Description = PermissionNames.CanAccessWHOLaboratories,
        },
        new()
        {
            Id = CanAccessWHOTemplatesPermissionId,
            Name = PermissionNames.CanAccessWHOTemplates,
            Description = PermissionNames.CanAccessWHOTemplates,
        },
        new()
        {
            Id = CanAccessWHOShipmentsPermissionId,
            Name = PermissionNames.CanAccessWHOShipments,
            Description = PermissionNames.CanAccessWHOShipments,
        },
        new()
        {
            Id = CanAccessWHOPendingRequestPermissionId,
            Name = PermissionNames.CanAccessWHOPendingRequest,
            Description = PermissionNames.CanAccessWHOPendingRequest,
        },
        new()
        {
            Id = CanAccessWHOUsersPermissionId,
            Name = PermissionNames.CanAccessWHOUsers,
            Description = PermissionNames.CanAccessWHOUsers,
        },
        new()
        {
            Id = CanAccessLaboratoryDashboardPermissionId,
            Name = PermissionNames.CanAccessLaboratoryDashboard,
            Description = PermissionNames.CanAccessLaboratoryDashboard,
        },
        new()
        {
            Id = CanAccessLaboratoryUserProfilePermissionId,
            Name = PermissionNames.CanAccessLaboratoryUserProfile,
            Description = PermissionNames.CanAccessLaboratoryUserProfile,
        },
        new()
        {
            Id = CanAccessLaboratoryStaffPermissionId,
            Name = PermissionNames.CanAccessLaboratoryStaff,
            Description = PermissionNames.CanAccessLaboratoryStaff,
        },
        new()
        {
            Id = CanAccessLaboratoryFacilityInstituteProfilePermissionId,
            Name = PermissionNames.CanAccessLaboratoryFacilityInstituteProfile,
            Description = PermissionNames.CanAccessLaboratoryFacilityInstituteProfile,
        },
        new()
        {
            Id = CanAccessLaboratoryBMEPPPermissionId,
            Name = PermissionNames.CanAccessLaboratoryBMEPP,
            Description = PermissionNames.CanAccessLaboratoryBMEPP,
        },
        new()
        {
            Id = CanAccessLaboratoryBMEPPCataloguePermissionId,
            Name = PermissionNames.CanAccessLaboratoryBMEPPCatalogue,
            Description = PermissionNames.CanAccessLaboratoryBMEPPCatalogue,
        },
        new()
        {
            Id = CanAccessLaboratoryTemplatesPermissionId,
            Name = PermissionNames.CanAccessLaboratoryTemplates,
            Description = PermissionNames.CanAccessLaboratoryTemplates,
        },
        new()
        {
            Id = CanAccessBioHubFacilityDashboardPermissionId,
            Name = PermissionNames.CanAccessBioHubFacilityDashboard,
            Description = PermissionNames.CanAccessBioHubFacilityDashboard,
        },
        new()
        {
            Id = CanAccessBioHubFacilityUserProfilePermissionId,
            Name = PermissionNames.CanAccessBioHubFacilityUserProfile,
            Description = PermissionNames.CanAccessBioHubFacilityUserProfile,
        },
        new()
        {
            Id = CanAccessBioHubFacilityLaboratoriesPermissionId,
            Name = PermissionNames.CanAccessBioHubFacilityFacilityLaboratories,
            Description = PermissionNames.CanAccessBioHubFacilityFacilityLaboratories,
        },
        new()
        {
            Id = CanAccessBioHubFacilityBMEPPPermissionId,
            Name = PermissionNames.CanAccessBioHubFacilityBMEPP,
            Description = PermissionNames.CanAccessBioHubFacilityBMEPP,
        },
        new()
        {
            Id = CanAccessBioHubFacilityTemplatesPermissionId,
            Name = PermissionNames.CanAccessBioHubFacilityTemplates,
            Description = PermissionNames.CanAccessBioHubFacilityTemplates,
        },
        new()
        {
            Id = CanReadLaboratoryStaffPermissionId,
            Name = PermissionNames.CanReadLaboratoryStaff,
            Description = PermissionNames.CanReadLaboratoryStaff,
        },
        new()
        {
            Id = CanEditLaboratoryStaffPermissionId,
            Name = PermissionNames.CanEditLaboratoryStaff,
            Description = PermissionNames.CanEditLaboratoryStaff,
        },
        new()
        {
            Id = CanDeleteLaboratoryStaffPermissionId,
            Name = PermissionNames.CanDeleteLaboratoryStaff,
            Description = PermissionNames.CanDeleteLaboratoryStaff,
        },
        new()
        {
            Id = CanApproveOrRejectUserRequestPermissionId,
            Name = PermissionNames.CanApproveOrRejectUserRequest,
            Description = PermissionNames.CanApproveOrRejectUserRequest,
        },
        new()
        {
            Id = CanCreateDocumentTemplatePermissionId,
            Name = PermissionNames.CanCreateDocumentTemplate,
            Description = PermissionNames.CanCreateDocumentTemplate,
        },
        new()
        {
            Id = CanEditDocumentTemplatePermissionId,
            Name = PermissionNames.CanEditDocumentTemplate,
            Description = PermissionNames.CanEditDocumentTemplate,
        },
        new()
        {
            Id = CanDeleteDocumentTemplatePermissionId,
            Name = PermissionNames.CanDeleteDocumentTemplate,
            Description = PermissionNames.CanDeleteDocumentTemplate,
        },
        new()
        {
            Id = CanReadDocumentTemplatePermissionId,
            Name = PermissionNames.CanReadDocumentTemplate,
            Description = PermissionNames.CanReadDocumentTemplate,
        },
        new()
        {
            Id = CanAccessRequestIniziationPermissionId,
            Name = PermissionNames.CanAccessRequestIniziation,
            Description = PermissionNames.CanAccessRequestIniziation,
        },
        new()
        {
            Id = CanAccessWorklistPermissionId,
            Name = PermissionNames.CanAccessWorklist,
            Description = PermissionNames.CanAccessWorklist,
        },
        new()
        {
            Id = CanReadSubmitSMTA1PermissionId,
            Name = PermissionNames.CanReadSubmitSMTA1,
            Description = PermissionNames.CanReadSubmitSMTA1,
        },
        new()
        {
            Id = CanDownloadFileSubmitSMTA1PermissionId,
            Name = PermissionNames.CanDownloadFileSubmitSMTA1,
            Description = PermissionNames.CanDownloadFileSubmitSMTA1,
        },
        new()
        {
            Id = CanSubmitSMTA1PermissionId,
            Name = PermissionNames.CanSubmitSMTA1,
            Description = PermissionNames.CanSubmitSMTA1,
        },
        new()
        {
            Id = CanReadWaitingForSMTA1SECsApprovalPermissionId,
            Name = PermissionNames.CanReadWaitingForSMTA1SECsApproval,
            Description = PermissionNames.CanReadWaitingForSMTA1SECsApproval,
        },
        new()
        {
            Id = CanSubmitWaitingForSMTA1SECsApprovalPermissionId,
            Name = PermissionNames.CanSubmitWaitingForSMTA1SECsApproval,
            Description = PermissionNames.CanSubmitWaitingForSMTA1SECsApproval,
        },
        new()
        {
            Id = CanDownloadFileWaitingForSMTA1SECsApprovalPermissionId,
            Name = PermissionNames.CanDownloadFileWaitingForSMTA1SECsApproval,
            Description = PermissionNames.CanDownloadFileWaitingForSMTA1SECsApproval,
        },

        new()
        {
            Id = CanReadSubmitAnnex2OfSMTA1PermissionId,
            Name = PermissionNames.CanReadSubmitAnnex2OfSMTA1,
            Description = PermissionNames.CanReadSubmitAnnex2OfSMTA1,
        },
        new()
        {
            Id = CanSubmitAnnex2OfSMTA1PermissionId,
            Name = PermissionNames.CanSubmitAnnex2OfSMTA1,
            Description = PermissionNames.CanSubmitAnnex2OfSMTA1,
        },
        new()
        {
            Id = CanDownloadFileSubmitAnnex2OfSMTA1PermissionId,
            Name = PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA1,
            Description = PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA1,
        },

        new()
        {
            Id = CanReceiveEmailsOnRequestInitiationPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnRequestInitiation,
            Description = PermissionNames.CanReceiveEmailsOnRequestInitiation,
        },
        new()
        {
            Id = CanReceiveEmailsOnSubmitSMTA1PermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitSMTA1,
            Description = PermissionNames.CanReceiveEmailsOnSubmitSMTA1,
        },
        new()
        {
            Id = CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalApproval,
        },
        new()
        {
            Id = CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalReject,
        },
        new()
        {
            Id = CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApproval,
        },
        new()
        {
            Id = CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalReject,
        },


        new()
        {
            Id = CanReceiveEmailsOnSubmitAnnex2OfSMTA1ApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitAnnex2OfSMTA1Approval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitAnnex2OfSMTA1Approval,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitAnnex2OfSMTA1RejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitAnnex2OfSMTA1Reject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitAnnex2OfSMTA1Reject,
        },
        new()
        {
            Id = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            Name = PermissionNames.CanReadWaitingForAnnex2OfSMTA1SECsApproval,
            Description = PermissionNames.CanReadWaitingForAnnex2OfSMTA1SECsApproval,
        },

        new()
        {
            Id = CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            Name = PermissionNames.CanSubmitWaitingForAnnex2OfSMTA1SECsApproval,
            Description = PermissionNames.CanSubmitWaitingForAnnex2OfSMTA1SECsApproval,
        },
        new()
        {
            Id = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPermissionId,
            Name = PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA1SECsApproval,
            Description = PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA1SECsApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalReject,
        },

        new()
        {
            Id = CanReadSubmitBookingFormOfSMTA1PermissionId,
            Name = PermissionNames.CanReadSubmitBookingFormOfSMTA1,
            Description = PermissionNames.CanReadSubmitBookingFormOfSMTA1,
        },

        new()
        {
            Id = CanSubmitBookingFormOfSMTA1PermissionId,
            Name = PermissionNames.CanSubmitBookingFormOfSMTA1,
            Description = PermissionNames.CanSubmitBookingFormOfSMTA1,
        },

        new()
        {
            Id = CanDownloadFileSubmitBookingFormOfSMTA1PermissionId,
            Name = PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA1,
            Description = PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA1,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitBookingFormOfSMTA1ApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitBookingFormOfSMTA1Approval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitBookingFormOfSMTA1Approval,
        },
        new()
        {
            Id = CanReceiveEmailsOnSubmitBookingFormOfSMTA1RejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitBookingFormOfSMTA1Reject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitBookingFormOfSMTA1Reject,
        },

        new()
        {
            Id = CanReadWaitForBookingFormSMTA1OPSApprovalPermissionId,
            Name = PermissionNames.CanReadWaitForBookingFormSMTA1OPSApproval,
            Description = PermissionNames.CanReadWaitForBookingFormSMTA1OPSApproval,
        },

        new()
        {
            Id = CanReadSMTA1ShipmentDocumentsPermissionId,
            Name = PermissionNames.CanReadSMTA1ShipmentDocuments,
            Description = PermissionNames.CanReadSMTA1ShipmentDocuments,
        },

        new()
        {
            Id = CanSubmitWaitForBookingFormSMTA1OPSApprovalPermissionId,
            Name = PermissionNames.CanSubmitWaitForBookingFormSMTA1OPSApproval,
            Description = PermissionNames.CanSubmitWaitForBookingFormSMTA1OPSApproval,
        },

        new()
        {
            Id = CanSubmitSMTA1ShipmentDocumentsPermissionId,
            Name = PermissionNames.CanSubmitSMTA1ShipmentDocuments,
            Description = PermissionNames.CanSubmitSMTA1ShipmentDocuments,
        },

        new()
        {
            Id = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForBookingFormSMTA1OPSApproval,
            Description = PermissionNames.CanDownloadFileWaitForBookingFormSMTA1OPSApproval,
        },

        new()
        {
            Id = CanDownloadSMTA1ShipmentDocumentsPermissionId,
            Name = PermissionNames.CanDownloadSMTA1ShipmentDocuments,
            Description = PermissionNames.CanDownloadSMTA1ShipmentDocuments,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnSMTA1ShipmentDocumentsApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSMTA1ShipmentDocumentsApproval,
            Description = PermissionNames.CanReceiveEmailsOnSMTA1ShipmentDocumentsApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnSMTA1ShipmentDocumentsRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSMTA1ShipmentDocumentsReject,
            Description = PermissionNames.CanReceiveEmailsOnSMTA1ShipmentDocumentsReject,
        },


        new()
        {
            Id = CanCreateCourierPermissionId,
            Name = PermissionNames.CanCreateCourier,
            Description = PermissionNames.CanCreateCourier,
        },

        new()
        {
            Id = CanEditCourierPermissionId,
            Name = PermissionNames.CanEditCourier,
            Description = PermissionNames.CanEditCourier,
        },

        new()
        {
            Id = CanReadCourierPermissionId,
            Name = PermissionNames.CanReadCourier,
            Description = PermissionNames.CanReadCourier,
        },

        new()
        {
            Id = CanDeleteCourierPermissionId,
            Name = PermissionNames.CanDeleteCourier,
            Description = PermissionNames.CanDeleteCourier,
        },




        new()
        {
            Id = CanCreateCourierStaffPermissionId,
            Name = PermissionNames.CanCreateCourierStaff,
            Description = PermissionNames.CanCreateCourierStaff,
        },

        new()
        {
            Id = CanEditCourierStaffPermissionId,
            Name = PermissionNames.CanEditCourierStaff,
            Description = PermissionNames.CanEditCourierStaff,
        },

        new()
        {
            Id = CanReadCourierStaffPermissionId,
            Name = PermissionNames.CanReadCourierStaff,
            Description = PermissionNames.CanReadCourierStaff,
        },

        new()
        {
            Id = CanDeleteCourierStaffPermissionId,
            Name = PermissionNames.CanDeleteCourierStaff,
            Description = PermissionNames.CanDeleteCourierStaff,
        },

                new()
        {
            Id = CanReadWaitForPickUpCompletedPermissionId,
            Name = PermissionNames.CanReadWaitForPickUpCompleted,
            Description = PermissionNames.CanReadWaitForPickUpCompleted,
        },

        new()
        {
            Id = CanReadWaitForDeliveryCompletedPermissionId,
            Name = PermissionNames.CanReadWaitForDeliveryCompleted,
            Description = PermissionNames.CanReadWaitForDeliveryCompleted,
        },

        new()
        {
            Id = CanReadWaitForArrivalConditionCheckPermissionId,
            Name = PermissionNames.CanReadWaitForArrivalConditionCheck,
            Description = PermissionNames.CanReadWaitForArrivalConditionCheck,
        },

        new()
        {
            Id = CanReadWaitForCommentBHFSendFeedbackPermissionId,
            Name = PermissionNames.CanReadWaitForCommentBHFSendFeedback,
            Description = PermissionNames.CanReadWaitForCommentBHFSendFeedback,
        },

        new()
        {
            Id = CanReadWaitForFinalApprovalPermissionId,
            Name = PermissionNames.CanReadWaitForFinalApproval,
            Description = PermissionNames.CanReadWaitForFinalApproval,
        },

        new()
        {
            Id = CanReadShipmentCompletedPermissionId,
            Name = PermissionNames.CanReadShipmentCompleted,
            Description = PermissionNames.CanReadShipmentCompleted,
        },

        new()
        {
            Id = CanSubmitWaitForPickUpCompletedPermissionId,
            Name = PermissionNames.CanSubmitWaitForPickUpCompleted,
            Description = PermissionNames.CanSubmitWaitForPickUpCompleted,
        },

        new()
        {
            Id = CanSubmitWaitForDeliveryCompletedPermissionId,
            Name = PermissionNames.CanSubmitWaitForDeliveryCompleted,
            Description = PermissionNames.CanSubmitWaitForDeliveryCompleted,
        },

        new()
        {
            Id = CanSubmitWaitForArrivalConditionCheckPermissionId,
            Name = PermissionNames.CanSubmitWaitForArrivalConditionCheck,
            Description = PermissionNames.CanSubmitWaitForArrivalConditionCheck,
        },

        new()
        {
            Id = CanSubmitWaitForCommentBHFSendFeedbackPermissionId,
            Name = PermissionNames.CanSubmitWaitForCommentBHFSendFeedback,
            Description = PermissionNames.CanSubmitWaitForCommentBHFSendFeedback,
        },

        new()
        {
            Id = CanSubmitWaitForFinalApprovalPermissionId,
            Name = PermissionNames.CanSubmitWaitForFinalApproval,
            Description = PermissionNames.CanSubmitWaitForFinalApproval,
        },

        new()
        {
            Id = CanSubmitShipmentCompletedPermissionId,
            Name = PermissionNames.CanSubmitShipmentCompleted,
            Description = PermissionNames.CanSubmitShipmentCompleted,
        },

        new()
        {
            Id = CanDownloadFileWaitForPickUpCompletedPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForPickUpCompleted,
            Description = PermissionNames.CanDownloadFileWaitForPickUpCompleted,
        },

        new()
        {
            Id = CanDownloadFileWaitForDeliveryCompletedPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForDeliveryCompleted,
            Description = PermissionNames.CanDownloadFileWaitForDeliveryCompleted,
        },

        new()
        {
            Id = CanDownloadFileWaitForArrivalConditionCheckPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForArrivalConditionCheck,
            Description = PermissionNames.CanDownloadFileWaitForArrivalConditionCheck,
        },

        new()
        {
            Id = CanDownloadFileWaitForCommentBHFSendFeedbackPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForCommentBHFSendFeedback,
            Description = PermissionNames.CanDownloadFileWaitForCommentBHFSendFeedback,
        },

        new()
        {
            Id = CanDownloadFileWaitForFinalApprovalPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForFinalApproval,
            Description = PermissionNames.CanDownloadFileWaitForFinalApproval,
        },

        new()
        {
            Id = CanDownloadFileShipmentCompletedPermissionId,
            Name = PermissionNames.CanDownloadFileShipmentCompleted,
            Description = PermissionNames.CanDownloadFileShipmentCompleted,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForPickUpCompletedApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForPickUpCompletedApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForPickUpCompletedApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForDeliveryCompletedApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForDeliveryCompletedApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForDeliveryCompletedApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForArrivalConditionCheckApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForArrivalConditionCheckApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForArrivalConditionCheckApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForFinalApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForFinalApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForFinalApprovalApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnShipmentCompletedApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnShipmentCompletedApproval,
            Description = PermissionNames.CanReceiveEmailsOnShipmentCompletedApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForPickUpCompletedRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForPickUpCompletedReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForPickUpCompletedReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForDeliveryCompletedRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForDeliveryCompletedReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForDeliveryCompletedReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForArrivalConditionCheckRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForArrivalConditionCheckReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForArrivalConditionCheckReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForCommentBHFSendFeedbackRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForCommentBHFSendFeedbackReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForCommentBHFSendFeedbackReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForFinalApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForFinalApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForFinalApprovalReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnShipmentCompletedRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnShipmentCompletedReject,
            Description = PermissionNames.CanReceiveEmailsOnShipmentCompletedReject,
        },






        new()
        {
            Id = CanReadSubmitSMTA2PermissionId,
            Name = PermissionNames.CanReadSubmitSMTA2,
            Description = PermissionNames.CanReadSubmitSMTA2,
        },

        new()
        {
            Id = CanSubmitSMTA2PermissionId,
            Name = PermissionNames.CanSubmitSMTA2,
            Description = PermissionNames.CanSubmitSMTA2,
        },

        new()
        {
            Id = CanDownloadFileSubmitSMTA2PermissionId,
            Name = PermissionNames.CanDownloadFileSubmitSMTA2,
            Description = PermissionNames.CanDownloadFileSubmitSMTA2,
        },

        new()
        {
            Id = CanReadWaitingForSMTA2SECsApprovalPermissionId,
            Name = PermissionNames.CanReadWaitingForSMTA2SECsApproval,
            Description = PermissionNames.CanReadWaitingForSMTA2SECsApproval,
        },

        new()
        {
            Id = CanSubmitWaitingForSMTA2SECsApprovalPermissionId,
            Name = PermissionNames.CanSubmitWaitingForSMTA2SECsApproval,
            Description = PermissionNames.CanSubmitWaitingForSMTA2SECsApproval,
        },

        new()
        {
            Id = CanDownloadFileWaitingForSMTA2SECsApprovalPermissionId,
            Name = PermissionNames.CanDownloadFileWaitingForSMTA2SECsApproval,
            Description = PermissionNames.CanDownloadFileWaitingForSMTA2SECsApproval,
        },

        new()
        {
            Id = CanReadSubmitAnnex2OfSMTA2PermissionId,
            Name = PermissionNames.CanReadSubmitAnnex2OfSMTA2,
            Description = PermissionNames.CanReadSubmitAnnex2OfSMTA2,
        },

        new()
        {
            Id = CanSubmitAnnex2OfSMTA2PermissionId,
            Name = PermissionNames.CanSubmitAnnex2OfSMTA2,
            Description = PermissionNames.CanSubmitAnnex2OfSMTA2,
        },

        new()
        {
            Id = CanDownloadFileSubmitAnnex2OfSMTA2PermissionId,
            Name = PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA2,
            Description = PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA2,
        },

        new()
        {
            Id = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            Name = PermissionNames.CanReadWaitingForAnnex2OfSMTA2SECsApproval,
            Description = PermissionNames.CanReadWaitingForAnnex2OfSMTA2SECsApproval,
        },

        new()
        {
            Id = CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            Name = PermissionNames.CanSubmitWaitingForAnnex2OfSMTA2SECsApproval,
            Description = PermissionNames.CanSubmitWaitingForAnnex2OfSMTA2SECsApproval,
        },

        new()
        {
            Id = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPermissionId,
            Name = PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA2SECsApproval,
            Description = PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA2SECsApproval,
        },

        new()
        {
            Id = CanReadSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            Name = PermissionNames.CanReadSubmitBiosafetyChecklistFormOfSMTA2,
            Description = PermissionNames.CanReadSubmitBiosafetyChecklistFormOfSMTA2,
        },

        new()
        {
            Id = CanSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            Name = PermissionNames.CanSubmitBiosafetyChecklistFormOfSMTA2,
            Description = PermissionNames.CanSubmitBiosafetyChecklistFormOfSMTA2,
        },

        new()
        {
            Id = CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2PermissionId,
            Name = PermissionNames.CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2,
            Description = PermissionNames.CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2,
        },

        new()
        {
            Id = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            Name = PermissionNames.CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            Description = PermissionNames.CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApproval,
        },

        new()
        {
            Id = CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            Name = PermissionNames.CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            Description = PermissionNames.CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApproval,
        },

        new()
        {
            Id = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApproval,
            Description = PermissionNames.CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApproval,
        },

        new()
        {
            Id = CanReadSubmitBookingFormOfSMTA2PermissionId,
            Name = PermissionNames.CanReadSubmitBookingFormOfSMTA2,
            Description = PermissionNames.CanReadSubmitBookingFormOfSMTA2,
        },

        new()
        {
            Id = CanSubmitBookingFormOfSMTA2PermissionId,
            Name = PermissionNames.CanSubmitBookingFormOfSMTA2,
            Description = PermissionNames.CanSubmitBookingFormOfSMTA2,
        },

        new()
        {
            Id = CanDownloadFileSubmitBookingFormOfSMTA2PermissionId,
            Name = PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA2,
            Description = PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA2,
        },

        new()
        {
            Id = CanReadWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            Name = PermissionNames.CanReadWaitForBookingFormSMTA2OPSsApproval,
            Description = PermissionNames.CanReadWaitForBookingFormSMTA2OPSsApproval,
        },

        new()
        {
            Id = CanSubmitWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            Name = PermissionNames.CanSubmitWaitForBookingFormSMTA2OPSsApproval,
            Description = PermissionNames.CanSubmitWaitForBookingFormSMTA2OPSsApproval,
        },

        new()
        {
            Id = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForBookingFormSMTA2OPSsApproval,
            Description = PermissionNames.CanDownloadFileWaitForBookingFormSMTA2OPSsApproval,
        },

        new()
        {
            Id = CanReadBHFSMTA2ShipmentDocumentsPermissionId,
            Name = PermissionNames.CanReadBHFSMTA2ShipmentDocuments,
            Description = PermissionNames.CanReadBHFSMTA2ShipmentDocuments,
        },

        new()
        {
            Id = CanSubmitBHFSMTA2ShipmentDocumentsPermissionId,
            Name = PermissionNames.CanSubmitBHFSMTA2ShipmentDocuments,
            Description = PermissionNames.CanSubmitBHFSMTA2ShipmentDocuments,
        },

        new()
        {
            Id = CanDownloadBHFSMTA2ShipmentDocumentsPermissionId,
            Name = PermissionNames.CanDownloadBHFSMTA2ShipmentDocuments,
            Description = PermissionNames.CanDownloadBHFSMTA2ShipmentDocuments,
        },

        new()
        {
            Id = CanReadQESMTA2ShipmentDocumentsPermissionId,
            Name = PermissionNames.CanReadQESMTA2ShipmentDocuments,
            Description = PermissionNames.CanReadQESMTA2ShipmentDocuments,
        },

        new()
        {
            Id = CanSubmitQESMTA2ShipmentDocumentsPermissionId,
            Name = PermissionNames.CanSubmitQESMTA2ShipmentDocuments,
            Description = PermissionNames.CanSubmitQESMTA2ShipmentDocuments,
        },

        new()
        {
            Id = CanDownloadQESMTA2ShipmentDocumentsPermissionId,
            Name = PermissionNames.CanDownloadQESMTA2ShipmentDocuments,
            Description = PermissionNames.CanDownloadQESMTA2ShipmentDocuments,
        },

        new()
        {
            Id = CanReadWaitForPickUpFromBioHubCompletedPermissionId,
            Name = PermissionNames.CanReadWaitForPickUpFromBioHubCompleted,
            Description = PermissionNames.CanReadWaitForPickUpFromBioHubCompleted,
        },

        new()
        {
            Id = CanSubmitWaitForPickUpFromBioHubCompletedPermissionId,
            Name = PermissionNames.CanSubmitWaitForPickUpFromBioHubCompleted,
            Description = PermissionNames.CanSubmitWaitForPickUpFromBioHubCompleted,
        },

        new()
        {
            Id = CanDownloadFileWaitForPickUpFromBioHubCompletedPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForPickUpFromBioHubCompleted,
            Description = PermissionNames.CanDownloadFileWaitForPickUpFromBioHubCompleted,
        },

        new()
        {
            Id = CanReadWaitForDeliveryFromBioHubCompletedPermissionId,
            Name = PermissionNames.CanReadWaitForDeliveryFromBioHubCompleted,
            Description = PermissionNames.CanReadWaitForDeliveryFromBioHubCompleted,
        },

        new()
        {
            Id = CanSubmitWaitForDeliveryFromBioHubCompletedPermissionId,
            Name = PermissionNames.CanSubmitWaitForDeliveryFromBioHubCompleted,
            Description = PermissionNames.CanSubmitWaitForDeliveryFromBioHubCompleted,
        },

        new()
        {
            Id = CanDownloadFileWaitForDeliveryFromBioHubCompletedPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForDeliveryFromBioHubCompleted,
            Description = PermissionNames.CanDownloadFileWaitForDeliveryFromBioHubCompleted,
        },

        new()
        {
            Id = CanReadWaitForArrivalConditionFromBioHubCheckPermissionId,
            Name = PermissionNames.CanReadWaitForArrivalConditionFromBioHubCheck,
            Description = PermissionNames.CanReadWaitForArrivalConditionFromBioHubCheck,
        },

        new()
        {
            Id = CanSubmitWaitForArrivalConditionFromBioHubCheckPermissionId,
            Name = PermissionNames.CanSubmitWaitForArrivalConditionFromBioHubCheck,
            Description = PermissionNames.CanSubmitWaitForArrivalConditionFromBioHubCheck,
        },

        new()
        {
            Id = CanDownloadFileWaitForArrivalConditionFromBioHubCheckPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForArrivalConditionFromBioHubCheck,
            Description = PermissionNames.CanDownloadFileWaitForArrivalConditionFromBioHubCheck,
        },

        new()
        {
            Id = CanReadWaitForCommentQESendFeedbackPermissionId,
            Name = PermissionNames.CanReadWaitForCommentQESendFeedback,
            Description = PermissionNames.CanReadWaitForCommentQESendFeedback,
        },

        new()
        {
            Id = CanSubmitWaitForCommentQESendFeedbackPermissionId,
            Name = PermissionNames.CanSubmitWaitForCommentQESendFeedback,
            Description = PermissionNames.CanSubmitWaitForCommentQESendFeedback,
        },

        new()
        {
            Id = CanDownloadFileWaitForCommentQESendFeedbackPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForCommentQESendFeedback,
            Description = PermissionNames.CanDownloadFileWaitForCommentQESendFeedback,
        },

        new()
        {
            Id = CanReadWaitForFinalApprovalFromBioHubPermissionId,
            Name = PermissionNames.CanReadWaitForFinalApprovalFromBioHub,
            Description = PermissionNames.CanReadWaitForFinalApprovalFromBioHub,
        },

        new()
        {
            Id = CanSubmitWaitForFinalApprovalFromBioHubPermissionId,
            Name = PermissionNames.CanSubmitWaitForFinalApprovalFromBioHub,
            Description = PermissionNames.CanSubmitWaitForFinalApprovalFromBioHub,
        },

        new()
        {
            Id = CanDownloadFileWaitForFinalApprovalFromBioHubPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForFinalApprovalFromBioHub,
            Description = PermissionNames.CanDownloadFileWaitForFinalApprovalFromBioHub,
        },

        new()
        {
            Id = CanReadShipmentFromBioHubCompletedPermissionId,
            Name = PermissionNames.CanReadShipmentFromBioHubCompleted,
            Description = PermissionNames.CanReadShipmentFromBioHubCompleted,
        },

        new()
        {
            Id = CanSubmitShipmentFromBioHubCompletedPermissionId,
            Name = PermissionNames.CanSubmitShipmentFromBioHubCompleted,
            Description = PermissionNames.CanSubmitShipmentFromBioHubCompleted,
        },

        new()
        {
            Id = CanDownloadFileShipmentFromBioHubCompletedPermissionId,
            Name = PermissionNames.CanDownloadFileShipmentFromBioHubCompleted,
            Description = PermissionNames.CanDownloadFileShipmentFromBioHubCompleted,
        },

        new()
        {
            Id = CanReceiveEmailsOnRequestInitiationApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnRequestInitiationApproval,
            Description = PermissionNames.CanReceiveEmailsOnRequestInitiationApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitSMTA2ApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitSMTA2Approval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitSMTA2Approval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitingForSMTA2SECsApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitingForSMTA2SECsApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitingForSMTA2SECsApprovalApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitAnnex2OfSMTA2ApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitAnnex2OfSMTA2Approval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitAnnex2OfSMTA2Approval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2ApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2Approval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2Approval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitBookingFormOfSMTA2ApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitBookingFormOfSMTA2Approval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitBookingFormOfSMTA2Approval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsApproval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsApproval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedApproval,
        },

        new()
        {
            Id = CanReceiveEmailOnNumberOfVialsWarningPermissionId,
            Name = PermissionNames.CanReceiveEmailOnNumberOfVialsWarning,
            Description = PermissionNames.CanReceiveEmailOnNumberOfVialsWarning,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForCommentQESendFeedbackApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForCommentQESendFeedbackApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForCommentQESendFeedbackApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForFinalApprovalFromBioHubApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForFinalApprovalFromBioHubApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForFinalApprovalFromBioHubApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnShipmentFromBioHubCompletedApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnShipmentFromBioHubCompletedApproval,
            Description = PermissionNames.CanReceiveEmailsOnShipmentFromBioHubCompletedApproval,
        },

        new()
        {
            Id = CanReceiveEmailsOnRequestInitiationRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnRequestInitiationReject,
            Description = PermissionNames.CanReceiveEmailsOnRequestInitiationReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitSMTA2RejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitSMTA2Reject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitSMTA2Reject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitingForSMTA2SECsApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitingForSMTA2SECsApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitingForSMTA2SECsApprovalReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitAnnex2OfSMTA2RejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitAnnex2OfSMTA2Reject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitAnnex2OfSMTA2Reject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2RejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2Reject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2Reject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitBookingFormOfSMTA2RejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitBookingFormOfSMTA2Reject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitBookingFormOfSMTA2Reject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsReject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsReject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForSMTA2FinalApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForSMTA2FinalApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForSMTA2FinalApprovalReject,
        },

        new()
        {
            Id = CanReceiveEmailsOnWaitForFinalApprovalFromBioHubRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForFinalApprovalFromBioHubReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForFinalApprovalFromBioHubReject,
        },

        new()
        {
            Id = CanReadDocumentPermissionId,
            Name = PermissionNames.CanReadDocument,
            Description = PermissionNames.CanReadDocument,
        },


        new()
        {
            Id = CanApproveBioHubFacilityCompletionPermissionId,
            Name = PermissionNames.CanApproveBioHubFacilityCompletion,
            Description = PermissionNames.CanApproveBioHubFacilityCompletion,
        },

        new()
        {
            Id = CanApproveLaboratoryCompletionPermissionId,
            Name = PermissionNames.CanApproveLaboratoryCompletion,
            Description = PermissionNames.CanApproveLaboratoryCompletion,
        },

        new()
        {
            Id = CanVerifyMaterialPermissionId,
            Name = PermissionNames.CanVerifyMaterial,
            Description = PermissionNames.CanVerifyMaterial,
        },


        new()
        {
            Id = CanSetMaterialReadyToSharePermissionId,
            Name = PermissionNames.CanSetMaterialReadyToShare,
            Description = PermissionNames.CanSetMaterialReadyToShare,
        },

        new()
        {
            Id = CanSetMaterialPublicPermissionId,
            Name = PermissionNames.CanSetMaterialPublic,
            Description = PermissionNames.CanSetMaterialPublic,
        },

        new()
        {
            Id = CanReceiveEmailsOnRequestAccessPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnRequestAccess,
            Description = PermissionNames.CanReceiveEmailsOnRequestAccess,
        },

        new()
        {
            Id = CanReadKpiDataPermissionId,
            Name = PermissionNames.CanReadKpiData,
            Description = PermissionNames.CanReadKpiData,
        },


        new()
        {
            Id = CanReceiveEmailsOnSubmitSMTA2PermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitSMTA2,
            Description = PermissionNames.CanReceiveEmailsOnSubmitSMTA2,
        },



        new()
        {
            Id = CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApproval,
        },


        new()
        {
            Id = CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalReject,
            Description = PermissionNames.CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalReject,
        },



        new()
        {
            Id = CanReceiveEmailsOnSMTA2BHFShipmentDocumentsApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSMTA2BHFShipmentDocumentsApproval,
            Description = PermissionNames.CanReceiveEmailsOnSMTA2BHFShipmentDocumentsApproval,
        },


        new()
        {
            Id = CanReceiveEmailsOnWaitForSMTA2PickUpCompletedApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForSMTA2PickUpCompletedApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForSMTA2PickUpCompletedApproval,
        },


        new()
        {
            Id = CanReceiveEmailsOnWaitForSMTA2PickUpCompletedRejectPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForSMTA2PickUpCompletedReject,
            Description = PermissionNames.CanReceiveEmailsOnWaitForSMTA2PickUpCompletedReject,
        },



        new()
        {
            Id = CanReceiveEmailsOnSMTA2QEShipmentDocumentsApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSMTA2QEShipmentDocumentsApproval,
            Description = PermissionNames.CanReceiveEmailsOnSMTA2QEShipmentDocumentsApproval,
        },


        new()
        {
            Id = CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApproval,
        },


        new()
        {
            Id = CanReceiveEmailsOnWaitForSMTA2FinalApprovalApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnWaitForSMTA2FinalApprovalApproval,
            Description = PermissionNames.CanReceiveEmailsOnWaitForSMTA2FinalApprovalApproval,
        },

        new()
        {
            Id = CanReadSMTA1WorkflowCompletePermissionId,
            Name = PermissionNames.CanReadSMTA1WorkflowComplete,
            Description = PermissionNames.CanReadSMTA1WorkflowComplete,
        },

        new()
        {
            Id = CanDownloadFileSMTA1WorkflowCompletePermissionId,
            Name = PermissionNames.CanDownloadFileSMTA1WorkflowComplete,
            Description = PermissionNames.CanDownloadFileSMTA1WorkflowComplete,
        },

        new()
        {
            Id = CanSubmitSMTA1WorkflowCompletePermissionId,
            Name = PermissionNames.CanSubmitSMTA1WorkflowComplete,
            Description = PermissionNames.CanSubmitSMTA1WorkflowComplete,
        },

        new()
        {
            Id = CanReceiveEmailsOnSMTA1WorkflowCompleteApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSMTA1WorkflowCompleteApproval,
            Description = PermissionNames.CanReceiveEmailsOnSMTA1WorkflowCompleteApproval,
        },

        new()
        {
            Id = CanReadSMTA2WorkflowCompletePermissionId,
            Name = PermissionNames.CanReadSMTA2WorkflowComplete,
            Description = PermissionNames.CanReadSMTA2WorkflowComplete,
        },

        new()
        {
            Id = CanDownloadFileSMTA2WorkflowCompletePermissionId,
            Name = PermissionNames.CanDownloadFileSMTA2WorkflowComplete,
            Description = PermissionNames.CanDownloadFileSMTA2WorkflowComplete,
        },

        new()
        {
            Id = CanSubmitSMTA2WorkflowCompletePermissionId,
            Name = PermissionNames.CanSubmitSMTA2WorkflowComplete,
            Description = PermissionNames.CanSubmitSMTA2WorkflowComplete,
        },

        new()
        {
            Id = CanReceiveEmailsOnSMTA2WorkflowCompleteApprovalPermissionId,
            Name = PermissionNames.CanReceiveEmailsOnSMTA2WorkflowCompleteApproval,
            Description = PermissionNames.CanReceiveEmailsOnSMTA2WorkflowCompleteApproval,
        },

        new()
        {
            Id = CanAccessSMTAWorkflowPermissionId,
            Name = PermissionNames.CanAccessSMTAWorkflow,
            Description = PermissionNames.CanAccessSMTAWorkflow,
        },


        new()
        {
            Id = CanAddMaterialNewVialsPermissionId,
            Name = PermissionNames.CanAddMaterialNewVials,
            Description = PermissionNames.CanAddMaterialNewVials,
        },

        new()
        {
            Id = CanEditMaterialOwnerBioHubFacilityPermissionId,
            Name = PermissionNames.CanEditMaterialOwnerBioHubFacility,
            Description = PermissionNames.CanEditMaterialOwnerBioHubFacility,
        },

        new()
        {
            Id = CanEditMaterialShipmentNumberOfVialsPermissionId,
            Name = PermissionNames.CanEditMaterialShipmentNumberOfVials,
            Description = PermissionNames.CanEditMaterialShipmentNumberOfVials,
        },

        new()
        {
            Id = CanEditMaterialWarningEmailCurrentNumberOfVialsThresholdPermissionId,
            Name = PermissionNames.CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold,
            Description = PermissionNames.CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold,
        },


        new()
        {
            Id = CanCreateResourcePermissionId,
            Name = PermissionNames.CanCreateResource,
            Description = PermissionNames.CanCreateResource,
        },

        new()
        {
            Id = CanEditResourcePermissionId,
            Name = PermissionNames.CanEditResource,
            Description = PermissionNames.CanEditResource,
        },

        new()
        {
            Id = CanReadResourcePermissionId,
            Name = PermissionNames.CanReadResource,
            Description = PermissionNames.CanReadResource,
        },

        new()
        {
            Id = CanDeleteResourcePermissionId,
            Name = PermissionNames.CanDeleteResource,
            Description = PermissionNames.CanDeleteResource,
        },


        new()
        {
            Id = CanCreateSpecimenTypePermissionId,
            Name = PermissionNames.CanCreateSpecimenType,
            Description = PermissionNames.CanCreateSpecimenType,
        },

        new()
        {
            Id = CanEditSpecimenTypePermissionId,
            Name = PermissionNames.CanEditSpecimenType,
            Description = PermissionNames.CanEditSpecimenType,
        },

        new()
        {
            Id = CanReadSpecimenTypePermissionId,
            Name = PermissionNames.CanReadSpecimenType,
            Description = PermissionNames.CanReadSpecimenType,
        },

        new()
        {
            Id = CanDeleteSpecimenTypePermissionId,
            Name = PermissionNames.CanDeleteSpecimenType,
            Description = PermissionNames.CanDeleteSpecimenType,
        },



        ////
        ///

        		new()
        {
            Id = CanReadSubmitSMTA1PastPermissionId,
            Name = PermissionNames.CanReadSubmitSMTA1Past,
            Description = PermissionNames.CanReadSubmitSMTA1Past,
        },

        new()
        {
            Id = CanReadWaitingForSMTA1SECsApprovalPastPermissionId,
            Name = PermissionNames.CanReadWaitingForSMTA1SECsApprovalPast,
            Description = PermissionNames.CanReadWaitingForSMTA1SECsApprovalPast,
        },

        new()
        {
            Id = CanReadSMTA1WorkflowCompletePastPermissionId,
            Name = PermissionNames.CanReadSMTA1WorkflowCompletePast,
            Description = PermissionNames.CanReadSMTA1WorkflowCompletePast,
        },

        new()
        {
            Id = CanSubmitSMTA1PastPermissionId,
            Name = PermissionNames.CanSubmitSMTA1Past,
            Description = PermissionNames.CanSubmitSMTA1Past,
        },

        new()
        {
            Id = CanSubmitWaitingForSMTA1SECsApprovalPastPermissionId,
            Name = PermissionNames.CanSubmitWaitingForSMTA1SECsApprovalPast,
            Description = PermissionNames.CanSubmitWaitingForSMTA1SECsApprovalPast,
        },

        new()
        {
            Id = CanSubmitSMTA1WorkflowCompletePastPermissionId,
            Name = PermissionNames.CanSubmitSMTA1WorkflowCompletePast,
            Description = PermissionNames.CanSubmitSMTA1WorkflowCompletePast,
        },

        new()
        {
            Id = CanDownloadFileSubmitSMTA1PastPermissionId,
            Name = PermissionNames.CanDownloadFileSubmitSMTA1Past,
            Description = PermissionNames.CanDownloadFileSubmitSMTA1Past,
        },

        new()
        {
            Id = CanDownloadFileWaitingForSMTA1SECsApprovalPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitingForSMTA1SECsApprovalPast,
            Description = PermissionNames.CanDownloadFileWaitingForSMTA1SECsApprovalPast,
        },

        new()
        {
            Id = CanDownloadFileSMTA1WorkflowCompletePastPermissionId,
            Name = PermissionNames.CanDownloadFileSMTA1WorkflowCompletePast,
            Description = PermissionNames.CanDownloadFileSMTA1WorkflowCompletePast,
        },

        new()
        {
            Id = CanReadSubmitAnnex2OfSMTA1PastPermissionId,
            Name = PermissionNames.CanReadSubmitAnnex2OfSMTA1Past,
            Description = PermissionNames.CanReadSubmitAnnex2OfSMTA1Past,
        },

        new()
        {
            Id = CanReadWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId,
            Name = PermissionNames.CanReadWaitingForAnnex2OfSMTA1SECsApprovalPast,
            Description = PermissionNames.CanReadWaitingForAnnex2OfSMTA1SECsApprovalPast,
        },

        new()
        {
            Id = CanReadSubmitBookingFormOfSMTA1PastPermissionId,
            Name = PermissionNames.CanReadSubmitBookingFormOfSMTA1Past,
            Description = PermissionNames.CanReadSubmitBookingFormOfSMTA1Past,
        },

        new()
        {
            Id = CanReadWaitForBookingFormSMTA1OPSApprovalPastPermissionId,
            Name = PermissionNames.CanReadWaitForBookingFormSMTA1OPSApprovalPast,
            Description = PermissionNames.CanReadWaitForBookingFormSMTA1OPSApprovalPast,
        },

        new()
        {
            Id = CanReadSMTA1ShipmentDocumentsPastPermissionId,
            Name = PermissionNames.CanReadSMTA1ShipmentDocumentsPast,
            Description = PermissionNames.CanReadSMTA1ShipmentDocumentsPast,
        },

        new()
        {
            Id = CanReadWaitForPickUpCompletedPastPermissionId,
            Name = PermissionNames.CanReadWaitForPickUpCompletedPast,
            Description = PermissionNames.CanReadWaitForPickUpCompletedPast,
        },

        new()
        {
            Id = CanReadWaitForDeliveryCompletedPastPermissionId,
            Name = PermissionNames.CanReadWaitForDeliveryCompletedPast,
            Description = PermissionNames.CanReadWaitForDeliveryCompletedPast,
        },

        new()
        {
            Id = CanReadWaitForArrivalConditionCheckPastPermissionId,
            Name = PermissionNames.CanReadWaitForArrivalConditionCheckPast,
            Description = PermissionNames.CanReadWaitForArrivalConditionCheckPast,
        },

        new()
        {
            Id = CanReadWaitForCommentBHFSendFeedbackPastPermissionId,
            Name = PermissionNames.CanReadWaitForCommentBHFSendFeedbackPast,
            Description = PermissionNames.CanReadWaitForCommentBHFSendFeedbackPast,
        },

        new()
        {
            Id = CanReadWaitForFinalApprovalPastPermissionId,
            Name = PermissionNames.CanReadWaitForFinalApprovalPast,
            Description = PermissionNames.CanReadWaitForFinalApprovalPast,
        },

        new()
        {
            Id = CanReadShipmentCompletedPastPermissionId,
            Name = PermissionNames.CanReadShipmentCompletedPast,
            Description = PermissionNames.CanReadShipmentCompletedPast,
        },

        new()
        {
            Id = CanSubmitAnnex2OfSMTA1PastPermissionId,
            Name = PermissionNames.CanSubmitAnnex2OfSMTA1Past,
            Description = PermissionNames.CanSubmitAnnex2OfSMTA1Past,
        },

        new()
        {
            Id = CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId,
            Name = PermissionNames.CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPast,
            Description = PermissionNames.CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPast,
        },

        new()
        {
            Id = CanSubmitBookingFormOfSMTA1PastPermissionId,
            Name = PermissionNames.CanSubmitBookingFormOfSMTA1Past,
            Description = PermissionNames.CanSubmitBookingFormOfSMTA1Past,
        },

        new()
        {
            Id = CanSubmitWaitForBookingFormSMTA1OPSApprovalPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForBookingFormSMTA1OPSApprovalPast,
            Description = PermissionNames.CanSubmitWaitForBookingFormSMTA1OPSApprovalPast,
        },

        new()
        {
            Id = CanSubmitSMTA1ShipmentDocumentsPastPermissionId,
            Name = PermissionNames.CanSubmitSMTA1ShipmentDocumentsPast,
            Description = PermissionNames.CanSubmitSMTA1ShipmentDocumentsPast,
        },

        new()
        {
            Id = CanSubmitWaitForPickUpCompletedPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForPickUpCompletedPast,
            Description = PermissionNames.CanSubmitWaitForPickUpCompletedPast,
        },

        new()
        {
            Id = CanSubmitWaitForDeliveryCompletedPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForDeliveryCompletedPast,
            Description = PermissionNames.CanSubmitWaitForDeliveryCompletedPast,
        },

        new()
        {
            Id = CanSubmitWaitForArrivalConditionCheckPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForArrivalConditionCheckPast,
            Description = PermissionNames.CanSubmitWaitForArrivalConditionCheckPast,
        },

        new()
        {
            Id = CanSubmitWaitForCommentBHFSendFeedbackPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForCommentBHFSendFeedbackPast,
            Description = PermissionNames.CanSubmitWaitForCommentBHFSendFeedbackPast,
        },

        new()
        {
            Id = CanSubmitWaitForFinalApprovalPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForFinalApprovalPast,
            Description = PermissionNames.CanSubmitWaitForFinalApprovalPast,
        },

        new()
        {
            Id = CanSubmitShipmentCompletedPastPermissionId,
            Name = PermissionNames.CanSubmitShipmentCompletedPast,
            Description = PermissionNames.CanSubmitShipmentCompletedPast,
        },

        new()
        {
            Id = CanDownloadFileSubmitAnnex2OfSMTA1PastPermissionId,
            Name = PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA1Past,
            Description = PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA1Past,
        },

        new()
        {
            Id = CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPast,
            Description = PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPast,
        },

        new()
        {
            Id = CanDownloadFileSubmitBookingFormOfSMTA1PastPermissionId,
            Name = PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA1Past,
            Description = PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA1Past,
        },

        new()
        {
            Id = CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPast,
            Description = PermissionNames.CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPast,
        },

        new()
        {
            Id = CanDownloadSMTA1ShipmentDocumentsPastPermissionId,
            Name = PermissionNames.CanDownloadSMTA1ShipmentDocumentsPast,
            Description = PermissionNames.CanDownloadSMTA1ShipmentDocumentsPast,
        },

        new()
        {
            Id = CanDownloadFileWaitForPickUpCompletedPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForPickUpCompletedPast,
            Description = PermissionNames.CanDownloadFileWaitForPickUpCompletedPast,
        },

        new()
        {
            Id = CanDownloadFileWaitForDeliveryCompletedPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForDeliveryCompletedPast,
            Description = PermissionNames.CanDownloadFileWaitForDeliveryCompletedPast,
        },

        new()
        {
            Id = CanDownloadFileWaitForArrivalConditionCheckPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForArrivalConditionCheckPast,
            Description = PermissionNames.CanDownloadFileWaitForArrivalConditionCheckPast,
        },

        new()
        {
            Id = CanDownloadFileWaitForCommentBHFSendFeedbackPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForCommentBHFSendFeedbackPast,
            Description = PermissionNames.CanDownloadFileWaitForCommentBHFSendFeedbackPast,
        },

        new()
        {
            Id = CanDownloadFileWaitForFinalApprovalPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForFinalApprovalPast,
            Description = PermissionNames.CanDownloadFileWaitForFinalApprovalPast,
        },

        new()
        {
            Id = CanDownloadFileShipmentCompletedPastPermissionId,
            Name = PermissionNames.CanDownloadFileShipmentCompletedPast,
            Description = PermissionNames.CanDownloadFileShipmentCompletedPast,
        },

        new()
        {
            Id = CanReadSubmitSMTA2PastPermissionId,
            Name = PermissionNames.CanReadSubmitSMTA2Past,
            Description = PermissionNames.CanReadSubmitSMTA2Past,
        },

        new()
        {
            Id = CanReadWaitingForSMTA2SECsApprovalPastPermissionId,
            Name = PermissionNames.CanReadWaitingForSMTA2SECsApprovalPast,
            Description = PermissionNames.CanReadWaitingForSMTA2SECsApprovalPast,
        },

        new()
        {
            Id = CanReadSMTA2WorkflowCompletePastPermissionId,
            Name = PermissionNames.CanReadSMTA2WorkflowCompletePast,
            Description = PermissionNames.CanReadSMTA2WorkflowCompletePast,
        },

        new()
        {
            Id = CanSubmitSMTA2PastPermissionId,
            Name = PermissionNames.CanSubmitSMTA2Past,
            Description = PermissionNames.CanSubmitSMTA2Past,
        },

        new()
        {
            Id = CanSubmitWaitingForSMTA2SECsApprovalPastPermissionId,
            Name = PermissionNames.CanSubmitWaitingForSMTA2SECsApprovalPast,
            Description = PermissionNames.CanSubmitWaitingForSMTA2SECsApprovalPast,
        },

        new()
        {
            Id = CanSubmitSMTA2WorkflowCompletePastPermissionId,
            Name = PermissionNames.CanSubmitSMTA2WorkflowCompletePast,
            Description = PermissionNames.CanSubmitSMTA2WorkflowCompletePast,
        },

        new()
        {
            Id = CanDownloadFileSubmitSMTA2PastPermissionId,
            Name = PermissionNames.CanDownloadFileSubmitSMTA2Past,
            Description = PermissionNames.CanDownloadFileSubmitSMTA2Past,
        },

        new()
        {
            Id = CanDownloadFileWaitingForSMTA2SECsApprovalPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitingForSMTA2SECsApprovalPast,
            Description = PermissionNames.CanDownloadFileWaitingForSMTA2SECsApprovalPast,
        },

        new()
        {
            Id = CanDownloadFileSMTA2WorkflowCompletePastPermissionId,
            Name = PermissionNames.CanDownloadFileSMTA2WorkflowCompletePast,
            Description = PermissionNames.CanDownloadFileSMTA2WorkflowCompletePast,
        },

        new()
        {
            Id = CanReadSubmitAnnex2OfSMTA2PastPermissionId,
            Name = PermissionNames.CanReadSubmitAnnex2OfSMTA2Past,
            Description = PermissionNames.CanReadSubmitAnnex2OfSMTA2Past,
        },

        new()
        {
            Id = CanReadWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId,
            Name = PermissionNames.CanReadWaitingForAnnex2OfSMTA2SECsApprovalPast,
            Description = PermissionNames.CanReadWaitingForAnnex2OfSMTA2SECsApprovalPast,
        },

        new()
        {
            Id = CanReadSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId,
            Name = PermissionNames.CanReadSubmitBiosafetyChecklistFormOfSMTA2Past,
            Description = PermissionNames.CanReadSubmitBiosafetyChecklistFormOfSMTA2Past,
        },

        new()
        {
            Id = CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId,
            Name = PermissionNames.CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast,
            Description = PermissionNames.CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast,
        },

        new()
        {
            Id = CanReadSubmitBookingFormOfSMTA2PastPermissionId,
            Name = PermissionNames.CanReadSubmitBookingFormOfSMTA2Past,
            Description = PermissionNames.CanReadSubmitBookingFormOfSMTA2Past,
        },

        new()
        {
            Id = CanReadWaitForBookingFormSMTA2OPSsApprovalPastPermissionId,
            Name = PermissionNames.CanReadWaitForBookingFormSMTA2OPSsApprovalPast,
            Description = PermissionNames.CanReadWaitForBookingFormSMTA2OPSsApprovalPast,
        },

        new()
        {
            Id = CanReadBHFSMTA2ShipmentDocumentsPastPermissionId,
            Name = PermissionNames.CanReadBHFSMTA2ShipmentDocumentsPast,
            Description = PermissionNames.CanReadBHFSMTA2ShipmentDocumentsPast,
        },

        new()
        {
            Id = CanReadQESMTA2ShipmentDocumentsPastPermissionId,
            Name = PermissionNames.CanReadQESMTA2ShipmentDocumentsPast,
            Description = PermissionNames.CanReadQESMTA2ShipmentDocumentsPast,
        },

        new()
        {
            Id = CanReadWaitForPickUpFromBioHubCompletedPastPermissionId,
            Name = PermissionNames.CanReadWaitForPickUpFromBioHubCompletedPast,
            Description = PermissionNames.CanReadWaitForPickUpFromBioHubCompletedPast,
        },

        new()
        {
            Id = CanReadWaitForDeliveryFromBioHubCompletedPastPermissionId,
            Name = PermissionNames.CanReadWaitForDeliveryFromBioHubCompletedPast,
            Description = PermissionNames.CanReadWaitForDeliveryFromBioHubCompletedPast,
        },

        new()
        {
            Id = CanReadWaitForArrivalConditionFromBioHubCheckPastPermissionId,
            Name = PermissionNames.CanReadWaitForArrivalConditionFromBioHubCheckPast,
            Description = PermissionNames.CanReadWaitForArrivalConditionFromBioHubCheckPast,
        },

        new()
        {
            Id = CanReadWaitForCommentQESendFeedbackPastPermissionId,
            Name = PermissionNames.CanReadWaitForCommentQESendFeedbackPast,
            Description = PermissionNames.CanReadWaitForCommentQESendFeedbackPast,
        },

        new()
        {
            Id = CanReadWaitForFinalApprovalFromBioHubPastPermissionId,
            Name = PermissionNames.CanReadWaitForFinalApprovalFromBioHubPast,
            Description = PermissionNames.CanReadWaitForFinalApprovalFromBioHubPast,
        },

        new()
        {
            Id = CanReadShipmentFromBioHubCompletedPastPermissionId,
            Name = PermissionNames.CanReadShipmentFromBioHubCompletedPast,
            Description = PermissionNames.CanReadShipmentFromBioHubCompletedPast,
        },

        new()
        {
            Id = CanSubmitAnnex2OfSMTA2PastPermissionId,
            Name = PermissionNames.CanSubmitAnnex2OfSMTA2Past,
            Description = PermissionNames.CanSubmitAnnex2OfSMTA2Past,
        },

        new()
        {
            Id = CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId,
            Name = PermissionNames.CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPast,
            Description = PermissionNames.CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPast,
        },

        new()
        {
            Id = CanSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId,
            Name = PermissionNames.CanSubmitBiosafetyChecklistFormOfSMTA2Past,
            Description = PermissionNames.CanSubmitBiosafetyChecklistFormOfSMTA2Past,
        },

        new()
        {
            Id = CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast,
            Description = PermissionNames.CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast,
        },

        new()
        {
            Id = CanSubmitBookingFormOfSMTA2PastPermissionId,
            Name = PermissionNames.CanSubmitBookingFormOfSMTA2Past,
            Description = PermissionNames.CanSubmitBookingFormOfSMTA2Past,
        },

        new()
        {
            Id = CanSubmitWaitForBookingFormSMTA2OPSsApprovalPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForBookingFormSMTA2OPSsApprovalPast,
            Description = PermissionNames.CanSubmitWaitForBookingFormSMTA2OPSsApprovalPast,
        },

        new()
        {
            Id = CanSubmitBHFSMTA2ShipmentDocumentsPastPermissionId,
            Name = PermissionNames.CanSubmitBHFSMTA2ShipmentDocumentsPast,
            Description = PermissionNames.CanSubmitBHFSMTA2ShipmentDocumentsPast,
        },

        new()
        {
            Id = CanSubmitQESMTA2ShipmentDocumentsPastPermissionId,
            Name = PermissionNames.CanSubmitQESMTA2ShipmentDocumentsPast,
            Description = PermissionNames.CanSubmitQESMTA2ShipmentDocumentsPast,
        },

        new()
        {
            Id = CanSubmitWaitForPickUpFromBioHubCompletedPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForPickUpFromBioHubCompletedPast,
            Description = PermissionNames.CanSubmitWaitForPickUpFromBioHubCompletedPast,
        },

        new()
        {
            Id = CanSubmitWaitForDeliveryFromBioHubCompletedPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForDeliveryFromBioHubCompletedPast,
            Description = PermissionNames.CanSubmitWaitForDeliveryFromBioHubCompletedPast,
        },

        new()
        {
            Id = CanSubmitWaitForArrivalConditionFromBioHubCheckPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForArrivalConditionFromBioHubCheckPast,
            Description = PermissionNames.CanSubmitWaitForArrivalConditionFromBioHubCheckPast,
        },

        new()
        {
            Id = CanSubmitWaitForCommentQESendFeedbackPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForCommentQESendFeedbackPast,
            Description = PermissionNames.CanSubmitWaitForCommentQESendFeedbackPast,
        },

        new()
        {
            Id = CanSubmitWaitForFinalApprovalFromBioHubPastPermissionId,
            Name = PermissionNames.CanSubmitWaitForFinalApprovalFromBioHubPast,
            Description = PermissionNames.CanSubmitWaitForFinalApprovalFromBioHubPast,
        },

        new()
        {
            Id = CanSubmitShipmentFromBioHubCompletedPastPermissionId,
            Name = PermissionNames.CanSubmitShipmentFromBioHubCompletedPast,
            Description = PermissionNames.CanSubmitShipmentFromBioHubCompletedPast,
        },

        new()
        {
            Id = CanDownloadFileSubmitAnnex2OfSMTA2PastPermissionId,
            Name = PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA2Past,
            Description = PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA2Past,
        },

        new()
        {
            Id = CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPast,
            Description = PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPast,
        },

        new()
        {
            Id = CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId,
            Name = PermissionNames.CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2Past,
            Description = PermissionNames.CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2Past,
        },

        new()
        {
            Id = CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast,
            Description = PermissionNames.CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast,
        },

        new()
        {
            Id = CanDownloadFileSubmitBookingFormOfSMTA2PastPermissionId,
            Name = PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA2Past,
            Description = PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA2Past,
        },

        new()
        {
            Id = CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPast,
            Description = PermissionNames.CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPast,
        },

        new()
        {
            Id = CanDownloadBHFSMTA2ShipmentDocumentsPastPermissionId,
            Name = PermissionNames.CanDownloadBHFSMTA2ShipmentDocumentsPast,
            Description = PermissionNames.CanDownloadBHFSMTA2ShipmentDocumentsPast,
        },

        new()
        {
            Id = CanDownloadQESMTA2ShipmentDocumentsPastPermissionId,
            Name = PermissionNames.CanDownloadQESMTA2ShipmentDocumentsPast,
            Description = PermissionNames.CanDownloadQESMTA2ShipmentDocumentsPast,
        },

        new()
        {
            Id = CanDownloadFileWaitForPickUpFromBioHubCompletedPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForPickUpFromBioHubCompletedPast,
            Description = PermissionNames.CanDownloadFileWaitForPickUpFromBioHubCompletedPast,
        },

        new()
        {
            Id = CanDownloadFileWaitForDeliveryFromBioHubCompletedPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForDeliveryFromBioHubCompletedPast,
            Description = PermissionNames.CanDownloadFileWaitForDeliveryFromBioHubCompletedPast,
        },

        new()
        {
            Id = CanDownloadFileWaitForArrivalConditionFromBioHubCheckPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForArrivalConditionFromBioHubCheckPast,
            Description = PermissionNames.CanDownloadFileWaitForArrivalConditionFromBioHubCheckPast,
        },

        new()
        {
            Id = CanDownloadFileWaitForCommentQESendFeedbackPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForCommentQESendFeedbackPast,
            Description = PermissionNames.CanDownloadFileWaitForCommentQESendFeedbackPast,
        },

        new()
        {
            Id = CanDownloadFileWaitForFinalApprovalFromBioHubPastPermissionId,
            Name = PermissionNames.CanDownloadFileWaitForFinalApprovalFromBioHubPast,
            Description = PermissionNames.CanDownloadFileWaitForFinalApprovalFromBioHubPast,
        },

        new()
        {
            Id = CanDownloadFileShipmentFromBioHubCompletedPastPermissionId,
            Name = PermissionNames.CanDownloadFileShipmentFromBioHubCompletedPast,
            Description = PermissionNames.CanDownloadFileShipmentFromBioHubCompletedPast,
        },


        new()
        {
            Id = CanAccessPastRequestIniziationPermissionId,
            Name = PermissionNames.CanAccessPastRequestIniziation,
            Description = PermissionNames.CanAccessPastRequestIniziation,
        },

        new()
        {
            Id = CanAccessPastWorklistPermissionId,
            Name = PermissionNames.CanAccessPastWorklist,
            Description = PermissionNames.CanAccessPastWorklist,
        },


        new()
        {
            Id = CanAccessPastSMTAWorkflowPermissionId,
            Name = PermissionNames.CanAccessPastSMTAWorkflow,
            Description = PermissionNames.CanAccessPastSMTAWorkflow,
        },

        new()
        {
            Id = CanReadOnBehalfOfRolesPermissionId,
            Name = PermissionNames.CanReadOnBehalfOfRoles,
            Description = PermissionNames.CanReadOnBehalfOfRoles,
        },

        new()
        {
            Id = CanEditMaterialShipmentInformationPermissionId,
            Name = PermissionNames.CanEditMaterialShipmentInformation,
            Description = PermissionNames.CanEditMaterialShipmentInformation,
        },

        new()
        {
            Id = CanReadEFormPermissionId,
            Name = PermissionNames.CanReadEForm,
            Description = PermissionNames.CanReadEForm,
        },

    };



    public static Guid CanReadBioHubFacilityPermissionId => Guid.Parse("11e30c51-c066-45aa-a077-24e4e79011af");
    public static Guid CanReadBSLLevelPermissionId => Guid.Parse("5cca86d7-8fa8-4cad-9c41-f70bb3ea85b4");
    public static Guid CanReadCountryPermissionId => Guid.Parse("fe52ac17-9dfa-4f05-b0a5-89c3256112db");
    public static Guid CanReadCultivabilityTypePermissionId => Guid.Parse("8985360d-fcef-4c4d-939f-abc242360267");
    public static Guid CanReadGeneticSequenceDataPermissionId => Guid.Parse("e61e5d97-729d-4c62-a5b4-bdd17acea1b0");
    public static Guid CanReadInternationalTaxonomyClassificationPermissionId => Guid.Parse("d05b4900-40fd-482b-97ed-f1a07edb66a3");
    public static Guid CanReadIsolationHostTypePermissionId => Guid.Parse("c26acc95-dbf0-4c98-9c55-af5f099a8c63");
    public static Guid CanReadIsolationTechniqueTypePermissionId => Guid.Parse("29b24350-f0fd-48a7-8fcd-80c89f9d1706");
    public static Guid CanReadLaboratoryPermissionId => Guid.Parse("187f3c4d-30e5-4f04-8710-71e991cdca57");
    public static Guid CanReadMaterialProductPermissionId => Guid.Parse("4254bfb6-b814-4198-9ab1-d840dbee4464");
    public static Guid CanReadMaterialPermissionId => Guid.Parse("e9b669f1-6cce-49ba-88f0-f532dab70254");
    public static Guid CanReadMaterialTypePermissionId => Guid.Parse("9cbebc10-bd62-45c3-b010-8b35e8047a41");
    public static Guid CanReadMaterialUsagePermissionId => Guid.Parse("5eea9279-4de2-4d95-a1e5-d33bbaad28fb");
    public static Guid CanReadPriorityRequestTypePermissionId => Guid.Parse("277bbc56-71a7-4380-9a59-efbb02556525");
    public static Guid CanReadRolePermissionId => Guid.Parse("f07ff21c-1c3c-4128-879a-3d450be2c86e");
    public static Guid CanReadShipmentPermissionId => Guid.Parse("953ed9c2-29e1-454f-9318-05fb39b6894d");
    public static Guid CanReadTemperatureUnitOfMeasurePermissionId => Guid.Parse("e8a6ba9e-d082-4f32-8ee9-a39646ae59eb");
    public static Guid CanReadTransportCategoryPermissionId => Guid.Parse("17874231-0dbe-45fb-b047-cc57251a3544");
    public static Guid CanReadTransportModePermissionId => Guid.Parse("9836f735-8638-4738-ae84-572546416bba");
    public static Guid CanReadUserRequestPermissionId => Guid.Parse("1783d9a7-8e56-49aa-9eb6-1b43bd2ab3aa");
    public static Guid CanReadUserRequestStatusPermissionId => Guid.Parse("993a4c1b-670c-423c-a6a9-a710a097be8d");
    public static Guid CanReadUserPermissionId => Guid.Parse("c76b3835-7479-467f-87f9-530e256951b7");

    public static Guid CanEditBioHubFacilityPermissionId => Guid.Parse("1b08ceb6-cdbb-40db-b814-f9fb2e44244f");
    public static Guid CanEditBSLLevelPermissionId => Guid.Parse("0472b1b0-2ee0-46ff-8fe3-5bad9905c36e");
    public static Guid CanEditCountryPermissionId => Guid.Parse("30f04612-fd75-43f8-872a-21249dc590a5");
    public static Guid CanEditCultivabilityTypePermissionId => Guid.Parse("3dda7a99-3b79-41a4-8414-f08d4fac84dd");
    public static Guid CanEditGeneticSequenceDataPermissionId => Guid.Parse("963decc8-91aa-48a8-8279-9beb3392103f");
    public static Guid CanEditInternationalTaxonomyClassificationPermissionId => Guid.Parse("c0448d2c-5956-4e7d-9cc4-7dce08641fe2");
    public static Guid CanEditIsolationHostTypePermissionId => Guid.Parse("6c150bbb-e48d-435b-922a-846d2f91351c");
    public static Guid CanEditIsolationTechniqueTypePermissionId => Guid.Parse("1389dac2-af92-44b7-b83b-20fe4224eb67");
    public static Guid CanEditLaboratoryPermissionId => Guid.Parse("1948ee06-2011-4665-923a-f8178d61fc8b");
    public static Guid CanEditMaterialProductPermissionId => Guid.Parse("d1cb3b25-e4db-499b-9a2c-2c5d2c4eb6cc");
    public static Guid CanEditMaterialPermissionId => Guid.Parse("54ed562c-2456-4ac9-9c42-ee677ac1ffae");
    public static Guid CanEditMaterialTypePermissionId => Guid.Parse("778af743-ad8b-4bb8-bd6d-f2bbe682727e");
    public static Guid CanEditMaterialUsagePermissionId => Guid.Parse("87bb8f0b-ee4e-4096-b8ae-26a58deea1e7");
    public static Guid CanEditPriorityRequestTypePermissionId => Guid.Parse("19f41a7c-020f-41e5-bd2b-0e16a2d55e3d");
    public static Guid CanEditRolePermissionId => Guid.Parse("d79c032e-7784-4685-a202-1483ccf7f751");
    public static Guid CanEditShipmentPermissionId => Guid.Parse("8df444b7-2970-40c7-b597-c22959308953");
    public static Guid CanEditTemperatureUnitOfMeasurePermissionId => Guid.Parse("4ca3c38f-b172-4e10-a87c-1e4bbc4b3ab1");
    public static Guid CanEditTransportCategoryPermissionId => Guid.Parse("ef1be8b1-c60c-4398-8db9-b71e6eef903d");
    public static Guid CanEditTransportModePermissionId => Guid.Parse("8b937b06-7d54-446f-93f4-505610bab4bb");
    public static Guid CanEditUserRequestPermissionId => Guid.Parse("88063ffb-4353-406a-8973-7df688ba76c6");
    public static Guid CanEditUserRequestStatusPermissionId => Guid.Parse("1095caa9-b9d2-4e5e-a0ce-9f377f1215da");
    public static Guid CanEditUserPermissionId => Guid.Parse("76135bc2-4e46-4769-8906-4763e7a9ed83");

    public static Guid CanDeleteBioHubFacilityPermissionId => Guid.Parse("d4739c03-e748-41f6-a4f9-017f5ebe49e9");
    public static Guid CanDeleteBSLLevelPermissionId => Guid.Parse("b2b9a004-e468-491c-928d-880464ad3f56");
    public static Guid CanDeleteCountryPermissionId => Guid.Parse("91592383-7c17-4ffe-9fce-883a054d9085");
    public static Guid CanDeleteCultivabilityTypePermissionId => Guid.Parse("55b0f4fe-feab-43eb-acf4-33489bbdcd94");
    public static Guid CanDeleteGeneticSequenceDataPermissionId => Guid.Parse("990e1e57-618f-4f5d-99cc-048bbe9d23f3");
    public static Guid CanDeleteInternationalTaxonomyClassificationPermissionId => Guid.Parse("ddf9dbc6-832e-4660-83f7-79f462881c71");
    public static Guid CanDeleteIsolationHostTypePermissionId => Guid.Parse("4fc6ba99-2392-4d31-aa4c-7f6bbc8968c5");
    public static Guid CanDeleteIsolationTechniqueTypePermissionId => Guid.Parse("1bb4e182-2c2c-467d-90a3-06ad88e1eb25");
    public static Guid CanDeleteLaboratoryPermissionId => Guid.Parse("d902c779-66ba-4868-9516-7d7452081507");
    public static Guid CanDeleteMaterialProductPermissionId => Guid.Parse("577417cf-6acf-4fc8-83f0-f5a2220f7f9f");
    public static Guid CanDeleteMaterialPermissionId => Guid.Parse("0261c588-770b-4885-95f5-b497f434c450");
    public static Guid CanDeleteMaterialTypePermissionId => Guid.Parse("daa3f717-7e29-42c9-b78d-7b498779d5f6");
    public static Guid CanDeleteMaterialUsagePermissionId => Guid.Parse("f401010a-c1d4-4a7c-8977-f03a04aebb0e");
    public static Guid CanDeletePriorityRequestTypePermissionId => Guid.Parse("e3cf3518-f124-455d-b751-558168efae42");
    public static Guid CanDeleteRolePermissionId => Guid.Parse("25359a80-b532-49e8-af60-65ff7740dc77");
    public static Guid CanDeleteShipmentPermissionId => Guid.Parse("4cbbb117-650b-47a4-9156-63291234992b");
    public static Guid CanDeleteTemperatureUnitOfMeasurePermissionId => Guid.Parse("89923505-e939-4bc4-86a8-3dd9f58cc5bb");
    public static Guid CanDeleteTransportCategoryPermissionId => Guid.Parse("afe518d1-5b4a-4e62-976d-ede211dd0a5a");
    public static Guid CanDeleteTransportModePermissionId => Guid.Parse("753cb6f1-7ca5-446b-925d-692cf6da758e");
    public static Guid CanDeleteUserRequestPermissionId => Guid.Parse("0b55ae24-bb0f-4eec-aa26-ce4b62588edc");
    public static Guid CanDeleteUserRequestStatusPermissionId => Guid.Parse("5b71c016-4f39-4761-9fc7-1fbd0a1d40d2");
    public static Guid CanDeleteUserPermissionId => Guid.Parse("10f36a64-f785-4a53-aa2b-9458f14458b0");

    public static Guid CanCreateBioHubFacilityPermissionId => Guid.Parse("a764d831-4ec0-4a0c-b76c-7ff246f93567");
    public static Guid CanCreateBSLLevelPermissionId => Guid.Parse("7b6820e7-f361-43fa-9860-bad7554ee27e");
    public static Guid CanCreateCountryPermissionId => Guid.Parse("1f9db87a-5fa5-4cce-a2cd-65afee30a0ad");
    public static Guid CanCreateCultivabilityTypePermissionId => Guid.Parse("3245632f-10a4-4091-88e2-cfacf8f1f12b");
    public static Guid CanCreateGeneticSequenceDataPermissionId => Guid.Parse("1881cea0-f104-4d59-b315-60be05da5781");
    public static Guid CanCreateInternationalTaxonomyClassificationPermissionId => Guid.Parse("22d6c094-ed28-4c2a-b8b6-6b1617ed03a5");
    public static Guid CanCreateIsolationHostTypePermissionId => Guid.Parse("6fe3e2bd-8d64-45ee-a05d-2aaebff4843a");
    public static Guid CanCreateIsolationTechniqueTypePermissionId => Guid.Parse("a2f956de-b57a-46ac-8313-6500f16deeb8");
    public static Guid CanCreateLaboratoryPermissionId => Guid.Parse("5847e5cb-b174-422b-8a30-0066e980a65f");
    public static Guid CanCreateMaterialProductPermissionId => Guid.Parse("cb7f4cbe-93f4-46ec-8906-5f42e19b5fa9");
    public static Guid CanCreateMaterialPermissionId => Guid.Parse("1a9c0c6a-2900-41f8-8148-e25db7d928c7");
    public static Guid CanCreateMaterialTypePermissionId => Guid.Parse("32ad101c-8209-49ec-b2ac-bbc25661513d");
    public static Guid CanCreateMaterialUsagePermissionId => Guid.Parse("d762170b-34aa-459f-b252-e9f2743be29b");
    public static Guid CanCreatePriorityRequestTypePermissionId => Guid.Parse("7296afa9-4115-4323-aa3d-11a6133adab4");
    public static Guid CanCreateRolePermissionId => Guid.Parse("038456a1-7287-47f3-bdc6-3a3d87eac924");
    public static Guid CanCreateShipmentPermissionId => Guid.Parse("6f7586db-3de8-472b-81f8-08757b229f83");
    public static Guid CanCreateTemperatureUnitOfMeasurePermissionId => Guid.Parse("a2bb422a-174f-42b6-897f-bf976d915e21");
    public static Guid CanCreateTransportCategoryPermissionId => Guid.Parse("4bd0db9e-4b82-4ba4-b420-7545b7cd58e7");
    public static Guid CanCreateTransportModePermissionId => Guid.Parse("2883f5c7-5d1d-48cd-8fd6-8842a559efee");
    public static Guid CanCreateUserRequestPermissionId => Guid.Parse("dac1aa46-71d0-4472-b320-115cd23cb919");
    public static Guid CanCreateUserRequestStatusPermissionId => Guid.Parse("d4ee406f-8383-48a7-9b46-927734a7603c");
    public static Guid CanCreateUserPermissionId => Guid.Parse("9266b91e-befd-4287-afb0-d98af911470a");
    public static Guid CanAccessWHODashboardPermissionId => Guid.Parse("552abb36-289b-4d79-9553-77426beecc29");
    public static Guid CanAccessWHOBMEPPPermissionId => Guid.Parse("cc197e70-690b-4b8f-be8c-05be2fea3100");
    public static Guid CanAccessWHOBioHubFacilitiesPermissionId => Guid.Parse("7c86e326-53ca-46be-baf5-b818c8575efa");
    public static Guid CanAccessWHOLaboratoriesPermissionId => Guid.Parse("35274b86-3247-4ea5-90e4-f86565ae8006");
    public static Guid CanAccessWHOTemplatesPermissionId => Guid.Parse("53a32aa6-104d-460b-9842-425483121f8c");
    public static Guid CanAccessWHOShipmentsPermissionId => Guid.Parse("25996975-aa29-4a2d-88ad-f59589a99fb4");
    public static Guid CanAccessWHOPendingRequestPermissionId => Guid.Parse("71f82588-26dc-4613-9cb8-e53310f42f7b");
    public static Guid CanAccessWHOUsersPermissionId => Guid.Parse("f0f9e9f5-e898-4dea-8b47-36449b78ed3f");
    public static Guid CanAccessLaboratoryDashboardPermissionId => Guid.Parse("02f2cb3c-4a13-4e21-af68-7a68751f7745");
    public static Guid CanAccessLaboratoryUserProfilePermissionId => Guid.Parse("ca7fbb6e-9cea-40e9-9ace-d6c828d555c1");
    public static Guid CanAccessLaboratoryStaffPermissionId => Guid.Parse("06d2de0c-981b-4f60-9a9b-874168bac588");
    public static Guid CanAccessLaboratoryFacilityInstituteProfilePermissionId => Guid.Parse("e3010ca7-94f7-4e8b-9bd6-1b5b28117dbf");
    public static Guid CanAccessLaboratoryBMEPPPermissionId => Guid.Parse("d3287765-2e99-46ea-9167-247924663758");
    public static Guid CanAccessLaboratoryBMEPPCataloguePermissionId => Guid.Parse("9e1810a5-9da8-4a3e-9575-96bfbad0aa30");
    public static Guid CanAccessLaboratoryTemplatesPermissionId => Guid.Parse("f0972a43-9053-433b-8a41-a7ed43246aed");
    public static Guid CanAccessBioHubFacilityDashboardPermissionId => Guid.Parse("51522784-d1df-4038-acb3-5cfc2a3c4f79");
    public static Guid CanAccessBioHubFacilityUserProfilePermissionId => Guid.Parse("4af0f1e0-a87a-42f4-a4dc-e681008cdb17");
    public static Guid CanAccessBioHubFacilityLaboratoriesPermissionId => Guid.Parse("ef47eabd-1a90-4d09-a426-fd1ae978f3d1");
    public static Guid CanAccessBioHubFacilityBMEPPPermissionId => Guid.Parse("b13b07c8-4245-46b3-ba0c-79c795206d23");
    public static Guid CanAccessBioHubFacilityTemplatesPermissionId => Guid.Parse("035aac5d-5e0e-4131-ab94-ecd5842831f1");

    public static Guid CanEditLaboratoryStaffPermissionId => Guid.Parse("a8b02744-714a-467b-9f6a-953a1c1da747");
    public static Guid CanDeleteLaboratoryStaffPermissionId => Guid.Parse("93117425-38eb-488a-adf3-1f271369dd7f");
    public static Guid CanReadLaboratoryStaffPermissionId => Guid.Parse("117ad9be-5a1d-4f9f-bf29-763b850ed32f");

    public static Guid CanApproveOrRejectUserRequestPermissionId => Guid.Parse("2e0c76b1-a652-4a35-acde-6f29c64788c8");


    public static Guid CanReadDocumentTemplatePermissionId => Guid.Parse("e29eab1d-c1ba-4e95-a759-30084d80133b");
    public static Guid CanCreateDocumentTemplatePermissionId => Guid.Parse("2a369b41-b579-44bc-8109-a3da58751542");
    public static Guid CanEditDocumentTemplatePermissionId => Guid.Parse("b030c24f-2499-4ea8-b60a-acca363f2013");
    public static Guid CanDeleteDocumentTemplatePermissionId => Guid.Parse("d8a9ed01-ac50-4baf-be71-0cdbf7a4383d");


    public static Guid CanAccessRequestIniziationPermissionId => Guid.Parse("a634920e-025b-47f0-8795-320f494155c4");
    public static Guid CanReadSubmitSMTA1PermissionId => Guid.Parse("db04253a-f1f2-4ba1-b73a-3dbeadda0152");
    public static Guid CanDownloadFileSubmitSMTA1PermissionId => Guid.Parse("3e7bbd97-106b-4fa9-bf44-f2a1d5382276");
    public static Guid CanSubmitSMTA1PermissionId => Guid.Parse("ad6e00c5-371c-4c61-b14b-82b329a221b0");
    public static Guid CanReadWaitingForSMTA1SECsApprovalPermissionId => Guid.Parse("55897755-297c-47b8-867d-82c3903efd55");
    public static Guid CanSubmitWaitingForSMTA1SECsApprovalPermissionId => Guid.Parse("1c890b17-cde2-4f35-b434-1a2057ea3e20");
    public static Guid CanDownloadFileWaitingForSMTA1SECsApprovalPermissionId => Guid.Parse("6cc2685d-b6b4-48b0-a8bd-85ef22a441e3");
    public static Guid CanAccessWorklistPermissionId => Guid.Parse("df1cb55c-5466-49cc-ac08-89f00d948df0");
    public static Guid CanReadSubmitAnnex2OfSMTA1PermissionId => Guid.Parse("8ec327d8-de95-44e9-8015-fe1c93861b18");
    public static Guid CanSubmitAnnex2OfSMTA1PermissionId => Guid.Parse("e3503539-4979-4797-8699-61c5dde33b77");
    public static Guid CanDownloadFileSubmitAnnex2OfSMTA1PermissionId => Guid.Parse("3c041011-6e99-468f-8734-818692e6b8dd");

    public static Guid CanReceiveEmailsOnRequestInitiationPermissionId => Guid.Parse("0c8b7899-4f93-440d-a2ab-24fcf116b66c");
    public static Guid CanReceiveEmailsOnSubmitSMTA1PermissionId => Guid.Parse("e9620843-400a-4a3e-946c-66bf0074013c");
    public static Guid CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalApprovalPermissionId => Guid.Parse("99063949-3d17-4a80-bd56-6fb13dbd190c");
    public static Guid CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalRejectPermissionId => Guid.Parse("ea66634f-5a61-4939-ac03-61b0246917b7");
    public static Guid CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApprovalPermissionId => Guid.Parse("52b04bf4-df88-46dd-a752-75558b9b2026");
    public static Guid CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalRejectPermissionId => Guid.Parse("aa7b788a-7f01-4ad8-95dd-a42f704cfa2d");
    public static Guid CanReceiveEmailsOnSubmitAnnex2OfSMTA1ApprovalPermissionId => Guid.Parse("1a2e2398-4bf7-4bc0-b318-6b69682613bd");
    public static Guid CanReceiveEmailsOnSubmitAnnex2OfSMTA1RejectPermissionId => Guid.Parse("6f415ebb-2d9c-4ae2-abf9-600e3ca9914f");
    public static Guid CanReadWaitingForAnnex2OfSMTA1SECsApprovalPermissionId => Guid.Parse("e311fc05-9f13-4fb6-9767-1a2ce7793aa4");
    public static Guid CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPermissionId => Guid.Parse("6c13e77e-ac4a-4758-97b2-5a99fa797ae2");
    public static Guid CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPermissionId => Guid.Parse("4145ff0c-86c9-44c4-9321-9067a938306b");
    public static Guid CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApprovalPermissionId => Guid.Parse("c2c89138-253a-472c-9ee8-570cea7b1751");
    public static Guid CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalRejectPermissionId => Guid.Parse("cc19a174-1ffd-44fe-8786-57f3d94a3eee");
    public static Guid CanReadSubmitBookingFormOfSMTA1PermissionId => Guid.Parse("21f4a6ce-1162-4c52-9d27-978c839b7fee");
    public static Guid CanSubmitBookingFormOfSMTA1PermissionId => Guid.Parse("9e208674-a72a-4ede-9093-b077e6ccce2a");
    public static Guid CanDownloadFileSubmitBookingFormOfSMTA1PermissionId => Guid.Parse("a5ea1b03-5bd2-408f-8bbc-60553fb7ccde");
    public static Guid CanReceiveEmailsOnSubmitBookingFormOfSMTA1ApprovalPermissionId => Guid.Parse("d5b54d64-337f-4238-809f-ce47778a7123");
    public static Guid CanReceiveEmailsOnSubmitBookingFormOfSMTA1RejectPermissionId => Guid.Parse("fd65f73b-b2ce-4dbf-b293-2320978f563c");
    public static Guid CanReadWaitForBookingFormSMTA1OPSApprovalPermissionId => Guid.Parse("ae34f1e8-75d2-4b3e-b86f-519a3ee32d0c");

    public static Guid CanReadSMTA1ShipmentDocumentsPermissionId => Guid.Parse("ee30f830-d946-4de5-82b2-83b1fbcad696");

    public static Guid CanSubmitWaitForBookingFormSMTA1OPSApprovalPermissionId => Guid.Parse("610b4618-58b5-49b3-9289-98adb23edf58");

    public static Guid CanSubmitSMTA1ShipmentDocumentsPermissionId => Guid.Parse("913c004a-f461-4ece-82a1-252b6093aeec");

    public static Guid CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPermissionId => Guid.Parse("19e90d9c-a9b3-44bd-8ed5-bf68b040454c");

    public static Guid CanDownloadSMTA1ShipmentDocumentsPermissionId => Guid.Parse("471095fc-efb4-4509-98fe-297c25f8ca22");

    public static Guid CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApprovalPermissionId => Guid.Parse("923dfa98-b663-457a-8fcb-6d2a397e57dd");
    public static Guid CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalRejectPermissionId => Guid.Parse("dedc8aa9-c2b2-4fc7-9c3b-20d1c9cb620e");
    public static Guid CanReceiveEmailsOnSMTA1ShipmentDocumentsApprovalPermissionId => Guid.Parse("aee67b6e-0f72-4dc6-a359-6e8d3698d7c0");
    public static Guid CanReceiveEmailsOnSMTA1ShipmentDocumentsRejectPermissionId => Guid.Parse("cd542cee-9be9-4b75-a526-12a9324450b7");

    public static Guid CanCreateCourierPermissionId => Guid.Parse("59001f29-12fc-4d23-bfd2-1e044fd6031a");
    public static Guid CanEditCourierPermissionId => Guid.Parse("f4e3bbeb-c1e4-4228-a013-ca93a8186035");
    public static Guid CanReadCourierPermissionId => Guid.Parse("a0ff57dc-9e5d-4f93-b830-c7b5e8baefa4");
    public static Guid CanDeleteCourierPermissionId => Guid.Parse("c3ffb4d7-87fd-4f65-90c9-a2c329f090aa");

    public static Guid CanCreateCourierStaffPermissionId => Guid.Parse("4e600810-2c06-404a-8e31-e2d5ba5bfc37");
    public static Guid CanEditCourierStaffPermissionId => Guid.Parse("01fec7ed-bb39-41b3-9a5c-837c3cce08cd");
    public static Guid CanReadCourierStaffPermissionId => Guid.Parse("0beb325b-a4b7-4e2b-9f19-d63fa294846d");
    public static Guid CanDeleteCourierStaffPermissionId => Guid.Parse("80c1ad5f-5674-416e-97f9-d3e24a1f27c7");


    public static Guid CanReadWaitForPickUpCompletedPermissionId => Guid.Parse("c2c9becc-7d08-49f6-bee1-10ca0698979c");
    public static Guid CanReadWaitForDeliveryCompletedPermissionId => Guid.Parse("91531eca-cc7d-4114-81b4-f38c230f80fb");
    public static Guid CanReadWaitForArrivalConditionCheckPermissionId => Guid.Parse("fca640d3-729f-4d80-8e14-cfd28176d233");
    public static Guid CanReadWaitForCommentBHFSendFeedbackPermissionId => Guid.Parse("17d443da-2a63-40ef-9863-c4fea17aef58");
    public static Guid CanReadWaitForFinalApprovalPermissionId => Guid.Parse("0852bacb-7f23-417b-92dc-858bb7c6c478");
    public static Guid CanReadShipmentCompletedPermissionId => Guid.Parse("13682b23-9c0e-4fde-9d56-339ec1c30c11");

    public static Guid CanSubmitWaitForPickUpCompletedPermissionId => Guid.Parse("144d5ba3-2cb9-4fee-b458-84b4676f42c0");
    public static Guid CanSubmitWaitForDeliveryCompletedPermissionId => Guid.Parse("97363ed1-f3e8-4367-9a2e-8f74ed37564c");
    public static Guid CanSubmitWaitForArrivalConditionCheckPermissionId => Guid.Parse("8d77c2b2-98fa-44cf-87a7-db2d6d4ca843");
    public static Guid CanSubmitWaitForCommentBHFSendFeedbackPermissionId => Guid.Parse("01283d8a-8e72-40e9-a3a5-8e399704db16");
    public static Guid CanSubmitWaitForFinalApprovalPermissionId => Guid.Parse("fc814b11-636f-4f33-ae92-1a4fc91d61e6");
    public static Guid CanSubmitShipmentCompletedPermissionId => Guid.Parse("264ae006-8597-4890-a919-3e7e4df88d4f");

    public static Guid CanDownloadFileWaitForPickUpCompletedPermissionId => Guid.Parse("3dc029cd-9321-4856-a0ed-a037997e04f7");
    public static Guid CanDownloadFileWaitForDeliveryCompletedPermissionId => Guid.Parse("197060b6-c963-4fc9-b043-c976d730a164");
    public static Guid CanDownloadFileWaitForArrivalConditionCheckPermissionId => Guid.Parse("586818f7-f4d3-4873-aa04-08b2ffe44e02");
    public static Guid CanDownloadFileWaitForCommentBHFSendFeedbackPermissionId => Guid.Parse("fec47e80-72d5-46c0-b4df-5ed1085186cb");
    public static Guid CanDownloadFileWaitForFinalApprovalPermissionId => Guid.Parse("c269e6dc-a1d0-437d-bab7-8bd066b53c6c");
    public static Guid CanDownloadFileShipmentCompletedPermissionId => Guid.Parse("af62b8ba-f38a-4405-aed5-b89908e96be4");

    public static Guid CanReceiveEmailsOnWaitForPickUpCompletedApprovalPermissionId => Guid.Parse("4cd59e9b-644a-402d-a71c-aaa25c5e182e");
    public static Guid CanReceiveEmailsOnWaitForDeliveryCompletedApprovalPermissionId => Guid.Parse("50ccd9f4-7696-4946-947f-64147cffb62c");
    public static Guid CanReceiveEmailsOnWaitForArrivalConditionCheckApprovalPermissionId => Guid.Parse("d62cb955-983d-4d23-9221-6bed69695a56");
    public static Guid CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApprovalPermissionId => Guid.Parse("abfb0635-065f-454a-a539-e632dae6cd1a");
    public static Guid CanReceiveEmailsOnWaitForFinalApprovalApprovalPermissionId => Guid.Parse("dd811cbb-36a6-44fd-a7ed-a96aa79b4411");
    public static Guid CanReceiveEmailsOnShipmentCompletedApprovalPermissionId => Guid.Parse("de81f98f-045a-4659-88a5-301ea84b3c1b");

    public static Guid CanReceiveEmailsOnWaitForPickUpCompletedRejectPermissionId => Guid.Parse("17f30c8b-d93d-4305-974e-6cac533717b3");
    public static Guid CanReceiveEmailsOnWaitForDeliveryCompletedRejectPermissionId => Guid.Parse("1e6cbf1a-a6a8-449a-b77c-02489886495b");
    public static Guid CanReceiveEmailsOnWaitForArrivalConditionCheckRejectPermissionId => Guid.Parse("9c71b80e-77a5-4069-85c5-073a586e9a2a");
    public static Guid CanReceiveEmailsOnWaitForCommentBHFSendFeedbackRejectPermissionId => Guid.Parse("974c60d2-f356-41cf-ac39-70d27f308bf3");
    public static Guid CanReceiveEmailsOnWaitForFinalApprovalRejectPermissionId => Guid.Parse("2f5fe2ef-e71f-4981-88ec-7d6201914774");
    public static Guid CanReceiveEmailsOnShipmentCompletedRejectPermissionId => Guid.Parse("aa7b3399-ff6b-40ff-8873-3383593e69d6");

    internal static Guid CanReadSubmitSMTA2PermissionId => Guid.Parse("8cf7e8d9-62ee-43cf-9e0b-1c91fb2f5da3");
    internal static Guid CanSubmitSMTA2PermissionId => Guid.Parse("0c581aa2-552a-4691-b5d0-bdc7edda5a5f");
    internal static Guid CanDownloadFileSubmitSMTA2PermissionId => Guid.Parse("46a198e4-5345-4773-8ba7-ed6cb3e6d350");
    internal static Guid CanReadWaitingForSMTA2SECsApprovalPermissionId => Guid.Parse("0e22d970-6688-4cbf-97c0-fd180259b19f");
    internal static Guid CanSubmitWaitingForSMTA2SECsApprovalPermissionId => Guid.Parse("17b7987c-5ca9-43cf-9fb9-630d06f140e7");
    internal static Guid CanDownloadFileWaitingForSMTA2SECsApprovalPermissionId => Guid.Parse("9a8f580c-d243-4136-bc74-59efa8f2cc20");
    internal static Guid CanReadSubmitAnnex2OfSMTA2PermissionId => Guid.Parse("8e0a60e1-c057-4fb5-abd7-de1bebac27ef");
    internal static Guid CanSubmitAnnex2OfSMTA2PermissionId => Guid.Parse("87f30eb9-88d1-49c1-b720-7db89fa0b2c3");
    internal static Guid CanDownloadFileSubmitAnnex2OfSMTA2PermissionId => Guid.Parse("6889ea19-7212-4047-86a6-5769ea6030b9");
    internal static Guid CanReadWaitingForAnnex2OfSMTA2SECsApprovalPermissionId => Guid.Parse("e7e37f08-c3c8-401a-a92c-9e959c0cb5de");
    internal static Guid CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPermissionId => Guid.Parse("120f9724-e424-46d1-b6a6-8f1907a88629");
    internal static Guid CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPermissionId => Guid.Parse("5b20754e-c093-42fb-a209-890113f18561");
    internal static Guid CanReadSubmitBiosafetyChecklistFormOfSMTA2PermissionId => Guid.Parse("a08db9b0-9b08-4aa9-a0c7-9fbaa805f44d");
    internal static Guid CanSubmitBiosafetyChecklistFormOfSMTA2PermissionId => Guid.Parse("d1f0caab-d0bf-4af9-b36d-879534bd1410");
    internal static Guid CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2PermissionId => Guid.Parse("a929989a-b0af-41cd-a6e4-09dce9b2bc5c");
    internal static Guid CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId => Guid.Parse("e149f2a6-87e5-46a1-98ec-91220ff4385c");
    internal static Guid CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId => Guid.Parse("bb8cb67d-d286-41d9-972d-9ce92cb1e6b5");
    internal static Guid CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPermissionId => Guid.Parse("a57a9de3-5cb1-4dd5-997b-52d139470939");
    internal static Guid CanReadSubmitBookingFormOfSMTA2PermissionId => Guid.Parse("d0aacf1b-60fb-4ffe-ad8d-8fb17cf3c48b");
    internal static Guid CanSubmitBookingFormOfSMTA2PermissionId => Guid.Parse("5402c87d-4b6b-4150-8b43-fb1e2253249b");
    internal static Guid CanDownloadFileSubmitBookingFormOfSMTA2PermissionId => Guid.Parse("3c5a4c5c-f5eb-4844-8220-39e4dab9ff65");
    internal static Guid CanReadWaitForBookingFormSMTA2OPSsApprovalPermissionId => Guid.Parse("c3382e71-b447-4b34-ab25-a65f84441a25");
    internal static Guid CanSubmitWaitForBookingFormSMTA2OPSsApprovalPermissionId => Guid.Parse("c086681e-1587-4474-b1ba-75ba282bd3a2");
    internal static Guid CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPermissionId => Guid.Parse("0f9a29d2-8c29-46f3-8bb3-1a85f72c2b30");
    internal static Guid CanReadBHFSMTA2ShipmentDocumentsPermissionId => Guid.Parse("23dfb3f5-9055-4b0f-9eaf-199a8f0f266a");
    internal static Guid CanSubmitBHFSMTA2ShipmentDocumentsPermissionId => Guid.Parse("b8ee909a-424a-49e6-b4d9-7fb6bf505e87");
    internal static Guid CanDownloadBHFSMTA2ShipmentDocumentsPermissionId => Guid.Parse("2640a188-6c01-43e0-9f83-5077f93a3cba");
    internal static Guid CanReadQESMTA2ShipmentDocumentsPermissionId => Guid.Parse("0eae0fcb-6045-4e52-a489-55e43cc74260");
    internal static Guid CanSubmitQESMTA2ShipmentDocumentsPermissionId => Guid.Parse("4b600d6f-02a8-4f7a-988b-5454f609ebc0");
    internal static Guid CanDownloadQESMTA2ShipmentDocumentsPermissionId => Guid.Parse("4883e1d5-6e7c-4148-a648-1947f1ad5e32");
    internal static Guid CanReadWaitForPickUpFromBioHubCompletedPermissionId => Guid.Parse("ab829127-06c1-43e0-8f8a-e1733013f1e8");
    internal static Guid CanSubmitWaitForPickUpFromBioHubCompletedPermissionId => Guid.Parse("ff158487-e08f-47bd-be66-a379d633a6b9");
    internal static Guid CanDownloadFileWaitForPickUpFromBioHubCompletedPermissionId => Guid.Parse("7fb26b31-018f-4201-a460-14c36a7d2a46");
    internal static Guid CanReadWaitForDeliveryFromBioHubCompletedPermissionId => Guid.Parse("e12eb92e-e48e-4611-b6bd-6af9ff8a1b86");
    internal static Guid CanSubmitWaitForDeliveryFromBioHubCompletedPermissionId => Guid.Parse("b8818961-b5e2-496d-ad1c-8eece6423723");
    internal static Guid CanDownloadFileWaitForDeliveryFromBioHubCompletedPermissionId => Guid.Parse("1162768f-f2ea-433b-9f50-e7321e8d010e");
    internal static Guid CanReadWaitForArrivalConditionFromBioHubCheckPermissionId => Guid.Parse("2041c074-3f48-443f-82d6-3a4c2961732c");
    internal static Guid CanSubmitWaitForArrivalConditionFromBioHubCheckPermissionId => Guid.Parse("ebca93ad-a173-4a8a-95b1-97beafa0c526");
    internal static Guid CanDownloadFileWaitForArrivalConditionFromBioHubCheckPermissionId => Guid.Parse("371b37ef-bf08-49ee-8a12-f799f7989850");
    internal static Guid CanReadWaitForCommentQESendFeedbackPermissionId => Guid.Parse("5937f35f-ea6b-45ef-9f15-0fbaaa939f39");
    internal static Guid CanSubmitWaitForCommentQESendFeedbackPermissionId => Guid.Parse("7c20c4c7-250f-47ae-8bae-99f0fbac7311");
    internal static Guid CanDownloadFileWaitForCommentQESendFeedbackPermissionId => Guid.Parse("71242f3a-90b6-4a30-96ac-8a3d1382b3c4");
    internal static Guid CanReadWaitForFinalApprovalFromBioHubPermissionId => Guid.Parse("16c9d15b-c688-49b8-945a-8d84d76bf6f9");
    internal static Guid CanSubmitWaitForFinalApprovalFromBioHubPermissionId => Guid.Parse("dedb49c8-60dc-46b6-ab61-6e51f1ecc4c0");
    internal static Guid CanDownloadFileWaitForFinalApprovalFromBioHubPermissionId => Guid.Parse("bb80bc97-6e38-447c-9363-3b4f8c659af6");
    internal static Guid CanReadShipmentFromBioHubCompletedPermissionId => Guid.Parse("d109119a-fb19-44d5-9b55-2be542838066");
    internal static Guid CanSubmitShipmentFromBioHubCompletedPermissionId => Guid.Parse("049bfad1-a7e2-4a94-a7f1-170f2c38f670");
    internal static Guid CanDownloadFileShipmentFromBioHubCompletedPermissionId => Guid.Parse("518907d1-9744-467c-acc5-d6a43d465a34");
    internal static Guid CanReceiveEmailsOnRequestInitiationApprovalPermissionId => Guid.Parse("dacc021a-c46b-4828-91e3-549068165835");
    internal static Guid CanReceiveEmailsOnSubmitSMTA2ApprovalPermissionId => Guid.Parse("28c230e4-5f19-42f1-b8d5-630f6f0627ce");
    internal static Guid CanReceiveEmailsOnWaitingForSMTA2SECsApprovalApprovalPermissionId => Guid.Parse("8b781195-f1f8-45d4-905e-f3d6cebc54e0");
    internal static Guid CanReceiveEmailsOnSubmitAnnex2OfSMTA2ApprovalPermissionId => Guid.Parse("84c79c38-5b05-4d53-aaf7-bf0ab8a6d0a1");
    internal static Guid CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApprovalPermissionId => Guid.Parse("1858e766-21bf-4394-a1e0-7182acec827c");
    internal static Guid CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2ApprovalPermissionId => Guid.Parse("37d9dd8e-b1b9-444b-b030-289522d895fc");
    internal static Guid CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovalPermissionId => Guid.Parse("38a120af-986d-40ea-ac15-f929d2c4a6b3");
    internal static Guid CanReceiveEmailsOnSubmitBookingFormOfSMTA2ApprovalPermissionId => Guid.Parse("ffa1732d-cc9a-47d7-bc7a-992d19a86e45");
    internal static Guid CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApprovalPermissionId => Guid.Parse("5e697188-2a1c-4c37-87b0-6c9a851efebe");
    internal static Guid CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsApprovalPermissionId => Guid.Parse("eafc7cf2-cfcd-415a-bf00-f03db0ddf697");
    internal static Guid CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsApprovalPermissionId => Guid.Parse("82f2f0f3-ee7a-49c2-b645-45cc82729c0d");
    internal static Guid CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedApprovalPermissionId => Guid.Parse("72056b64-bfca-4184-94f9-e0ad6d8b29dd");
    internal static Guid CanReceiveEmailOnNumberOfVialsWarningPermissionId => Guid.Parse("67119370-0b81-4f99-838b-e24fca12830b");
    internal static Guid CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedApprovalPermissionId => Guid.Parse("be040512-db0f-4ca2-a228-00ceb72385ab");
    internal static Guid CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApprovalPermissionId => Guid.Parse("bfc56956-110d-4071-a4c0-a48424894629");
    internal static Guid CanReceiveEmailsOnWaitForCommentQESendFeedbackApprovalPermissionId => Guid.Parse("5749618a-94e8-460c-9b3e-f929f91653dc");
    internal static Guid CanReceiveEmailsOnWaitForFinalApprovalFromBioHubApprovalPermissionId => Guid.Parse("d8d0bf2f-cfea-4cd6-99af-8e00bb7f5092");
    internal static Guid CanReceiveEmailsOnShipmentFromBioHubCompletedApprovalPermissionId => Guid.Parse("d2490783-2505-4c82-873c-183dc5ee9ac9");
    internal static Guid CanReceiveEmailsOnRequestInitiationRejectPermissionId => Guid.Parse("9ed53341-ae00-4e38-90de-94659e17a484");
    internal static Guid CanReceiveEmailsOnSubmitSMTA2RejectPermissionId => Guid.Parse("a5cee57d-7cc3-4232-a7c3-58019721a18c");
    internal static Guid CanReceiveEmailsOnWaitingForSMTA2SECsApprovalRejectPermissionId => Guid.Parse("56a93747-942f-4e8a-8b3c-33ad370e0577");
    internal static Guid CanReceiveEmailsOnSubmitAnnex2OfSMTA2RejectPermissionId => Guid.Parse("60e7d1ee-ea93-43c0-8d10-f1422178eca7");
    internal static Guid CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalRejectPermissionId => Guid.Parse("b27a67dc-294f-45f3-8d66-5adcfae4c43d");
    internal static Guid CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2RejectPermissionId => Guid.Parse("4c8a0363-0648-448c-9eb4-da61d867cffb");
    internal static Guid CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectPermissionId => Guid.Parse("17226307-4a0f-4786-9953-bbc7882f8b63");
    internal static Guid CanReceiveEmailsOnSubmitBookingFormOfSMTA2RejectPermissionId => Guid.Parse("b2476091-686d-40e9-b21b-d74a7ad1110e");
    internal static Guid CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalRejectPermissionId => Guid.Parse("91b4b278-2e3e-4a78-890e-f468dc4c7d49");
    internal static Guid CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsRejectPermissionId => Guid.Parse("4566d1cd-7ac0-4fa6-a419-f3ddcf407d6c");
    internal static Guid CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsRejectPermissionId => Guid.Parse("81b6bf70-88f5-4575-959a-f0e5fd7f4d49");
    internal static Guid CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedRejectPermissionId => Guid.Parse("3bdcb515-6e95-46dc-a4e3-d3fef85b3058");
    internal static Guid CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedRejectPermissionId => Guid.Parse("73f173d2-39e7-402e-8d66-1b2f4aae70d9");
    internal static Guid CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckRejectPermissionId => Guid.Parse("35ea815c-efaa-454f-a293-763b4e315bcd");
    internal static Guid CanReceiveEmailsOnWaitForSMTA2FinalApprovalRejectPermissionId => Guid.Parse("03f4f7e7-98e1-4f90-bef0-72f65b51e6ec");
    internal static Guid CanReceiveEmailsOnWaitForFinalApprovalFromBioHubRejectPermissionId => Guid.Parse("85f3199f-4788-45ca-9848-9b8246017575");

    internal static Guid CanApproveBioHubFacilityCompletionPermissionId => Guid.Parse("ebadae65-e35c-4175-b32b-fe60f9063ff6");
    internal static Guid CanApproveLaboratoryCompletionPermissionId => Guid.Parse("1b5e4764-4f8f-4193-b5a9-65ed78fa444a");
    internal static Guid CanVerifyMaterialPermissionId => Guid.Parse("5f0fde67-3204-47f1-90d5-84e5b8033d5b");
    internal static Guid CanSetMaterialReadyToSharePermissionId => Guid.Parse("1d4e5b8e-4983-4a69-a811-6861442fced8");
    internal static Guid CanSetMaterialPublicPermissionId => Guid.Parse("5cb45e4d-ecd3-4666-8fc7-89bea825f717");

    internal static Guid CanReadDocumentPermissionId => Guid.Parse("931695c7-7db8-4fd2-9fe0-b900eb21a426");
    internal static Guid CanReceiveEmailsOnRequestAccessPermissionId => Guid.Parse("4a023ddc-e3d2-4595-818b-85f9321d2ba5");

    internal static Guid CanReadKpiDataPermissionId => Guid.Parse("f052d6d5-3e96-4e19-a860-6b57c5d57dd3");


    internal static Guid CanReceiveEmailsOnSubmitSMTA2PermissionId => Guid.Parse("42797c54-0b91-499f-85ad-a7515f449a6f");
    internal static Guid CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApprovalPermissionId => Guid.Parse("43b2abf8-d108-42f5-9bf7-57f6a6f9861f");
    internal static Guid CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalRejectPermissionId => Guid.Parse("a23fb6b8-539e-400d-b38c-778c48acf07b");
    internal static Guid CanReceiveEmailsOnSMTA2BHFShipmentDocumentsApprovalPermissionId => Guid.Parse("3efa8e54-61a8-4a7d-9f15-afd97542f77e");
    internal static Guid CanReceiveEmailsOnWaitForSMTA2PickUpCompletedApprovalPermissionId => Guid.Parse("f5c60dab-ab1d-4232-a27e-1c8edd7af178");
    internal static Guid CanReceiveEmailsOnWaitForSMTA2PickUpCompletedRejectPermissionId => Guid.Parse("fda645e7-f530-4929-a97e-70f8bcf34f01");
    internal static Guid CanReceiveEmailsOnSMTA2QEShipmentDocumentsApprovalPermissionId => Guid.Parse("840621cc-f168-4ab2-bb79-185860be5c58");
    internal static Guid CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApprovalPermissionId => Guid.Parse("84a2a5b9-c2f6-4d0e-ae51-26a008247205");
    internal static Guid CanReceiveEmailsOnWaitForSMTA2FinalApprovalApprovalPermissionId => Guid.Parse("d90570a6-b034-49ac-8a98-13beb08f567f");



    internal static Guid CanReadSMTA1WorkflowCompletePermissionId => Guid.Parse("fee98874-af7c-4b9f-ad5c-b20b7811abf2");
    internal static Guid CanSubmitSMTA1WorkflowCompletePermissionId => Guid.Parse("2bc511e1-1beb-49b2-9016-75d5e2729b14");
    internal static Guid CanDownloadFileSMTA1WorkflowCompletePermissionId => Guid.Parse("6f62be1c-b475-4f16-bfc9-1e45fc0a1e3e");
    internal static Guid CanReceiveEmailsOnSMTA1WorkflowCompleteApprovalPermissionId => Guid.Parse("3ce4414d-40e7-4867-8ed7-d053f15ce626");


    internal static Guid CanReadSMTA2WorkflowCompletePermissionId => Guid.Parse("e70ef2a2-e637-47aa-83b2-6c2e3ae3cf1b");
    internal static Guid CanSubmitSMTA2WorkflowCompletePermissionId => Guid.Parse("c8adf206-b4d0-4850-b55b-110b566f3ada");
    internal static Guid CanDownloadFileSMTA2WorkflowCompletePermissionId => Guid.Parse("8f1bf35f-0aaf-412b-bab2-92cc17ed5ada");
    internal static Guid CanReceiveEmailsOnSMTA2WorkflowCompleteApprovalPermissionId => Guid.Parse("06bd367e-a1cf-4a1e-ab2a-6823f082d444");

    internal static Guid CanAccessSMTAWorkflowPermissionId => Guid.Parse("3114070b-2a32-46cf-96e7-1bd782ff7bb2");


    internal static Guid CanAddMaterialNewVialsPermissionId => Guid.Parse("805404ed-f71a-4e0d-bc43-a344cb06097b");

    internal static Guid CanEditMaterialOwnerBioHubFacilityPermissionId => Guid.Parse("3be6d1d6-d71a-4fb0-8283-acce6dbdf1b3");
    internal static Guid CanEditMaterialShipmentNumberOfVialsPermissionId => Guid.Parse("cf46bebe-978d-4f18-92f2-1ef5fe5ecd28");

    internal static Guid CanCreateResourcePermissionId => Guid.Parse("722274bd-a74c-49e0-bcd3-193db8feda74");
    internal static Guid CanEditResourcePermissionId => Guid.Parse("fcb6c7f3-502f-40aa-b0d2-272d87e83850");
    internal static Guid CanReadResourcePermissionId => Guid.Parse("e0dc5286-b788-4c9a-a614-ac97871b8d9a");
    internal static Guid CanDeleteResourcePermissionId => Guid.Parse("00ececc7-8f01-450a-8342-1bdcb83524be");

    internal static Guid CanEditMaterialWarningEmailCurrentNumberOfVialsThresholdPermissionId => Guid.Parse("9480b275-bfda-49fe-9926-c8fed0946f5c");

    internal static Guid CanCreateSpecimenTypePermissionId => Guid.Parse("72a56f7f-221b-409b-bab0-80b4dea7d0c5");
    internal static Guid CanEditSpecimenTypePermissionId => Guid.Parse("f4da5978-419e-4179-bb27-dd288e0ed1dd");
    internal static Guid CanReadSpecimenTypePermissionId => Guid.Parse("f0448daf-f6c2-4e29-a630-1a74d6058ab8");
    internal static Guid CanDeleteSpecimenTypePermissionId => Guid.Parse("528fcf80-3c09-4d90-9468-dce3221314de");



    internal static Guid CanReadSubmitSMTA1PastPermissionId => Guid.Parse("657e5e63-9548-4331-833c-9cfbea286617");
    internal static Guid CanReadWaitingForSMTA1SECsApprovalPastPermissionId => Guid.Parse("387422e1-6af2-4491-9760-3d7f3100f897");
    internal static Guid CanReadSMTA1WorkflowCompletePastPermissionId => Guid.Parse("80b3e660-635e-4e0f-93dc-54f57134156a");
    internal static Guid CanSubmitSMTA1PastPermissionId => Guid.Parse("f269e9b4-093e-4882-93f8-ec4b1a316286");
    internal static Guid CanSubmitWaitingForSMTA1SECsApprovalPastPermissionId => Guid.Parse("9aba208a-31b8-46e6-936d-8156905b637d");
    internal static Guid CanSubmitSMTA1WorkflowCompletePastPermissionId => Guid.Parse("ed2aaefe-14ca-4a8d-b802-c7bbcbff5d47");
    internal static Guid CanDownloadFileSubmitSMTA1PastPermissionId => Guid.Parse("d3a4f92a-7c0a-4137-9a03-f0e68ea84336");
    internal static Guid CanDownloadFileWaitingForSMTA1SECsApprovalPastPermissionId => Guid.Parse("6be94cba-52ac-49ff-8601-6087a07f5af4");
    internal static Guid CanDownloadFileSMTA1WorkflowCompletePastPermissionId => Guid.Parse("9ccef138-8c9e-4303-8c0b-2d89cbf692fc");
    internal static Guid CanReadSubmitAnnex2OfSMTA1PastPermissionId => Guid.Parse("b8474354-4599-422c-bf3f-c41dbd657630");
    internal static Guid CanReadWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId => Guid.Parse("2b1aa7c7-3798-41d6-b41d-9f520da8c1c9");
    internal static Guid CanReadSubmitBookingFormOfSMTA1PastPermissionId => Guid.Parse("b59a5f2f-1732-4f35-a1fa-77c7404bf933");
    internal static Guid CanReadWaitForBookingFormSMTA1OPSApprovalPastPermissionId => Guid.Parse("b7ad399f-c834-451f-9a89-3a8c5c16fcf8");
    internal static Guid CanReadSMTA1ShipmentDocumentsPastPermissionId => Guid.Parse("3b83662b-b7d9-4f1a-a2ae-35e0bb3401d7");
    internal static Guid CanReadWaitForPickUpCompletedPastPermissionId => Guid.Parse("298ff369-a9e3-4ad7-98b6-9eb6c14b7a86");
    internal static Guid CanReadWaitForDeliveryCompletedPastPermissionId => Guid.Parse("2c0bbf4c-0946-43d7-b443-71dc6996f69c");
    internal static Guid CanReadWaitForArrivalConditionCheckPastPermissionId => Guid.Parse("5874a32d-041a-454a-b262-8e64f23793c5");
    internal static Guid CanReadWaitForCommentBHFSendFeedbackPastPermissionId => Guid.Parse("8c7846ec-63f2-44c5-aa12-62127e1956af");
    internal static Guid CanReadWaitForFinalApprovalPastPermissionId => Guid.Parse("41aca98d-157b-4e6a-9346-b473d2751679");
    internal static Guid CanReadShipmentCompletedPastPermissionId => Guid.Parse("decfea60-b60c-4daa-b656-74228587a88b");
    internal static Guid CanSubmitAnnex2OfSMTA1PastPermissionId => Guid.Parse("7fc67d3c-4be7-4f59-aa47-806f784a9d61");
    internal static Guid CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId => Guid.Parse("aa33f817-af49-445a-828a-29d880d8b1d5");
    internal static Guid CanSubmitBookingFormOfSMTA1PastPermissionId => Guid.Parse("79f0742e-4714-455a-bfeb-8e50fda5137a");
    internal static Guid CanSubmitWaitForBookingFormSMTA1OPSApprovalPastPermissionId => Guid.Parse("e07b3345-1eaf-423f-aa82-d58d5b0e7639");
    internal static Guid CanSubmitSMTA1ShipmentDocumentsPastPermissionId => Guid.Parse("662db581-ed14-43af-89c6-6d84c3a37678");
    internal static Guid CanSubmitWaitForPickUpCompletedPastPermissionId => Guid.Parse("13f1c7d0-c568-42ab-802e-01cc9174532d");
    internal static Guid CanSubmitWaitForDeliveryCompletedPastPermissionId => Guid.Parse("68daa548-2313-41c4-a6d9-79e3acdc7336");
    internal static Guid CanSubmitWaitForArrivalConditionCheckPastPermissionId => Guid.Parse("ca021487-7214-43b8-8f03-dca2e35d3ca6");
    internal static Guid CanSubmitWaitForCommentBHFSendFeedbackPastPermissionId => Guid.Parse("14da33b6-38db-4e5a-92a2-5dbe16c124e7");
    internal static Guid CanSubmitWaitForFinalApprovalPastPermissionId => Guid.Parse("ef23095c-9345-4fc8-ae50-f68d52facdda");
    internal static Guid CanSubmitShipmentCompletedPastPermissionId => Guid.Parse("e528f1dd-00a5-414b-be90-0620c16e5713");
    internal static Guid CanDownloadFileSubmitAnnex2OfSMTA1PastPermissionId => Guid.Parse("2962ad28-18a6-4ad3-8b2b-fb9b60451e3c");
    internal static Guid CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPastPermissionId => Guid.Parse("b005829d-1800-42fb-95f3-8546862f319a");
    internal static Guid CanDownloadFileSubmitBookingFormOfSMTA1PastPermissionId => Guid.Parse("d7a8dc4e-6e5a-4845-850b-3b4b207b63d5");
    internal static Guid CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPastPermissionId => Guid.Parse("2d3f1fb8-5aea-4819-a6ae-d8100e9b45dd");
    internal static Guid CanDownloadSMTA1ShipmentDocumentsPastPermissionId => Guid.Parse("ada73287-8222-4f3f-bd31-7b0f8453f9f3");
    internal static Guid CanDownloadFileWaitForPickUpCompletedPastPermissionId => Guid.Parse("0c332d81-a1ee-44a5-9742-9eb53a32fc56");
    internal static Guid CanDownloadFileWaitForDeliveryCompletedPastPermissionId => Guid.Parse("19732dee-ad63-4824-825a-d7e06c673fb5");
    internal static Guid CanDownloadFileWaitForArrivalConditionCheckPastPermissionId => Guid.Parse("19771877-bd90-43cf-a74f-ceae5349a32c");
    internal static Guid CanDownloadFileWaitForCommentBHFSendFeedbackPastPermissionId => Guid.Parse("cabae6e2-905e-4292-898c-778dca85c8f2");
    internal static Guid CanDownloadFileWaitForFinalApprovalPastPermissionId => Guid.Parse("7f97b807-37a3-46a3-82d2-c3de97896044");
    internal static Guid CanDownloadFileShipmentCompletedPastPermissionId => Guid.Parse("40df09d2-8432-4ed6-870b-24793422205a");
    internal static Guid CanReadSubmitSMTA2PastPermissionId => Guid.Parse("335c848a-98bd-4996-92fa-29450417936a");
    internal static Guid CanReadWaitingForSMTA2SECsApprovalPastPermissionId => Guid.Parse("3a90513c-c056-4e55-b6eb-7357bbaa555f");
    internal static Guid CanReadSMTA2WorkflowCompletePastPermissionId => Guid.Parse("68944ad7-587f-4e81-bb59-cd645471908b");
    internal static Guid CanSubmitSMTA2PastPermissionId => Guid.Parse("3e19078b-05b2-4f32-8b9f-4b36dc7f6301");
    internal static Guid CanSubmitWaitingForSMTA2SECsApprovalPastPermissionId => Guid.Parse("8493b54b-3f59-4b2c-ad10-1bb546184007");
    internal static Guid CanSubmitSMTA2WorkflowCompletePastPermissionId => Guid.Parse("cb35ea8a-fd9e-423c-ae2f-3b8fbef1b49a");
    internal static Guid CanDownloadFileSubmitSMTA2PastPermissionId => Guid.Parse("65656639-e7f5-4345-b05f-3bde49b0d2bb");
    internal static Guid CanDownloadFileWaitingForSMTA2SECsApprovalPastPermissionId => Guid.Parse("dec3f331-0465-4781-a21f-535f5a4fff2f");
    internal static Guid CanDownloadFileSMTA2WorkflowCompletePastPermissionId => Guid.Parse("d59d8032-9faa-4583-a74d-9e2056963143");
    internal static Guid CanReadSubmitAnnex2OfSMTA2PastPermissionId => Guid.Parse("0ade8c7c-63f5-4cb6-8388-f20df982a24f");
    internal static Guid CanReadWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId => Guid.Parse("e398f9b6-00b5-4a4a-ad91-b8f03a7064ee");
    internal static Guid CanReadSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId => Guid.Parse("4a6bbbe1-5bb2-44fb-8d79-7cc8673a917f");
    internal static Guid CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId => Guid.Parse("ff1c74b6-0d2a-4f38-9830-b1ef700974a9");
    internal static Guid CanReadSubmitBookingFormOfSMTA2PastPermissionId => Guid.Parse("ff50be91-12c1-42af-806e-eb33bc712937");
    internal static Guid CanReadWaitForBookingFormSMTA2OPSsApprovalPastPermissionId => Guid.Parse("eb1dd196-2ced-4997-bf52-989fa0e6f470");
    internal static Guid CanReadBHFSMTA2ShipmentDocumentsPastPermissionId => Guid.Parse("f8884e7b-35cf-4b9c-aec2-fbcd653ddb0a");
    internal static Guid CanReadQESMTA2ShipmentDocumentsPastPermissionId => Guid.Parse("f617e533-4f52-402b-b58c-9423070340ff");
    internal static Guid CanReadWaitForPickUpFromBioHubCompletedPastPermissionId => Guid.Parse("fb77f2d0-706a-411c-90ad-d9e222dbadf9");
    internal static Guid CanReadWaitForDeliveryFromBioHubCompletedPastPermissionId => Guid.Parse("dc4f2940-a701-4205-9018-ca220f5eef08");
    internal static Guid CanReadWaitForArrivalConditionFromBioHubCheckPastPermissionId => Guid.Parse("adb57865-1db3-48b3-a221-c817496a67cc");
    internal static Guid CanReadWaitForCommentQESendFeedbackPastPermissionId => Guid.Parse("9530c41c-f689-4788-b9e7-2cf82786e705");
    internal static Guid CanReadWaitForFinalApprovalFromBioHubPastPermissionId => Guid.Parse("2bb8ee02-06a5-49e7-b806-ff3568e8835c");
    internal static Guid CanReadShipmentFromBioHubCompletedPastPermissionId => Guid.Parse("61dda01e-7abf-4e35-ad32-b97001d430a5");
    internal static Guid CanSubmitAnnex2OfSMTA2PastPermissionId => Guid.Parse("20991a45-6b4f-44c6-949b-f469959cd05d");
    internal static Guid CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId => Guid.Parse("9f6c4ac0-ad96-4de9-b035-1614cd403e7d");
    internal static Guid CanSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId => Guid.Parse("2532cc81-1f51-4044-9899-3f690e40e856");
    internal static Guid CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId => Guid.Parse("41211872-1df6-49b0-aa8a-f5fc73859ae3");
    internal static Guid CanSubmitBookingFormOfSMTA2PastPermissionId => Guid.Parse("d9f59035-089f-4be1-943e-508d961d7533");
    internal static Guid CanSubmitWaitForBookingFormSMTA2OPSsApprovalPastPermissionId => Guid.Parse("fb76bdbb-4bcb-4b0b-a2f2-a3e3e03cbeb6");
    internal static Guid CanSubmitBHFSMTA2ShipmentDocumentsPastPermissionId => Guid.Parse("34aa91ef-83f6-4ecd-be3d-4ff31799be44");
    internal static Guid CanSubmitQESMTA2ShipmentDocumentsPastPermissionId => Guid.Parse("bf1f53b9-74d1-4b77-9f69-6c8f932b009f");
    internal static Guid CanSubmitWaitForPickUpFromBioHubCompletedPastPermissionId => Guid.Parse("b5ab5bb5-af33-40d1-bb13-6f821379fae6");
    internal static Guid CanSubmitWaitForDeliveryFromBioHubCompletedPastPermissionId => Guid.Parse("737a07f8-081d-4af6-a634-4abaa4c44fbb");
    internal static Guid CanSubmitWaitForArrivalConditionFromBioHubCheckPastPermissionId => Guid.Parse("ca49dd1d-1fca-4194-a0ff-62da00e73e7a");
    internal static Guid CanSubmitWaitForCommentQESendFeedbackPastPermissionId => Guid.Parse("b826c03a-27d4-42bc-b9a4-da0d5e361dbd");
    internal static Guid CanSubmitWaitForFinalApprovalFromBioHubPastPermissionId => Guid.Parse("b4c92e3a-88d9-47c3-9888-5e007571b215");
    internal static Guid CanSubmitShipmentFromBioHubCompletedPastPermissionId => Guid.Parse("1ad49c82-f68f-40de-8317-d1998cc22826");
    internal static Guid CanDownloadFileSubmitAnnex2OfSMTA2PastPermissionId => Guid.Parse("a4e33294-3cf6-4c68-b94d-15906111df02");
    internal static Guid CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPastPermissionId => Guid.Parse("ded5f788-69ca-42b7-8359-fd3ae745eeb8");
    internal static Guid CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2PastPermissionId => Guid.Parse("9b245d66-6c73-4bae-831a-eb12b87a99f9");
    internal static Guid CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPastPermissionId => Guid.Parse("e7eaa4cd-cd65-4e4e-b382-5d1ed10831df");
    internal static Guid CanDownloadFileSubmitBookingFormOfSMTA2PastPermissionId => Guid.Parse("f45fa0b9-1cb2-445e-871d-53bfafe5d5a3");
    internal static Guid CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPastPermissionId => Guid.Parse("f80b75b6-06c9-41d1-981c-49143214072a");
    internal static Guid CanDownloadBHFSMTA2ShipmentDocumentsPastPermissionId => Guid.Parse("bd16df19-80ea-493a-aac7-4aae78933135");
    internal static Guid CanDownloadQESMTA2ShipmentDocumentsPastPermissionId => Guid.Parse("badd1a59-847f-45dd-9910-00c224a6082e");
    internal static Guid CanDownloadFileWaitForPickUpFromBioHubCompletedPastPermissionId => Guid.Parse("2ced842b-dddb-4d03-9278-940228ee60bc");
    internal static Guid CanDownloadFileWaitForDeliveryFromBioHubCompletedPastPermissionId => Guid.Parse("95c45925-f40e-455a-bebb-9170762ae7ba");
    internal static Guid CanDownloadFileWaitForArrivalConditionFromBioHubCheckPastPermissionId => Guid.Parse("ab8ab51a-47c5-46b1-bc64-bdd1803219e8");
    internal static Guid CanDownloadFileWaitForCommentQESendFeedbackPastPermissionId => Guid.Parse("f05c8081-ecca-4afc-b304-e435168c320b");
    internal static Guid CanDownloadFileWaitForFinalApprovalFromBioHubPastPermissionId => Guid.Parse("4c22f1a0-a739-4cf2-ae90-9d2332e6aba7");
    internal static Guid CanDownloadFileShipmentFromBioHubCompletedPastPermissionId => Guid.Parse("9b1cdfd1-7f39-42eb-9636-1f21d3c9c9e7");

    internal static Guid CanAccessPastRequestIniziationPermissionId => Guid.Parse("aa0885f0-cec7-470a-a3a7-bd872d9ef861");
    internal static Guid CanAccessPastWorklistPermissionId => Guid.Parse("01669d31-9475-4954-8571-e0e6d930c9d7");

    internal static Guid CanAccessPastSMTAWorkflowPermissionId => Guid.Parse("2d100775-f9c5-498c-b962-16b37517b210");

    internal static Guid CanReadOnBehalfOfRolesPermissionId => Guid.Parse("45c0c03d-9d67-4737-a521-39bea788a791");

    internal static Guid CanEditMaterialShipmentInformationPermissionId => Guid.Parse("b764c232-d9f6-4fb6-a71e-34db28ef0bfd");

    internal static Guid CanReadEFormPermissionId => Guid.Parse("c2bac728-9741-443c-ab2d-1bd37902855c");


    private async Task AddPermissions(CancellationToken cancellationToken)
    {

        await _db.AddRangeAsync(Permissions, cancellationToken: cancellationToken);
    }
    private async Task AddOrUpdatePermissions(CancellationToken cancellationToken)
    {
        var rows = from o in _db.Permissions
                   select o;

        foreach (var row in rows)
        {
            _db.Permissions.Remove(row);
        }

        foreach (var permission in Permissions)
        {
            if (await _db.Permissions.Where(x => x.Id == permission.Id).AnyAsync(cancellationToken))
            {
                _db.Update(permission);
            }
            else
            {
                await _db.AddAsync(permission);
            }
        }
    }
}