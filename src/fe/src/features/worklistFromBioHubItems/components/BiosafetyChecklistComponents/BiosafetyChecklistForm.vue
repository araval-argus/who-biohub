<template>
  <v-card class="mb-5">
    <v-card-text>
      <div v-for="i in ConditionsNumber" :key="i">
        <BiosafetyChecklistFormElement
          v-model="biosafetyChecklist[i - 1]"
          :can-edit="canEdit"
          :mandatory-message-text="mandatoryMessageText"
          @updateBiosafetyChecklist="updateBiosafetyChecklist"
        >
        </BiosafetyChecklistFormElement>
      </div>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import BiosafetyChecklistFormElement from "./BiosafetyChecklistFormElement.vue";
import { WorklistFromBioHubItemBiosafetyChecklistOfSMTA2 } from "@/models/WorklistFromBioHubItemBiosafetyChecklistOfSMTA2";

@Component({
  components: {
    BiosafetyChecklistFormElement,
  },
})
export default class BiosafetyChecklistForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  // Props
  @Prop({ required: true, type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: String, default: "Please accept" })
  readonly mandatoryMessageText: string;

  @Prop({ type: Array, default: "Please accept" })
  readonly biosafetyChecklist: Array<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2>;

  $refs!: {
    form: any;
  };

  get ConditionsNumber(): number {
    return this.biosafetyChecklist.length;
  }

  validate() {
    this.$refs.form.validate();
  }

  updateBiosafetyChecklist(
    model: WorklistFromBioHubItemBiosafetyChecklistOfSMTA2
  ) {
    this.$emit("updateBiosafetyChecklist", model);
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
