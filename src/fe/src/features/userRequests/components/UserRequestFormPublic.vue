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
            <v-col cols="12" md="6" lg="6">
              <text-field
                v-model="model.FirstName"
                label="First name"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="FirstName"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-field
                v-model="model.LastName"
                label="Last name"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="LastName"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.Email"
                label="Email"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Email"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.RoleId"
                :items="rolesItems"
                item-text="Text"
                item-value="Value"
                label="Institute Type"
                :properties-errors="propertiesErrors"
                property-name="RoleId"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="12" lg="12">
              <text-field
                v-model="model.InstituteName"
                label="Intitute name"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="InstituteName"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="12" lg="12">
              <dropdown
                v-model="model.CountryId"
                :items="countriesItems"
                :menu-props="{ auto: true }"
                item-text="Text"
                item-value="Value"
                label="Country"
                :properties-errors="propertiesErrors"
                property-name="CountryId"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col cols="12" md="12" lg="12">
              <text-area
                v-model="model.Purpose"
                label="Purpose"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="Purpose"
                @input="update"
              >
              </text-area>
            </v-col>
            <v-col v-if="!readonly" cols="12" md="12" lg="12">
              <h4>
                I agree to the
                <a @click="clickTermsAndCondition">Terms &amp; Conditions</a> of
                our operational platform
              </h4>
              <checkbox
                v-model="model.TermsAndConditionAccepted"
                label=""
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="TermsAndConditionAccepted"
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
import BackButton from "@/components/BackButton.vue";
import Checkbox from "@/components/Checkbox.vue";
import Dropdown from "@/components/Dropdown.vue";
import TextArea from "@/components/TextArea.vue";
import TextField from "@/components/TextField.vue";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import { Laboratory } from "@/models/Laboratory";
import { Role } from "@/models/Role";
import { DropdownItem } from "@/models/DropdownItem";
import { RolePublic } from "@/models/RolePublic";
import { UserRequest } from "@/models/UserRequest";
import { Component, Vue, Prop, Model } from "vue-property-decorator";
import { CountryModule } from "../../countries/store";
import { UserRequestModule } from "../store";
import { AppModule } from "../../../store/MainStore";
import { docUrl } from "./resources";
import { termsAndConditionName } from "./resources";

@Component({
  components: {
    BackButton,
    TextFieldFloat,
    TextField,
    TextArea,
    Dropdown,
    Checkbox,
  },
})
export default class UserRequestFormPublic extends Vue {
  // Props
  @Prop({ type: Boolean, default: true })
  readonly backButtonVisible: boolean;

  @Prop({ type: Boolean, default: false })
  readonly readonly: boolean;

  @Prop({ required: true, type: String, default: "User Request" })
  readonly title: string;

  @Prop({ required: true, type: Array, default: [] })
  readonly roles: Array<Role>;

  @Prop({ required: true, type: Array, default: [] })
  readonly laboratories: Array<Laboratory>;

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object }) model!: UserRequest;

  // Events
  update() {
    this.$emit("update", this.model);
  }

  validate() {
    this.$refs.form.validate();
  }

  onBack() {
    UserRequestModule.SET_ERROR(undefined);
  }

  clickTermsAndCondition(): void {
    const termsAndConditionNameString = termsAndConditionName ?? "";

    const url = termsAndConditionNameString;
    window.open(url, "_blank");
  }

  async mounted() {
    try {
      await CountryModule.ListCountriesPublic();
    } finally {
      this.update();
      AppModule.HideLoading();
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

  get rolesItems(): Array<DropdownItem> {
    const roles = this.roles;
    if (!roles) return new Array<DropdownItem>();

    return roles.map((r) => {
      return {
        Value: r.Id,
        Text: r.Name,
      };
    });
  }

  get propertiesErrors(): any {
    return UserRequestModule.ErrorMessage;
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
