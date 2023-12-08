<template>
  <v-form ref="form" lazy-validation readonly class="ma-10">
    <text-field
      v-model="Annex2OfSMTA1.ShipmentRequestNumber"
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
      :laboratory-focal-points="LaboratoryFocalPoints"
    ></LaboratoryFocalPointsTable>
    <v-spacer></v-spacer>
    <div>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-container class="px-0" fluid>
          <p>
            The Provider understands that the BMEPP are covered by the terms and
            conditions set out in the SMTA 1
          </p>
          <v-radio-group v-model="Annex2OfSMTA1.Annex2TermsAndConditions">
            <v-radio :key="0" label="Yes" :value="true"></v-radio>
            <v-radio :key="1" label="No" :value="false"></v-radio>
          </v-radio-group>
        </v-container>
      </v-card-actions>
      <text-area
        v-model="Annex2OfSMTA1.Annex2Comment"
        label="Comments"
        readonly
        property-name="Annex2Comment"
      >
      </text-area>
    </div>
    <v-spacer></v-spacer>

    <h4>BMEPP shipping information</h4>
    <ShippingInformationsTable
      :can-edit="false"
      :can-read="true"
      :material-shipping-informations="MaterialShippingInformations"
    >
    </ShippingInformationsTable>
    <h4>SARS-CoV-2 BMEPP laboratory analysis information</h4>
    <MaterialLaboratoryAnalysisInformationTable
      :material-laboratory-analysis-informations="
        MaterialLaboratoryAnalysisInformations
      "
    >
    </MaterialLaboratoryAnalysisInformationTable>
    <h4>BMEPP clinical details</h4>
    <ClinicalDetailsTable :material-clinical-details="MaterialClinicalDetails">
    </ClinicalDetailsTable>

    <text-field
      v-model="Annex2OfSMTA1.WHODocumentRegistrationNumber"
      label="WHO Document Registration Number"
      readonl
      :properties-errors="propertiesErrors"
      property-name="WHODocumentRegistrationNumber"
      @input="update"
    >
    </text-field>
    <text-area
      v-model="Annex2OfSMTA1.Annex2ApprovalComment"
      label="Annex2 Approval Comment"
      :readonly="false"
      :properties-errors="propertiesErrors"
      property-name="Annex2ApprovalComment"
      @input="update"
    ></text-area>

    <text-field
      v-if="Annex2OfSMTA1.Annex2OfSMTA1SignatureText != ''"
      v-model="Annex2OfSMTA1.Annex2OfSMTA1SignatureText"
      label="Signature"
      readonly
      property-name="Annex2OfSMTA1SignatureText"
      @input="update"
    >
    </text-field>
    <date-picker
      v-model="Annex2OfSMTA1.ApprovalDate"
      label="Approval Date"
      readonly
      property-name="ApprovalDate"
    >
    </date-picker>
  </v-form>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { MaterialClinicalDetail } from "@/models/MaterialClinicalDetail";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import TextArea from "@/components/TextArea.vue";
import Dropdown from "@/components/Dropdown.vue";
import DatePicker from "@/components/DatePicker.vue";
import ShippingInformationsTable from "../../worklistToBioHubItems/components/Annex2ofSMTA1Components/ShippingInformationsTable.vue";
import ClinicalDetailsTable from "../../worklistToBioHubItems/components/Annex2ofSMTA1Components/ClinicalDetailsTable.vue";
import MaterialLaboratoryAnalysisInformationTable from "../../worklistToBioHubItems/components/Annex2ofSMTA1Components/MaterialLaboratoryAnalysisInformationTable.vue";
import LaboratoryFocalPointsTable from "../../worklistItemsCommonComponents/Annex2ofSMTAComponents/LaboratoryFocalPointsTable.vue";
import Checkbox from "@/components/Checkbox.vue";
import { Annex2OfSMTA1Data } from "@/models/Annex2OfSMTA1Data";
import { EFormModule } from "../store";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import { MaterialLaboratoryAnalysisInformation } from "@/models/MaterialLaboratoryAnalysisInformation";

@Component({
  components: {
    TextFieldFloat,
    TextField,
    Dropdown,
    ShippingInformationsTable,
    ClinicalDetailsTable,
    MaterialLaboratoryAnalysisInformationTable,
    LaboratoryFocalPointsTable,
    Checkbox,
    TextArea,
    DatePicker,
  },
})
export default class Annex2OfSMTA1DataForm extends Vue {
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

  get Annex2OfSMTA1(): Annex2OfSMTA1Data | undefined {
    return EFormModule.Annex2OfSMTA1;
  }

  get LaboratoryFocalPoints(): Array<WorklistItemUser> {
    return this.Annex2OfSMTA1?.LaboratoryFocalPoints ?? [];
  }

  get MaterialShippingInformations(): Array<MaterialShippingInformation> {
    return this.Annex2OfSMTA1?.MaterialShippingInformations ?? [];
  }

  get MaterialClinicalDetails(): Array<MaterialClinicalDetail> {
    const clinicalDetails = new Array<MaterialClinicalDetail>();

    if (this.Annex2OfSMTA1 != undefined) {
      this.Annex2OfSMTA1.MaterialShippingInformations.forEach((si) => {
        si.MaterialClinicalDetails.forEach((cd) => {
          clinicalDetails.push(cd);
        });
      });
    }

    return clinicalDetails;
  }

  get MaterialLaboratoryAnalysisInformations(): Array<MaterialLaboratoryAnalysisInformation> {
    const materialLaboratoryAnalysisInformations =
      new Array<MaterialLaboratoryAnalysisInformation>();

    if (this.Annex2OfSMTA1 != undefined) {
      this.Annex2OfSMTA1.MaterialShippingInformations.forEach((si) => {
        si.MaterialLaboratoryAnalysisInformation.forEach((la) => {
          materialLaboratoryAnalysisInformations.push(la);
        });
      });
    }

    return materialLaboratoryAnalysisInformations;
  }

  get BioHubFacilityInfo(): string {
    if (this.Annex2OfSMTA1 != undefined) {
      return (
        this.Annex2OfSMTA1.BioHubFacilityName +
        " " +
        this.Annex2OfSMTA1.BioHubFacilityAddress +
        " " +
        this.Annex2OfSMTA1.BioHubFacilityCountry
      );
    } else return "";
  }

  get LaboratoryInfo(): string {
    if (this.Annex2OfSMTA1 != undefined) {
      return (
        this.Annex2OfSMTA1.LaboratoryName +
        " " +
        this.Annex2OfSMTA1.LaboratoryAddress +
        " " +
        this.Annex2OfSMTA1.LaboratoryCountry
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
