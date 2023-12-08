<template>
  <div v-if="model">
    <UserRequestFormPublic
      ref="userRequestFormPublic"
      v-model="model"
      :title="title"
      :readonly="true"
      :roles="roles"
      :laboratories="laboratories"
    >
    </UserRequestFormPublic>
    <v-card class="mt-5">
      <v-card-text>
        <v-form ref="form" lazy-validation :readonly="readonly" class="ma-2">
          <div>
            <v-row>
              <v-col cols="12" md="12" lg="12">
                <checkbox
                  v-model="model.IsConfirmed"
                  label="ID confirmed"
                  :readonly="readonly"
                  :properties-errors="propertiesErrors"
                  property-name="IsConfirmed"
                  @change="update"
                >
                </checkbox>
              </v-col>
              <v-col v-if="model.IsConfirmed" cols="12" md="6" lg="6">
                <date-picker
                  v-model="model.ConfirmationDate"
                  label="Select the ID confirmation date"
                  :readonly="readonly"
                  :properties-errors="propertiesErrors"
                  property-name="ConfirmationDate"
                  @input="update"
                >
                </date-picker>
              </v-col>
              <v-col v-if="model.IsConfirmed" cols="12" md="12" lg="12">
                <dropdown
                  v-model="model.Status"
                  :items="statusItems"
                  item-text="Text"
                  item-value="Value"
                  label="Response"
                  :readonly="readonly"
                  :properties-errors="propertiesErrors"
                  property-name="Status"
                  @change="updateStatusDropdown"
                ></dropdown>
              </v-col>
              <v-col
                v-if="model.IsConfirmed && model.Status != 0"
                cols="12"
                md="12"
                lg="12"
              >
                <text-editor
                  v-model="model.Message"
                  :label="textEditorLabel"
                  :readonly="readonly"
                  :properties-errors="propertiesErrors"
                  property-name="Message"
                  @input="update"
                >
                </text-editor>
              </v-col>
            </v-row>
          </div>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <slot></slot>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script lang="ts">
import Checkbox from "@/components/Checkbox.vue";
import DatePicker from "@/components/DatePicker.vue";
import Dropdown from "@/components/Dropdown.vue";
import { UserRequestStatusModule } from "@/features/userRequestsStatuses/store";
import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";
import { DropdownItem } from "@/models/DropdownItem";
import { UserRequest } from "@/models/UserRequest";
import { Component, Vue, Prop, Model } from "vue-property-decorator";
import { UserRequestModule } from "../store";
import UserRequestFormPublic from "./UserRequestFormPublic.vue";
import { Laboratory } from "@/models/Laboratory";
import { Role } from "@/models/Role";
import TextEditor from "@/components/TextEditor.vue";
import { AppModule } from "../../../store/MainStore";

@Component({
  components: {
    TextEditor,
    DatePicker,
    Dropdown,
    Checkbox,
    UserRequestFormPublic,
  },
})
export default class UserRequestFormApprove extends Vue {
  @Prop({ type: Boolean, default: false })
  readonly readonly: boolean;

  @Prop({ required: true, type: Array, default: [] })
  readonly roles: Array<Role>;

  @Prop({ required: true, type: Array, default: [] })
  readonly laboratories: Array<Laboratory>;

  @Prop({ type: String, default: "Pending request details" })
  readonly title: string;

  $refs!: {
    form: any;
  };
  // Model
  @Model("update", { type: Object }) model!: UserRequest;

  get textEditorLabel(): string {
    if (this.readonly == false) {
      return "Message (the message is automatically load from a set of prefefined messages based on the type of the response)";
    }
    return "";
  }

  // Events
  update() {
    this.$emit("update", this.model);
  }

  async updateStatusDropdown(value: any) {
    this.model.Message =
      UserRequestStatusModule.UserRequestStatuses?.find(
        (x) => x.Status == value
      )?.Message ?? "";

    this.model.Message = this.model.Message.replace(
      "{firstname}",
      this.model.FirstName
    );
    this.model.Message = this.model.Message.replace(
      "{lastname}",
      this.model.LastName
    );

    this.$emit("update", this.model);
  }

  validate() {
    this.$refs.form.validate();
  }

  updateInstituteName(instituteName: string) {
    this.model.InstituteName = instituteName;
    this.$emit("update", this.model);
  }

  onBack() {
    UserRequestModule.SET_ERROR(undefined);
  }

  get statusItems(): Array<DropdownItem> {
    let items: DropdownItem[] = [];

    items.push({
      Value: UserRegistrationStatus.Pending,
      Text: "Please Approve or Reject",
    });

    items.push({
      Value: UserRegistrationStatus.Approved,
      Text: "Approve",
    });

    items.push({
      Value: UserRegistrationStatus.Rejected,
      Text: "Reject",
    });

    return items;
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
