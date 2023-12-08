<template>
  <v-card>
    <v-card-text>
      <v-form ref="form" lazy-validation :readonly="readonly" class="ma-2">
        <div>
          <v-row>
            <v-col cols="12" md="6" lg="6">
              <text-field
                :key="keyMaterialNumber"
                v-model="model.MaterialNumber"
                label="Provider's Material ID"
                readonly
                property-name="MaterialNumber"
                :properties-errors="allPropertiesErrors"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <date-picker
                :key="keyFreezingDate"
                v-model="model.FreezingDate"
                label="Freezing Date"
                :readonly="!canEdit"
                property-name="FreezingDate"
                :properties-errors="allPropertiesErrors"
                @input="updateFreezingDate"
              >
              </date-picker>
            </v-col>
          </v-row>
          <v-row>
            <v-col
              cols="12"
              md="3"
              lg="3"
              @focusout="handleFocusOutTemperature"
            >
              <text-field-float
                :key="keyTemperature"
                v-model="model.Temperature"
                label="Storage Condition Temperature"
                :step="0.01"
                :precision="2"
                :buttons="false"
                :readonly="!canEdit"
                decimal
                :properties-errors="propertiesErrors"
                property-name="Temperature"
                @input="update"
              >
              </text-field-float>
            </v-col>
            <v-col
              cols="12"
              md="3"
              lg="3"
              @focusout="handleFocusOutUnitOfMeasureId"
            >
              <dropdown
                :key="keyUnitOfMeasureId"
                v-model="model.UnitOfMeasureId"
                :items="temperatureUnitOfMeasuresItems"
                item-text="Text"
                item-value="Value"
                label="Unit Of Measure"
                :readonly="!canEdit"
                :properties-errors="allPropertiesErrors"
                property-name="UnitOfMeasureId"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col
              cols="12"
              md="6"
              lg="6"
              @focusout="handleFocusOutVirusConcentration"
            >
              <text-field
                :key="keyVirusConcentration"
                v-model="model.VirusConcentration"
                label="Virus Concentration"
                :readonly="!canEdit"
                property-name="VirusConcentration"
                :properties-errors="allPropertiesErrors"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>
          <v-row v-if="cultureIsolate">
            <v-col
              cols="12"
              md="6"
              lg="6"
              @focusout="handleFocusOutCulturingCellLine"
            >
              <text-field
                :key="keyCulturingCellLine"
                v-model="model.CulturingCellLine"
                label="Culturing Cell Line"
                :readonly="!canEdit"
                property-name="CulturingCellLine"
                :properties-errors="allPropertiesErrors"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col
              cols="12"
              md="6"
              lg="6"
              @focusout="handleFocusOutCulturingPassagesNumber"
            >
              <text-field-float
                :key="keyCulturingPassagesNumber"
                v-model="model.CulturingPassagesNumber"
                label="Culturing Passages Number"
                :buttons="false"
                :readonly="!canEdit"
                property-name="CulturingPassagesNumber"
                :properties-errors="allPropertiesErrors"
                @input="update"
              >
              </text-field-float>
            </v-col>
          </v-row>

          <v-row v-if="clinicalSpecimen">
            <v-col
              cols="12"
              md="6"
              lg="6"
              @focusout="handleFocusOutCollectedSpecimenTypes"
            >
              <dropdown
                :key="keyCollectedSpecimenTypes"
                v-model="CollectedSpecimenType"
                :items="SpecimenTypes"
                item-text="Text"
                item-value="Value"
                label="Collected Specimen Types"
                :readonly="!canEdit"
                property-name="CollectedSpecimenTypes"
                :properties-errors="allPropertiesErrors"
                @change="updateCollectedSpecimenTypes"
              ></dropdown>
            </v-col>

            <v-col
              cols="12"
              md="3"
              lg="3"
              @focusout="handleFocusOutTypeOfTransportMedium"
            >
              <text-field
                :key="keyTypeOfTransportMedium"
                v-model="model.TypeOfTransportMedium"
                label="Type Of Transport Medium"
                :readonly="!canEdit"
                property-name="TypeOfTransportMedium"
                :properties-errors="allPropertiesErrors"
                @input="update"
              >
              </text-field>
            </v-col>

            <v-col
              cols="12"
              md="3"
              lg="3"
              @focusout="handleFocusOutBrandOfTransportMedium"
            >
              <text-field
                :key="keyBrandOfTransportMedium"
                v-model="model.BrandOfTransportMedium"
                label="Brand Of Transport Medium"
                :readonly="!canEdit"
                property-name="BrandOfTransportMedium"
                :properties-errors="allPropertiesErrors"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col
              cols="12"
              md="5"
              lg="5"
              @focusout="handleFocusOutGSDUploadedToDatabase"
            >
              <dropdown
                :key="keyGSDUploadedToDatabase"
                v-model="model.GSDUploadedToDatabase"
                :items="GSDUploadedToDatabases"
                item-text="Text"
                item-value="Value"
                label="GSD Uploaded To Database"
                :readonly="!canEdit"
                property-name="GSDUploadedToDatabase"
                :properties-errors="allPropertiesErrors"
                @change="updateGSDUploadedToDatabase"
              ></dropdown>
            </v-col>
            <v-col
              v-if="GSDUploadedToDatabaseYes"
              cols="12"
              md="3"
              lg="3"
              @focusout="handleFocusOutDatabaseUsedForGSDUploading"
            >
              <dropdown
                :key="keyDatabaseUsedForGSDUploading"
                v-model="model.DatabaseUsedForGSDUploadingId"
                :items="DatabasesUsedForGSDUploadings"
                item-text="Text"
                item-value="Value"
                label="Database Used For GSD Uploading"
                :readonly="!canEdit"
                property-name="DatabaseUsedForGSDUploadingId"
                :properties-errors="allPropertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>

            <v-col
              v-if="GSDUploadedToDatabaseYes"
              cols="12"
              md="4"
              lg="4"
              @focusout="handleFocusOutAccessionNumberInGSDDatabase"
            >
              <text-field
                :key="keyAccessionNumberInGSDDatabase"
                v-model="model.AccessionNumberInGSDDatabase"
                label="Accession Number In GSD Database"
                :readonly="!canEdit"
                property-name="AccessionNumberInGSDDatabase"
                :properties-errors="allPropertiesErrors"
                @input="update"
              >
              </text-field>
            </v-col>

            <v-col v-if="GSDUploadedToDatabaseOther" cols="12" md="7" lg="7">
              <p class="ma-5" style="color: orange">
                Please specify some database information in the Additional
                Information box below
              </p>
            </v-col>
          </v-row>
        </div>
      </v-form>
    </v-card-text>

    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { MaterialLaboratoryAnalysisInformation } from "@/models/MaterialLaboratoryAnalysisInformation";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import DatePicker from "@/components/DatePicker.vue";
