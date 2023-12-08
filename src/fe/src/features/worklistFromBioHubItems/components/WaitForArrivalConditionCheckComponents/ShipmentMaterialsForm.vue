<template>
  <div>
    <v-card-text>
      <v-form ref="form" lazy-validation :readonly="readonly" class="ma-2">
        <div>
          <v-row>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="model.MaterialNumber"
                label="BioHub's Reference Number"
                readonly
                property-name="MaterialNumber"
                :properties-errors="allPropertiesErrors"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="MaterialProductName"
                label="Type Of Material"
                readonly
                property-name="MaterialProductName"
                :properties-errors="allPropertiesErrors"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="TransportCategoryName"
                label="Transport Category"
                readonly
                property-name="TransportCategoryName"
                :properties-errors="allPropertiesErrors"
              >
              </text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="4" lg="4">
              <date-picker
                v-model="model.CollectionDate"
                label="Collection Date"
                readonly
                property-name="CollectionDate"
                :properties-errors="allPropertiesErrors"
              >
              </date-picker>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="model.Location"
                label="Location"
                readonly
                property-name="Location"
                :properties-errors="allPropertiesErrors"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <dropdown
                v-model="model.IsolationHostTypeId"
                :items="isolationHostTypesItems"
                item-text="Text"
                item-value="Value"
                label="Host"
                readonly
                property-name="IsolationHostTypeId"
                :properties-errors="allPropertiesErrors"
              ></dropdown>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="4" lg="4">
              <dropdown
                v-model="model.Gender"
                :items="genders"
                item-text="Text"
                item-value="Value"
                label="Gender"
                readonly
                property-name="Gender"
                :properties-errors="allPropertiesErrors"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field-float
                v-model="model.Age"
                label="Age"
                :buttons="false"
                readonly
                property-name="Age"
                :properties-errors="allPropertiesErrors"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="model.PatientStatus"
                label="Patient Status"
                readonly
                property-name="PatientStatus"
                :properties-errors="allPropertiesErrors"
              >
              </text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="6" lg="6">
              <text-field-float
                v-model="model.Quantity"
                label="Number of vials"
                :buttons="false"
                readonly
                property-name="Quantity"
                :properties-errors="allPropertiesErrors"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-field-float
                v-model="model.Amount"
                label="Volume per vial (ml/vial)"
                :buttons="false"
                readonly
                property-name="Amount"
                :properties-errors="allPropertiesErrors"
              >
              </text-field-float>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" md="4" lg="4" @focusout="handleFocusOutCondition">
              <dropdown
                :key="keyCondition"
                v-model="Condition"
                :items="conditions"
                item-text="Text"
                item-value="Value"
                label="Condition"
                :readonly="!canEdit"
                property-name="Condition"
                :properties-errors="allPropertiesErrors"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="8" lg="8">
              <text-field
                v-model="Note"
                label="Note"
                :readonly="!canEdit"
                property-name="Note"
                :properties-errors="allPropertiesErrors"
                @input="update"
              >
              </text-field>
            </v-col>
          </v-row>
        </div>

        <v-card-actions v-if="saveVisible">
          <v-spacer></v-spacer>
          <v-container class="px-0" fluid>
            <CardActionsGenericButton text="Save" @click="save">
            </CardActionsGenericButton>
          </v-container>
        </v-card-actions>
      </v-form>
    </v-card-text>

    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import DatePicker from "@/components/DatePicker.vue";
import Dropdown from "@/components/Dropdown.vue";
import { IsolationHostTypeModule } from "../../../isolationHostTypes/store";
import { DropdownItem } from "@/models/DropdownItem";
import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import ShipmentMaterialsTable from "./ShipmentMaterialsTable.vue";
import { WorklistFromBioHubItemModule } from "../../store";
import CardActionsGenericButton from "@/components/CardActionsGenericButton.vue";
import { TransportCategoryModule } from "../../../transportCategories/store";
import { MaterialProductModule } from "../../../materialProducts/store";

@Component({
  components: {
    TextFieldFloat,
    TextField,
    Dropdown,
    DatePicker,
    CardActionsGenericButton,
  },
})
export default class ShippingMaterialsForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private Note = "";

  private keyCondition = 1;

  private Condition = ShipmentMaterialCondition.Intact;

  private propertiesErrors = new Map<string, Array<string>>();

  // Props
  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Number, default: 0 })
  readonly index: number;

  @Prop({ type: Boolean, default: false })
  readonly isCurrent: boolean;

  @Prop({ type: Boolean, default: true })
  readonly saveVisible: boolean;

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object }) model!: WorklistFromBioHubItemMaterial;

  save() {
    this.$emit("save", this.Note, this.Condition, this.model.Id);
  }

  update() {
    this.setPropertiesErrors(this.propertiesErrors);
  }

  updateNote(note: string) {
    this.Note = note;
  }

  handleFocusOutCondition(): void {
    this.keyCondition = this.keyCondition + 1;
  }

  updateCondition(condition: ShipmentMaterialCondition) {
    this.Condition = condition;
  }

  setPropertiesErrors(
    errorList: Map<string, Array<string>> | undefined
  ): Map<string, Array<string>> {
    if (errorList === undefined) {
      errorList = new Map<string, Array<string>>();
    }

    if (this.Condition === undefined || this.Condition === null) {
      errorList.set("Condition", ["'Condition' is Required"]);
    } else {
      errorList.delete("Condition");
    }
    return errorList;
  }

  validate() {
    this.$refs.form.validate();
  }

  get MaterialProductName(): string {
    const materialProduct = MaterialProductModule.MaterialProducts.filter(
      (mp) => {
        return this.model.MaterialProductId == mp.Id;
      }
    ).map((m) => {
      return {
        materialProductName: m.Name,
      };
    });

    if (materialProduct.length == 0) {
      return "";
    } else {
      return materialProduct[0].materialProductName;
    }
  }

  get TransportCategoryName(): string {
    const transportCategory =
      TransportCategoryModule.TransportCategories.filter((tc) => {
        return this.model.TransportCategoryId == tc.Id;
      }).map((m) => {
        return {
          transportCategoryName: m.Name,
        };
      });

    if (transportCategory.length == 0) {
      return "";
    } else {
      return transportCategory[0].transportCategoryName;
    }
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

  get conditions(): Array<DropdownItem> {
    const conditionsList = new Array<DropdownItem>();
    conditionsList.push({
      Text: "Intact",
      Value: ShipmentMaterialCondition.Intact,
    });
    conditionsList.push({
      Text: "Damaged",
      Value: ShipmentMaterialCondition.Damaged,
    });
    return conditionsList;
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

  @Watch("isCurrent")
  isCurrentChange() {
    this.updateNote(this.model.Note);
    this.updateCondition(this.model.Condition);
    this.setPropertiesErrors(this.PropertiesErrors);
    this.keyCondition = this.keyCondition + 1;
  }

  mounted() {
    this.updateNote(this.model.Note);
    this.updateCondition(this.model.Condition);
    this.setPropertiesErrors(this.PropertiesErrors);
    this.keyCondition = this.keyCondition + 1;
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
