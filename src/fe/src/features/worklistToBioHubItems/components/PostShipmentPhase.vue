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

import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import ShipmentCompletedPhase from "./ShipmentCompletedComponents/ShipmentCompletedPhase.vue";

@Component({
  components: {
    ShipmentCompletedPhase,
  },
})
export default class PostShipmentPhase extends Vue {
  @Model("update", { type: Object }) model!: WorklistToBioHubItem;
  // Props

  get CurrentStatus(): WorklistToBioHubStatus {
    return this.model.CurrentStatus;
  }

  get ShipmentCompleted(): boolean {
    return this.model.CurrentStatus == WorklistToBioHubStatus.ShipmentCompleted;
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
