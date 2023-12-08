<template>
  <div v-if="!isPublicBmeppCataloguePage && canRead">
    <div>
      <MaterialsTable
        :is-public-bmepp-catalogue-page="isPublicBmeppCataloguePage"
        :is-laboratory-bmepp-page="isLaboratoryBmeppPage"
        :is-laboratory-bmepp-catalogue-page="isLaboratoryBmeppCataloguePage"
        :filter-by-provider-id="filterByProviderId"
        :provider-id="providerId"
        :bio-hub-facility-area="BioHubFacilityArea"
        :loading="loading"
        :can-create="canCreate"
        :can-edit="canEdit"
        :can-delete="canDelete"
        @selected="selected"
        @create="create"
        @edit="editItem"
        @delete="deleteItem"
      >
      </MaterialsTable>
    </div>
  </div>
  <div v-else>
    <v-skeleton-loader v-if="loading"> </v-skeleton-loader>
    <div v-else>
      <span v-if="error"> Error retrieving Materials: {{ error }} </span>
      <MaterialsTablePublic
        v-else
        :is-public-bmepp-catalogue-page="isPublicBmeppCataloguePage"
        :is-laboratory-bmepp-page="isLaboratoryBmeppPage"
        :is-laboratory-bmepp-catalogue-page="isLaboratoryBmeppCataloguePage"
        :filter-by-provider-id="filterByProviderId"
        :provider-id="providerId"
        :bio-hub-facility-area="BioHubFacilityArea"
        :loading="loading"
        @selected="selected"
        @create="create"
        @edit="editItem"
        @delete="deleteItem"
      >
      </MaterialsTablePublic>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import MaterialsTable from "./components/MaterialsTable.vue";
import MaterialsTablePublic from "./components/MaterialsTablePublic.vue";
import { MaterialModule } from "./store";
import { InternationalTaxonomyClassificationModule } from "../internationalTaxonomyClassifications/store";
import { LaboratoryModule } from "../laboratories/store";
import { CountryModule } from "../countries/store";
import { AppError } from "@/models/shared/Error";
import { Material } from "@/models/Material";
import { getMaterialProviderId } from "@/utils/helper";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { MaterialStatus } from "@/models/enums/MaterialStatus";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";

