<template>
  <v-card class="mb-5 timeline-stages">
    <v-card-text>
      <v-stepper v-model="currentStep" flat>
        <v-stepper-header>
          <v-stepper-step :complete="SubmitSMTA1Completed" step="1">
            Submit SMTA 1
          </v-stepper-step>
          <v-divider></v-divider>
          <v-stepper-step
            :complete="WaitingForSMTA1SECsApprovalCompleted"
            step="2"
          >
            Waiting For SMTA 1 Secretariat's Approval
          </v-stepper-step>
          <v-divider></v-divider>
          <v-stepper-step :complete="SMTA1Completed" step="3">
            SMTA 1 Completed
          </v-stepper-step>
        </v-stepper-header>
      </v-stepper>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";

@Component({
  components: {},
})
export default class MacroTimeline extends Vue {
  @Prop({
    type: undefined,
    default: SMTA1WorkflowStatus.RequestInitiation,
  })
  readonly currentStatus: SMTA1WorkflowStatus;

  get SubmitSMTA1Completed(): boolean {
    return this.currentStatus > SMTA1WorkflowStatus.SubmitSMTA1;
  }

  get WaitingForSMTA1SECsApprovalCompleted(): boolean {
    return this.currentStatus > SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval;
  }

  get SMTA1Completed(): boolean {
    return this.currentStatus >= SMTA1WorkflowStatus.SMTA1WorkflowComplete;
  }

  get currentStep(): number {
    if (this.SubmitSMTA1Completed == false) {
      return 1;
    } else if (
      this.SubmitSMTA1Completed == true &&
      this.WaitingForSMTA1SECsApprovalCompleted == false
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
