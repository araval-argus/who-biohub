<template>
  <v-container v-if="canCreate" fluid>
    <MaterialForm
      ref="materialForm"
      v-model="material"
      :is-public-page="false"
      :is-laboratory-bmepp-page="isLaboratoryBmeppPage"
      :laboratory-area="LaboratoryArea"
      :provider-id="providerId"
      title="Create Material"
    >
      <CardActionsSaveCancel @save="onSave" @cancel="onCancel">
      </CardActionsSaveCancel>
    </MaterialForm>
  </v-container>
</template>

<script lang="ts">
import MaterialForm from "./components/MaterialForm.vue";
import CardActionsSaveCancel from "@/components/CardActionsSaveCancel.vue";
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
import { AppModule } from "./../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { Readiness } from "@/models/enums/Readiness";

@Component({ components: { MaterialForm, CardActionsSaveCancel } })
export default class MaterialsPageCreate extends Vue {
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

  get isLaboratoryBmeppPage(): boolean {
    if (this.$route.name == "laboratoryarea-material-create") {
      return true;
    }
    return false;
  }

  get LaboratoryArea(): boolean {
    return this.$route.name === "laboratoryarea-material-create";
  }

  get providerId(): string {
    return getMaterialProviderId(this.$route.params.providerId);
  }

  get material(): Material {
    return MaterialModule.MaterialCreate;
  }

  set material(material: Material) {
    MaterialModule.SET_MATERIAL_CREATE(material);
  }

  async onSave(): Promise<void> {
    this.$refs.materialForm.validate();
    await MaterialModule.CreateMaterial()
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
  }

  async mounted() {
    MaterialModule.CLEAR_MATERIAL_CREATE();
    this.material.ReadyToShare = true;
    this.material.BHFShareReadiness = Readiness.Ready;
    try {
      if (AuthModule.UserToken == undefined) {
        await AuthModule.setUserTokenAsync();
      }
      if (AuthModule.IsLogged == false) {
        await AuthModule.setUserInfoAsync().then(async () => {
          await this.loadPageInfo();
        });
      } else {
        await this.loadPageInfo();
      }

      //Temporary workaround
      if (
        this.material.ProviderLaboratoryId != null &&
        this.material.ProviderLaboratoryId != "" &&
        this.material.ProviderLaboratoryId != undefined
      ) {
        this.$refs.materialForm.updateProvider(
          this.material.ProviderLaboratoryId
        );
      } else if (
        this.material.ProviderBioHubFacilityId != null &&
        this.material.ProviderBioHubFacilityId != "" &&
        this.material.ProviderBioHubFacilityId != undefined
      ) {
        this.$refs.materialForm.updateProvider(
          this.material.ProviderBioHubFacilityId
        );
      }
    } finally {
      AppModule.HideLoading();
    }
  }
}
</script>
