<template>
  <div>
    <v-data-table
      :headers="headers"
      :items="pickupGridItems"
      :search="search"
      :sort-by.sync="sortBy"
      :sort-desc.sync="sortDesc"
    >
      <template #[`item.ShipmentReferenceNumber`]="{ item }">
        <v-text-field
          v-if="canEdit && isPickup"
          v-model="item.ShipmentReferenceNumber"
          :error-messages="getShipmentReferenceNumberErrorMessage(item)"
          :error-count="getShipmentReferenceNumberErrorMessage(item).length"
          @input="updateShipmentReferenceNumber(item)"
        ></v-text-field>
        <span v-else>{{ item.ShipmentReferenceNumber }}</span>
      </template>
      <template v-if="isPickup" #[`item.DateOfPickup`]="{ item }">
        <date-picker
          v-if="canEdit"
          v-model="item.DateOfPickup"
          :readonly="!canEdit"
          property-name="DateOfPickup"
          :properties-errors="getDateOfPickupErrorMessage(item)"
          @input="updateDateOfPickup(item)"
        >
        </date-picker>

        <span v-else>{{ getFormatDate(item.DateOfPickup) }}</span>
      </template>
      <template v-if="isDelivery" #[`item.DateOfDelivery`]="{ item }">
        <date-picker
          v-if="canEdit"
          v-model="item.DateOfDelivery"
          :readonly="!canEdit"
          property-name="DateOfDelivery"
          :properties-errors="getDateOfDeliveryErrorMessage(item)"
          @input="updateDateOfDelivery(item)"
        >
        </date-picker>

        <span v-else>{{ getFormatDate(item.DateOfDelivery) }}</span>
      </template>
      <template v-if="isDelivery" #[`item.TransportModeId`]="{ item }">
        <v-select
          v-if="canEdit"
          v-model="item.TransportModeId"
          :items="transportModeItems"
          item-text="Text"
          item-value="Value"
          @change="updateTransportMode(item)"
          :error-messages="getTransportModeErrorMessage(item)"
          :error-count="errorCount(item)"
          :error="isError(item)"
        ></v-select>
        <span v-else>{{ item.TransportModeName }}</span>
      </template>
    </v-data-table>

    <ConfirmationDialogComponent
      ref="confirmationDialogComponent"
      @onConfirm="executeDelete"
    >
    </ConfirmationDialogComponent>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import ConfirmationDialogComponent from "../../../components/ConfirmationDialogComponent.vue";
import { BookingFormOfSMTAPickupDeliveryGridItem } from "@/models/BookingFormOfSMTAPickupDeliveryGridItem";
import { BookingFormOfSMTA } from "@/models/BookingFormOfSMTA";
import DatePicker from "@/components/DatePicker.vue";
import TextField from "@/components/TextField.vue";
import { TransportModeModule } from "../../transportModes/store";
import { DropdownItem } from "@/models/DropdownItem";

@Component({
  components: { ConfirmationDialogComponent, DatePicker, TextField },
})
export default class PickupDeliveryCompletedTable extends Vue {
  private deleteClicked = false;

  private search = "";

  private baseHeaders = [
    {
      text: "Booking Form",
      align: "start",
      sortable: true,
      value: "BookingForm",
    },
    {
      text: "Shipment Reference Number",
      align: "start",
      sortable: true,
      value: "ShipmentReferenceNumber",
    },
  ];

  private pickupHeaders = [
    {
      text: "Date Of Pickup",
      align: "start",
      sortable: true,
      value: "DateOfPickup",
    },
  ];

  private deliveryHeaders = [
    {
      text: "Date Of Delivery",
      align: "start",
      sortable: true,
      value: "DateOfDelivery",
    },

    {
      text: "Transport Mode",
      align: "start",
      sortable: true,
      value: "TransportModeId",
    },
  ];

  @Prop({ type: Array, default: ["UserName"] })
  readonly sortBy: Array<string>;

