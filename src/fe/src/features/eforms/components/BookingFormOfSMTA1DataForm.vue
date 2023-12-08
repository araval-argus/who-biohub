<template>
  <v-form ref="form" lazy-validation readonly class="mt-10">
    <text-field
      v-model="BookingFormOfSMTA1.ShipmentRequestNumber"
      label="Shipment Request Number"
      readonly
      property-name="ShipmentRequestNumber"
    >
    </text-field>

    <text-field
      v-model="BioHubFacilityInfo"
      label="BioHub Facility"
      readonly
      property-name="BioHubFacilityInfo"
    >
    </text-field>

    <text-field
      v-model="LaboratoryInfo"
      label="Provider"
      readonly
      property-name="LaboratoryInfo"
    >
    </text-field>

    <BookingFormTabsComponent
      :can-edit="false"
      :can-read="canRead"
      :booking-forms="BookingFormOfSMTA1.BookingForms"
      :request-user-first-name="BookingFormOfSMTA1.RequestUserFirstName"
      :request-user-last-name="BookingFormOfSMTA1.RequestUserLastName"
      :request-user-email="BookingFormOfSMTA1.RequestUserEmail"
      :request-user-job-title="BookingFormOfSMTA1.RequestUserJobTitle"
      :request-user-mobile-phone="BookingFormOfSMTA1.RequestUserMobilePhone"
      :request-user-business-phone="BookingFormOfSMTA1.RequestUserBusinessPhone"
      :pickup-institute-address="BookingFormOfSMTA1.LaboratoryAddress"
      :pickup-institute-name="BookingFormOfSMTA1.LaboratoryName"
      :pickup-institute-country="BookingFormOfSMTA1.LaboratoryCountry"
      :delivery-institute-address="BookingFormOfSMTA1.BioHubFacilityAddress"
      :delivery-institute-name="BookingFormOfSMTA1.BioHubFacilityName"
      :delivery-institute-country="BookingFormOfSMTA1.BioHubFacilityCountry"
      pickup-institute-title="The Laboratory"
      delivery-institute-title="BioHub Facility"
      :courier-visible="canReadCourier"
      :delivery-users="BookingFormOfSMTA1.BioHubFacilityFocalPoints"
      :material-shipping-informations="
        BookingFormOfSMTA1.MaterialShippingInformations
      "
      :couriers="BookingFormOfSMTA1.Couriers"
    >
    </BookingFormTabsComponent>
    <v-spacer></v-spacer>

    <text-field
      v-if="BookingFormOfSMTA1.BookingFormOfSMTA1SignatureText != ''"
      v-model="BookingFormOfSMTA1.BookingFormOfSMTA1SignatureText"
      label="Signature"
      readonly
      property-name="BookingFormOfSMTA1SignatureText"
      @input="update"
    >
    </text-field>
    <date-picker
      v-model="BookingFormOfSMTA1.ApprovalDate"
      label="Approval Date"
      readonly
      property-name="ApprovalDate"
    >
    </date-picker>
  </v-form>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import TextArea from "@/components/TextArea.vue";
import Dropdown from "@/components/Dropdown.vue";
import DatePicker from "@/components/DatePicker.vue";
import BookingFormTabsComponent from "../../worklistItemsCommonComponents/BookingFormComponents/BookingFormTabsComponent.vue";

import Checkbox from "@/components/Checkbox.vue";
import { BookingFormOfSMTA1Data } from "@/models/BookingFormOfSMTA1Data";
import { EFormModule } from "../store";
import { WorklistItemUser } from "@/models/WorklistItemUser";

@Component({
  components: {
    TextFieldFloat,
    TextField,
    Dropdown,
    BookingFormTabsComponent,
    Checkbox,
    TextArea,
    DatePicker,
  },
})
export default class BookingFormOfSMTA1DataForm extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  @Prop({ type: Boolean, default: false })
  readonly canRead: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canReadCourier: boolean;

  $refs!: {
    form: any;
  };

  get BookingFormOfSMTA1(): BookingFormOfSMTA1Data | undefined {
    return EFormModule.BookingFormOfSMTA1;
  }

  get BioHubFacilityFocalPoints(): Array<WorklistItemUser> {
    return this.BookingFormOfSMTA1?.BioHubFacilityFocalPoints ?? [];
  }

  get MaterialShippingInformations(): Array<MaterialShippingInformation> {
    return this.BookingFormOfSMTA1?.MaterialShippingInformations ?? [];
  }

  get BioHubFacilityInfo(): string {
    if (this.BookingFormOfSMTA1 != undefined) {
      return (
        this.BookingFormOfSMTA1.BioHubFacilityName +
        " " +
        this.BookingFormOfSMTA1.BioHubFacilityAddress +
        " " +
        this.BookingFormOfSMTA1.BioHubFacilityCountry
      );
    } else return "";
  }

  get LaboratoryInfo(): string {
    if (this.BookingFormOfSMTA1 != undefined) {
      return (
        this.BookingFormOfSMTA1.LaboratoryName +
        " " +
        this.BookingFormOfSMTA1.LaboratoryAddress +
        " " +
        this.BookingFormOfSMTA1.LaboratoryCountry
      );
    } else return "";
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
