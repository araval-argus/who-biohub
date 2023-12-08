<template>
  <div>
    <Annex2OfSMTA2Phase
      v-model="model"
      v-if="SubmitAnnex2OfSMTA2 || WaitingForAnnex2OfSMTA2SECsApproval"
      @submit="submit"
      @saveAsDraft="saveAsDraft"
      @downloadFile="downloadFile"
    >
    </Annex2OfSMTA2Phase>

    <BiosafetyChecklistPhase
      v-model="model"
      v-if="
        SubmitBiosafetyChecklistFormOfSMTA2 ||
        WaitForBiosafetyChecklistFormSMTA2BSFsApproval
      "
      @submit="submit"
      @saveAsDraft="saveAsDraft"
      @downloadFile="downloadFile"
    >
    </BiosafetyChecklistPhase>

    <BookingFormPhase
      v-model="model"
      v-if="SubmitBookingFormOfSMTA2 || WaitForBookingFormSMTA2OPSsApproval"
      @submit="submit"
      @saveAsDraft="saveAsDraft"
      @downloadFile="downloadFile"
    >
    </BookingFormPhase>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";

import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import Annex2OfSMTA2Phase from "./Annex2ofSMTA2Components/Annex2OfSMTA2Phase.vue";
import BiosafetyChecklistPhase from "./BiosafetyChecklistComponents/BiosafetyChecklistPhase.vue";
import BookingFormPhase from "./BookingFormComponents/BookingFormPhase.vue";

@Component({
  components: {
    Annex2OfSMTA2Phase,
    BiosafetyChecklistPhase,
    BookingFormPhase,
  },
})
export default class PreShipmentPhase extends Vue {
  @Model("update", { type: Object }) model!: WorklistFromBioHubItem;
  // Props

  get CurrentStatus(): WorklistFromBioHubStatus {
    return this.model.CurrentStatus;
  }

  get SubmitAnnex2OfSMTA2(): boolean {
    return (
      this.model.CurrentStatus == WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2
    );
  }

  get WaitingForAnnex2OfSMTA2SECsApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval
    );
  }

  get SubmitBiosafetyChecklistFormOfSMTA2(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2
    );
  }

  get WaitForBiosafetyChecklistFormSMTA2BSFsApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval
    );
  }

  get SubmitBookingFormOfSMTA2(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2
    );
  }

  get WaitForBookingFormSMTA2OPSsApproval(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval
    );
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

.shipment-action-vcard {
  margin-left: 12px;
  margin-top: 20px;
}
</style>
