<template>
  <v-card v-if="model">
    <v-card-title>
      <BackButton @back="onBack" />
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
                :properties-errors="propertiesErrors"
                property-name="Description"
                label="Description"
                :readonly="readonly"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="4">
              <text-field
                v-model="model.Address"
                :properties-errors="propertiesErrors"
                property-name="Address"
                label="Address"
                :readonly="readonly"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="6" lg="4">
              <text-field-float
                v-model="model.Latitude"
                :properties-errors="propertiesErrors"
                property-name="Latitude"
                label="Latitude"
                :step="0.00000001"
                :precision="9"
                :buttons="false"
                :readonly="readonly"
                decimal
                @input="update"
              >
              </text-field-float>
            </v-col>
            <v-col cols="12" md="6" lg="4">
              <text-field-float
                v-model="model.Longitude"
                :properties-errors="propertiesErrors"
                property-name="Longitude"
                label="Longitude"
                :step="0.00000001"
                :precision="9"
                :buttons="false"
                :readonly="readonly"
                decimal
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
                :menu-props="{ auto: true }"
                :items="countriesItems"
                :properties-errors="propertiesErrors"
                property-name="CountryId"
                item-text="Text"
                item-value="Value"
                label="Country"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="6" lg="3">
              <dropdown
                v-model="model.BSLLevelId"
                :items="bslLevelsItems"
                :properties-errors="propertiesErrors"
                property-name="BSLLevelId"
                item-text="Text"
                item-value="Value"
                label="Biosafety Level"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="6" lg="3">
              <checkbox
                v-model="model.IsPublicFacing"
                label="Observable in Public Page"
                :readonly="readonly"
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
import { Component, Vue, Prop, Model } from "vue-property-decorator";
import BackButton from "@/components/BackButton.vue";
import { BioHubFacility } from "@/models/BioHubFacility";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import Dropdown from "@/components/Dropdown.vue";
import Checkbox from "@/components/Checkbox.vue";
import { BSLLevelModule } from "../../bsllevels/store";
import { DropdownItem } from "@/models/DropdownItem";
import { CountryModule } from "../../countries/store";
import { BioHubFacilityModule } from "../store";

import { AppModule } from "../../../store/MainStore";

@Component({
  components: { BackButton, TextFieldFloat, TextField, Dropdown, Checkbox },
})
export default class BioHubFacilitiesForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  // Props
  @Prop({ type: Boolean, default: false })
  readonly readonly: boolean;

  @Prop({ required: true, type: String, default: "BioHubFacility" })
  readonly title: string;

  // Model
  @Model("update", { type: Object }) model!: BioHubFacility;

  $refs!: {
    form: any;
  };

  validate() {
    this.$refs.form.validate();
  }

  // Events
  update() {
    this.$emit("update", this.model);
  }

  onBack() {
    BioHubFacilityModule.SET_ERROR(undefined);
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
    return BioHubFacilityModule.ErrorMessage;
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
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
