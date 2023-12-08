<template>
  <v-card class="mb-5">
    <v-card-text>
      <dropdown
        v-model="userId"
        :items="eligibleUserList"
        item-text="UserName"
        item-value="UserId"
        label="Name of the Focal Point to be contacted for BioHub purposes"
        :readonly="!canEdit"
        property-name="UserId"
        @change="update"
      ></dropdown>

      <CardActionsGenericButton
        v-if="userId !== '' && canEdit"
        text="Add"
        @click="addUser"
      >
      </CardActionsGenericButton>
    </v-card-text>
    <v-card-text v-if="laboratoryFocalPointWarningVisible">
      <h4 style="color: red">Please add at least a Laboratory Focal Point</h4>
    </v-card-text>
    <v-card-text>
      <LaboratoryFocalPointsTable
        :can-edit="canEdit"
        :laboratory-focal-points="laboratoryFocalPoints"
        @updateLaboratoryFocalPoint="updateLaboratoryFocalPoint"
        @removeLaboratoryFocalPoint="removeLaboratoryFocalPoint"
      ></LaboratoryFocalPointsTable>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import TextArea from "@/components/TextArea.vue";
import Dropdown from "@/components/Dropdown.vue";
import CardActionsGenericButton from "../../../components/CardActionsGenericButton.vue";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import LaboratoryFocalPointsTable from "./LaboratoryFocalPointsTable.vue";
@Component({
  components: {
    CardActionsGenericButton,
    TextFieldFloat,
    TextField,
    Dropdown,
    TextArea,
    LaboratoryFocalPointsTable,
  },
})
export default class LaboratoryFocalPointForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private userId = "";
  // Props

  @Prop({ required: true, type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ required: true, type: Array, default: [] })
  readonly laboratoryFocalPoints: Array<WorklistItemUser>;

  @Prop({ required: true, type: Array, default: [] })
  readonly allLaboratoryUsers: Array<WorklistItemUser>;

  $refs!: {
    form: any;
  };

  // Events
  addUser() {
    const userId = this.userId;
    this.$emit("addLaboratoryFocalPoint", userId);
    this.userId = "";
  }

  removeLaboratoryFocalPoint(id: string) {
    this.$emit("removeLaboratoryFocalPoint", id);
  }

  updateLaboratoryFocalPoint(user: WorklistItemUser) {
    this.$emit("updateLaboratoryFocalPoint", user);
  }

  validate() {
    this.$refs.form.validate();
  }

  get LaboratoryFocalPointWarningVisible(): boolean {
    return this.laboratoryFocalPoints.length == 0;
  }

  get eligibleUserList(): Array<WorklistItemUser> {
    const totalUsers = this.allLaboratoryUsers;

    if (!totalUsers) return new Array<WorklistItemUser>();

    const selectedUsers = this.laboratoryFocalPoints;

    let filteredUsers = new Array<WorklistItemUser>();
    totalUsers.forEach((item) => {
      let found = false;
      selectedUsers.forEach((elem) => {
        if (elem.UserId == item.UserId) {
          found = true;
        }
      });
      if (found == false) {
        filteredUsers.push(item);
      }
    });

    return filteredUsers.map((l) => {
      return {
        Id: l.Id,
        UserId: l.UserId,
        WorklistItemId: l.WorklistItemId,
        ParentId: l.ParentId,
        UserName: l.UserName,
        Country: l.Country,
        Laboratory: l.Laboratory,
        BioHubFacility: l.BioHubFacility,
        MobilePhone: l.MobilePhone,
        BusinessPhone: l.BusinessPhone,
        Email: l.Email,
        JobTitle: l.JobTitle,
        Other: l.Other,
        LaboratoryId: l.LaboratoryId,
        BioHubFacilityId: l.BioHubFacilityId,
        CourierId: l.CourierId,
      };
    });
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
