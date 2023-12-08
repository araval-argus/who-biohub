<template>
  <div>
    <ShipmentCompletedPhase
      v-model="model"
      v-if="ShipmentCompleted"
    ></ShipmentCompletedPhase>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";

import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import ShipmentCompletedPhase from "./ShipmentCompletedComponents/ShipmentCompletedPhase.vue";

@Component({
  components: {
    ShipmentCompletedPhase,
  },
})
export default class PostShipmentPhase extends Vue {
  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;
  // Props

  get CurrentStatus(): WorklistFromBioHubStatus {
    return this.model.CurrentStatus;
  }

  get worklistItemTitle(): string {
    return this.model.WorklistItemTitle;
  }

  get ShipmentCompleted(): boolean {
    return (
      this.model.CurrentStatus == WorklistFromBioHubStatus.ShipmentCompleted
    );
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
