<template>
  <div>
    <v-card-title>
      {{ title }}
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-text>
      <div>
        <v-row class="mb-5">
          <v-col cols="12" md="12" lg="12">
            <date-picker
              :key="keyDate"
              v-model="model.Date"
              label="Date"
              :readonly="!canEdit"
              property-name="Date"
              :properties-errors="allPropertiesErrors"
              @input="updateDate"
            >
            </date-picker>
          </v-col>
        </v-row>

        <v-card class="mb-5">
          <v-card-title>
            <v-spacer></v-spacer>
            <v-card-text>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <h4>Requesting Laboratory User</h4>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="requestUserFirstName"
                    label="Name"
                    readonly
                  >
                  </text-field>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="requestUserLastName"
                    label="Surname"
                    readonly
                  >
                  </text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="6" lg="6">
                  <text-field v-model="requestUserEmail" label="Email" readonly>
                  </text-field>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="requestUserJobTitle"
                    label="Job Title"
                    readonly
                  >
                  </text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="requestUserMobilePhone"
                    label="Mobile"
                    readonly
                  >
                  </text-field>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="requestUserBusinessPhone"
                    label="Business (landline)"
                    readonly
                  >
                  </text-field>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card-title>
        </v-card>

        <v-card class="mb-5">
          <v-card-title>
            <v-spacer></v-spacer>
            <v-card-text>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <date-picker
                    :key="keyRequestDateOfPickup"
                    v-model="model.RequestDateOfPickup"
                    label="Request Date of Pickup"
                    :readonly="!canEdit"
                    property-name="RequestDateOfPickup"
                    :properties-errors="allPropertiesErrors"
                    @input="updateRequestDateOfPickup"
                  >
                  </date-picker>
                </v-col>
              </v-row>

              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <p>
                    Please note that for transportation issues any request
                    placed to the BioHub after 14:00 CET will be dealt the next
                    day.
                  </p>
                </v-col>
              </v-row>

              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <BookingFormPickupUserForm
                    v-model="model"
                    :can-edit="canEdit"
                    :all-possible-pickup-users="allPossiblePickupUsers"
                    @BookingFormPickupUserForm="bookingFormPickupUserForm"
                    @addBookingFormPickupUser="addBookingFormPickupUser"
                    @removeBookingFormPickupUser="removeBookingFormPickupUser"
                    @clearBookingFormPickupUser="clearBookingFormPickupUser"
                  >
                  </BookingFormPickupUserForm>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card-title>
        </v-card>

        <v-row>
          <v-col cols="12" md="6" lg="6">
            <h2>Place of Pickup</h2>
          </v-col>
          <v-col cols="12" md="6" lg="6">
            <h2>Place of Delivery</h2>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="6" lg="6">
            <h3>{{ pickupInstituteTitle }}</h3>
          </v-col>
          <v-col cols="12" md="6" lg="6">
            <h3>{{ deliveryInstituteTitle }}</h3>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" md="6" lg="6">
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="wrap-text"
                  >Name: {{ pickupInstituteName }}</v-list-item-title
                >
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="wrap-text"
                  >Address: {{ pickupInstituteAddress }}</v-list-item-title
                >
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="wrap-text"
                  >Country: {{ pickupInstituteCountry }}</v-list-item-title
                >
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="wrap-text"
                  >Contact person: see the section above</v-list-item-title
                >
              </v-list-item-content>
            </v-list-item>
          </v-col>
          <v-col cols="12" md="6" lg="6">
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="wrap-text"
                  >Name: {{ deliveryInstituteName }}</v-list-item-title
                >
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="wrap-text"
                  >Address: {{ deliveryInstituteAddress }}</v-list-item-title
                >
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="wrap-text"
                  >Country: {{ deliveryInstituteCountry }}</v-list-item-title
                >
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="wrap-text"
                  >Contact person:</v-list-item-title
                >
              </v-list-item-content>
            </v-list-item>

            <v-list-item v-for="item in DeliveryUsers" :key="item">
              <v-list-item-icon
                ><v-icon v-text="mdiAccount"></v-icon
              ></v-list-item-icon>
              <v-list-item-content>
                <v-list-item-title>{{ item }}</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-col>
        </v-row>
        <v-row v-if="isFromBioHub">
          <v-col cols="12" md="12" lg="12">
            <h2>Details of Shipment</h2>
            <MaterialsTable
              :can-edit="canEditMaterialsTable"
              :hide-amount="false"
              :materials="materials"
              :check-quantity="canEditMaterialsTable"
              :transport-category-id="model.TransportCategoryId"
              @updateMaterial="updateMaterial"
            ></MaterialsTable>
          </v-col>
        </v-row>
        <v-row v-else>
          <v-col cols="12" md="12" lg="12">
            <h2>Details of Shipment</h2>
            <ShippingInformationsTable
              :can-edit="false"
              :can-read="true"
              :transport-category-id="model.TransportCategoryId"
              :material-shipping-informations="materialShippingInformations"
            >
            </ShippingInformationsTable>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="12" lg="12">
            <text-field
              v-model="TransportCategoryName"
              readonly
              label="Transport Category"
            ></text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" md="12" lg="12">
            <h4>Temperature Transport Condition</h4>

            <h4
              v-if="model.TemperatureTransportCondition == null"
              style="color: red"
            >
              Please specify a Value
            </h4>

            <v-container class="px-0" fluid>
              <v-radio-group v-model="model.TemperatureTransportCondition">
                <v-radio
                  :key="0"
                  label="Dry Shipper (-210° C to -195°C)"
                  :value="0"
                ></v-radio>
                <v-radio :key="1" label="Dry Ice (-70°C)" :value="1"></v-radio>
                <v-radio :key="2" label="Frozen (-20°C)" :value="2"></v-radio>
                <v-radio
                  :key="3"
                  label="ICEPACKS (0°C to +4°C)"
                  :value="3"
                ></v-radio>
                <v-radio
                  :key="4"
                  label="RT-ROOM TEMPERATURE (18°C to 25°C)"
                  :value="4"
                ></v-radio>
              </v-radio-group>
            </v-container>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="6" lg="6">
            <text-field
              v-model="model.TotalNumberOfVials"
              label="Total Number Of Vials"
              readonly
              property-name="TotalNumberOfVials"
              :properties-errors="allPropertiesErrors"
            >
            </text-field>
          </v-col>
          <v-col cols="12" md="6" lg="6">
            <text-field
              v-model="model.TotalAmount"
              label="Total Amount (in ml)"
              readonly
              property-name="TotalAmount"
              :properties-errors="allPropertiesErrors"
            >
            </text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" md="12" lg="12">
            <text-field
              v-model="model.NumberOfInnerPackagingAndSize"
              :readonly="!canEdit"
              label="Number of inner packaging and size (if available):"
              property-name="NumberOfInnerPackagingAndSize"
              :properties-errors="allPropertiesErrors"
              @input="update"
            ></text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="12" lg="12">
            <p>
              Please report the HS Code 300 212 002 on your packing list and on
              your no commercial invoice
            </p>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="12" lg="12">
            <p>
              NOTE: Courier company or his agent will provide dry ice, adequate
              packaging materials and paperwork (house air waybill, dg forms)
              for your shipment.
            </p>
          </v-col>
        </v-row>
        <div v-if="courierVisible" class="mb-5" outlined>
          <v-card-title>
            <v-spacer></v-spacer>
            <v-card-text>
              <v-row>
                <v-col>
                  <BookingFormCourierUserForm
                    v-model="model"
                    :can-edit="canEdit"
                    :booking-form-id="bookingFormId"
                    :all-possible-courier-users="allPossibleCourierUsers"
                    @BookingFormCourierUserForm="bookingFormCourierUserForm"
                    @addBookingFormCourierUser="addBookingFormCourierUser"
                    @removeBookingFormCourierUser="removeBookingFormCourierUser"
                    @clearBookingFormCourierUser="clearBookingFormCourierUser"
                    :couriers="couriers"
                  >
                  </BookingFormCourierUserForm>
                </v-col>
              </v-row>
              <v-row v-if="courierVisible">
                <v-col cols="12" md="12" lg="12">
                  <date-picker
                    :key="keyEstimateDateOfPickup"
                    v-model="model.EstimateDateOfPickup"
                    label="Estimate Date of Pickup"
                    :readonly="!canEdit"
                    property-name="EstimateDateOfPickup"
                    :properties-errors="allPropertiesErrors"
                    @input="updateEstimateDateOfPickup"
                  >
                  </date-picker>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card-title>
        </div>
      </div>
    </v-card-text>
    <v-card-actions>
      <slot></slot>
    </v-card-actions>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { BookingFormOfSMTA } from "@/models/BookingFormOfSMTA";
