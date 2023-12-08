<template>
  <v-form
    v-if="model"
    ref="form"
    lazy-validation
    :readonly="readonly"
    class="ma-2"
  >
    <div>
      <v-row>
        <v-col
          cols="12"
          md="12"
          lg="12"
          @focusout="handleFocusOutMaterialNumber"
        >
          <text-field
            :key="keyMaterialNumber"
            v-model="model.MaterialNumber"
            label="Provider's Material ID"
            :readonly="
              !canEdit ||
              clinicalDetailsVisible ||
              materialLaboratoryAnalysisInformationVisible
            "
            property-name="MaterialNumber"
            :properties-errors="allPropertiesErrors"
            @input="update"
          >
          </text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col
          cols="12"
          md="12"
          lg="12"
          @focusout="handleFocusOutMaterialProductId"
        >
          <dropdown
            :key="keyMaterialProductId"
            v-model="model.MaterialProductId"
            :items="materialProductsItems"
            item-text="Text"
            item-value="Value"
            label="Type of Material"
            :readonly="
              !canEdit ||
              clinicalDetailsVisible ||
              materialLaboratoryAnalysisInformationVisible
            "
            property-name="MaterialProductId"
            :properties-errors="allPropertiesErrors"
            @change="update"
          ></dropdown>
        </v-col>
      </v-row>

      <v-row>
        <v-col
          cols="12"
          md="11"
          lg="11"
          @focusout="handleFocusOutTransportCategoryId"
        >
          <dropdown
            :key="keyTransportCategoryId"
            v-model="model.TransportCategoryId"
            :items="transportCategoriesItems"
            item-text="Text"
            item-value="Value"
            label="Transport Category"
            :readonly="!canEdit"
            property-name="TransportCategoryId"
            :properties-errors="allPropertiesErrors"
            @change="update"
          ></dropdown>
        </v-col>
        <v-col cols="12" md="1" lg="1">
          <v-icon small @click="openInformationPopup()">
            mdi-information
          </v-icon>
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" md="6" lg="6" @focusout="handleFocusOutQuantity">
          <text-field-float
            :key="keyQuantity"
            v-model="model.Quantity"
            label="Quantity (Number of vials)"
            :buttons="false"
            :readonly="!canEdit"
            property-name="Quantity"
            :properties-errors="allPropertiesErrors"
            @input="update"
          >
          </text-field-float>
        </v-col>
        <v-col cols="12" md="6" lg="6" @focusout="handleFocusOutAmount">
          <text-field-float
            :key="keyAmount"
            v-model="model.Amount"
            :step="0.01"
            :precision="2"
            label="Amount in ml per vial (e.g. 1.0)"
            :buttons="false"
            decimal
            :readonly="!canEdit"
            property-name="Amount"
            :properties-errors="allPropertiesErrors"
            @input="update"
          >
          </text-field-float>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="12" lg="12" @focusout="handleFocusOutCondition">
          <text-field
            :key="keyCondition"
            v-model="model.Condition"
            label="Condition"
            :readonly="!canEdit"
            property-name="Condition"
            :properties-errors="allPropertiesErrors"
            @input="update"
          >
          </text-field>
        </v-col>
      </v-row>
      <div v-if="materialLaboratoryAnalysisInformationVisible">
        <h4>C.2) SARS-CoV-2 BMEPP laboratory analysis information</h4>
        <template
          v-for="MaterialLaboratoryAnalysisInformationElement in model.MaterialLaboratoryAnalysisInformation"
        >
          <MaterialLaboratoryAnalysisInformationForm
            v-bind:key="MaterialLaboratoryAnalysisInformationElement"
            :model="MaterialLaboratoryAnalysisInformationElement"
            :can-edit="canEdit"
            :culture-isolate="cultureIsolate"
            :clinical-specimen="clinicalSpecimen"
            @updateMaterialLaboratoryAnalysisInformation="
              updateMaterialLaboratoryAnalysisInformation
            "
          >
          </MaterialLaboratoryAnalysisInformationForm>
        </template>
        <v-spacer></v-spacer>
      </div>

      <div v-if="clinicalDetailsVisible">
        <h4>C.3) SARS-CoV-2 BMEPP clinical details</h4>
        <ClinicalDetailForm
          v-for="i in 1"
          :key="i"
          v-model="model.MaterialClinicalDetails[i - 1]"
          :index="i"
          :can-edit="canEdit"
          @updateMaterialClinicalDetail="updateMaterialClinicalDetail"
        >
        </ClinicalDetailForm>
        <v-spacer></v-spacer>
      </div>
    </div>
    <v-row>
      <v-col cols="12" md="12" lg="12">
        <text-area
          :key="key"
          v-model="model.AdditionalInformation"
          label="Additional Information"
          :readonly="!canEdit"
          property-name="AdditionalInformation"
          :properties-errors="allPropertiesErrors"
          @input="update"
        >
        </text-area>
      </v-col>
    </v-row>

    <ShippingInformationPopup ref="shippingInformationPopup">
    </ShippingInformationPopup>
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
import { WorklistToBioHubItemModule } from "../../store";
import { MaterialProductModule } from "../../../materialProducts/store";
import { DropdownItem } from "@/models/DropdownItem";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import ClinicalDetailForm from "./ClinicalDetailForm.vue";
import MaterialLaboratoryAnalysisInformationForm from "./MaterialLaboratoryAnalysisInformationForm.vue";
import { TransportCategoryModule } from "../../../transportCategories/store";
import ShippingInformationPopup from "./ShippingInformationPopup.vue";
import { SeedData } from "@/models/constants/SeedData";

