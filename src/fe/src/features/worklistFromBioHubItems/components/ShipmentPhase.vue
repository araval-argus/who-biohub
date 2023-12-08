<template>
  <div>
    <PickupDeliveryPhase
      v-model="model"
      v-if="WaitForPickUpCompleted || WaitForDeliveryCompleted"
      @submit="submit"
      @saveAsDraft="saveAsDraft"
      @downloadFile="downloadFile"
    >
    </PickupDeliveryPhase>

    <WaitForArrivalConditionCheckPhase
      v-model="model"
      v-if="
        WaitForArrivalConditionCheck ||
        WaitForCommentQESendFeedback ||
        WaitForFinalApproval
      "
      @submit="submit"
      @saveAsDraft="saveAsDraft"
      @downloadFile="downloadFile"
    >
    </WaitForArrivalConditionCheckPhase>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import PickupDeliveryPhase from "./PickupDeliveryComponents/PickupDeliveryPhase.vue";
import WaitForArrivalConditionCheckPhase from "./WaitForArrivalConditionCheckComponents/WaitForArrivalConditionCheckPhase.vue";

@Component({
  components: {
    PickupDeliveryPhase,
    WaitForArrivalConditionCheckPhase,
  },
})
export default class ShipmentPhase extends Vue {
  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;
  // Props

  get CurrentStatus(): WorklistFromBioHubStatus {
    return this.model.CurrentStatus;
  }

  get worklistItemTitle(): string {
    return this.model.WorklistItemTitle;
  }

  get SubmitBHFSMTA2ShipmentDocuments(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments
    );
  }

  get SubmitQESMTA2ShipmentDocuments(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments
    );
  }

  get WaitForPickUpCompleted(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForPickUpCompleted
    );
  }

  get WaitForDeliveryCompleted(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForDeliveryCompleted
    );
  }

  get WaitForArrivalConditionCheck(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForArrivalConditionCheck
    );
  }

  get WaitForCommentQESendFeedback(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForCommentQESendFeedback
    );
  }

  get WaitForFinalApproval(): boolean {
    return (
      this.model.CurrentStatus == WorklistFromBioHubStatus.WaitForFinalApproval
    );
  }

  get ShipmentCompleted(): boolean {
    return (
      this.model.CurrentStatus == WorklistFromBioHubStatus.ShipmentCompleted
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
