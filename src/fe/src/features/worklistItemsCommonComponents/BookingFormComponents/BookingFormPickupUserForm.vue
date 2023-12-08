<template>
  <div class="mb-5">
    <v-card-text>
      <dropdown
        v-if="canEdit"
        v-model="userId"
        :items="eligibleUserList"
        item-text="UserName"
        item-value="UserId"
        label="Person to be contacted for the pick-up"
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
    <v-card-text>
      <p v-if="canEdit">
        If you don't find the pick-up person, please register him/her in the
        tool and then come back here to complete the form
      </p>
    </v-card-text>

    <BookingFormPickupUserTable
      :can-edit="canEdit"
      v-model="model"
      @removeBookingFormPickupUser="removeBookingFormPickupUser"
    ></BookingFormPickupUserTable>

    <v-card-text v-if="BookingFormPickupUserWarningVisible">
      <h4 style="color: red">Please add at least a pick-up person</h4>
    </v-card-text>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import Dropdown from "@/components/Dropdown.vue";
//import { WorklistToBioHubItemModule } from "../../store";
import { UserModule } from "../../users/store";
import CardActionsGenericButton from "@/components/CardActionsGenericButton.vue";
import { BookingFormOfSMTA } from "@/models/BookingFormOfSMTA";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import BookingFormPickupUserTable from "./BookingFormPickupUserTable.vue";

@Component({
  components: {
    Dropdown,
    BookingFormPickupUserTable,
    CardActionsGenericButton,
  },
})
export default class BookingFormPickupUserForm extends Vue {
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

  @Prop({ type: String, default: "" })
  readonly bookingFormId: string;

  @Prop({ type: Array, default: [] })
  readonly allPossiblePickupUsers: Array<WorklistItemUser>;

  @Model("update", { type: Object }) model!: BookingFormOfSMTA;

  $refs!: {
    form: any;
  };

  // Events
  addUser() {
    const userToAdd = this.allPossiblePickupUsers.find(
      (x) => x.UserId == this.userId
    );

    if (userToAdd !== undefined) {
      this.$emit("addBookingFormPickupUser", userToAdd);
    }
    this.userId = "";
  }

  removeBookingFormPickupUser(id: string) {
    this.$emit("removeBookingFormPickupUser", id);
  }

  validate() {
    this.$refs.form.validate();
  }

  get BookingFormPickupUserWarningVisible(): boolean {
    const bookingFormPickupUsers = this.model.BookingFormPickupUsers;
    return bookingFormPickupUsers.length == 0;
  }

  get eligibleUserList(): Array<WorklistItemUser> {
    if (!this.allPossiblePickupUsers) return new Array<WorklistItemUser>();
    let selectedUsers = this.model.BookingFormPickupUsers;

    let filteredUsers = new Array<WorklistItemUser>();
    this.allPossiblePickupUsers.forEach((item) => {
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
