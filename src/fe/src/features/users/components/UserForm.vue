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
                label="Email Address"
                :readonly="EmailReadonly"
                :properties-errors="propertiesErrors"
                property-name="Email"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-field
                v-model="model.MobilePhone"
                label="Mobile Phone"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="MobilePhone"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col cols="12" md="6" lg="6">
              <text-field
                v-model="model.BusinessPhone"
                label="Business Phone (Business)"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="BusinessPhone"
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
                label="Role"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="RoleId"
                @change="update"
              ></dropdown>
            </v-col>
            <v-col v-if="jobTitleVisible" cols="12" md="6" lg="6">
              <text-field
                v-model="model.JobTitle"
                label="Job Title"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="JobTitle"
                @input="update"
              >
              </text-field>
            </v-col>
            <v-col v-if="operationalFocalPointVisible" cols="12" md="6" lg="6">
              <checkbox
                v-model="model.OperationalFocalPoint"
                label="Operational Focal Point"
                :readonly="readonly"
                :properties-errors="propertiesErrors"
                property-name="OperationalFocalPoint"
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
import { DropdownItem } from "@/models/DropdownItem";
import { User } from "@/models/User";
import { Component, Vue, Prop, Model } from "vue-property-decorator";
import { UserModule } from "../store";
import { Role } from "@/models/Role";
import { AppModule } from "../../../store/MainStore";

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
export default class UserForm extends Vue {
  // Props
  @Prop({ type: Boolean, default: true })
  readonly backButtonVisible: boolean;

  @Prop({ type: Boolean, default: false })
  readonly readonly: boolean;

  @Prop({ type: Boolean, default: false })
  readonly create: boolean;

  @Prop({ type: Boolean, default: false })
  readonly edit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly emailEditable: boolean;

  @Prop({ required: true, type: String, default: "User Request" })
  readonly title: string;

  @Prop({ type: Array, default: true })
  readonly roles: Array<Role>;

  @Prop({ type: Boolean, default: true })
  readonly jobTitleVisible: boolean;

  @Prop({ type: Boolean, default: true })
  readonly operationalFocalPointVisible: boolean;

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object }) model!: User;

  // Events
  update() {
    this.$emit("update", this.model);
  }

  validate() {
    this.$refs.form.validate();
  }

  onBack() {
    UserModule.SET_ERROR(undefined);
  }

  updateBioHubFacilityId(bioHubFacilityId: string) {
    this.model.BioHubFacilityId = bioHubFacilityId;
    this.$emit("update", this.model);
  }

  updateCourierId(courierId: string) {
    this.model.CourierId = courierId;
    this.$emit("update", this.model);
  }

  get rolesItems(): Array<DropdownItem> {
    const Roles = this.roles;
    if (!Roles) return new Array<DropdownItem>();

    return Roles.map((r) => {
      return {
        Value: r.Id,
        Text: r.Name,
      };
    });
  }

  get EmailReadonly(): boolean {
    if (this.readonly == true) {
      return true;
    }

    if (this.create == true) {
      return false;
    }

    if (this.edit == true && this.emailEditable == true) {
      return false;
    }

    return true;
  }

  get propertiesErrors(): any {
    return UserModule.ErrorMessage;
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
