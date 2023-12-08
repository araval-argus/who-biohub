<template>
  <div>
    <v-card class="mb-5">
      <v-card-text>
        <v-data-table
          :headers="headers"
          :items="bookingFormCourierUserGridItems"
          :search="search"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
        >
          <template #[`item.Email`]="{ item }">
            <a :href="getMailTo(item.Email)">{{ item.Email }}</a>
          </template>
          <template v-if="hasActionHeader" #[`item.actions`]="{ item }">
            <v-icon v-if="canEdit" small @click="deleteItem(item)">
              mdi-delete-circle-outline
            </v-icon>
          </template>
        </v-data-table>
      </v-card-text>
    </v-card>
    <ConfirmationDialogComponent
      ref="confirmationDialogComponent"
      @onConfirm="executeDelete"
    >
    </ConfirmationDialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model } from "vue-property-decorator";
import ConfirmationDialogComponent from "@/components/ConfirmationDialogComponent.vue";
import { WorklistItemUserGridItem } from "@/models/WorklistItemUserGridItem";
import { WorklistItemUser } from "@/models/WorklistItemUser";
//import { WorklistToBioHubItemModule } from "../../store";
import { BookingFormOfSMTA } from "@/models/BookingFormOfSMTA";

@Component({ components: { ConfirmationDialogComponent } })
export default class BookingFormCourierUserTable extends Vue {
  private deleteClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "Email",
      align: "start",
      sortable: true,
      value: "Email",
    },
    {
      text: "Name",
      align: "start",
      sortable: true,
      value: "UserName",
    },
    {
      text: "Mobile Phone",
      align: "start",
      sortable: true,
      value: "MobilePhone",
    },
    {
      text: "LandLine",
      align: "start",
      sortable: true,
      value: "BusinessPhone",
    },
  ];

  private actionHeader = [
    {
      text: "Actions",
      align: "start",
      sortable: false,
      value: "actions",
    },
  ];

  private editableHeaders = this.actionHeader.concat(this.baseHeaders);

  @Prop({ type: Array, default: ["UserName"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Model("update", { type: Object }) model!: BookingFormOfSMTA;

  get bookingFormCourierUserGridItems(): Array<WorklistItemUserGridItem> {
    let bookingFormCourierUsers = this.model.BookingFormCourierUsers;

    if (
      bookingFormCourierUsers === undefined ||
      bookingFormCourierUsers.length == 0
    )
      return new Array<WorklistItemUserGridItem>();

    return bookingFormCourierUsers.map((l) => {
      return {
        Id: l.Id,
        UserId: l.UserId,
        UserName: l.UserName,
        Country: l.Country,
        Laboratory: l.Laboratory,
        BioHubFacility: l.BioHubFacility,
        MobilePhone: l.MobilePhone,
        BusinessPhone: l.BusinessPhone,
        Email: l.Email,
        JobTitle: l.JobTitle,
        Other: l.Other,
        WorklistItemId: l.WorklistItemId,
        LaboratoryId: l.LaboratoryId,
        BioHubFacilityId: l.BioHubFacilityId,
        CourierId: l.CourierId,
      };
    });
  }

  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  get hasActionHeader(): boolean {
    return this.canEdit;
  }

  get headers(): any {
    if (this.hasActionHeader == true) {
      return this.editableHeaders;
    }
    return this.baseHeaders;
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }

  executeDelete(item: WorklistItemUserGridItem): void {
    this.$emit("removeBookingFormCourierUser", item.Id);
    this.deleteClicked = false;
  }

  deleteItem(item: WorklistItemUserGridItem): void {
    this.deleteClicked = true;
    this.$refs.confirmationDialogComponent.showDialog(item);
  }

  getMailTo(email: string): string {
    return "mailto:" + email;
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
