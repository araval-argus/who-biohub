<template>
  <div>
    <v-card v-if="WaitForPickUpCompleted" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <PickupDeliveryCompletedTable
          :can-edit="hasSubmitPermissionByStatus"
          :booking-forms="BookingForms"
          :is-pickup="true"
          @updateBookingFormShipmentReferenceNumber="
            updateShipmentReferenceNumber
          "
          @updateBookingFormDateOfPickup="updateDateOfPickup"
        >
        </PickupDeliveryCompletedTable>

        <v-row v-if="model.IsPast == true">
          <v-col cols="12" sm="6" md="6"> </v-col>
          <v-col cols="12" sm="6" md="6">
            <date-picker
              v-if="model.IsPast"
              v-model="model.AssignedOperationDate"
              label="Assigned Operation Date"
              :readonly="!hasSubmitPermissionByStatus"
              property-name="AssignedOperationDate"
              :properties-errors="propertiesErrors"
              @input="update"
            >
            </date-picker>
          </v-col>
        </v-row>

        <CardActionsGenericButton
          color="primary"
          style="display: inline-block; float: right"
          text="Save As Draft"
          @click="saveAsDraft"
        >
        </CardActionsGenericButton>
        <CardActionsGenericButton
          v-if="IsBookingFormPickupCompleted"
          style="display: inline-block; float: right"
          text="Submit"
          @click="submit"
        >
        </CardActionsGenericButton>
      </v-card-text>
      <v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <template v-if="hasSubmitPermissionByStatus">
            <p v-if="CanSubmitSMTA1ShipmentDocuments">
              <strong
                >it is appreciated if you could submit other shipment-related
                documents (e.g. Packiling List, Customs Declaration) using the
                'Upload' button below</strong
              >
            </p>
          </template>
          <template v-else>
            <p v-if="CanSubmitSMTA1ShipmentDocuments">
              Although no actions should be taken from your side at this stage,
              <strong
                >it is appreciated if you could submit other shipment-related
                documents (e.g. Packiling List, Customs Declaration) using the
                'Upload' button below</strong
              >, which are fianlized in communication between you and the
              courier company. You can find templates for some of the documents
              here.
            </p>
            <p v-else>No action should be taken from your side at this stage</p>
          </template>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card-text>
      <v-card-text>
        <ShipmentDocumentComponent v-model="model" @downloadFile="downloadFile">
        </ShipmentDocumentComponent>
      </v-card-text>
    </v-card>

    <v-card v-if="WaitForDeliveryCompleted" outlined>
      <v-card-text v-if="hasSubmitPermissionByStatus">
        <PickupDeliveryCompletedTable
          :can-edit="hasSubmitPermissionByStatus"
          :booking-forms="BookingForms"
          :is-delivery="true"
          @updateBookingFormDateOfDelivery="updateDateOfDelivery"
          @updateBookingFormTransportMode="updateTransportMode"
        >
        </PickupDeliveryCompletedTable>

        <v-row v-if="model.IsPast == true">
          <v-col cols="12" sm="6" md="6"> </v-col>
          <v-col cols="12" sm="6" md="6">
            <date-picker
              v-if="model.IsPast"
              v-model="model.AssignedOperationDate"
              label="Assigned Operation Date"
              :readonly="!hasSubmitPermissionByStatus"
              property-name="AssignedOperationDate"
              :properties-errors="propertiesErrors"
              @input="update"
            >
            </date-picker>
          </v-col>
        </v-row>

        <CardActionsGenericButton
          color="primary"
          style="display: inline-block; float: right"
          text="Save As Draft"
          @click="saveAsDraft"
        >
        </CardActionsGenericButton>
        <CardActionsGenericButton
          v-if="IsBookingFormDeliveryCompleted"
          style="display: inline-block; float: right"
          text="Submit"
          @click="submit"
        >
        </CardActionsGenericButton>
      </v-card-text>
      <v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <template v-if="hasSubmitPermissionByStatus">
            <p v-if="CanSubmitSMTA1ShipmentDocuments">
              <strong
                >it is appreciated if you could submit other shipment-related
                documents (e.g. Packiling List, Customs Declaration) using the
                'Upload' button below</strong
              >
            </p>
          </template>
          <template v-else>
            <p v-if="CanSubmitSMTA1ShipmentDocuments">
              Although no actions should be taken from your side at this stage,
              <strong
                >it is appreciated if you could submit other shipment-related
                documents (e.g. Packiling List, Customs Declaration) using the
                'Upload' button below</strong
              >, which are fianlized in communication between you and the
              courier company. You can find templates for some of the documents
              here.
            </p>
            <p v-else>No action should be taken from your side at this stage</p>
          </template>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card-text>
      <v-card-text>
        <ShipmentDocumentComponent v-model="model" @downloadFile="downloadFile">
        </ShipmentDocumentComponent>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Model } from "vue-property-decorator";
