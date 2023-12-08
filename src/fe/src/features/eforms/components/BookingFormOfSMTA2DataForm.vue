<template>
  <v-form ref="form" lazy-validation readonly class="mt-10">
    <text-field
      v-model="BookingFormOfSMTA2.ShipmentRequestNumber"
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
      :booking-forms="BookingFormOfSMTA2.BookingForms"
      :request-user-first-name="BookingFormOfSMTA2.RequestUserFirstName"
      :request-user-last-name="BookingFormOfSMTA2.RequestUserLastName"
      :request-user-email="BookingFormOfSMTA2.RequestUserEmail"
      :request-user-job-title="BookingFormOfSMTA2.RequestUserJobTitle"
      :request-user-mobile-phone="BookingFormOfSMTA2.RequestUserMobilePhone"
      :request-user-business-phone="BookingFormOfSMTA2.RequestUserBusinessPhone"
      :pickup-institute-address="BookingFormOfSMTA2.BioHubFacilityAddress"
      :pickup-institute-name="BookingFormOfSMTA2.BioHubFacilityName"
      :pickup-institute-country="BookingFormOfSMTA2.BioHubFacilityCountry"
      :delivery-institute-address="BookingFormOfSMTA2.LaboratoryAddress"
      :delivery-institute-name="BookingFormOfSMTA2.LaboratoryName"
      :delivery-institute-country="BookingFormOfSMTA2.LaboratoryCountry"
      pickup-institute-title="BioHub Facility"
      delivery-institute-title="The Laboratory"
      :courier-visible="canReadCourier"
      :delivery-users="BookingFormOfSMTA2.LaboratoryFocalPoints"
      :materials="BookingFormOfSMTA2.WorklistFromBioHubItemMaterials"
      :is-from-bio-hub="true"
      :couriers="BookingFormOfSMTA2.Couriers"
    >
    </BookingFormTabsComponent>
    <v-spacer></v-spacer>

    <text-field
      v-if="BookingFormOfSMTA2.BookingFormOfSMTA2SignatureText != ''"
      v-model="BookingFormOfSMTA2.BookingFormOfSMTA2SignatureText"
      label="Signature"
      readonly
      property-name="BookingFormOfSMTA2SignatureText"
      @input="update"
    >
    </text-field>
    <date-picker
      v-model="BookingFormOfSMTA2.ApprovalDate"
      label="Approval Date"
      readonly
      property-name="ApprovalDate"
    >
    </date-picker>
  </v-form>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import TextArea from "@/components/TextArea.vue";
import Dropdown from "@/components/Dropdown.vue";
import DatePicker from "@/components/DatePicker.vue";
import BookingFormTabsComponent from "../../worklistItemsCommonComponents/BookingFormComponents/BookingFormTabsComponent.vue";
import Checkbox from "@/components/Checkbox.vue";
import { BookingFormOfSMTA2Data } from "@/models/BookingFormOfSMTA2Data";
import { EFormModule } from "../store";

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
export default class BookingFormOfSMTA2DataForm extends Vue {
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

  get BookingFormOfSMTA2(): BookingFormOfSMTA2Data | undefined {
    return EFormModule.BookingFormOfSMTA2;
  }

  get BioHubFacilityInfo(): string {
    if (this.BookingFormOfSMTA2 != undefined) {
      return (
        this.BookingFormOfSMTA2.BioHubFacilityName +
        " " +
        this.BookingFormOfSMTA2.BioHubFacilityAddress +
        " " +
        this.BookingFormOfSMTA2.BioHubFacilityCountry
      );
    } else return "";
  }

  get LaboratoryInfo(): string {
    if (this.BookingFormOfSMTA2 != undefined) {
      return (
        this.BookingFormOfSMTA2.LaboratoryName +
        " " +
        this.BookingFormOfSMTA2.LaboratoryAddress +
        " " +
        this.BookingFormOfSMTA2.LaboratoryCountry
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
