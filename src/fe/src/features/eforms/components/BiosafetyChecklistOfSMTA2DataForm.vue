<template>
  <v-form ref="form" lazy-validation readonly class="ma-10">
    <text-field
      v-model="BiosafetyChecklistOfSMTA2.ShipmentRequestNumber"
      label="Shipment Request Number"
      readonly
      property-name="ShipmentRequestNumber"
    >
    </text-field>

    <text-field
      v-model="BioHubFacilityInfo"
      label="BioHub Facility"
      readonly
      property-name="BioHubFacilityInfo"
    >
    </text-field>

    <text-field
      v-model="LaboratoryInfo"
      label="Provider"
      readonly
      property-name="LaboratoryInfo"
    >
    </text-field>
    <h4 class="mb-5">Qualified Entity Requirements Checklist</h4>
    <BiosafetyChecklistForm
      :can-edit="false"
      :can-read="canRead"
      mandatory-message-text=""
      :biosafety-checklist="
        BiosafetyChecklistOfSMTA2.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s
      "
    >
    </BiosafetyChecklistForm>
    <v-container class="px-0" fluid>
      <div
        v-if="
          BiosafetyChecklistOfSMTA2.BiosafetyChecklistThreadComments &&
          BiosafetyChecklistOfSMTA2.BiosafetyChecklistThreadComments.length > 0
        "
      >
        <h2 class="mb-10">Thread</h2>
        <div
          v-for="i in BiosafetyChecklistOfSMTA2.BiosafetyChecklistThreadComments
            .length"
          :key="i"
        >
          <BiosafetyChecklistCommentFlowComponent
            v-model="
              BiosafetyChecklistOfSMTA2.BiosafetyChecklistThreadComments[i - 1]
            "
            :show-arrow="
              i <
              BiosafetyChecklistOfSMTA2.BiosafetyChecklistThreadComments.length
            "
          >
          </BiosafetyChecklistCommentFlowComponent>
        </div>
      </div>
    </v-container>

    <text-area
      v-model="BiosafetyChecklistOfSMTA2.BiosafetyChecklistApprovalComment"
      label="Biosafety Checklist Approval Comment"
      :readonly="false"
      :properties-errors="propertiesErrors"
      property-name="BiosafetyChecklistApprovalComment"
      @input="update"
    ></text-area>

    <text-field
      v-if="
        BiosafetyChecklistOfSMTA2.BiosafetyChecklistOfSMTA2SignatureText != ''
      "
      v-model="BiosafetyChecklistOfSMTA2.BiosafetyChecklistOfSMTA2SignatureText"
      label="Signature"
      readonly
      property-name="BiosafetyChecklistOfSMTA2SignatureText"
      @input="update"
    >
    </text-field>
    <date-picker
      v-model="BiosafetyChecklistOfSMTA2.ApprovalDate"
      label="Approval Date"
      readonly
      property-name="ApprovalDate"
    >
    </date-picker>
  </v-form>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import TextArea from "@/components/TextArea.vue";

import DatePicker from "@/components/DatePicker.vue";

import Checkbox from "@/components/Checkbox.vue";
import { BiosafetyChecklistOfSMTA2Data } from "@/models/BiosafetyChecklistOfSMTA2Data";
import { EFormModule } from "../store";

import BiosafetyChecklistForm from "../../worklistFromBioHubItems/components/BiosafetyChecklistComponents/BiosafetyChecklistForm.vue";
import BiosafetyChecklistCommentFlowComponent from "../../worklistFromBioHubItems/components/BiosafetyChecklistComponents/BiosafetyChecklistCommentFlowComponent.vue";

@Component({
  components: {
    TextFieldFloat,
    TextField,
    Checkbox,
    TextArea,
    DatePicker,
    BiosafetyChecklistForm,
    BiosafetyChecklistCommentFlowComponent,
  },
})
export default class BiosafetyChecklistOfSMTA2DataForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  @Prop({ type: Boolean, default: false })
  readonly canRead: boolean;

  $refs!: {
    form: any;
  };

  get BiosafetyChecklistOfSMTA2(): BiosafetyChecklistOfSMTA2Data | undefined {
    return EFormModule.BiosafetyChecklistOfSMTA2;
  }

  get BioHubFacilityInfo(): string {
    if (this.BiosafetyChecklistOfSMTA2 != undefined) {
      return (
        this.BiosafetyChecklistOfSMTA2.BioHubFacilityName +
        " " +
        this.BiosafetyChecklistOfSMTA2.BioHubFacilityAddress +
        " " +
        this.BiosafetyChecklistOfSMTA2.BioHubFacilityCountry
      );
    } else return "";
  }

  get LaboratoryInfo(): string {
    if (this.BiosafetyChecklistOfSMTA2 != undefined) {
      return (
        this.BiosafetyChecklistOfSMTA2.LaboratoryName +
        " " +
        this.BiosafetyChecklistOfSMTA2.LaboratoryAddress +
        " " +
        this.BiosafetyChecklistOfSMTA2.LaboratoryCountry
      );
    } else return "";
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