import TextArea from "@/components/TextArea.vue";

import { WorklistToBioHubItemModule } from "../../store";
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import {
  GetWorklistToBioHubStatusPermission,
  hasPermission,
} from "../../../../utils/helper";
import FormPopup from "../../../../components/FormPopup.vue";
import { FormPopupItem } from "@/models/FormPopupItem";
import DownloadDocumentComponent from "./../DownloadDocumentComponent.vue";
import CardActionsGenericButton from "../../../../components/CardActionsGenericButton.vue";
import Checkbox from "@/components/Checkbox.vue";
import PickupDeliveryCompletedTable from "../../../worklistItemsCommonComponents/PickupDeliveryComponents/PickupDeliveryCompletedTable.vue";
import ShipmentMaterialsSection from "./../WaitForArrivalConditionCheckComponents/ShipmentMaterialsSection.vue";
import FeedbackFlowComponent from "./../WaitForArrivalConditionCheckComponents/FeedbackFlowComponent.vue";
import { PermissionNames } from "@/models/constants/PermissionNames";
import ShipmentDocumentComponent from "../ShipmentDocumentsComponents/ShipmentDocumentsComponent.vue";
import { PermissionType } from "@/models/enums/PermissionType";
import DatePicker from "@/components/DatePicker.vue";
import { TransportModeModule } from "../../../transportModes/store";
import { BookingFormOfSMTA } from "@/models/BookingFormOfSMTA";

@Component({
  components: {
    DownloadDocumentComponent,
    FormPopup,
    CardActionsGenericButton,
    TextArea,
    Checkbox,
    PickupDeliveryCompletedTable,
    ShipmentMaterialsSection,
    FeedbackFlowComponent,
    ShipmentDocumentComponent,
    DatePicker,
  },
})
export default class PickupDeliveryPhase extends Vue {
  private newFilePopupItems: Array<FormPopupItem> = [];

  $refs!: {
    newDocumentFilePopup: FormPopup;
    newSignatureFilePopup: FormPopup;
  };

  @Model("update", { type: Object }) model!: WorklistToBioHubItem;
  // Props

  get BookingForms(): Array<BookingFormOfSMTA> {
    return WorklistToBioHubItemModule.BookingForms;
  }

  get CurrentStatus(): WorklistToBioHubStatus {
    return this.model.CurrentStatus;
  }

  get CanSubmitSMTA1ShipmentDocuments(): boolean {
    if (this.model.IsPast == true) {
      return hasPermission(PermissionNames.CanSubmitSMTA1ShipmentDocumentsPast);
    } else {
      return hasPermission(PermissionNames.CanSubmitSMTA1ShipmentDocuments);
    }
  }

  get WaitForPickUpCompleted(): boolean {
    return (
      this.model.CurrentStatus == WorklistToBioHubStatus.WaitForPickUpCompleted
    );
  }

  get WaitForDeliveryCompleted(): boolean {
    return (
      this.model.CurrentStatus ==
      WorklistToBioHubStatus.WaitForDeliveryCompleted
    );
  }

  get IsBookingFormPickupCompleted(): boolean {
    if (this.model.IsPast == true && this.model.AssignedOperationDate == null) {
      return false;
    }

    if (
      this.model.BookingForms === undefined ||
      this.model.BookingForms.length == 0
    ) {
      return false;
    }

    let bookingFormsPickupCompleted = true;
    this.model.BookingForms.forEach((elem) => {
      if (elem.Date === undefined || elem.Date === null) {
        bookingFormsPickupCompleted = false;
      }

      if (elem.DateOfPickup === undefined || elem.DateOfPickup === null) {
        bookingFormsPickupCompleted = false;
      }

      if (
        elem.ShipmentReferenceNumber === undefined ||
        elem.ShipmentReferenceNumber === null ||
        elem.ShipmentReferenceNumber === ""
      ) {
        bookingFormsPickupCompleted = false;
      }
    });

    return bookingFormsPickupCompleted;
  }

