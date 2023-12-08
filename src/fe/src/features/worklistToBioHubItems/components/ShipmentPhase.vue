<template>
  <div>
    <PickupDeliveryPhase
      v-model="model"
      v-if="WaitForPickUpCompleted || WaitForDeliveryCompleted"
      @submit="submit"
      @saveAsDraft="saveAsDraft"
      @downloadFile="downloadFile"
    ></PickupDeliveryPhase>

    <WaitForArrivalConditionCheckPhase
      v-model="model"
      v-if="
        WaitForArrivalConditionCheck ||
        WaitForCommentBHFSendFeedback ||
        WaitForFinalApproval
      "
      @submit="submit"
      @saveAsDraft="saveAsDraft"
      @downloadFile="downloadFile"
    ></WaitForArrivalConditionCheckPhase>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";

import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import PickupDeliveryPhase from "./PickupDeliveryComponents/PickupDeliveryPhase.vue";
import WaitForArrivalConditionCheckPhase from "./WaitForArrivalConditionCheckComponents/WaitForArrivalConditionCheckPhase.vue";

@Component({
  components: {
    PickupDeliveryPhase,
    WaitForArrivalConditionCheckPhase,
  },
})
export default class ShipmentPhase extends Vue {
  @Model("update", { type: Object }) model!: WorklistToBioHubItem;
  // Props

  get CurrentStatus(): WorklistToBioHubStatus {
    return this.model.CurrentStatus;
  }

  get worklistItemTitle(): string {
    return this.model.WorklistItemTitle;
  }

  get SubmitSMTA1ShipmentDocuments(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments
    );
  }

  get WaitForPickUpCompleted(): boolean {
    return (
      this.model.CurrentStatus == WorklistToBioHubStatus.WaitForPickUpCompleted
    );
  }

  get WaitForDeliveryCompleted(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.WaitForDeliveryCompleted
    );
  }

  get WaitForArrivalConditionCheck(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.WaitForArrivalConditionCheck
    );
  }

  get WaitForCommentBHFSendFeedback(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.WaitForCommentBHFSendFeedback
    );
  }

  get WaitForFinalApproval(): boolean {
    return (
      this.model.CurrentStatus == WorklistToBioHubStatus.WaitForFinalApproval
    );
  }

  update() {
    this.$emit("update", this.model);
  }

  downloadFile() {
    this.$emit("downloadFile");
  }

  submit() {
    this.$emit("submit");
  }

  saveAsDraft() {
    this.$emit("saveAsDraft");
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
