<template>
  <v-card v-if="model">
    <v-card-text>
      <v-form ref="form" lazy-validation :readonly="readonly" class="ma-2">
        <div>
          <v-row>
            <v-col cols="12" md="4" lg="4">
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
            <v-col cols="12" md="4" lg="4">
              <date-picker
                :key="keyCollectionDate"
                v-model="model.CollectionDate"
                label="Collection Date"
                :readonly="!canEdit"
                property-name="CollectionDate"
                :properties-errors="allPropertiesErrors"
                @input="updateCollectionDate"
              >
              </date-picker>
            </v-col>
            <v-col cols="12" md="4" lg="4" @focusout="handleFocusOutLocation">
              <text-field
                :key="keyLocation"
                v-model="model.Location"
                label="Location"
                :readonly="!canEdit"
                property-name="Location"
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
              @focusout="handleFocusOutIsolationHostTypeId"
            >
              <dropdown
                :key="keyIsolationHostTypeId"
                v-model="model.IsolationHostTypeId"
                :items="isolationHostTypesItems"
                item-text="Text"
                item-value="Value"
                label="Host"
                :readonly="!canEdit"
                property-name="IsolationHostTypeId"
                :properties-errors="allPropertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="5" lg="5" @focusout="handleFocusOutGender">
              <dropdown
                :key="keyGender"
                v-model="model.Gender"
                :items="genders"
                item-text="Text"
                item-value="Value"
                label="Gender"
                :readonly="!canEdit"
                property-name="Gender"
                :properties-errors="allPropertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="6" lg="6" @focusout="handleFocusOutAge">
              <text-field-float
                :key="keyAge"
                v-model="model.Age"
                label="Age"
                :buttons="false"
                :readonly="!canEdit"
                property-name="Age"
                :properties-errors="allPropertiesErrors"
                @input="update"
              >
              </text-field-float>
            </v-col>
          </v-row>
          <v-row>
            <v-col
              cols="12"
              md="12"
              lg="12"
              @focusout="handleFocusOutPatientStatus"
            >
              <text-field
                :key="keyPatientStatus"
                v-model="model.PatientStatus"
                label="Patient Status"
                :readonly="!canEdit"
                property-name="PatientStatus"
                :properties-errors="allPropertiesErrors"
                @input="update"
              >
              </text-field>
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
import { MaterialClinicalDetail } from "@/models/MaterialClinicalDetail";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import DatePicker from "@/components/DatePicker.vue";
import Dropdown from "@/components/Dropdown.vue";
import { IsolationHostTypeModule } from "../../../isolationHostTypes/store";
import { DropdownItem } from "@/models/DropdownItem";
import { Gender } from "@/models/enums/Gender";
import { WorklistToBioHubItemModule } from "../../store";

@Component({
  components: {
    TextFieldFloat,
    TextField,
    Dropdown,
    DatePicker,
  },
})
export default class ClinicalDetailForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  // Props
  private keyPatientStatus = 1;
  private keyCollectionDate = 1;
  private keyLocation = 1;
  private keyIsolationHostTypeId = 1;
  private keyGender = 1;
  private keyAge = 1;

  private propertiesErrors = new Map<string, Array<string>>();

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Number, default: 0 })
  readonly index: number;

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object }) model!: MaterialClinicalDetail;

  // Events
  update() {
    WorklistToBioHubItemModule.UPDATE_TEMPORARY_MATERIAL_CLINICAL_DETAIL(
      this.model
    );
    this.setPropertiesErrors(this.propertiesErrors);
    this.$emit("updateMaterialClinicalDetail");
  }

  updateCollectionDate() {
    WorklistToBioHubItemModule.UPDATE_TEMPORARY_MATERIAL_CLINICAL_DETAIL(
      this.model
    );
    this.setPropertiesErrors(this.propertiesErrors);
    this.keyCollectionDate = this.keyCollectionDate + 1;
    this.$emit("updateMaterialClinicalDetail");
  }

  handleFocusOutPatientStatus(): void {
    this.keyPatientStatus = this.keyPatientStatus + 1;
  }

  handleFocusOutLocation(): void {
    this.keyLocation = this.keyLocation + 1;
  }
  handleFocusOutIsolationHostTypeId(): void {
    this.keyIsolationHostTypeId = this.keyIsolationHostTypeId + 1;
  }
  handleFocusOutGender(): void {
    this.keyGender = this.keyGender + 1;
  }
  handleFocusOutAge(): void {
    this.keyAge = this.keyAge + 1;
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
      this.model.CollectionDate === undefined ||
      this.model.CollectionDate === null
    ) {
      errorList.set("CollectionDate", ["'Collection Date' is Required"]);
    } else {
      errorList.delete("CollectionDate");
    }

    if (this.model.Location === undefined || this.model.Location === "") {
      errorList.set("Location", ["'Location' is Required"]);
    } else {
      errorList.delete("Location");
    }

    if (
      this.model.IsolationHostTypeId === undefined ||
      this.model.IsolationHostTypeId === ""
    ) {
      errorList.set("IsolationHostTypeId", ["'Host' is Required"]);
    } else {
      errorList.delete("IsolationHostTypeId");
    }

    if (this.model.Gender === undefined || this.model.Gender === null) {
      errorList.set("Gender", ["'Gender' is Required"]);
    } else {
      errorList.delete("Gender");
    }

    if (
      this.model.Age === undefined ||
      this.model.Age === null ||
      this.model.Age < 0 ||
      isNaN(this.model.Age) === true
    ) {
      errorList.set("Age", ["'Age' is Required"]);
    } else {
      errorList.delete("Age");
    }

    if (
      this.model.PatientStatus === undefined ||
      this.model.PatientStatus === ""
    ) {
      errorList.set("PatientStatus", ["'Patient Status' is Required"]);
    } else {
      errorList.delete("PatientStatus");
    }
    return errorList;
  }

  validate() {
    this.$refs.form.validate();
  }

  get isolationHostTypesItems(): Array<DropdownItem> {
    const IsolationHostTypes = IsolationHostTypeModule.IsolationHostTypes;
    if (!IsolationHostTypes) return new Array<DropdownItem>();

    return IsolationHostTypes.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get genders(): Array<DropdownItem> {
    const gendersList = new Array<DropdownItem>();
    gendersList.push({ Text: "Male", Value: Gender.Male });
    gendersList.push({ Text: "Female", Value: Gender.Female });
    gendersList.push({ Text: "Undisclosed", Value: Gender.Undisclosed });
    return gendersList;
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
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