@Component({
  components: {
    CardActionsGenericButton,
    TextFieldFloat,
    TextField,
    Dropdown,
    ClinicalDetailForm,
    MaterialLaboratoryAnalysisInformationForm,
    TextArea,
    ShippingInformationPopup,
  },
})
export default class ShippingInformationForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private propertiesErrors = new Map<string, Array<string>>();
  private keyMaterialProductId = 1;
  private keyTransportCategoryId = 1;
  private keyMaterialNumber = 1;
  private keyQuantity = 1;
  private keyCondition = 1;
  private keyAmount = 1;
  private clinicalDetailsVisible = false;
  private materialLaboratoryAnalysisInformationVisible = false;
  // Props

  @Prop({ required: true, type: Boolean, default: false })
  readonly canEdit: boolean;

  $refs!: {
    shippingInformationPopup: ShippingInformationPopup;
    form: any;
  };
  // Model
  @Model("update", { type: Object }) model!: MaterialShippingInformation;

  // Events
  update() {
    this.$emit("update", this.model);
    this.setPropertiesErrors(this.propertiesErrors);
  }

  updateMaterialClinicalDetail() {
    this.$emit("updateMaterialClinicalDetail");
  }

  updateMaterialLaboratoryAnalysisInformation() {
    this.$emit("updateMaterialLaboratoryAnalysisInformation");
  }

  validate() {
    this.$refs.form.validate();
  }

  openInformationPopup(): void {
    this.$refs.shippingInformationPopup.showPopup();
  }

  setPropertiesErrors(
    errorList: Map<string, Array<string>> | undefined
  ): Map<string, Array<string>> {
    if (errorList === undefined) {
      errorList = new Map<string, Array<string>>();
    }
    if (
      this.model.MaterialNumber === undefined ||
      this.model.MaterialNumber === ""
    ) {
      errorList.set("MaterialNumber", ["'Provider's Material ID' is Required"]);
    } else {
      errorList.delete("MaterialNumber");
    }

    if (
      this.model.MaterialProductId === undefined ||
      this.model.MaterialProductId === ""
    ) {
      errorList.set("MaterialProductId", ["'Type of Material' is Required"]);
    } else {
      errorList.delete("MaterialProductId");
    }

    if (
      this.model.TransportCategoryId === undefined ||
      this.model.TransportCategoryId === ""
    ) {
      errorList.set("TransportCategoryId", [
        "'Transport Category' is Required",
      ]);
    } else {
      errorList.delete("TransportCategoryId");
    }

    if (
      this.model.Quantity === undefined ||
      this.model.Quantity === null ||
      this.model.Quantity <= 0
    ) {
      errorList.set("Quantity", ["'Quantity' is Required"]);
    } else {
      errorList.delete("Quantity");
    }

    if (
      this.model.Amount === undefined ||
      this.model.Amount === null ||
      this.model.Amount <= 0
    ) {
      errorList.set("Amount", ["'Amount' is Required"]);
    } else {
      errorList.delete("Amount");
    }

    if (this.model.Condition === undefined || this.model.Condition === "") {
      errorList.set("Condition", ["'Condition' is Required"]);
    } else {
      errorList.delete("Condition");
    }

    return errorList;
  }

  get cultureIsolate(): boolean {
    return this.model.MaterialProductId == SeedData.CulturedIsolateProductId;
  }

  get clinicalSpecimen(): boolean {
    return this.model.MaterialProductId == SeedData.ClinicalSpecimenProductId;
  }

  get materialProductsItems(): Array<DropdownItem> {
    const MaterialProducts = MaterialProductModule.MaterialProducts;
    if (!MaterialProducts) return new Array<DropdownItem>();

    return MaterialProducts.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get transportCategoriesItems(): Array<DropdownItem> {
    const TransportCategories = TransportCategoryModule.TransportCategories;
    if (!TransportCategories) return new Array<DropdownItem>();

    return TransportCategories.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get allPropertiesErrors(): Map<string, Array<string>> {
    let result = this.propertiesErrors ?? new Map<string, Array<string>>();

    this.PropertiesErrors.forEach((value, key, map) => {
      result.set(key, value);
    });

    return result;
  }

  get PropertiesErrors(): Map<string, Array<string>> {
    let result = this.setPropertiesErrors(undefined);
    return result;
  }

  get clinicalDetailsVisibleCondition(): boolean {
    return (
      this.model.MaterialNumber != null &&
      this.model.MaterialNumber != "" &&
      this.model.MaterialNumber != undefined
    );
  }

  // get materialLaboratoryAnalysisInformationVisibleCondition(): boolean {
  //   return (
  //     this.model.MaterialNumber != null &&
  //     this.model.MaterialNumber != "" &&
  //     this.model.MaterialNumber != undefined &&
  //     this.model.MaterialProductId != null &&
  //     this.model.MaterialProductId != "" &&
  //     this.model.MaterialProductId != undefined
  //   );
  // }

  handleFocusOutMaterialNumber(): void {
    if (
      this.clinicalDetailsVisibleCondition &&
      this.model.MaterialProductId != null &&
      this.model.MaterialProductId != "" &&
      this.model.MaterialProductId != undefined &&
      this.clinicalDetailsVisible == false &&
      this.materialLaboratoryAnalysisInformationVisible == false
    ) {
      const info = new Map<string, string>();
      info.set("Quantity", "1");
      info.set("MaterialNumber", this.model.MaterialNumber);
      WorklistToBioHubItemModule.INITIALIZE_TEMPORARY_MATERIAL_LABORATORY_ANALYSIS_INFORMATION(
        info
      );
      WorklistToBioHubItemModule.INITIALIZE_TEMPORARY_MATERIAL_CLINICAL_DETAILS(
        info
      );
      this.clinicalDetailsVisible = true;
      this.materialLaboratoryAnalysisInformationVisible = true;
    }
    this.keyMaterialNumber = this.keyMaterialNumber + 1;
  }

  handleFocusOutQuantity(): void {
    this.keyQuantity = this.keyQuantity + 1;
  }
  handleFocusOutCondition(): void {
    this.keyCondition = this.keyCondition + 1;
  }

  handleFocusOutAmount(): void {
    this.keyAmount = this.keyAmount + 1;
  }

  handleFocusOutMaterialProductId(): void {
    if (
      this.clinicalDetailsVisibleCondition &&
      this.model.MaterialProductId != null &&
      this.model.MaterialProductId != "" &&
      this.model.MaterialProductId != undefined &&
      this.clinicalDetailsVisible == false &&
      this.materialLaboratoryAnalysisInformationVisible == false
    ) {
      const info = new Map<string, string>();
      info.set("Quantity", "1");
      info.set("MaterialNumber", this.model.MaterialNumber);
      WorklistToBioHubItemModule.INITIALIZE_TEMPORARY_MATERIAL_LABORATORY_ANALYSIS_INFORMATION(
        info
      );
      WorklistToBioHubItemModule.INITIALIZE_TEMPORARY_MATERIAL_CLINICAL_DETAILS(
        info
      );
      this.clinicalDetailsVisible = true;
      this.materialLaboratoryAnalysisInformationVisible = true;
    }
    this.keyMaterialProductId = this.keyMaterialProductId + 1;
  }

  handleFocusOutTransportCategoryId(): void {
    this.keyTransportCategoryId = this.keyTransportCategoryId + 1;
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
