<template>
  <!-- <v-card class="mb-5"> -->
  <v-card-text>
    <div v-for="i in ConditionsNumber" :key="i">
      <ConditionFormElement
        v-model="conditions[i - 1]"
        :can-edit="canEdit"
        @updateAnnex2OfSMTA2Condition="updateAnnex2OfSMTA2Condition"
      >
      </ConditionFormElement>
    </div>
  </v-card-text>
  <!-- </v-card> -->
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import ConditionFormElement from "./ConditionFormElement.vue";
import { WorklistFromBioHubItemAnnex2OfSMTA2Condition } from "@/models/WorklistFromBioHubItemAnnex2OfSMTA2Condition";

@Component({
  components: {
    ConditionFormElement,
  },
})
export default class ConditionsForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  // // Model
  // @Model("update", { type: Object }) model!: WorklistFromBioHubItem;

  // Props

  @Prop({ required: true, type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ required: true, type: Array, default: [] })
  readonly conditions: Array<WorklistFromBioHubItemAnnex2OfSMTA2Condition>;

  $refs!: {
    form: any;
  };

  get ConditionsNumber(): number {
    return this.conditions.length;
  }

  validate() {
    this.$refs.form.validate();
  }

  updateAnnex2OfSMTA2Condition(
    condition: WorklistFromBioHubItemAnnex2OfSMTA2Condition
  ) {
    this.$emit("updateAnnex2OfSMTA2Condition", condition);
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
