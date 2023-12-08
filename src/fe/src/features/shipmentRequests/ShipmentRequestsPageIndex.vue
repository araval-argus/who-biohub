<template>
  <div v-if="canRead">
    <div>
      <h3 class="mb-10">
        To see the details and take action for a shipment request, please click
        the specific shipment in the table
      </h3>
      <WorklistToBioHubItemsTable
        :loading="loading"
        @selected="selectedToBioHub"
      >
      </WorklistToBioHubItemsTable>
    </div>
    <div>
      <WorklistFromBioHubItemsTable
        :loading="loading"
        @selected="selectedFromBioHub"
      >
      </WorklistFromBioHubItemsTable>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import WorklistToBioHubItemsTable from "../worklistToBioHubItems/components/WorklistToBioHubItemsTable.vue";
import WorklistFromBioHubItemsTable from "../worklistFromBioHubItems/components/WorklistFromBioHubItemsTable.vue";

import { AppError } from "@/models/shared/Error";
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";

import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { WorklistToBioHubItemModule } from "../worklistToBioHubItems/store";
import { WorklistFromBioHubItemModule } from "../worklistFromBioHubItems/store";
import { WorklistToBioHubItemGridItem } from "@/models/WorklistToBioHubItemGridItem";
import { getAreaFromRoleType } from "../../utils/helper";

@Component({
  components: { WorklistToBioHubItemsTable, WorklistFromBioHubItemsTable },
})
export default class ShipmentRequestsPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return (
      hasPermission(PermissionNames.CanAccessWorklist) ||
      hasPermission(PermissionNames.CanAccessPastWorklist)
    );
  }

  async loadPageInfo() {
    await WorklistToBioHubItemModule.ListWorklistToBioHubItems();
    await WorklistFromBioHubItemModule.ListWorklistFromBioHubItems();
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

  selectedToBioHub(item: WorklistToBioHubItem): void {
    WorklistToBioHubItemModule.SET_WORKLISTTOBIOHUBITEM(item);
    const area = getAreaFromRoleType();

    this.$router.push({
      name: area + "-worklist-to-bio-hub-details",
      params: { id: item.Id },
    });
  }

  selectedFromBioHub(item: WorklistFromBioHubItem): void {
    WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM(item);
    const area = getAreaFromRoleType();

    this.$router.push({
      name: area + "-worklist-from-bio-hub-details",
      params: { id: item.Id },
    });
  }

  // async deleteToBioHubItem(item: WorklistToBioHubItemGridItem): Promise<void> {}
}
</script>
