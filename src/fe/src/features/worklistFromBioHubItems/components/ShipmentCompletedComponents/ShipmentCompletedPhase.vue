<template>
  <div>
    <v-card v-if="ShipmentCompleted" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <v-card-actions>
          <v-container class="px-0" fluid>
            <ShipmentCompletedMaterialsTable></ShipmentCompletedMaterialsTable>
          </v-container>
        </v-card-actions>
      </v-card-text>
      <v-card-text v-else>
        <v-card-actions>
          <v-spacer></v-spacer>
          <p>No action should be taken from your side at this stage</p>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";

import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import {
  GetWorklistFromBioHubStatusPermission,
  hasPermission,
} from "../../../../utils/helper";
import { WorklistFromBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubSubmitPermissionsByStatus";
import ShipmentCompletedMaterialsTable from "./ShipmentCompletedMaterialsTable.vue";
import { PermissionType } from "@/models/enums/PermissionType";

@Component({
  components: {
    ShipmentCompletedMaterialsTable,
  },
})
export default class ShipmentCompletedPhase extends Vue {
  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;
  // Props

  get CurrentStatus(): WorklistFromBioHubStatus {
    return this.model.CurrentStatus;
  }

  get ShipmentCompleted(): boolean {
    return (
      this.model.CurrentStatus == WorklistFromBioHubStatus.ShipmentCompleted
    );
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistFromBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Update,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}

.signature-hover {
  cursor: pointer;
}
</style>
