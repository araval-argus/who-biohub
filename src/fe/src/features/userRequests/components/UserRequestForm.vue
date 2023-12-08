<template>
  <v-card>
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
                :items="roles"
                item-text="Name"
                secondary-property="Description"
                item-value="Id"
                label="Role"
                :properties-errors="propertiesErrors"
                property-name="RoleId"
                @change="update"
              ></dropdown>
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
import { CountryGridItem } from "@/models/CountryGridItem";
import { UserRequest } from "@/models/UserRequest";
import { Component, Vue, Prop, Model } from "vue-property-decorator";
import { CountryModule } from "../../countries/store";
import { UserRequestModule } from "../store";
import { Role } from "@/models/Role";

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
export default class UserRequestForm extends Vue {
  // Props
  @Prop({ type: Boolean, default: true })
  readonly backButtonVisible: boolean;

  @Prop({ type: Boolean, default: false })
  readonly readonly: boolean;

  @Prop({ required: true, type: String, default: "User Request" })
  readonly title: string;

  @Prop({ type: Array, default: true })
  readonly roles: Array<Role>;

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

  updateLaboratoryId(laboratoryId: string) {
    this.model.LaboratoryId = laboratoryId;
    this.$emit("update", this.model);
  }

  updateCountryId(countryId: string) {
    this.model.CountryId = countryId;
    this.$emit("update", this.model);
  }

  onBack() {
    UserRequestModule.SET_ERROR(undefined);
  }

  get countriesItems(): Array<CountryGridItem> {
    const Countries = CountryModule.Countries;
    if (!Countries) return new Array<CountryGridItem>();

    return Countries.map((l) => {
      return {
        Id: l.Id,
        Name: l.Name,
        Uncode: l.Uncode,
        IsActive: l.IsActive,
        FullName: l.FullName,
        Latitude: l.Latitude,
        Longitude: l.Longitude,
        Iso2: l.Iso2,
        Iso3: l.Iso3,
        GmtHour: l.GmtHour,
        GmtMin: l.GmtMin,
      };
    });
  }

  // get rolesItems(): Array<RoleGridItem> {
  //   const Roles = RoleModule.Roles.filter((r) => r.AddToRegistration);
  //   if (!Roles) return new Array<RoleGridItem>();

  //   return Roles.map((r) => {
  //     return {
  //       Id: r.Id,
  //       Name: r.Name,
  //       Description: r.Description,
  //       RoleType: r.RoleType,
  //       AddToRegistration: r.AddToRegistration.toString(),
  //     };
  //   });
  // }

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
