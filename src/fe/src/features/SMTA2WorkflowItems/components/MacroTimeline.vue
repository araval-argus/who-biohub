<template>
  <v-card class="mb-5 timeline-stages">
    <v-card-text>
      <v-stepper v-model="currentStep" flat>
        <v-stepper-header>
          <v-stepper-step :complete="SubmitSMTA2Completed" step="1">
            Submit SMTA 2
          </v-stepper-step>
          <v-divider></v-divider>
          <v-stepper-step
            :complete="WaitingForSMTA2SECsApprovalCompleted"
            step="2"
          >
            Waiting For SMTA 2 Secretariat's Approval
          </v-stepper-step>
          <v-divider></v-divider>
          <v-stepper-step :complete="SMTA2Completed" step="3">
            SMTA 2 Completed
          </v-stepper-step>
        </v-stepper-header>
      </v-stepper>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

import { SMTA2WorkflowStatus } from "@/models/enums/SMTA2WorkflowStatus";

@Component({
  components: {},
})
export default class MacroTimeline extends Vue {
  @Prop({
    type: undefined,
    default: SMTA2WorkflowStatus.RequestInitiation,
  })
  readonly currentStatus: SMTA2WorkflowStatus;

  get SubmitSMTA2Completed(): boolean {
    return this.currentStatus > SMTA2WorkflowStatus.SubmitSMTA2;
  }

  get WaitingForSMTA2SECsApprovalCompleted(): boolean {
    return this.currentStatus > SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval;
  }

  get SMTA2Completed(): boolean {
    return this.currentStatus >= SMTA2WorkflowStatus.SMTA2WorkflowComplete;
  }

  get currentStep(): number {
    if (this.SubmitSMTA2Completed == false) {
      return 1;
    } else if (
      this.SubmitSMTA2Completed == true &&
      this.WaitingForSMTA2SECsApprovalCompleted == false
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
