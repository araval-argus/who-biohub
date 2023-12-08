<template>
  <div v-if="canRead">
    <span v-if="error"> Error retrieving bioHubFacilities: {{ error }} </span>
    <BioHubFacilitiesTable
      v-else
      :loading="loading"
      :can-create="canCreate"
      :can-edit="canEdit"
      :can-delete="canDelete"
      @selected="selected"
      @create="create"
      @edit="editItem"
      @delete="deleteItem"
    >
    </BioHubFacilitiesTable>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import BioHubFacilitiesTable from "./components/BioHubFacilitiesTable.vue";
import { BioHubFacilityModule } from "./store";
import { AppError } from "@/models/shared/Error";
import { BioHubFacility } from "@/models/BioHubFacility";
import { BSLLevelModule } from "../bsllevels/store";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { CountryModule } from "../countries/store";

@Component({ components: { BioHubFacilitiesTable } })
export default class BioHubFacilitiesPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  async loadPageInfo() {
    await BSLLevelModule.ListBSLLevels();
    await CountryModule.ListCountries();
    await BioHubFacilityModule.ListBioHubFacilities();
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    AppModule.HideLoading();
  }

  get error(): AppError | undefined {
    return BioHubFacilityModule.Error;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadBioHubFacility);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateBioHubFacility);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditBioHubFacility);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteBioHubFacility);
  }

  editItem(item: BioHubFacility): void {
    BioHubFacilityModule.SET_BIOHUBFACILITY(item);
    this.$router.push({
      name: "whoarea-biohubfacility-edit",
      params: { id: item.Id },
    });
  }

  selected(item: BioHubFacility): void {
    BioHubFacilityModule.SET_BIOHUBFACILITY(item);

    this.$router.push({
      name: "whoarea-biohubfacility-details",
      params: { id: item.Id },
    });
  }

  create(): void {
    this.$router.push({
      name: "whoarea-biohubfacility-create",
    });
  }

  async deleteItem(item: BioHubFacility): Promise<void> {
    BioHubFacilityModule.SET_BIOHUBFACILITY(item);
    await BioHubFacilityModule.DeleteBioHubFacility();
    await BioHubFacilityModule.ListBioHubFacilities();
    return;
  }
}
</script>
