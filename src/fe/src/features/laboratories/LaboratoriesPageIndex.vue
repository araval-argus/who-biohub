<template>
  <div>
    <LaboratoriesTable
      :loading="loading"
      :can-create="canCreate"
      :can-edit="canEdit"
      :can-delete="canDelete"
      @selected="selected"
      @create="create"
      @edit="editItem"
      @delete="deleteItem"
    >
    </LaboratoriesTable>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import LaboratoriesTable from "./components/LaboratoriesTable.vue";
import { LaboratoryModule } from "./store";
import { AppError } from "@/models/shared/Error";
import { Laboratory } from "@/models/Laboratory";
import { BSLLevelModule } from "../bsllevels/store";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { CountryModule } from "../countries/store";

@Component({ components: { LaboratoriesTable } })
export default class LaboratoriesPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get detailRouting(): Map<string, string> {
    var dictionary = new Map<string, string>();
    dictionary.set("whoarea-laboratories", "whoarea-laboratory-details");
    dictionary.set(
      "biohubfacilityarea-laboratories",
      "biohubfacilityarea-laboratory-details"
    );

    return dictionary;
  }

  async loadPageInfo() {
    await BSLLevelModule.ListBSLLevels();
    await CountryModule.ListCountries();
    await LaboratoryModule.ListLaboratories();
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

  get canRead(): boolean {
    return (
      hasPermission(PermissionNames.CanReadLaboratory) &&
      hasPermission(PermissionNames.CanAccessWHOLaboratories)
    );
  }

  get canCreate(): boolean {
    return (
      hasPermission(PermissionNames.CanCreateLaboratory) &&
      hasPermission(PermissionNames.CanAccessWHOLaboratories)
    );
  }

  get canEdit(): boolean {
    return (
      hasPermission(PermissionNames.CanEditLaboratory) &&
      hasPermission(PermissionNames.CanAccessWHOLaboratories)
    );
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteLaboratory);
  }

  get error(): AppError | undefined {
    return LaboratoryModule.Error;
  }

  editItem(item: Laboratory): void {
    LaboratoryModule.SET_LABORATORY(item);
    this.$router.push({
      name: "whoarea-laboratory-edit",
      params: { id: item.Id },
    });
  }

  selected(item: Laboratory): void {
    LaboratoryModule.SET_LABORATORY(item);
    if (this.$route.name != null && this.$route.name != undefined) {
      var route = this.detailRouting.get(this.$route.name);
      if (route != null && route != undefined) {
        this.$router.push({
          name: route,
          params: { id: item.Id },
        });
      } else {
        this.$router.push({
          name: "whoarea-laboratory-details",
          params: { id: item.Id },
        });
      }
    } else {
      this.$router.push({
        name: "whoarea-laboratory-details",
        params: { id: item.Id },
      });
    }
  }

  create(): void {
    this.$router.push({
      name: "whoarea-laboratory-create",
    });
  }

  async deleteItem(item: Laboratory): Promise<void> {
    LaboratoryModule.SET_LABORATORY(item);
    await LaboratoryModule.DeleteLaboratory();
    await LaboratoryModule.ListLaboratories();
    return;
  }
}
</script>