import Dropdown from "@/components/Dropdown.vue";
import Multiselect from "@/components/Multiselect.vue";
import { SpecimenTypeModule } from "../../../specimenTypes/store";
import { DropdownItem } from "@/models/DropdownItem";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { WorklistToBioHubItemModule } from "../../store";
import { GeneticSequenceDataModule } from "../../../geneticSequenceDatas/store";
import { TemperatureUnitOfMeasureModule } from "../../../temperatureUnitOfMeasures/store";

@Component({
  components: {
    TextFieldFloat,
    TextField,
    Dropdown,
    DatePicker,
  },
})
export default class MaterialLaboratoryAnalysisInformationForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  // Props
  private keyCulturingCellLine = 1;
  private keyFreezingDate = 1;
  private keyTemperature = 1;
  private keyUnitOfMeasureId = 1;
  private keyGSDUploadedToDatabase = 1;
  private keyVirusConcentration = 1;

  private keyCulturingPassagesNumber = 1;
  private keyCollectedSpecimenTypes = 1;
  private keyTypeOfTransportMedium = 1;
  private keyBrandOfTransportMedium = 1;

  private keyDatabaseUsedForGSDUploading = 1;
  private keyAccessionNumberInGSDDatabase = 1;

  private CollectedSpecimenType = "";

  private GSDUploadedToDatabaseYes = false;
  private GSDUploadedToDatabaseOther = false;

  private propertiesErrors = new Map<string, Array<string>>();

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly cultureIsolate: boolean;

  @Prop({ type: Boolean, default: false })
  readonly clinicalSpecimen: boolean;

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object })
  model!: MaterialLaboratoryAnalysisInformation;

  // Events
  update() {
    WorklistToBioHubItemModule.UPDATE_TEMPORARY_MATERIAL_LABORATORY_ANALYSIS_INFORMATION(
      this.model
    );
    this.setPropertiesErrors(this.propertiesErrors);
    this.$emit("updateMaterialLaboratoryAnalysisInformation");
  }

  updateFreezingDate() {
    WorklistToBioHubItemModule.UPDATE_TEMPORARY_MATERIAL_LABORATORY_ANALYSIS_INFORMATION(
      this.model
    );
    this.setPropertiesErrors(this.propertiesErrors);
    this.keyFreezingDate = this.keyFreezingDate + 1;
    this.$emit("updateMaterialLaboratoryAnalysisInformation");
  }

  updateCollectedSpecimenTypes() {
    this.model.CollectedSpecimenTypes = [this.CollectedSpecimenType];

    WorklistToBioHubItemModule.UPDATE_TEMPORARY_MATERIAL_LABORATORY_ANALYSIS_INFORMATION(
      this.model
    );
    this.setPropertiesErrors(this.propertiesErrors);
    this.keyCollectedSpecimenTypes = this.keyCollectedSpecimenTypes + 1;
    this.$emit("updateMaterialLaboratoryAnalysisInformation");
  }

  updateGSDUploadedToDatabase() {
    WorklistToBioHubItemModule.UPDATE_TEMPORARY_MATERIAL_LABORATORY_ANALYSIS_INFORMATION(
      this.model
    );
    if (this.model.GSDUploadedToDatabase == YesNoOption.Yes) {
      this.GSDUploadedToDatabaseYes = true;
    } else {
      this.GSDUploadedToDatabaseYes = false;
    }

    if (this.model.GSDUploadedToDatabase == YesNoOption.Other) {
      this.GSDUploadedToDatabaseOther = true;
    } else {
      this.GSDUploadedToDatabaseOther = false;
    }

    this.setPropertiesErrors(this.propertiesErrors);
    this.$emit("updateMaterialLaboratoryAnalysisInformation");
  }

  handleFocusOutCulturingCellLine(): void {
    this.keyCulturingCellLine = this.keyCulturingCellLine + 1;
  }

  handleFocusOutTemperature(): void {
    this.keyTemperature = this.keyTemperature + 1;
  }
  handleFocusOutUnitOfMeasureId(): void {
    this.keyUnitOfMeasureId = this.keyUnitOfMeasureId + 1;
  }
  handleFocusOutGSDUploadedToDatabase(): void {
    this.keyGSDUploadedToDatabase = this.keyGSDUploadedToDatabase + 1;
  }
  handleFocusOutVirusConcentration(): void {
    this.keyVirusConcentration = this.keyVirusConcentration + 1;
  }

  handleFocusOutCulturingPassagesNumber(): void {
    this.keyCulturingPassagesNumber = this.keyCulturingPassagesNumber + 1;
  }

  handleFocusOutCollectedSpecimenTypes(): void {
    this.keyCollectedSpecimenTypes = this.keyCollectedSpecimenTypes + 1;
  }

  handleFocusOutTypeOfTransportMedium(): void {
    this.keyTypeOfTransportMedium = this.keyTypeOfTransportMedium + 1;
  }

  handleFocusOutBrandOfTransportMedium(): void {
    this.keyBrandOfTransportMedium = this.keyBrandOfTransportMedium + 1;
  }

  handleFocusOutDatabaseUsedForGSDUploading(): void {
    this.keyDatabaseUsedForGSDUploading =
      this.keyDatabaseUsedForGSDUploading + 1;
  }

  handleFocusOutAccessionNumberInGSDDatabase(): void {
    this.keyAccessionNumberInGSDDatabase =
      this.keyAccessionNumberInGSDDatabase + 1;
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
      errorList.set("MaterialNumber", ["'BMEPP Number' is Required"]);
    } else {
      errorList.delete("MaterialNumber");
    }
    if (
      this.model.FreezingDate === undefined ||
      this.model.FreezingDate === null
    ) {
      errorList.set("FreezingDate", ["'Freezing Date' is Required"]);
    } else {
      errorList.delete("FreezingDate");
    }

    if (
      this.model.Temperature === undefined ||
      this.model.Temperature === null
    ) {
      errorList.set("Temperature", ["'Temperature' is Required"]);
    } else {
      errorList.delete("Temperature");
    }

    if (
      this.model.UnitOfMeasureId === undefined ||
      this.model.UnitOfMeasureId === ""
    ) {
      errorList.set("UnitOfMeasureId", ["'Unit Of Measure' is Required"]);
    } else {
      errorList.delete("UnitOfMeasureId");
    }

    if (
      this.model.VirusConcentration === undefined ||
      this.model.VirusConcentration === null ||
      this.model.VirusConcentration === ""
    ) {
      errorList.set("VirusConcentration", [
        "'Virus Concentration' is Required",
      ]);
    } else {
      errorList.delete("VirusConcentration");
    }

    if (this.cultureIsolate) {
      if (
        this.model.CulturingCellLine === undefined ||
        this.model.CulturingCellLine === ""
      ) {
        errorList.set("CulturingCellLine", [
          "'Culturing Cell Line' is Required",
        ]);
      } else {
        errorList.delete("CulturingCellLine");
      }

      if (
        this.model.CulturingPassagesNumber === undefined ||
        this.model.CulturingPassagesNumber === null ||
        this.model.CulturingPassagesNumber <= 0
      ) {
        errorList.set("CulturingPassagesNumber", [
          "'Culturing Passages Number' is Required",
        ]);
      } else {
        errorList.delete("CulturingPassagesNumber");
      }
    } else {
      errorList.delete("CulturingCellLine");
      errorList.delete("CulturingPassagesNumber");
    }

    if (this.clinicalSpecimen) {
      if (
        this.model.CollectedSpecimenTypes === undefined ||
        this.model.CollectedSpecimenTypes.length === 0
      ) {
        errorList.set("CollectedSpecimenTypes", [
          "'Collected Specimen Types' is Required",
        ]);
      } else {
        errorList.delete("CollectedSpecimenTypes");
      }

      if (
        this.model.TypeOfTransportMedium === undefined ||
        this.model.TypeOfTransportMedium === ""
      ) {
        errorList.set("TypeOfTransportMedium", [
          "'Type Of Transport Medium' is Required",
        ]);
      } else {
        errorList.delete("TypeOfTransportMedium");
      }

      if (
        this.model.BrandOfTransportMedium === undefined ||
        this.model.BrandOfTransportMedium === ""
      ) {
        errorList.set("BrandOfTransportMedium", [
          "'Brand Of Transport Medium' is Required",
        ]);
      } else {
        errorList.delete("BrandOfTransportMedium");
      }
    } else {
      errorList.delete("CollectedSpecimenTypes");
      errorList.delete("TypeOfTransportMedium");
      errorList.delete("BrandOfTransportMedium");
    }

    if (
      this.model.GSDUploadedToDatabase === undefined ||
      this.model.GSDUploadedToDatabase === null
    ) {
      errorList.set("GSDUploadedToDatabase", [
        "'GSD Uploaded To Database' is Required",
      ]);
    } else {
      errorList.delete("GSDUploadedToDatabase");
    }

    if (this.model.GSDUploadedToDatabase == YesNoOption.Yes) {
      if (
        this.model.DatabaseUsedForGSDUploadingId === undefined ||
        this.model.DatabaseUsedForGSDUploadingId === ""
      ) {
        errorList.set("DatabaseUsedForGSDUploadingId", [
          "'Database Used For GSD Uploading' is Required",
        ]);
      } else {
        errorList.delete("DatabaseUsedForGSDUploadingId");
      }

      if (
        this.model.AccessionNumberInGSDDatabase === undefined ||
        this.model.AccessionNumberInGSDDatabase === ""
      ) {
        errorList.set("AccessionNumberInGSDDatabase", [
          "'Accession Number In GSD Database' is Required",
        ]);
      } else {
        errorList.delete("AccessionNumberInGSDDatabase");
      }
    } else {
      errorList.delete("DatabaseUsedForGSDUploadingId");
      errorList.delete("AccessionNumberInGSDDatabase");
    }

    return errorList;
  }

  validate() {
    this.$refs.form.validate();
  }

  get temperatureUnitOfMeasuresItems(): Array<DropdownItem> {
    const TemperatureUnitOfMeasures =
      TemperatureUnitOfMeasureModule.TemperatureUnitOfMeasures;
    if (!TemperatureUnitOfMeasures) return new Array<DropdownItem>();

    return TemperatureUnitOfMeasures.map((l) => {
      return {
        Value: l.Id,
        Text: l.Unit,
      };
    });
  }

  get SpecimenTypes(): Array<DropdownItem> {
    const SpecimenTypes = SpecimenTypeModule.SpecimenTypes;
    if (!SpecimenTypes) return new Array<DropdownItem>();

    return SpecimenTypes.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get DatabasesUsedForGSDUploadings(): Array<DropdownItem> {
    const GeneticSequenceDatas = GeneticSequenceDataModule.GeneticSequenceDatas;
    if (!GeneticSequenceDatas) return new Array<DropdownItem>();

    return GeneticSequenceDatas.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get GSDUploadedToDatabases(): Array<DropdownItem> {
    const GSDUploadedToDatabasesList = new Array<DropdownItem>();
    GSDUploadedToDatabasesList.push({
      Text: "Yes",
      Value: YesNoOption.Yes,
    });
    GSDUploadedToDatabasesList.push({
      Text: "No",
      Value: YesNoOption.No,
    });
    GSDUploadedToDatabasesList.push({
      Text: "Other",
      Value: YesNoOption.Other,
    });
    return GSDUploadedToDatabasesList;
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

  mounted() {
    if (this.model.CollectedSpecimenTypes.length > 0) {
      this.CollectedSpecimenType = this.model.CollectedSpecimenTypes[0];
    }
  }

  @Watch("model.CollectedSpecimenTypes.length")
  CollectedSpecimenTypesChange() {
    if (this.model.CollectedSpecimenTypes.length > 0) {
      this.CollectedSpecimenType = this.model.CollectedSpecimenTypes[0];
    }
  }

  @Watch("cultureIsolate")
  CultureIsolateChange() {
    // if (this.cultureIsolate == false) {
    //   this.model.CulturingCellLine = "";
    //   this.model.CulturingPassagesNumber = "";
    // }
    this.setPropertiesErrors(this.propertiesErrors);
  }

  @Watch("clinicalSpecimen")
  ClinicalSpecimentChange() {
    // if (this.clinicalSpecimen == false) {
    //   this.CollectedSpecimenType = "";
    //   this.model.CollectedSpecimenTypes = [];
    //   this.model.TypeOfTransportMedium = "";
    //   this.model.BrandOfTransportMedium = "";
    // }
    this.setPropertiesErrors(this.propertiesErrors);
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
