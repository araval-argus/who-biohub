<template>
  <div v-if="!isPublicPage && canRead">
    <WorklistTimelineComponent
      v-if="MaterialTimeline != undefined"
      timeline-title=""
      :worklist-timeline-events-days="MaterialTimeline.Events"
    >
    </WorklistTimelineComponent>
    <MaterialForm
      v-if="!loading"
      v-model="material"
      :is-public-page="isPublicPage"
      :laboratory-area="LaboratoryArea"
      :provider-id="providerId"
      title="Material Details"
      readonly
    >
    </MaterialForm>
  </div>
  <div v-else>
    <MaterialFormPublic
      v-if="!loading"
      v-model="material"
      :is-public-page="isPublicPage"
      :laboratory-area="LaboratoryArea"
      :provider-id="providerId"
      title="Material Details"
      readonly
    >
    </MaterialFormPublic>
  </div>
</template>

<script lang="ts">
import MaterialForm from "./components/MaterialForm.vue";
import MaterialFormPublic from "./components/MaterialFormPublic.vue";
import { Component, Vue } from "vue-property-decorator";
import { Material } from "@/models/Material";
import { MaterialPublic } from "@/models/MaterialPublic";
import { MaterialModule } from "./store";
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
  components: { MaterialForm, MaterialFormPublic, WorklistTimelineComponent },
})
export default class MaterialsPageDetails extends Vue {
  get material(): Material | MaterialPublic | undefined {
    if (this.isPublicPage) {
      return MaterialModule.MaterialPublic;
    } else {
      return MaterialModule.Material;
    }
  }

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

  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get isPublicPage(): boolean {
    if (
      this.$route.name == "public-material-details" ||
      this.$route.name == "public-material-provider-details"
    ) {
      return true;
    }
    return false;
  }

  get LaboratoryArea(): boolean {
    return (
      this.$route.name === "laboratoryarea-material-details-bmepp" ||
      this.$route.name === "laboratoryarea-material-details-bmepp-catalogue"
    );
  }

  get providerId(): string {
    return getMaterialProviderId(this.$route.params.providerId);
  }

  get MaterialTimeline(): WorklistTimeline | undefined {
    return MaterialModule.MaterialTimeline;
  }

  async loadPublicPageInfo() {
    await CountryModule.ListCountriesPublic();
    await MaterialTypeModule.ListMaterialTypesPublic();
    await MaterialProductModule.ListMaterialProductsPublic();
    await TransportCategoryModule.ListTransportCategoriesPublic();
    await TemperatureUnitOfMeasureModule.ListTemperatureUnitOfMeasuresPublic();
    await MaterialUsagePermissionModule.ListMaterialUsagePermissionsPublic();
    await GeneticSequenceDataModule.ListGeneticSequenceDatasPublic();
    await InternationalTaxonomyClassificationModule.ListInternationalTaxonomyClassificationsPublic();
    await IsolationHostTypeModule.ListIsolationHostTypesPublic();
    await LaboratoryModule.ListLaboratoriesPublic();
    await BioHubFacilityModule.ListBioHubFacilitiesPublic();
    await MaterialModule.ReadMaterialPublic(this.$route.params.id);
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
      if (this.isPublicPage == true) {
        await this.loadPublicPageInfo();
      } else {
        MaterialModule.CLEAR_MATERIAL();
        await this.loadPageInfo();
      }
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
