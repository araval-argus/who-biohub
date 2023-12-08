<template>
  <v-stepper v-model="items" :flat="flat" :outlined="outlined" vertical>
    <template v-for="item in items">
      <v-stepper-step
        v-if="item.LastSubmissionApproved == true"
        :key="item"
        complete
      >
        <StatusSummaryItem
          :workflow-item="item"
          @downloadHistoryFile="downloadHistoryFile"
          @downloadFile="downloadFile"
        ></StatusSummaryItem>
      </v-stepper-step>
      <v-stepper-step
        v-else
        :key="item"
        error-icon="mdi-close-circle"
        :rules="[() => false]"
      >
        <StatusSummaryItem
          :workflow-item="item"
          @downloadHistoryFile="downloadHistoryFile"
          @downloadFile="downloadFile"
        ></StatusSummaryItem>
      </v-stepper-step>
      <v-stepper-content v-if="items.length > 1" :key="item">
      </v-stepper-content>
    </template>
  </v-stepper>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model } from "vue-property-decorator";
import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";
import StatusSummaryItem from "./StatusSummaryItem.vue";

@Component({
  components: { StatusSummaryItem },
})
export default class SMTA1WorkflowItemsTimeline extends Vue {
  @Model("update", { type: Object }) model!: SMTA1WorkflowItem;
  // Props
  @Prop({ type: Array, default: [] })
  readonly items: Array<SMTA1WorkflowItem>;

  @Prop({ type: Boolean, default: true })
  readonly flat: boolean;

  @Prop({ type: Boolean, default: false })
  readonly outlined: boolean;

  get steps(): number {
    return this.items.length;
  }

  update() {
    this.$emit("update", this.model);
  }

  downloadHistoryFile() {
    this.$emit("downloadHistoryFile");
  }
  downloadFile() {
    this.$emit("downloadFile");
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
