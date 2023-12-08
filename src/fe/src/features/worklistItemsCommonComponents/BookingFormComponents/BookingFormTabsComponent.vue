<template>
  <div>
    <v-toolbar white flat>
      <v-spacer></v-spacer>

      <template #extension>
        <v-tabs v-model="tab" align-with-title>
          <v-tabs-slider></v-tabs-slider>

          <v-tab v-for="i in BookingFormsNumber" :key="i">
            {{ tabName(bookingForms[i - 1]) }}
          </v-tab>
        </v-tabs>
      </template>
    </v-toolbar>
    <v-tabs-items v-model="tab">
      <v-tab-item v-for="i in BookingFormsNumber" :key="i">
        <BookingFormTabElementFormComponent
          v-model="bookingForms[i - 1]"
          :can-edit="canEdit"
          :request-user-first-name="requestUserFirstName"
          :request-user-last-name="requestUserLastName"
          :request-user-email="requestUserEmail"
          :request-user-job-title="requestUserJobTitle"
          :request-user-mobile-phone="requestUserMobilePhone"
          :request-user-business-phone="requestUserBusinessPhone"
          :pickup-institute-address="pickupInstituteAddress"
          :pickup-institute-name="pickupInstituteName"
          :pickup-institute-country="pickupInstituteCountry"
          :delivery-institute-address="deliveryInstituteAddress"
          :delivery-institute-name="deliveryInstituteName"
          :delivery-institute-country="deliveryInstituteCountry"
          :pickup-institute-title="pickupInstituteTitle"
          :delivery-institute-title="deliveryInstituteTitle"
          :courier-visible="courierVisible"
          :bio-hub-facility-focal-points="bioHubFacilityFocalPoints"
          :is-from-bio-hub="isFromBioHub"
          :can-edit-materials-table="canEditMaterialsTable"
          :materials="materials"
          :all-possible-pickup-users="allPossiblePickupUsers"
          :all-possible-courier-users="allPossibleCourierUsers"
          :delivery-users="deliveryUsers"
          :material-shipping-informations="materialShippingInformations"
          :couriers="couriers"
          @updateBookingForm="updateBookingForm"
          @addBookingFormCourierUser="addBookingFormCourierUser"
          @removeBookingFormCourierUser="removeBookingFormCourierUser"
          @clearBookingFormCourierUser="clearBookingFormCourierUser"
          @addBookingFormPickupUser="addBookingFormPickupUser"
          @removeBookingFormPickupUser="removeBookingFormPickupUser"
          @updateMaterial="updateMaterial"
        >
        </BookingFormTabElementFormComponent>
      </v-tab-item>
    </v-tabs-items>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { BookingFormOfSMTA } from "@/models/BookingFormOfSMTA";
import BookingFormTabElementFormComponent from "./BookingFormTabElementFormComponent.vue";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { Courier } from "@/models/Courier";

@Component({
  components: {
    BookingFormTabElementFormComponent,
  },
})
export default class BookingFormTabsComponent extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private tab = null;

  // Props

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canRead: boolean;

  @Prop({ type: Array, default: false })
  readonly bookingForms: Array<BookingFormOfSMTA>;

  @Prop({ type: String, default: "" })
  readonly requestUserFirstName: string;

  @Prop({ type: String, default: "" })
  readonly requestUserLastName: string;

  @Prop({ type: String, default: "" })
  readonly requestUserEmail: string;

  @Prop({ type: String, default: "" })
  readonly requestUserJobTitle: string;

  @Prop({ type: String, default: "" })
  readonly requestUserBusinessPhone: string;

  @Prop({ type: String, default: "" })
  readonly requestUserMobilePhone: string;

  @Prop({ type: String, default: "" })
  readonly pickupInstituteName: string;

  @Prop({ type: String, default: "" })
  readonly pickupInstituteAddress: string;

  @Prop({ type: String, default: "" })
  readonly pickupInstituteCountry: string;

  @Prop({ type: String, default: "" })
  readonly deliveryInstituteName: string;

  @Prop({ type: String, default: "" })
  readonly deliveryInstituteAddress: string;

  @Prop({ type: String, default: "" })
  readonly deliveryInstituteCountry: string;

  @Prop({ type: String, default: "" })
  readonly pickupInstituteTitle: string;

  @Prop({ type: String, default: "" })
  readonly deliveryInstituteTitle: string;

  @Prop({ type: Boolean, default: false })
  readonly courierVisible: boolean;

  @Prop({ type: Array, default: [] })
  readonly deliveryUsers: Array<WorklistItemUser>;

  @Prop({ type: Boolean, default: false })
  readonly isFromBioHub: boolean;

  @Prop({ type: Array, default: [] })
  readonly materials: Array<WorklistFromBioHubItemMaterial>;

  @Prop({ type: Boolean, default: false })
  readonly canEditMaterialsTable: boolean;

  @Prop({ type: Array, default: [] })
  readonly allPossiblePickupUsers: Array<WorklistItemUser>;

  @Prop({ type: Array, default: [] })
  readonly allPossibleCourierUsers: Array<WorklistItemUser>;

  @Prop({ type: Array, default: [] })
  readonly materialShippingInformations: Array<MaterialShippingInformation>;

  @Prop({ type: Array, default: [] })
  readonly couriers: Array<Courier>;

  $refs!: {
    form: any;
  };

  get BookingFormsNumber(): number {
    return this.bookingForms.length;
  }

  tabName(item: BookingFormOfSMTA): string {
    return item.TransportCategoryName;
  }

  updateBookingForm(item: BookingFormOfSMTA) {
    this.$emit("updateBookingForm", item);
  }

  addBookingFormCourierUser(user: WorklistItemUser, bookingFormId: string) {
    this.$emit("addBookingFormCourierUser", user, bookingFormId);
  }

  removeBookingFormCourierUser(id: string, bookingFormId: string) {
    this.$emit("removeBookingFormCourierUser", id, bookingFormId);
  }

  clearBookingFormCourierUser(bookingFormId: string) {
    this.$emit("clearBookingFormCourierUser", bookingFormId);
  }

  addBookingFormPickupUser(user: WorklistItemUser, bookingFormId: string) {
    this.$emit("addBookingFormPickupUser", user, bookingFormId);
  }

  removeBookingFormPickupUser(id: string, bookingFormId: string) {
    this.$emit("removeBookingFormPickupUser", id, bookingFormId);
  }

  updateMaterial(
    material: WorklistFromBioHubItemMaterial,
    bookingFormId: string
  ) {
    this.$emit("updateMaterial", material, bookingFormId);
  }

  validate() {
    this.$refs.form.validate();
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}
</style>
