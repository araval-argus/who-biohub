<template>
  <v-card v-if="model">
    <v-card-title>
      <BackButton v-if="backButtonVisible" @back="onBack"></BackButton>
      <h2>{{ title }}</h2>
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-text>
      <v-form ref="form" lazy-validation :readonly="readonly" class="ma-2">
        <div>
          <v-row>
            <v-col cols="12" md="8" lg="4">
              <text-field
                v-model="model.Name"
                label="Name"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Name"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="4" lg="4">
              <text-field
                v-model="model.Abbreviation"
                label="Abbreviation"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Abbreviation"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="4">
              <text-field
                v-model="model.Description"
                label="Description"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Description"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="4">
              <text-field
                v-model="model.Address"
                label="Address"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Address"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="6" lg="4">
              <text-field-float
                v-model="model.Latitude"
                label="Latitude"
                :step="0.00000001"
                :precision="9"
                :buttons="false"
                :readonly="readonly"
                decimal
                :properties-errors="propertiesErrors"
                property-name="Latitude"
                @input="update"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="6" lg="4">
              <text-field-float
                v-model="model.Longitude"
                label="Longitude"
                :step="0.00000001"
                :precision="9"
                :buttons="false"
                :readonly="readonly"
                decimal
                :properties-errors="propertiesErrors"
                property-name="Longitude"
                @input="update"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="6" lg="3">
              <checkbox
                v-model="model.IsActive"
                label="Is Active"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="IsActive"
                @change="update"
              >
              </checkbox>
            </v-col>
            <v-col cols="12" md="6" lg="3">
              <dropdown
                v-model="model.CountryId"
                :items="countriesItems"
                :menu-props="{ auto: true }"
                item-text="Text"
                item-value="Value"
                label="Country"
                :properties-errors="propertiesErrors"
                property-name="Country"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col v-if="showBslLevel" cols="12" md="6" lg="3">
              <dropdown
                v-model="model.BSLLevelId"
                :items="bslLevelsItems"
                item-text="Text"
                item-value="Value"
                label="Biosafety level"
                :properties-errors="propertiesErrors"
                property-name="BSLLevelId"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col
              v-if="laboratoryArea || bioHubFacilityArea"
              cols="12"
              md="12"
              lg="4"
            >
              <text-field
                v-model="IsPublicFacingString"
                label="Observable in Public Page"
                readonly
                disabled
                solo
              >
              </text-field>
            </v-col>
            <v-col v-else cols="12" md="6" lg="3">
              <checkbox
                v-model="model.IsPublicFacing"
                label="Observable in Public Page"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="IsPublicFacing"
                @change="update"
              >
              </checkbox>
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
import BackButton from "@/components/BackButton.vue";
import { Laboratory } from "@/models/Laboratory";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import Dropdown from "@/components/Dropdown.vue";
import Checkbox from "@/components/Checkbox.vue";
import { BSLLevelModule } from "../../bsllevels/store";
import { CountryModule } from "../../countries/store";
import { DropdownItem } from "@/models/DropdownItem";
import { LaboratoryModule } from "../store";

@Component({
  components: {
    BackButton,
    TextFieldFloat,
    TextField,
    Dropdown,
    Checkbox,
  },
})
export default class LaboratoriesForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  // Props
  @Prop({ type: Boolean, default: true })
  readonly backButtonVisible: boolean;

  @Prop({ type: Boolean, default: false })
  readonly readonly: boolean;

  @Prop({ required: true, type: String, default: "Laboratory" })
  readonly title: string;

  @Prop({ type: Boolean, default: false })
  readonly laboratoryArea: boolean;

  @Prop({ type: Boolean, default: false })
  readonly bioHubFacilityArea: boolean;

  @Prop({ type: Boolean, default: true })
  readonly showBslLevel: boolean;

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object }) model!: Laboratory;

  // Events
  update() {
    this.$emit("update", this.model);
  }

  validate() {
    this.$refs.form.validate();
  }

  onBack() {
    LaboratoryModule.SET_ERROR(undefined);
  }

  get bslLevelsItems(): Array<DropdownItem> {
    const BSLLevels = BSLLevelModule.BSLLevels.sort(this.sortByName);
    if (!BSLLevels) return new Array<DropdownItem>();

    return BSLLevels.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  sortByName(a: any, b: any) {
    if (a.Name > b.Name) {
      return 1;
    }
    if (a.Name < b.Name) {
      return -1;
    }
    return 0;
  }

  get IsPublicFacingString(): string {
    if (this.model !== undefined) {
      if (this.model.IsPublicFacing == true) {
        return "This laboratory is public";
      } else {
        return "This laboratory is WHO internal";
      }
    } else {
      return "";
    }
  }

  get countriesItems(): Array<DropdownItem> {
    const Countries = CountryModule.Countries;
    if (!Countries) return new Array<DropdownItem>();

    return Countries.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get propertiesErrors(): any {
    return LaboratoryModule.ErrorMessage;
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
