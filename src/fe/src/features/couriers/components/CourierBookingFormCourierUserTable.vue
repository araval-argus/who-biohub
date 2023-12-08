<template>
  <div>
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
    </v-data-table>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { CourierBookingFormUser } from "@/models/CourierBookingFormUser";
import { CourierModule } from "../store";

@Component({ components: {} })
export default class CourierBookingFormCourierUserTable extends Vue {
  private deleteClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "Name of State Member",
      align: "start",
      sortable: true,
      value: "Country",
    },
    {
      text: "Operational Focal Point",
      align: "start",
      sortable: true,
      value: "UserName",
    },
    {
      text: "Email",
      align: "start",
      sortable: true,
      value: "Email",
    },
    {
      text: "Job Title",
      align: "start",
      sortable: true,
      value: "JobTitle",
    },
    {
      text: "Mobile Phone",
      align: "start",
      sortable: true,
      value: "MobilePhone",
    },
    {
      text: "Business Line",
      align: "start",
      sortable: true,
      value: "BusinessLine",
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

  @Prop({ type: String, default: "" })
  readonly bookingFormId: string;

  get bookingFormCourierUserGridItems(): Array<CourierBookingFormUser> {
    return CourierModule.BookingFormCourierUsers;
  }

  get hasActionHeader(): boolean {
    return false;
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
