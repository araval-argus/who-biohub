<template>
  <div class="mb-5">
    <v-card-text>
      <dropdown
        v-if="canEdit"
        v-model="model.CourierId"
        :items="courierList"
        item-text="Text"
        item-value="Value"
        label="Select Courier"
        :readonly="!canEdit"
        property-name="Id"
        @change="update"
      ></dropdown>
    </v-card-text>
    <v-card-text v-if="courierInfo !== undefined">
      <v-row>
        <v-col cols="12" md="12" lg="12">
          <h4>Courier Info</h4>
        </v-col>
        <v-col cols="12" md="6" lg="6">
          <text-field v-model="courierInfo.Address" label="Address" readonly>
          </text-field>
        </v-col>
        <v-col cols="12" md="6" lg="6">
          <text-field
            v-model="courierInfo.BusinessPhone"
            label="Business (landline)"
            readonly
          >
          </text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="6" lg="6">
          <text-field
            v-model="courierInfo.WHOAccountNumber"
            label="WHO Account Number"
            readonly
          >
          </text-field>
        </v-col>
        <v-col cols="12" md="6" lg="6">
          <text-field v-model="courierInfo.Country" label="Country" readonly>
          </text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="6" lg="6">
          <text-field v-model="courierInfo.Email" label="Email" readonly>
          </text-field>
        </v-col>
      </v-row>
    </v-card-text>
    <v-card-text>
      <dropdown
        v-if="canEdit"
        v-model="userId"
        :items="eligibleUserList"
        item-text="UserName"
        item-value="UserId"
        label="Contact Person"
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
        If you don't find the contact person, please register him/her in the
        tool under Courier
      </p>
    </v-card-text>

    <v-card-text>
      <BookingFormCourierUserTable
        :can-edit="canEdit"
        v-model="model"
        @removeBookingFormCourierUser="removeBookingFormCourierUser"
      ></BookingFormCourierUserTable>
    </v-card-text>
    <v-card-text v-if="CourierFormPickupUserWarningVisible">
      <h4 style="color: red">
        Please add a courier and at least a contact person
      </h4>
    </v-card-text>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import Dropdown from "@/components/Dropdown.vue";
import CardActionsGenericButton from "@/components/CardActionsGenericButton.vue";
import { DropdownItem } from "@/models/DropdownItem";
import { CourierGridItem } from "@/models/CourierGridItem";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import BookingFormCourierUserTable from "./BookingFormCourierUserTable.vue";
import { BookingFormOfSMTA } from "@/models/BookingFormOfSMTA";
import TextField from "@/components/TextField.vue";
import { CourierModule } from "../../couriers/store";
import { CountryModule } from "../../countries/store";
import { Courier } from "@/models/Courier";

@Component({
  components: {
    Dropdown,
    BookingFormCourierUserTable,
    CardActionsGenericButton,
    TextField,
  },
})
export default class BookingFormCourierUserForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private userId = "";
  // Props

  @Model("update", { type: Object }) model!: BookingFormOfSMTA;

  @Prop({ required: true, type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Array, default: [] })
  readonly allPossibleCourierUsers: Array<WorklistItemUser>;

  @Prop({ type: Array, default: [] })
  readonly couriers: Array<Courier>;

  $refs!: {
    form: any;
  };

  update() {
    this.$emit("BookingFormCourierUserForm", this.model);
  }

  // Events
  addUser() {
    const totalUsers = this.allPossibleCourierUsers;

    const userToAdd = totalUsers.find((x) => x.UserId == this.userId);

    if (userToAdd !== undefined) {
      this.$emit("addBookingFormCourierUser", userToAdd);
    }
    this.userId = "";
  }

  removeBookingFormCourierUser(id: string) {
    this.$emit("removeBookingFormCourierUser", id);
  }

  validate() {
    this.$refs.form.validate();
  }

  get courierList(): Array<DropdownItem> {
    const couriers = this.couriers;
    if (!couriers) return new Array<DropdownItem>();

    return couriers.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  get courierInfo(): CourierGridItem | undefined {
    const couriers = this.couriers;

    const courier = couriers.filter((c) => c.Id == this.model.CourierId);

    if (courier == undefined || courier.length == 0) {
      return undefined;
    }

    var country = CountryModule.Countries.filter((c) => {
      return courier[0].CountryId == c.Id;
    }).map((m) => {
      return {
        countryName: m.Name,
      };
    });

    if (country.length == 0) {
      country.push({
        countryName: "",
      });
    }

    return {
      Id: courier[0].Id,
      Name: courier[0].Name,
      WHOAccountNumber: courier[0].WHOAccountNumber,
      BusinessPhone: courier[0].BusinessPhone,
      Email: courier[0].Email,
      IsActive: courier[0].IsActive,
      Description: courier[0].Description,
      Address: courier[0].Address,
      Latitude: courier[0].Latitude,
      Longitude: courier[0].Longitude,
      Country: country[0].countryName,
      CountryId: courier[0].CountryId,
    } as CourierGridItem;
  }

  get eligibleUserList(): Array<WorklistItemUser> {
    const totalUsersForTheSelectedCourier = this.allPossibleCourierUsers.filter(
      (c) => {
        return c.CourierId == this.model.CourierId;
      }
    );

    if (!totalUsersForTheSelectedCourier) return new Array<WorklistItemUser>();

    let selectedUsers = this.model.BookingFormCourierUsers;

    let filteredUsers = new Array<WorklistItemUser>();

    if (!selectedUsers) {
      filteredUsers = totalUsersForTheSelectedCourier;
    } else {
      totalUsersForTheSelectedCourier.forEach((item) => {
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
    }

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

  get CourierFormPickupUserWarningVisible(): boolean {
    const bookingFormCourierUsers = this.model.BookingFormCourierUsers;

    return bookingFormCourierUsers.length == 0;
  }

  @Watch("model.CourierId")
  CourierIdChange() {
    this.$emit("clearBookingFormCourierUser");
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