  @Prop({ type: Array, default: [false] })
  readonly sortDesc: Array<string>;

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Array, default: [] })
  readonly bookingForms: Array<BookingFormOfSMTA>;

  @Prop({ type: Boolean, default: false })
  readonly isPickup: boolean;

  @Prop({ type: Boolean, default: false })
  readonly isDelivery: boolean;

  get pickupGridItems(): Array<BookingFormOfSMTAPickupDeliveryGridItem> {
    if (!this.bookingForms)
      return new Array<BookingFormOfSMTAPickupDeliveryGridItem>();

    return this.bookingForms.map((l) => {
      return {
        Id: l.Id,
        BookingForm: l.TransportCategoryName,
        ShipmentReferenceNumber: l.ShipmentReferenceNumber,
        DateOfPickup: l.DateOfPickup,
        DateOfDelivery: l.DateOfDelivery,
        WorklistItemId: l.WorklistItemId,
        TransportModeId: l.TransportModeId,
        TransportModeName: l.TransportModeName,
        TransportModeDescription: l.TransportModeDescription,
      };
    });
  }

  $refs!: {
    confirmationDialogComponent: ConfirmationDialogComponent;
  };

  get hasActionHeader(): boolean {
    return false;
  }

  get headers(): any {
    let headers = this.baseHeaders;

    if (this.isPickup) {
      headers = headers.concat(this.pickupHeaders);
    }

    if (this.isDelivery) {
      headers = headers.concat(this.deliveryHeaders);
    }

    return headers;
  }

  updateShipmentReferenceNumber(
    item: BookingFormOfSMTAPickupDeliveryGridItem
  ): void {
    this.$emit(
      "updateBookingFormShipmentReferenceNumber",
      item.Id,
      item.ShipmentReferenceNumber
    );
  }

  getShipmentReferenceNumberErrorMessage(
    item: BookingFormOfSMTAPickupDeliveryGridItem
  ): Array<string> {
    if (
      item.ShipmentReferenceNumber === undefined ||
      item.ShipmentReferenceNumber === "" ||
      item.ShipmentReferenceNumber === null
    ) {
      return ["'Field' is Required"];
    } else {
      return [];
    }
  }

  getDateOfPickupErrorMessage(
    item: BookingFormOfSMTAPickupDeliveryGridItem
  ): Map<string, Array<string>> {
    let errorList = new Map<string, Array<string>>();

    if (item.DateOfPickup === undefined || item.DateOfPickup === null) {
      errorList.set("DateOfPickup", ["'Field' is Required"]);
    } else {
      errorList.delete("DateOfPickup");
    }
    return errorList;
  }

  updateDateOfPickup(item: BookingFormOfSMTAPickupDeliveryGridItem): void {
    this.$emit("updateBookingFormDateOfPickup", item.Id, item.DateOfPickup);
  }

  get transportModeItems(): Array<DropdownItem> {
    const TransportModes = TransportModeModule.TransportModes;
    if (!TransportModes) return new Array<DropdownItem>();

    return TransportModes.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
  }

  getDateOfDeliveryErrorMessage(
    item: BookingFormOfSMTAPickupDeliveryGridItem
  ): Map<string, Array<string>> {
    let errorList = new Map<string, Array<string>>();

    if (item.DateOfDelivery === undefined || item.DateOfDelivery === null) {
      errorList.set("DateOfDelivery", ["'Field' is Required"]);
    } else {
      errorList.delete("DateOfDelivery");
    }
    return errorList;
  }

  getTransportModeErrorMessage(
    item: BookingFormOfSMTAPickupDeliveryGridItem
  ): string | Array<string> {
    if (
      item.TransportModeId === undefined ||
      item.TransportModeId === null ||
      item.TransportModeId === ""
    ) {
      return ["'Transport Mode' is Required"];
    } else {
      return "";
    }
  }

  isError(item: BookingFormOfSMTAPickupDeliveryGridItem): boolean {
    if (
      item.TransportModeId === undefined ||
      item.TransportModeId === null ||
      item.TransportModeId === ""
    ) {
      return true;
    }
    return false;
  }

  errorCount(item: BookingFormOfSMTAPickupDeliveryGridItem): number {
    if (
      item.TransportModeId === undefined ||
      item.TransportModeId === null ||
      item.TransportModeId === ""
    ) {
      return 1;
    }
    return 0;
  }

  updateDateOfDelivery(item: BookingFormOfSMTAPickupDeliveryGridItem): void {
    this.$emit("updateBookingFormDateOfDelivery", item.Id, item.DateOfDelivery);
  }

  updateTransportMode(item: BookingFormOfSMTAPickupDeliveryGridItem): void {
    this.$emit("updateBookingFormTransportMode", item.Id, item.TransportModeId);
  }

  getBookingForm(id: string): BookingFormOfSMTA {
    const bookingForm: BookingFormOfSMTA | undefined = this.bookingForms.find(
      (x) => x.Id == id
    );
    if (bookingForm === undefined)
      throw {
        message: `Unexpected undefined for Booking Form with id ${id}`,
      };
    return bookingForm;
  }

  getFormatDate(date: Date | string): string {
    let parsedDate = new Date(date);
    const month = (parsedDate.getMonth() + 1).toString().padStart(2, "0");
    const day = parsedDate.getDate().toString().padStart(2, "0");
    const year = parsedDate.getFullYear();

    return day + "/" + month + "/" + year;
  }
}
</script>

<style lang="scss">
tr {
  cursor: pointer;
}
</style>
