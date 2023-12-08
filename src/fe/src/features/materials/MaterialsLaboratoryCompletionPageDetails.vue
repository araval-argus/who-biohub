<template>
  <div>
    <WorklistTimelineComponent
      v-if="MaterialTimeline != undefined"
      timeline-title=""
      :worklist-timeline-events-days="MaterialTimeline.Events"
    >
    </WorklistTimelineComponent>
    <MaterialLaboratoryCompletionForm
      v-if="isMaterialSet && !loading"
      ref="materialForm"
      v-model="material"
      title="Material Detail"
      :readonly="!canEdit"
      :can-verify-material="canVerifyMaterial"
    >
      <CardActionsSaveCancel v-if="canEdit" @save="onSave" @cancel="onCancel">
      </CardActionsSaveCancel>

      <v-container class="px-0" fluid>
        <CardActionsGenericButton
          v-if="approveVisible && canApprove"
          text="Approve"
          @click="onApprove"
        >
        </CardActionsGenericButton>
      </v-container>
    </MaterialLaboratoryCompletionForm>
  </div>
</template>

<script lang="ts">
import MaterialLaboratoryCompletionForm from "./components/MaterialLaboratoryCompletionForm.vue";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import CardActionsGenericButton from "../../components/CardActionsGenericButton.vue";
import { Component, Vue } from "vue-property-decorator";
import { MaterialModule } from "./store";
import { MaterialLaboratoryCompletion } from "@/models/MaterialLaboratoryCompletion";
import { getMaterialProviderId } from "@/utils/helper";
import { AuthModule } from "../auth/store";
import { LaboratoryModule } from "./../laboratories/store";
import { BioHubFacilityModule } from "./../biohubfacilities/store";
import { CountryModule } from "./../countries/store";
import { CountryGridItem } from "@/models/CountryGridItem";
import { MaterialTypeModule } from "./../materialTypes/store";
import { MaterialTypeGridItem } from "@/models/MaterialTypeGridItem";
import { MaterialProductModule } from "./../materialProducts/store";
import { MaterialProductGridItem } from "@/models/MaterialProductGridItem";
import { TransportCategoryModule } from "./../transportCategories/store";
import { TransportCategoryGridItem } from "@/models/TransportCategoryGridItem";
import { TemperatureUnitOfMeasureModule } from "./../temperatureUnitOfMeasures/store";
import { TemperatureUnitOfMeasureGridItem } from "@/models/TemperatureUnitOfMeasureGridItem";
import { MaterialUsagePermissionModule } from "./../materialUsagePermissions/store";
import { MaterialUsagePermissionGridItem } from "@/models/MaterialUsagePermissionGridItem";
import { GeneticSequenceDataModule } from "./../geneticSequenceDatas/store";
import { GeneticSequenceDataGridItem } from "@/models/GeneticSequenceDataGridItem";
import { InternationalTaxonomyClassificationModule } from "./../internationalTaxonomyClassifications/store";
import { InternationalTaxonomyClassificationGridItem } from "@/models/InternationalTaxonomyClassificationGridItem";
import { IsolationHostTypeModule } from "./../isolationHostTypes/store";
import { IsolationHostTypeGridItem } from "@/models/IsolationHostTypeGridItem";
import { CultivabilityTypeModule } from "./../cultivabilityTypes/store";
import { CultivabilityTypeGridItem } from "@/models/CultivabilityTypeGridItem";
import { IsolationTechniqueTypeModule } from "./../isolationTechniqueTypes/store";
import { IsolationTechniqueTypeGridItem } from "@/models/IsolationTechniqueTypeGridItem";
import { MaterialProviderLaboratoryDropDown } from "@/models/MaterialProviderLaboratoryDropDown";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { MaterialValidationSelection } from "@/models/enums/MaterialValidationSelection";
import { SpecimenTypeModule } from "../specimenTypes/store";
import { GSDUploadingStatus } from "@/models/enums/GSDUploadingStatus";
import { SeedData } from "@/models/constants/SeedData";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import WorklistTimelineComponent from "@/components/WorklistTimelineComponent.vue";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";