  get IsBookingFormDeliveryCompleted(): boolean {
    if (this.model.IsPast == true && this.model.AssignedOperationDate == null) {
      return false;
    }

    if (
      this.model.BookingForms === undefined ||
      this.model.BookingForms.length == 0
    ) {
      return false;
    }

    let bookingFormsDeliveryCompleted = true;
    this.model.BookingForms.forEach((elem) => {
      if (elem.Date === undefined || elem.Date === null) {
        bookingFormsDeliveryCompleted = false;
      }

      if (elem.DateOfDelivery === undefined || elem.DateOfDelivery === null) {
        bookingFormsDeliveryCompleted = false;
      }

      if (
        elem.TransportModeId === undefined ||
        elem.TransportModeId === null ||
        elem.TransportModeId === ""
      ) {
        bookingFormsDeliveryCompleted = false;
      }
    });

    return bookingFormsDeliveryCompleted;
  }

  get hasDownloadPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.DownloadFile,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }

    return hasPermission(requiredPermissionByStatus);
  }

  get hasReadPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Read,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  get hasSubmitPermissionByStatus(): boolean {
    if (this.model === undefined) {
      return false;
    }
    const requiredPermissionByStatus = GetWorklistToBioHubStatusPermission(
      this.model.CurrentStatus,
      PermissionType.Update,
      this.model.IsPast
    );
    if (requiredPermissionByStatus === undefined) {
      return false;
    }
    return hasPermission(requiredPermissionByStatus);
  }

  getBookingForm(id: string): BookingFormOfSMTA {
    const bookingForm: BookingFormOfSMTA | undefined = this.BookingForms.find(
      (x) => x.Id == id
    );
    if (bookingForm === undefined)
      throw {
        message: `Unexpected undefined for Booking Form with id ${id}`,
      };
    return bookingForm;
  }

  update() {
    this.$emit("update", this.model);
  }

  updateShipmentReferenceNumber(
    bookingFormId: string,
    shipmentReferenceNumber: string
  ): void {
    const bookingForm: BookingFormOfSMTA = this.getBookingForm(bookingFormId);
    bookingForm.ShipmentReferenceNumber = shipmentReferenceNumber;
    WorklistToBioHubItemModule.UPDATE_SHIPMENTREFERENCENUMBER_FIELD_BOOKING_FORM(
      bookingForm
    );
  }

  updateDateOfPickup(bookingFormId: string, dateOfPickup: Date): void {
    const bookingForm: BookingFormOfSMTA = this.getBookingForm(bookingFormId);
    bookingForm.DateOfPickup = dateOfPickup;
    WorklistToBioHubItemModule.UPDATE_DATEOFPICKUP_FIELD_BOOKING_FORM(
      bookingForm
    );
  }

  updateDateOfDelivery(bookingFormId: string, dateOfDelivery: Date): void {
    const bookingForm: BookingFormOfSMTA = this.getBookingForm(bookingFormId);
    bookingForm.DateOfDelivery = dateOfDelivery;
    WorklistToBioHubItemModule.UPDATE_DATEOFDELIVERY_FIELD_BOOKING_FORM(
      bookingForm
    );
  }

  updateTransportMode(bookingFormId: string, transportModeId: string): void {
    const bookingForm: BookingFormOfSMTA = this.getBookingForm(bookingFormId);
    bookingForm.TransportModeId = transportModeId;
    WorklistToBioHubItemModule.UPDATE_TRANSPORTMODE_FIELD_BOOKING_FORM(
      bookingForm
    );
  }

  updateLastSubmissionApproved(approved: boolean) {
    this.model.LastSubmissionApproved = approved;
    this.$emit("update", this.model);
  }

  setSaveAsDraft(saveAsDraft: boolean) {
    this.model.IsSaveDraft = saveAsDraft;
    this.$emit("update", this.model);
  }

  downloadFile() {
    this.$emit("downloadFile");
  }

  submit() {
    this.updateLastSubmissionApproved(true);
    this.setSaveAsDraft(false);
    this.$emit("submit");
  }

  submitForNewFeedback() {
    this.updateLastSubmissionApproved(false);

    this.setSaveAsDraft(false);
    this.$emit("submit");
  }

  saveAsDraft() {
    this.setSaveAsDraft(true);
    this.$emit("saveAsDraft");
  }

  async mounted() {
    if (this.WaitForDeliveryCompleted) {
      await TransportModeModule.ListTransportModes();
    }
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}

.signature-hover {
  cursor: pointer;
}
</style>
