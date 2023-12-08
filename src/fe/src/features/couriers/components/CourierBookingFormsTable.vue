<template>
  <div>
    <v-card class="mb-5">
      <v-card-title>
        <h2>{{ title }}</h2>
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Table Search"
          single-line
          hide-details
          class="mr-8"
        ></v-text-field>
        <v-btn v-if="canCreate" color="primary" @click="create"> Create </v-btn>
      </v-card-title>
      <v-card-text>
        <div v-if="loading">
          <v-skeleton-loader type="table-tbody"></v-skeleton-loader>
          <v-skeleton-loader type="table-tfoot"></v-skeleton-loader>
        </div>
        <v-data-table
          v-else
          :headers="headers"
          :items="courierBookingFormGridItems"
          :search="search"
          :custom-filter="customSearch"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
          @click:row="selected"
        >
          <template #[`item.Email`]="{ item }">
            <a :href="getMailTo(item.Email)" @click="emailClicked">{{
              item.Email
            }}</a>
          </template>
        </v-data-table>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../components/ConfirmationDialogComponent.vue";
import { CourierBookingForm } from "@/models/CourierBookingForm";
import { CourierBookingFormGridItem } from "@/models/CourierBookingFormGridItem";
import { CourierModule } from "../store";
import { CountryModule } from "../../countries/store";
import BackButton from "@/components/BackButton.vue";
import { customTableSearch } from "../../../utils/helper";

@Component({ components: { ConfirmationDialogComponent, BackButton } })
export default class CourierBookingFormBookingFormsTable extends Vue {
  private search = "";

  private emailClick = false;

  private title = "Booking Forms";
  private baseHeaders = [
    {
      text: "In-Application Shipment Reference Number",
      align: "start",
      sortable: true,
      value: "WorklistReferenceNumber",
    },
    {
      text: "Shipment Reference Number",
      align: "start",
      sortable: true,
      value: "ShipmentReferenceNumber",
    },
    {
      text: "Shipment Direction",
      align: "start",
      sortable: true,
      value: "ShipmentDirection",
    },
    {
      text: "Date",
      align: "start",
      sortable: true,
      value: "Date",
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

  @Prop({ type: Boolean, default: true })
  readonly loading: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canCreate: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canDelete: boolean;

  @Prop({ type: Array, default: ["Date"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [true, false] })
  readonly sortDesc: Array<string>;

  get courierBookingFormGridItems(): Array<CourierBookingFormGridItem> {
    const courierBookingForms = CourierModule.CourierBookingForms;
    if (!courierBookingForms) return new Array<CourierBookingFormGridItem>();

    return courierBookingForms.map((l) => {
      return {
        Id: l.Id,
        WorklistToBioHubItemId: l.WorklistToBioHubItemId,
        WorklistFromBioHubItemId: l.WorklistFromBioHubItemId,
        ShipmentReferenceNumber: l.ShipmentReferenceNumber,
        ShipmentDirection: l.ShipmentDirection,
        WorklistReferenceNumber: l.WorklistReferenceNumber,
        Date: this.getFormatDate(l.Date),
      };
    });
  }
  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  customSearch(value: any, search: string | null, item: any): boolean {
    return customTableSearch(value, search, item);
  }

  selected(item: CourierBookingFormGridItem): void {
    if (this.emailClick == false) {
      const lab: CourierBookingForm = this.getCourierBookingForm(item.Id);
      this.$emit("selected", lab);
    }
    this.emailClick = false;
  }

  getCourierBookingForm(id: string): CourierBookingForm {
    const lab: CourierBookingForm | undefined =
      CourierModule.CourierBookingForms.find((x) => x.Id == id);
    if (lab === undefined)
      throw {
        message: `Unexpected undefined for CourierBookingForm with id ${id}`,
      };
    return lab;
  }

  getMailTo(email: string): string {
    return "mailto:" + email;
  }

  emailClicked(): void {
    this.emailClick = true;
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
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
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
