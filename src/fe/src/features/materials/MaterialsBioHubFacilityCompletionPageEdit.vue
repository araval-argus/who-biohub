<template>
  <div>
    <WorklistTimelineComponent
      v-if="MaterialTimeline != undefined"
      timeline-title=""
      :worklist-timeline-events-days="MaterialTimeline.Events"
    >
    </WorklistTimelineComponent>
    <MaterialForm
      v-if="isMaterialSet && !loading"
      ref="materialForm"
      v-model="material"
      :readonly="!canEdit"
      title="Material Edit"
    >
      <CardActionsSaveCancel v-if="canEdit" @save="onSave" @cancel="onCancel">
      </CardActionsSaveCancel>

      <CardActionsGenericButton
        v-if="approveVisible && canApprove"
        text="Approve"
        @click="onApprove"
      >
      </CardActionsGenericButton>
    </MaterialForm>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";

import MaterialForm from "./components/MaterialForm.vue";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import CardActionsGenericButton from "../../components/CardActionsGenericButton.vue";

import { MaterialModule } from "./store";
import { Material } from "@/models/Material";
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
import { WorklistTimeline } from "@/models/WorklistTimeline";
import WorklistTimelineComponent from "@/components/WorklistTimelineComponent.vue";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";

@Component({
  components: {
    MaterialForm,
    CardActionsSaveCancel,
    CardActionsGenericButton,
  },
})
export default class MaterialsBioHubFacilityCompletionPageEdit extends Vue {
  $refs!: {
    materialForm: MaterialForm;
  };

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadMaterial);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditMaterial);
  }

  get canApprove(): boolean {
    return hasPermission(PermissionNames.CanApproveBioHubFacilityCompletion);
  }

  get isMaterialSet(): boolean {
    return MaterialModule.Material !== undefined;
  }

  get loading(): boolean {
    return AppModule.IsLoadingActive && !this.isMaterialSet;
  }

  get material(): Material {
    const lab = MaterialModule.Material;
    if (lab) return lab;

    throw { message: "" };
  }

  set material(lab: Material) {
    MaterialModule.SET_MATERIAL(lab);
  }

  get approveVisible(): boolean {
    if (
      this.material.ReferenceNumber === "" ||
      this.material.ReferenceNumber === undefined ||
      this.material.ReferenceNumber === null
    ) {
      return false;
    }

    if (
      this.material.Name === "" ||
      this.material.Name === undefined ||
      this.material.Name === null
    ) {
      return false;
    }
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

    return true;
  }

  get MaterialTimeline(): WorklistTimeline | undefined {
    return MaterialModule.MaterialTimeline;
  }

  private showLoading() {
    AppModule.ShowLoading();
  }

  setApprove(approve: boolean) {
    this.material.Approve = approve;
  }

  async onSave(): Promise<void> {
    this.$refs.materialForm.validate();
    this.setApprove(false);
    await MaterialModule.UpdateMaterialForBioHubFacilityCompletion()
      .then((response) => {
        MaterialModule.CLEAR_MATERIAL();
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  async onApprove(): Promise<void> {
    this.$refs.materialForm.validate();
    this.setApprove(true);
    await MaterialModule.UpdateMaterialForBioHubFacilityCompletion()
      .then((response) => {
        MaterialModule.CLEAR_MATERIAL();
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  // get LaboratoryArea(): boolean {
  //   return this.$route.name === "laboratoryarea-material-edit";
  // }

  // get BioHubFacilityArea(): boolean {
  //   return this.$route.name === "biohubfacilityarea-material-edit";
  // }

  onCancel(): void {
    MaterialModule.SET_ERROR(undefined);
    MaterialModule.CLEAR_MATERIAL();
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
    await MaterialModule.ReadMaterialForBioHubFacilityCompletion(
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

  // @Watch("isMaterialSet")
  // MaterialSet() {
  //   if (this.isMaterialSet == true && !this.loading) {
  //     AppModule.HideLoading();
  //   }
  // }
}
</script>