@Component({
  components: {
    MaterialLaboratoryCompletionForm,
    CardActionsSaveCancel,
    CardActionsGenericButton,
  },
})
export default class MaterialsLaboratoryCompletionPageDetails extends Vue {
  $refs!: {
    materialForm: MaterialLaboratoryCompletionForm;
  };

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadMaterial);
  }

  get canEdit(): boolean {
    return false;
  }

  get canApprove(): boolean {
    return false;
  }

  get canVerifyMaterial(): boolean {
    return false;
  }

  get isMaterialSet(): boolean {
    return MaterialModule.MaterialLaboratoryCompletion !== undefined;
  }

  get loading(): boolean {
    return AppModule.IsLoadingActive && !this.isMaterialSet;
  }

  get material(): MaterialLaboratoryCompletion {
    const lab = MaterialModule.MaterialLaboratoryCompletion;
    if (lab) return lab;

    throw { message: "" };
  }

  set material(lab: MaterialLaboratoryCompletion) {
    MaterialModule.SET_MATERIAL_LABORATORY_COMPLETION(lab);
  }

  get approveVisible(): boolean {
    if (
      this.material.ReferenceNumber === "" ||
      this.material.ReferenceNumber === undefined ||
      this.material.ReferenceNumber === null
    ) {
      return false;
    }
    // if (
    //   this.material.Name === "" ||
    //   this.material.Name === undefined ||
    //   this.material.Name === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.Description === "" ||
    //   this.material.Description === undefined ||
    //   this.material.Description === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.Temperature === undefined ||
    //   this.material.Temperature === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.SampleId === "" ||
    //   this.material.SampleId === undefined ||
    //   this.material.SampleId === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.Lineage === "" ||
    //   this.material.Lineage === undefined ||
    //   this.material.Lineage === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.Variant === "" ||
    //   this.material.Variant === undefined ||
    //   this.material.Variant === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.VariantAssessment === "" ||
    //   this.material.VariantAssessment === undefined ||
    //   this.material.VariantAssessment === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.StrainDesignation === "" ||
    //   this.material.StrainDesignation === undefined ||
    //   this.material.StrainDesignation === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.Genotype === "" ||
    //   this.material.Genotype === undefined ||
    //   this.material.Genotype === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.Serotype === "" ||
    //   this.material.Serotype === undefined ||
    //   this.material.Serotype === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.DatabaseAccessionId === "" ||
    //   this.material.DatabaseAccessionId === undefined ||
    //   this.material.DatabaseAccessionId === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.OriginalGeneticSequence === "" ||
    //   this.material.OriginalGeneticSequence === undefined ||
    //   this.material.OriginalGeneticSequence === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.GSDCulturedMaterialCellLine1 === "" ||
    //   this.material.GSDCulturedMaterialCellLine1 === undefined ||
    //   this.material.GSDCulturedMaterialCellLine1 === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.GSDCulturedMaterialCellLine2 === "" ||
    //   this.material.GSDCulturedMaterialCellLine2 === undefined ||
    //   this.material.GSDCulturedMaterialCellLine2 === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.FacilityGSD === "" ||
    //   this.material.FacilityGSD === undefined ||
    //   this.material.FacilityGSD === null
    // ) {
    //   return false;
    // }
    // if (this.material.GMO === undefined || this.material.GMO === null) {
    //   return false;
    // }
    // // if (
    // //   this.material.ProductionCellLine === "" ||
    // //   this.material.ProductionCellLine === undefined ||
    // //   this.material.ProductionCellLine === null
    // // ) {
    // //   return false;
    // // }
    // if (
    //   this.material.Infectivity === "" ||
    //   this.material.Infectivity === undefined ||
    //   this.material.Infectivity === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.ViralTiter === "" ||
    //   this.material.ViralTiter === undefined ||
    //   this.material.ViralTiter === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.TypeId === "" ||
    //   this.material.TypeId === undefined ||
    //   this.material.TypeId === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.SuspectedEpidemiologicalOriginId === "" ||
    //   this.material.SuspectedEpidemiologicalOriginId === undefined ||
    //   this.material.SuspectedEpidemiologicalOriginId === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.ProductTypeId === "" ||
    //   this.material.ProductTypeId === undefined ||
    //   this.material.ProductTypeId === null
    // ) {
    //   return false;
    // }
    // // if (
    // //   this.material.TransportCategoryId === "" ||
    // //   this.material.TransportCategoryId === undefined ||
    // //   this.material.TransportCategoryId === null
    // // ) {
    // //   return false;
    // // }
    // if (
    //   this.material.UnitOfMeasureId === "" ||
    //   this.material.UnitOfMeasureId === undefined ||
    //   this.material.UnitOfMeasureId === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.UsagePermissionId === "" ||
    //   this.material.UsagePermissionId === undefined ||
    //   this.material.UsagePermissionId === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.GeneticSequenceDataId === "" ||
    //   this.material.GeneticSequenceDataId === undefined ||
    //   this.material.GeneticSequenceDataId === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.InternationalTaxonomyClassificationId === "" ||
    //   this.material.InternationalTaxonomyClassificationId === undefined ||
    //   this.material.InternationalTaxonomyClassificationId === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.IsolationHostTypeId === "" ||
    //   this.material.IsolationHostTypeId === undefined ||
    //   this.material.IsolationHostTypeId === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.CultivabilityTypeId === "" ||
    //   this.material.CultivabilityTypeId === undefined ||
    //   this.material.CultivabilityTypeId === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.IsolationTechniqueTypeId === "" ||
    //   this.material.IsolationTechniqueTypeId === undefined ||
    //   this.material.IsolationTechniqueTypeId === null
    // ) {
    //   return false;
    // }
    // // if (
    // //   this.material.ProviderLaboratoryId === "" ||
    // //   this.material.ProviderLaboratoryId === undefined ||
    // //   this.material.ProviderLaboratoryId === null
    // // ) {
    // //   return false;
    // // }
    // // if (
    // //   this.material.ProviderBioHubFacilityId === "" ||
    // //   this.material.ProviderBioHubFacilityId === undefined ||
    // //   this.material.ProviderBioHubFacilityId === null
    // // ) {
    // //   return false;
    // // }
    // if (
    //   this.material.Location === "" ||
    //   this.material.Location === undefined ||
    //   this.material.Location === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.PatientStatus === "" ||
    //   this.material.PatientStatus === undefined ||
    //   this.material.PatientStatus === null
    // ) {
    //   return false;
    // }
    // if (
    //   this.material.Age === undefined ||
    //   this.material.Age === null ||
    //   isNaN(this.material.Age) ||
    //   this.material.Age < 0
    // ) {
    //   return false;
    // }
    // if (this.material.Gender === undefined || this.material.Gender === null) {
    //   return false;
    // }
    // if (
    //   this.material.CollectionDate === undefined ||
    //   this.material.CollectionDate === null
    // ) {
    //   return false;
    // }

    // if (
    //   this.material.CulturingCellLine1 === "" ||
    //   this.material.CulturingCellLine1 === undefined ||
    //   this.material.CulturingCellLine1 === null
    // ) {
    //   return false;
    // }

    // if (
    //   this.material.CulturingCellLine2 === "" ||
    //   this.material.CulturingCellLine2 === undefined ||
    //   this.material.CulturingCellLine2 === null
    // ) {
    //   return false;
    // }

    // if (
    //   this.material.CulturingCellLine3 === "" ||
    //   this.material.CulturingCellLine3 === undefined ||
    //   this.material.CulturingCellLine3 === null
    // ) {
    //   return false;
    // }

    if (
      this.material.ReferenceNumberValidation !==
        MaterialValidationSelection.Verified &&
      this.material.ReferenceNumberValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.NameValidation !== MaterialValidationSelection.Verified &&
      this.material.NameValidation !== MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.TypeValidation !== MaterialValidationSelection.Verified &&
      this.material.TypeValidation !== MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.PathogenValidation !==
        MaterialValidationSelection.Verified &&
      this.material.PathogenValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.VariantValidation !==
        MaterialValidationSelection.Verified &&
      this.material.VariantValidation !== MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.VariantAssessmentValidation !==
        MaterialValidationSelection.Verified &&
      this.material.VariantAssessmentValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.GMOValidation !== MaterialValidationSelection.Verified &&
      this.material.GMOValidation !== MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.LineageValidation !==
        MaterialValidationSelection.Verified &&
      this.material.LineageValidation !== MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.SuspectedEpidemiologicalOriginValidation !==
        MaterialValidationSelection.Verified &&
      this.material.SuspectedEpidemiologicalOriginValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.UsagePermissionValidation !==
        MaterialValidationSelection.Verified &&
      this.material.UsagePermissionValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.OwnerBioHubFacilityValidation !==
        MaterialValidationSelection.Verified &&
      this.material.OwnerBioHubFacilityValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.DateOfBMEPPReceiptValidation !==
        MaterialValidationSelection.Verified &&
      this.material.DateOfBMEPPReceiptValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.ProductTypeValidation !==
        MaterialValidationSelection.Verified &&
      this.material.ProductTypeValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.TemperatureValidation !==
        MaterialValidationSelection.Verified &&
      this.material.TemperatureValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.UnitOfMeasureValidation !==
        MaterialValidationSelection.Verified &&
      this.material.UnitOfMeasureValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.OriginalProductTypeValidation !==
        MaterialValidationSelection.Verified &&
      this.material.OriginalProductTypeValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.TransportCategoryValidation !==
        MaterialValidationSelection.Verified &&
      this.material.TransportCategoryValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.ShipmentNumberOfVialsValidation !==
        MaterialValidationSelection.Verified &&
      this.material.ShipmentNumberOfVialsValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.ShipmentAmountValidation !==
        MaterialValidationSelection.Verified &&
      this.material.ShipmentAmountValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.FreezingDateValidation !==
        MaterialValidationSelection.Verified &&
      this.material.FreezingDateValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.ShipmentTemperatureValidation !==
        MaterialValidationSelection.Verified &&
      this.material.ShipmentTemperatureValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.ShipmentUnitOfMeasureValidation !==
        MaterialValidationSelection.Verified &&
      this.material.ShipmentUnitOfMeasureValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.VirusConcentrationValidation !==
        MaterialValidationSelection.Verified &&
      this.material.VirusConcentrationValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }

    if (
      this.material.OriginalProductTypeId == SeedData.CulturedIsolateProductId
    ) {
      if (
        this.material.CulturingCellLineValidation !==
          MaterialValidationSelection.Verified &&
        this.material.CulturingCellLineValidation !==
          MaterialValidationSelection.Unverified
      ) {
        return false;
      }
      if (
        this.material.CulturingPassagesNumberValidation !==
          MaterialValidationSelection.Verified &&
        this.material.CulturingPassagesNumberValidation !==
          MaterialValidationSelection.Unverified
      ) {
        return false;
      }
    }

    if (
      this.material.OriginalProductTypeId == SeedData.ClinicalSpecimenProductId
    ) {
      if (
        this.material.MaterialCollectedSpecimenTypesValidation !==
          MaterialValidationSelection.Verified &&
        this.material.MaterialCollectedSpecimenTypesValidation !==
          MaterialValidationSelection.Unverified
      ) {
        return false;
      }
      if (
        this.material.TypeOfTransportMediumValidation !==
          MaterialValidationSelection.Verified &&
        this.material.TypeOfTransportMediumValidation !==
          MaterialValidationSelection.Unverified
      ) {
        return false;
      }
      if (
        this.material.BrandOfTransportMediumValidation !==
          MaterialValidationSelection.Verified &&
        this.material.BrandOfTransportMediumValidation !==
          MaterialValidationSelection.Unverified
      ) {
        return false;
      }
    }
    if (
      this.material.CollectionDateValidation !==
        MaterialValidationSelection.Verified &&
      this.material.CollectionDateValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.LocationValidation !==
        MaterialValidationSelection.Verified &&
      this.material.LocationValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.IsolationHostTypeValidation !==
        MaterialValidationSelection.Verified &&
      this.material.IsolationHostTypeValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.GenderValidation !== MaterialValidationSelection.Verified &&
      this.material.GenderValidation !== MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.AgeValidation !== MaterialValidationSelection.Verified &&
      this.material.AgeValidation !== MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.PatientStatusValidation !==
        MaterialValidationSelection.Verified &&
      this.material.PatientStatusValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.CulturingResultValidation !==
        MaterialValidationSelection.Verified &&
      this.material.CulturingResultValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.CulturingResultDateValidation !==
        MaterialValidationSelection.Verified &&
      this.material.CulturingResultDateValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.IsolationTechniqueTypeValidation !==
        MaterialValidationSelection.Verified &&
      this.material.IsolationTechniqueTypeValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.QualityControlResultValidation !==
        MaterialValidationSelection.Verified &&
      this.material.QualityControlResultValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.QualityControlResultDateValidation !==
        MaterialValidationSelection.Verified &&
      this.material.QualityControlResultDateValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.InfectivityValidation !==
        MaterialValidationSelection.Verified &&
      this.material.InfectivityValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.ViralTiterValidation !==
        MaterialValidationSelection.Verified &&
      this.material.ViralTiterValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.GSDAnalysisResultValidation !==
        MaterialValidationSelection.Verified &&
      this.material.GSDAnalysisResultValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.GSDAnalysisResultDateValidation !==
        MaterialValidationSelection.Verified &&
      this.material.GSDAnalysisResultDateValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.GSDUploadingStatusValidation !==
        MaterialValidationSelection.Verified &&
      this.material.GSDUploadingStatusValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.GSDUploadingStatus == GSDUploadingStatus.Uploaded &&
      this.material.GSDUploadingDateValidation !==
        MaterialValidationSelection.Verified &&
      this.material.GSDUploadingDateValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.GeneticSequenceDataValidation !==
        MaterialValidationSelection.Verified &&
      this.material.GeneticSequenceDataValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.DatabaseUploadedByValidation !==
        MaterialValidationSelection.Verified &&
      this.material.DatabaseUploadedByValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.DatabaseAccessionIdValidation !==
        MaterialValidationSelection.Verified &&
      this.material.DatabaseAccessionIdValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.MaterialGSDInfoValidation !==
        MaterialValidationSelection.Verified &&
      this.material.MaterialGSDInfoValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.StrainDesignationValidation !==
        MaterialValidationSelection.Verified &&
      this.material.StrainDesignationValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    if (
      this.material.DescriptionValidation !==
        MaterialValidationSelection.Verified &&
      this.material.DescriptionValidation !==
        MaterialValidationSelection.Unverified
    ) {
      return false;
    }
    // if (
    //   this.material.SampleIdValidation !==
    //     MaterialValidationSelection.Verified &&
    //   this.material.SampleIdValidation !==
    //     MaterialValidationSelection.Unverified
    // ) {
    //   return false;
    // }

    return true;
  }

  get MaterialTimeline(): WorklistTimeline | undefined {
    return MaterialModule.MaterialTimeline;
  }

  setApprove(approve: boolean) {
    this.material.Approve = approve;
  }

  async onSave(): Promise<void> {
    this.$refs.materialForm.validate();
    this.setApprove(false);
    await MaterialModule.UpdateMaterialForLaboratoryCompletion()
      .then((response) => {
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  async onApprove(): Promise<void> {
    this.$refs.materialForm.validate();
    this.setApprove(true);
    await MaterialModule.UpdateMaterialForLaboratoryCompletion()
      .then((response) => {
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  onCancel(): void {
    MaterialModule.SET_ERROR(undefined);
    this.$router.back();
  }

  async loadPageInfo() {
    await CountryModule.ListCountries();
    await MaterialTypeModule.ListMaterialTypes();
    await MaterialProductModule.ListMaterialProducts();
    await TransportCategoryModule.ListTransportCategories();
    await TemperatureUnitOfMeasureModule.ListTemperatureUnitOfMeasures();
    await MaterialUsagePermissionModule.ListMaterialUsagePermissions();
    await GeneticSequenceDataModule.ListGeneticSequenceDatas();
    await InternationalTaxonomyClassificationModule.ListInternationalTaxonomyClassifications();
    await IsolationHostTypeModule.ListIsolationHostTypes();
    await CultivabilityTypeModule.ListCultivabilityTypes();
    await IsolationTechniqueTypeModule.ListIsolationTechniqueTypes();
    await LaboratoryModule.ListLaboratories();
    await BioHubFacilityModule.ListBioHubFacilities();
    await MaterialModule.ReadMaterialForLaboratoryCompletion(
      this.$route.params.id
    );
    await SpecimenTypeModule.ListSpecimenTypes();
    await MaterialModule.ListMaterialEvents(this.$route.params.id);
  }

  async mounted() {
    try {
      MaterialModule.CLEAR_MATERIAL();
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
