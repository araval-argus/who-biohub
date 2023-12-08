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
        <v-col cols="12" md="12" lg="12" @focusout="handleFocusOutCellLine">
          <text-field
            :key="keyCellLine"
            v-model="model.CellLine"
            label="Cell Line"
            :readonly="!canEdit"
            property-name="CellLine"
            :properties-errors="allPropertiesErrors"
            @input="update"
          >
          </text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="12" lg="12" @focusout="handleFocusOutGSDType">
          <dropdown
            :key="keyGSDType"
            v-model="model.GSDType"
            :items="GSDTypesItems"
            item-text="Text"
            item-value="Value"
            label="GSD of"
            :readonly="
              !canEdit ||
              clinicalDetailsVisible ||
              materialLaboratoryAnalysisInformationVisible
            "
            property-name="GSDType"
            :properties-errors="allPropertiesErrors"
            @change="update"
          ></dropdown>
        </v-col>
      </v-row>

      <v-row>
        <v-col
          cols="12"
          md="12"
          lg="12"
          @focusout="handleFocusOutPassageNumber"
        >
          <dropdown
            :key="keyPassageNumber"
            v-model="model.PassageNumber"
            :items="passageNumbersItems"
            :menu-props="{ auto: true }"
            item-text="Text"
            item-value="Value"
            label="Passage Number"
            :readonly="!canEdit"
            property-name="PassageNumber"
            :properties-errors="allPropertiesErrors"
            @change="update"
          ></dropdown>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="12" lg="12" @focusout="handleFocusOutGSDFasta">
          <text-area
            :key="keyGSDFasta"
            v-model="model.GSDFasta"
            label="GSDFasta"
            :readonly="!canEdit"
            property-name="GSDFasta"
            :properties-errors="allPropertiesErrors"
            @input="update"
          >
          </text-area>
        </v-col>
      </v-row>

      <v-spacer></v-spacer>
    </div>
  </v-form>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextArea from "@/components/TextArea.vue";
import TextField from "@/components/TextField.vue";
import Dropdown from "@/components/Dropdown.vue";
import { DropdownItem } from "@/models/DropdownItem";
import CardActionsGenericButton from "../../../components/CardActionsGenericButton.vue";
import { GSDType } from "@/models/enums/GSDType";
import { MaterialGSDInfo } from "@/models/MaterialGSDInfo";

@Component({
  components: {
    CardActionsGenericButton,
    TextField,
    Dropdown,
    TextArea,
  },
})
export default class MaterialGSDInfoForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private propertiesErrors = new Map<string, Array<string>>();
  private keyGSDType = 1;
  private keyPassageNumber = 1;
  private keyCellLine = 1;
  private keyGSDFasta = 1;

  // Props

  @Prop({ required: true, type: Boolean, default: false })
  readonly canEdit: boolean;

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object }) model!: MaterialGSDInfo;

  // Events
  update() {
    this.$emit("update", this.model);
    this.setPropertiesErrors(this.propertiesErrors);
  }

  validate() {
    this.$refs.form.validate();
  }

  setPropertiesErrors(
    errorList: Map<string, Array<string>> | undefined
  ): Map<string, Array<string>> {
    if (errorList === undefined) {
      errorList = new Map<string, Array<string>>();
    }
    if (this.model.CellLine === undefined || this.model.CellLine === "") {
      errorList.set("CellLine", ["'Cell Line' is Required"]);
    } else {
      errorList.delete("CellLine");
    }

    if (this.model.GSDType === undefined) {
      errorList.set("GSDType", ["'GSD Type' is Required"]);
    } else {
      errorList.delete("GSDType");
    }

    if (
      this.model.PassageNumber === undefined ||
      this.model.PassageNumber < 0 ||
      isNaN(this.model.PassageNumber)
    ) {
      errorList.set("PassageNumber", ["'PassageNumber' is Required"]);
    } else {
      errorList.delete("PassageNumber");
    }

    if (this.model.GSDFasta === undefined || this.model.GSDFasta === "") {
      errorList.set("GSDFasta", ["'GSDFasta' is Required"]);
    } else {
      errorList.delete("GSDFasta");
    }

    return errorList;
  }

  get GSDTypesItems(): Array<DropdownItem> {
    const GSDTypesItemsList = new Array<DropdownItem>();
    GSDTypesItemsList.push({
      Text: "Original Material",
      Value: GSDType.OriginalMaterial,
    });
    GSDTypesItemsList.push({
      Text: "Cultured Material",
      Value: GSDType.CulturedMaterial,
    });
    return GSDTypesItemsList;
  }

  get passageNumbersItems(): Array<DropdownItem> {
    const passageNumbersItemsList = new Array<DropdownItem>();

    for (let i = 0; i < 20; i++) {
      passageNumbersItemsList.push({ Text: i.toString(), Value: i });
    }

    return passageNumbersItemsList;
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

  handleFocusOutCellLine(): void {
    this.keyCellLine = this.keyCellLine + 1;
  }

  handleFocusOutGSDFasta(): void {
    this.keyGSDFasta = this.keyGSDFasta + 1;
  }

  handleFocusOutGSDType(): void {
    this.keyGSDType = this.keyGSDType + 1;
  }

  handleFocusOutPassageNumber(): void {
    this.keyPassageNumber = this.keyPassageNumber + 1;
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
