<template>
  <div v-if="canRead">
    <div>
      <h3 class="mb-10">
        To see the details and take action for a SMTA request, please click the
        specific SMTA in the table
      </h3>
      <SMTA1WorkflowItemsTable :loading="loading" @selected="selectedSMTA1">
      </SMTA1WorkflowItemsTable>
    </div>
    <div>
      <SMTA2WorkflowItemsTable :loading="loading" @selected="selectedSMTA2">
      </SMTA2WorkflowItemsTable>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import SMTA1WorkflowItemsTable from "../SMTA1WorkflowItems/components/SMTA1WorkflowItemsTable.vue";
import SMTA2WorkflowItemsTable from "../SMTA2WorkflowItems/components/SMTA2WorkflowItemsTable.vue";

import { AppError } from "@/models/shared/Error";
import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";
import { SMTA2WorkflowItem } from "@/models/SMTA2WorkflowItem";

import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { SMTA1WorkflowItemModule } from "../SMTA1WorkflowItems/store";
import { SMTA2WorkflowItemModule } from "../SMTA2WorkflowItems/store";
import { SMTA1WorkflowItemGridItem } from "@/models/SMTA1WorkflowItemGridItem";
import { SMTA2WorkflowItemGridItem } from "@/models/SMTA2WorkflowItemGridItem";
import { getAreaFromRoleType } from "../../utils/helper";

@Component({
  components: { SMTA1WorkflowItemsTable, SMTA2WorkflowItemsTable },
})
export default class SMTARequestsPageIndex extends Vue {
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
    await SMTA1WorkflowItemModule.ListSMTA1WorkflowItems();
    await SMTA2WorkflowItemModule.ListSMTA2WorkflowItems();
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

  selectedSMTA1(item: SMTA1WorkflowItem): void {
    const area = getAreaFromRoleType();

    this.$router.push({
      name: area + "-smta1-workflow-details",
      params: { id: item.Id },
    });
  }

  selectedSMTA2(item: SMTA2WorkflowItem): void {
    const area = getAreaFromRoleType();

    this.$router.push({
      name: area + "-smta2-workflow-details",
      params: { id: item.Id },
    });
  }

  // async deleteToBioHubItem(item: SMTA1WorkflowItemGridItem): Promise<void> {}
}
</script>
