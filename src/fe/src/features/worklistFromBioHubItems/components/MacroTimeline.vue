<template>
  <v-card class="mb-5 timeline-stages">
    <v-card-text>
      <v-stepper v-model="currentStep" flat>
        <v-stepper-header>
          <v-stepper-step :complete="PreShipmentPhaseCompleted" step="1">
            Pre-Shipment
          </v-stepper-step>
          <v-divider></v-divider>
          <v-stepper-step :complete="ShipmentPhaseCompleted" step="2">
            Shipment
          </v-stepper-step>
          <v-divider></v-divider>
          <v-stepper-step :complete="PostShipmentPhaseCompleted" step="3">
            Post-Shipment
          </v-stepper-step>
        </v-stepper-header>
      </v-stepper>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";

@Component({
  components: {},
})
export default class MacroTimeline extends Vue {
  @Prop({
    type: undefined,
    default: WorklistFromBioHubStatus.RequestInitiation,
  })
  readonly currentStatus: WorklistFromBioHubStatus;

  get SMTA2PhaseCompleted(): boolean {
    return this.currentStatus >= WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2;
  }

  get PreShipmentPhaseCompleted(): boolean {
    return (
      this.currentStatus >=
      WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments
    );
  }

  get ShipmentPhaseCompleted(): boolean {
    return this.currentStatus >= WorklistFromBioHubStatus.ShipmentCompleted;
  }

  get PostShipmentPhaseCompleted(): boolean {
    return this.currentStatus >= WorklistFromBioHubStatus.ShipmentCompleted;
  }

  get currentStep(): number {
    if (this.PreShipmentPhaseCompleted == false) {
      return 1;
    } else if (
      this.PreShipmentPhaseCompleted == true &&
      this.ShipmentPhaseCompleted == false
    ) {
      return 2;
    } else {
      return 3;
    }
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}

.v-stepper__step--active:not(.v-stepper__step--complete)
  .v-stepper__step__step {
  color: transparent;
}
.v-stepper__step:not(.v-stepper__step--complete):not(.v-stepper__step--error)
  .v-stepper__step__step {
  color: transparent;
}
</style>
