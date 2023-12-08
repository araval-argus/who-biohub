<template>
  <div>
    <MaterialFormPublic
      v-if="!loading"
      v-model="material"
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

@Component({ components: { MaterialFormPublic } })
export default class MaterialsPageDetailsPublic extends Vue {
  get material(): MaterialPublic | undefined {
    return MaterialModule.MaterialPublic;
  }

  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get isPublicPage(): boolean {
    return true;
  }

  get providerId(): string {
    return getMaterialProviderId(this.$route.params.providerId);
  }

  async loadPageInfo() {
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

  async mounted() {
    try {
      MaterialModule.CLEAR_MATERIAL_PUBLIC();
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
