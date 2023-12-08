<template>
  <v-form ref="form" lazy-validation readonly class="ma-10">
    <text-field
      v-model="Annex2OfSMTA2.ShipmentRequestNumber"
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

    <LaboratoryFocalPointsTable
      :can-edit="false"
      :laboratory-focal-points="Annex2OfSMTA2.LaboratoryFocalPoints"
    ></LaboratoryFocalPointsTable>
    <v-spacer></v-spacer>

    <h4>Information about the BMEPP requested in this shipment</h4>
    <MaterialsTable
      :can-edit="false"
      :can-delete="false"
      :hide-amount="false"
      :materials="Annex2OfSMTA2.WorklistFromBioHubItemMaterials"
    ></MaterialsTable>
    <h4>
      Conditions for shipment of BMEPP by a WHO BioHub Facility to a Qualified
      Entity
    </h4>
    <ConditionsForm
      :can-edit="false"
      :conditions="Annex2OfSMTA2.WorklistFromBioHubItemAnnex2OfSMTA2Conditions"
      @updateAnnex2OfSMTA2Condition="updateAnnex2OfSMTA2Condition"
    >
    </ConditionsForm>

    <text-field
      v-model="Annex2OfSMTA2.WHODocumentRegistrationNumber"
      label="WHO Document Registration Number"
      readonl
      :properties-errors="propertiesErrors"
      property-name="WHODocumentRegistrationNumber"
      @input="update"
    >
    </text-field>
    <text-area
      v-model="Annex2OfSMTA2.Annex2ApprovalComment"
      label="Annex2 Approval Comment"
      :readonly="false"
      :properties-errors="propertiesErrors"
      property-name="Annex2ApprovalComment"
      @input="update"
    ></text-area>

    <text-field
      v-if="Annex2OfSMTA2.Annex2OfSMTA2SignatureText != ''"
      v-model="Annex2OfSMTA2.Annex2OfSMTA2SignatureText"
      label="Signature"
      readonly
      property-name="Annex2OfSMTA2SignatureText"
      @input="update"
    >
    </text-field>
    <date-picker
      v-model="Annex2OfSMTA2.ApprovalDate"
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
import Dropdown from "@/components/Dropdown.vue";
import DatePicker from "@/components/DatePicker.vue";
import LaboratoryFocalPointsTable from "../../worklistItemsCommonComponents/Annex2ofSMTAComponents/LaboratoryFocalPointsTable.vue";

import Checkbox from "@/components/Checkbox.vue";
import { Annex2OfSMTA2Data } from "@/models/Annex2OfSMTA2Data";
import { EFormModule } from "../store";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import MaterialsTable from "../../worklistFromBioHubItems/components/Annex2ofSMTA2Components/MaterialsTable.vue";
import ConditionsForm from "../../worklistFromBioHubItems/components/Annex2ofSMTA2Components/ConditionsForm.vue";

@Component({
  components: {
    TextFieldFloat,
    TextField,
    Dropdown,
    LaboratoryFocalPointsTable,
    Checkbox,
    TextArea,
    DatePicker,
    MaterialsTable,
    ConditionsForm,
  },
})
export default class Annex2OfSMTA2DataForm extends Vue {
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

  get Annex2OfSMTA2(): Annex2OfSMTA2Data | undefined {
    return EFormModule.Annex2OfSMTA2;
  }

  get BioHubFacilityInfo(): string {
    if (this.Annex2OfSMTA2 != undefined) {
      return (
        this.Annex2OfSMTA2.BioHubFacilityName +
        " " +
        this.Annex2OfSMTA2.BioHubFacilityAddress +
        " " +
        this.Annex2OfSMTA2.BioHubFacilityCountry
      );
    } else return "";
  }

  get LaboratoryInfo(): string {
    if (this.Annex2OfSMTA2 != undefined) {
      return (
        this.Annex2OfSMTA2.LaboratoryName +
        " " +
        this.Annex2OfSMTA2.LaboratoryAddress +
        " " +
        this.Annex2OfSMTA2.LaboratoryCountry
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