import CardActionsGenericButton from "@/components/CardActionsGenericButton.vue";
import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";
import BookingFormPickupUserForm from "./BookingFormPickupUserForm.vue";
import BookingFormCourierUserForm from "./BookingFormCourierUserForm.vue";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import DatePicker from "@/components/DatePicker.vue";
import Dropdown from "@/components/Dropdown.vue";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import ShippingInformationsTable from "../../worklistToBioHubItems/components/Annex2ofSMTA1Components/ShippingInformationsTable.vue";
import MaterialsTable from "../../worklistFromBioHubItems/components/Annex2ofSMTA2Components/MaterialsTable.vue";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { Courier } from "@/models/Courier";

@Component({
  components: {
    CardActionsGenericButton,
    BookingFormPickupUserForm,
    BookingFormCourierUserForm,
    TextField,
    DatePicker,
    TextFieldFloat,
    ShippingInformationsTable,
    MaterialsTable,
  },
})
export default class BookingFormTabElementFormComponent extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private propertiesErrors = new Map<string, Array<string>>();
  private keyDate = 1;
  private keyRequestDateOfPickup = 1;
  private keyEstimateDateOfPickup = 1;

  private mdiAccount = "mdi-account";

  // Props

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canRead: boolean;

  @Prop({ type: String, default: "" })
  readonly title: string;

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

  // Model
  @Model("update", { type: Object }) model!: BookingFormOfSMTA;

  get DeliveryUsers(): Array<string> {
    if (this.deliveryUsers == undefined) {
      return new Array<string>();
    }
    let result = new Array<string>();
    this.deliveryUsers.forEach((elem) => {
      result.push(elem.UserName);
    });
    return result;
  }

  get TransportCategoryName(): string {
    return this.model.TransportCategoryName;
  }

  // get courierVisible(): boolean {
  //   return (
  //     WorklistToBioHubItemModule.Status ==
  //     WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval
  //   );
  // }

  get allPropertiesErrors(): Map<string, Array<string>> {
    let result = this.propertiesErrors ?? new Map<string, Array<string>>();

    this.PropertiesErrors.forEach((value, key, map) => {
      result.set(key, value);
    });

    return result;
  }

  get PropertiesErrors(): Map<string, Array<string>> {
    let result = this.setPropertiesErrors(undefined);
    return result;
  }

  get bookingFormId(): string {
    return this.model.Id;
  }

  update() {
    //WorklistToBioHubItemModule.UPDATE_BOOKING_FORM(this.model);
    this.$emit("updateBookingForm", this.model);
    this.setPropertiesErrors(this.propertiesErrors);
  }

  updateDate() {
    //WorklistToBioHubItemModule.UPDATE_BOOKING_FORM(this.model);
    this.$emit("updateBookingForm", this.model);
    this.setPropertiesErrors(this.propertiesErrors);
    this.keyDate = this.keyDate + 1;
  }

  updateRequestDateOfPickup() {
    //WorklistToBioHubItemModule.UPDATE_BOOKING_FORM(this.model);
    this.$emit("updateBookingForm", this.model);
    this.setPropertiesErrors(this.propertiesErrors);
    this.keyRequestDateOfPickup = this.keyRequestDateOfPickup + 1;
  }

  updateEstimateDateOfPickup() {
    //WorklistToBioHubItemModule.UPDATE_BOOKING_FORM(this.model);
    this.$emit("updateBookingForm", this.model);
    this.setPropertiesErrors(this.propertiesErrors);
    this.keyEstimateDateOfPickup = this.keyEstimateDateOfPickup + 1;
  }

  bookingFormCourierUserForm() {
    //WorklistToBioHubItemModule.UPDATE_BOOKING_FORM(this.model);
    this.$emit("updateBookingForm", this.model);
    this.setPropertiesErrors(this.propertiesErrors);
  }

  addBookingFormCourierUser(user: WorklistItemUser) {
    this.$emit("addBookingFormCourierUser", user, this.model.Id);
  }

  removeBookingFormCourierUser(id: string) {
    this.$emit("removeBookingFormCourierUser", id, this.model.Id);
  }

  clearBookingFormCourierUser() {
    this.$emit("clearBookingFormCourierUser", this.model.Id);
  }

  addBookingFormPickupUser(user: WorklistItemUser) {
    this.$emit("addBookingFormPickupUser", user, this.model.Id);
  }

  removeBookingFormPickupUser(id: string) {
    this.$emit("removeBookingFormPickupUser", id, this.model.Id);
  }

  updateMaterial(material: WorklistFromBioHubItemMaterial) {
    this.$emit("updateMaterial", material, this.model.Id);
  }

  setPropertiesErrors(
    errorList: Map<string, Array<string>> | undefined
  ): Map<string, Array<string>> {
    if (errorList === undefined) {
      errorList = new Map<string, Array<string>>();
    }
    if (this.model.Date === undefined || this.model.Date === null) {
      errorList.set("Date", ["'Date' is Required"]);
    } else {
      errorList.delete("Date");
    }
    if (
      this.model.RequestDateOfPickup === undefined ||
      this.model.RequestDateOfPickup === null
    ) {
      errorList.set("RequestDateOfPickup", [
        "'Request Date of Pickup' is Required",
      ]);
    } else {
      errorList.delete("RequestDateOfPickup");
    }

    if (
      this.model.EstimateDateOfPickup === undefined ||
      this.model.EstimateDateOfPickup === null
    ) {
      errorList.set("EstimateDateOfPickup", [
        "'Estimate Date of Pickup' is Required",
      ]);
    } else {
      errorList.delete("EstimateDateOfPickup");
    }

    return errorList;
  }
}
</script>

<style lang="scss">
div.v-menu__content.theme--light.v-menu__content--auto.menuable__content__active {
  max-height: none !important;
}

.wrap-text {
  white-space: normal;
}
</style>
