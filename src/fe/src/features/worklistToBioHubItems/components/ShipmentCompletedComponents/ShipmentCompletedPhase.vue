<template>
  <div>
    <v-card v-if="ShipmentCompleted" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <v-card-actions>
          <v-container class="px-0" fluid>
            <ShipmentCompletedMaterialsTable></ShipmentCompletedMaterialsTable>
            <h3>Damaged Materials</h3>
            <ShipmentMaterialsSection
              :can-edit="false"
              :only-damaged="true"
              :save-visible="false"
            >
            </ShipmentMaterialsSection>
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

import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import {
  GetWorklistToBioHubStatusPermission,
  hasPermission,
} from "../../../../utils/helper";
import { WorklistToBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistToBioHubDownloadPermissionsByStatus";
import { WorklistToBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistToBioHubReadPermissionsByStatus";
import { WorklistToBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistToBioHubSubmitPermissionsByStatus";

import ShipmentCompletedMaterialsTable from "./ShipmentCompletedMaterialsTable.vue";
import { IsolationHostTypeModule } from "../../../isolationHostTypes/store";
import { MaterialProductModule } from "../../../materialProducts/store";
import ShipmentMaterialsSection from "../WaitForArrivalConditionCheckComponents/ShipmentMaterialsSection.vue";
import { PermissionType } from "@/models/enums/PermissionType";
@Component({
  components: {
    ShipmentCompletedMaterialsTable,
    ShipmentMaterialsSection,
  },
})
export default class ShipmentCompletedPhase extends Vue {
  @Model("update", { type: Object }) model!: WorklistToBioHubItem;
  // Props

  get CurrentStatus(): WorklistToBioHubStatus {
    return this.model.CurrentStatus;
  }

  get ShipmentCompleted(): boolean {
    return this.model.CurrentStatus == WorklistToBioHubStatus.ShipmentCompleted;
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.DownloadFile,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }

    return hasPermission(requiredPermissionByStatus);
  }

  get hasReadPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Read,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }

    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Update,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  async mounted() {
    await IsolationHostTypeModule.ListIsolationHostTypes();
    await MaterialProductModule.ListMaterialProducts();
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
