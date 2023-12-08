<template>
  <div>
    <WorklistTimelineComponent
      v-if="MaterialTimeline != undefined"
      timeline-title=""
      :worklist-timeline-events-days="MaterialTimeline.Events"
    >
    </WorklistTimelineComponent>
    <MaterialForm
      v-if="isMaterialSet && !loading && canEdit"
      ref="materialForm"
      v-model="material"
      :is-public-page="false"
      :is-laboratory-bmepp-page="isLaboratoryBmeppPage"
      :laboratory-area="LaboratoryArea"
      :bio-hub-facility-area="BioHubFacilityArea"
      :provider-id="providerId"
      title="Material Edit"
    >
      <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
      </CardActionsSaveCancel>
    </MaterialForm>
  </div>
</template>

<script lang="ts">
import MaterialForm from "./components/MaterialForm.vue";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { Component, Vue } from "vue-property-decorator";
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
import { SpecimenTypeModule } from "../specimenTypes/store";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import WorklistTimelineComponent from "@/components/WorklistTimelineComponent.vue";

@Component({
  components: {
    MaterialForm,
    CardActionsSaveCancel,
    WorklistTimelineComponent,
  },
})
export default class MaterialsPageEdit extends Vue {
  $refs!: {
    materialForm: MaterialForm;
  };

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadMaterial);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateMaterial);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditMaterial);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteMaterial);
  }

  get isMaterialSet(): boolean {
    return MaterialModule.Material !== undefined;
  }

  get loading(): boolean {
    return AppModule.IsLoadingActive && !this.isMaterialSet;
  }

  get MaterialTimeline(): WorklistTimeline | undefined {
    return MaterialModule.MaterialTimeline;
  }

  get material(): Material {
    const lab = MaterialModule.Material;
    if (lab) return lab;

    throw { message: "" };
  }

  set material(lab: Material) {
    MaterialModule.SET_MATERIAL(lab);
  }

  async onSave(): Promise<void> {
    this.$refs.materialForm.validate();
    await MaterialModule.UpdateMaterial()
      .then((response) => {
        MaterialModule.CLEAR_MATERIAL();
        this.$router.back();
      })
      .catch((err) => {
        console.log(err);
      });
  }

  get isLaboratoryBmeppPage(): boolean {
    if (this.$route.name == "laboratoryarea-material-edit") {
      return true;
    }
    return false;
  }

  get LaboratoryArea(): boolean {
    return this.$route.name === "laboratoryarea-material-edit";
  }

  get BioHubFacilityArea(): boolean {
    return this.$route.name === "biohubfacilityarea-material-edit";
  }

  get providerId(): string {
    return getMaterialProviderId(this.$route.params.providerId);
  }

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
    await MaterialModule.ReadMaterial(this.$route.params.id);
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
