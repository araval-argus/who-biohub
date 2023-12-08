<template>
  <div>
    <Annex2OfSMTA1Phase
      v-model="model"
      v-if="SubmitAnnex2OfSMTA1 || WaitingForAnnex2OfSMTA1SECsApproval"
      @submit="submit"
      @saveAsDraft="saveAsDraft"
      @downloadFile="downloadFile"
    >
    </Annex2OfSMTA1Phase>
    <BookingFormPhase
      v-model="model"
      v-if="SubmitBookingFormOfSMTA1 || WaitForBookingFormSMTA1OPSApproval"
      @submit="submit"
      @saveAsDraft="saveAsDraft"
      @downloadFile="downloadFile"
    >
    </BookingFormPhase>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import Annex2OfSMTA1Phase from "./Annex2ofSMTA1Components/Annex2OfSMTA1Phase.vue";
import BookingFormPhase from "./BookingFormComponents/BookingFormPhase.vue";

@Component({
  components: {
    Annex2OfSMTA1Phase,
    BookingFormPhase,
  },
})
export default class PreShipmentPhase extends Vue {
  @Model("update", { type: Object }) model!: WorklistToBioHubItem;
  // Props

  get CurrentStatus(): WorklistToBioHubStatus {
    return this.model.CurrentStatus;
  }

  get SubmitAnnex2OfSMTA1(): boolean {
    return (
      this.model.CurrentStatus == WorklistToBioHubStatus.SubmitAnnex2OfSMTA1
    );
  }

  get WaitingForAnnex2OfSMTA1SECsApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval
    );
  }

  get SubmitBookingFormOfSMTA1(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.SubmitBookingFormOfSMTA1
    );
  }

  get WaitForBookingFormSMTA1OPSApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval
    );
  }

  update() {
    this.$emit("update", this.model);
  }

  setSaveAsDraft(saveAsDraft: boolean) {
    this.model.IsSaveDraft = saveAsDraft;
    this.$emit("update", this.model);
  }

  downloadFile() {
    this.$emit("downloadFile");
  }

  submit() {
    this.setSaveAsDraft(false);
    this.$emit("submit");
  }

  saveAsDraft() {
    this.setSaveAsDraft(true);
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

.shipment-action-vcard {
  margin-left: 12px;
  margin-top: 20px;
}
</style>