@Component({ components: { MaterialsTable, MaterialsTablePublic } })
export default class MaterialsPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
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

  async loadPageInfo() {
    await InternationalTaxonomyClassificationModule.ListInternationalTaxonomyClassifications();
    await LaboratoryModule.ListLaboratories();
    await CountryModule.ListCountries();
    await MaterialModule.ListMaterials();
  }

  async loadPublicPageInfo() {
    await InternationalTaxonomyClassificationModule.ListInternationalTaxonomyClassificationsPublic();
    await CountryModule.ListCountriesPublic();
    await LaboratoryModule.ListLaboratoriesPublic();
    await MaterialModule.ListMaterialsPublic();
  }

  async mounted() {
    try {
      if (this.isPublicBmeppCataloguePage == false) {
        await this.loadPageInfo();
      } else {
        await this.loadPublicPageInfo();
      }
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    AppModule.HideLoading();
  }

  get detailRouting(): Map<string, string> {
    var dictionary = new Map<string, string>();
    dictionary.set("public-materials", "public-material-details");
    dictionary.set(
      "public-materials-provider",
      "public-material-provider-details"
    );

    dictionary.set(
      "laboratoryarea-materials-bmepp",
      "laboratoryarea-material-details-bmepp-catalogue"
    );

    dictionary.set(
      "laboratoryarea-materials-bmepp-catalogue",
      "laboratoryarea-material-details-bmepp-catalogue"
    );
    dictionary.set(
      "laboratoryarea-materials-bmepp-catalogue-provider",
      "laboratoryarea-material-details-bmepp-catalogue"
    );

    return dictionary;
  }

  get editRouting(): Map<string, string> {
    var dictionary = new Map<string, string>();

    return dictionary;
  }

  get createRouting(): Map<string, string> {
    var dictionary = new Map<string, string>();

    dictionary.set(
      "laboratoryarea-materials-bmepp",
      "laboratoryarea-material-create"
    );
    dictionary.set(
      "biohubfacilityarea-materials-bmepp",
      "biohubfacilityarea-material-create"
    );
    dictionary.set("whoarea-materials", "whoarea-material-create");
    return dictionary;
  }

  get routingNamePrefix(): string {
    var routeName = this.$route.name;
    if (routeName != null && routeName != undefined) {
      if (routeName.startsWith("who")) {
        return "whoarea-";
      } else if (routeName.startsWith("laboratory")) {
        return "laboratoryarea-";
      } else {
        return "biohubfacilityarea-";
      }
    }

    return "laboratoryarea-";
  }

  get providerId(): string {
    return getMaterialProviderId(this.$route.params.providerId);
  }
  get filterByProviderId(): boolean {
    return (
      this.$route.params.providerId != undefined &&
      this.$route.params.providerId != null &&
      this.$route.params.providerId != ""
    );
  }

  get BioHubFacilityArea(): boolean {
    return this.$route.name == "biohubfacilityarea-materials-bmepp";
  }

  get isLaboratoryBmeppPage(): boolean {
    if (this.$route.name == "laboratoryarea-materials-bmepp") {
      return true;
    }
    return false;
  }

  get isLaboratoryBmeppCataloguePage(): boolean {
    if (
      this.$route.name == "laboratoryarea-materials-bmepp-catalogue" ||
      this.$route.name == "laboratoryarea-materials-bmepp-catalogue-provider"
    ) {
      return true;
    }
    return false;
  }

  get isPublicBmeppCataloguePage(): boolean {
    if (
      this.$route.name == "public-materials" ||
      this.$route.name == "public-materials-provider"
    ) {
      return true;
    }
    return false;
  }

  get isFromPublicBmeppLink(): boolean {
    if (this.$route.name == "public-materials-provider") {
      return true;
    }
    return false;
  }

  get error(): AppError | undefined {
    return MaterialModule.Error;
  }

  editItem(item: Material): void {
    let destinationStatusRoute = "material-edit";
    if (item.Status == MaterialStatus.WaitingForBioHubFacilityCompletion) {
      destinationStatusRoute = "material-edit-biohubcompletion";
    } else if (item.Status == MaterialStatus.WaitingForLaboratoryCompletion) {
      destinationStatusRoute = "material-edit-laboratorycompletion";
    }

    if (
      item.ShipmentMaterialCondition == ShipmentMaterialCondition.Damaged &&
      !hasPermission(PermissionNames.CanEditMaterialShipmentInformation)
    ) {
      destinationStatusRoute = "material-details-bmepp";
    }

    var customParams = {};
    if (this.providerId == "") {
      customParams = {
        id: item.Id,
      };
    } else {
      customParams = {
        id: item.Id,
        providerId: this.providerId,
      };
    }
    if (this.$route.name != null && this.$route.name != undefined) {
      var route = this.editRouting.get(this.$route.name);
      if (route != null && route != undefined) {
        this.$router.push({
          name: route,
          params: customParams,
        });
      } else {
        this.$router.push({
          name: this.routingNamePrefix + destinationStatusRoute,
          params: customParams,
        });
      }
    } else {
      this.$router.push({
        name: this.routingNamePrefix + destinationStatusRoute,
        params: customParams,
      });
    }
  }

  selected(item: Material): void {
    let destinationStatusRoute = "material-details-bmepp";
    if (item.Status == MaterialStatus.WaitingForBioHubFacilityCompletion) {
      destinationStatusRoute = "material-details-biohubcompletion";
    } else if (item.Status == MaterialStatus.WaitingForLaboratoryCompletion) {
      destinationStatusRoute = "material-details-laboratorycompletion";
    }

    var customParams = {};
    if (this.providerId == "") {
      customParams = {
        id: item.Id,
      };
    } else {
      customParams = {
        id: item.Id,
        providerId: this.providerId,
      };
    }

    if (this.$route.name != null && this.$route.name != undefined) {
      var route = this.detailRouting.get(this.$route.name);
      if (route != null && route != undefined) {
        this.$router.push({
          name: route,
          params: customParams,
        });
      } else {
        this.$router.push({
          name: this.routingNamePrefix + destinationStatusRoute,
          params: customParams,
        });
      }
    } else {
      this.$router.push({
        name: this.routingNamePrefix + destinationStatusRoute,
        params: customParams,
      });
    }
  }

  create(): void {
    if (this.$route.name != null && this.$route.name != undefined) {
      var route = this.createRouting.get(this.$route.name);
      if (route != null && route != undefined) {
        this.$router.push({
          name: route,
          params: {
            providerId: this.providerId,
          },
        });
      } else {
        this.$router.push({
          name: this.routingNamePrefix + "material-create",
        });
      }
    } else {
      this.$router.push({
        name: this.routingNamePrefix + "material-create",
      });
    }
  }

  async deleteItem(item: Material): Promise<void> {
    MaterialModule.SET_MATERIAL(item);
    await MaterialModule.DeleteMaterial();
    await MaterialModule.ListMaterials();
    return;
  }
}
</script>
