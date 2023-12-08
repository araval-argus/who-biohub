<template>
  <div v-if="canRead">
    <span v-if="error"> Error retrieving couriers: {{ error }} </span>
    <CouriersTable
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
    </CouriersTable>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import CouriersTable from "./components/CouriersTable.vue";
import { CourierModule } from "./store";
import { AppError } from "@/models/shared/Error";
import { Courier } from "@/models/Courier";
import { CountryModule } from "../countries/store";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { CouriersTable } })
export default class CouriersPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  async loadPageInfo() {
    await CountryModule.ListCountries();
    await CourierModule.ListCouriers();
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
    return CourierModule.Error;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadCourier);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateCourier);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditCourier);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteCourier);
  }

  editItem(item: Courier): void {
    CourierModule.SET_COURIER(item);
    this.$router.push({
      name: "whoarea-courier-edit",
      params: { id: item.Id },
    });
  }

  selected(item: Courier): void {
    CourierModule.SET_COURIER(item);

    this.$router.push({
      name: "whoarea-courier-details",
      params: { id: item.Id },
    });
  }

  create(): void {
    this.$router.push({
      name: "whoarea-courier-create",
    });
  }

  async deleteItem(item: Courier): Promise<void> {
    CourierModule.SET_COURIER(item);
    await CourierModule.DeleteCourier();
    await CourierModule.ListCouriers();
    return;
  }
}
</script>
