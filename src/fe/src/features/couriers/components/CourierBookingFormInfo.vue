<template>
  <v-card v-if="model">
    <v-card-title>
      <BackButton @back="onBack" />
      <h2>{{ title }}</h2>
      <v-spacer></v-spacer>
    </v-card-title>
    <v-card-text>
      <div>
        <v-row class="mb-5">
          <v-col cols="12" md="12" lg="12">
            <date-picker
              v-model="model.Date"
              label="Date"
              readonly
              property-name="Date"
              :properties-errors="allPropertiesErrors"
              @input="updateDate"
            >
            </date-picker>
          </v-col>
        </v-row>

        <v-card class="mb-5" outlined>
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
                    v-model="model.RequestingUserFirstName"
                    label="Name"
                    readonly
                  >
                  </text-field>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="model.RequestingUserLastName"
                    label="Surname"
                    readonly
                  >
                  </text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="model.RequestingUserEmail"
                    label="Email"
                    readonly
                  >
                  </text-field>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="model.RequestingUserJobTitle"
                    label="Job Title"
                    readonly
                  >
                  </text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="model.RequestingUserMobilePhone"
                    label="Mobile"
                    readonly
                  >
                  </text-field>
                </v-col>
                <v-col cols="12" md="6" lg="6">
                  <text-field
                    v-model="model.RequestingUserBusinessPhone"
                    label="Business (landline)"
                    readonly
                  >
                  </text-field>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card-title>
        </v-card>

        <v-card class="mb-5" outlined>
          <v-card-title>
            <v-spacer></v-spacer>
            <v-card-text>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <date-picker
                    v-model="model.RequestDateOfPickup"
                    label="Request Date of Pickup"
                    readonly
                    property-name="RequestDateOfPickup"
                    :properties-errors="allPropertiesErrors"
                    @input="updateRequestDateOfPickup"
                  >
                  </date-picker>
                </v-col>
              </v-row>

              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <CourierBookingFormPickupUserTable>
                  </CourierBookingFormPickupUserTable>
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
            <h3>{{ pickupInstituteReference }}</h3>
          </v-col>
          <v-col cols="12" md="6" lg="6">
            <h3>{{ deliveryInstituteReference }}</h3>
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
        <v-row>
          <v-col cols="12" md="12" lg="12">
            <h2>Details of Shipment</h2>
            <CourierShippingInformationsTable v-if="ToBioHub">
            </CourierShippingInformationsTable>
            <CourierMaterialsTable v-else> </CourierMaterialsTable>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="12" lg="12">
            <text-field
              v-model="SubstanceCategoryName"
              readonly
              label="Substance Category"
            ></text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" md="12" lg="12">
            <h4>Temperature Transport Condition</h4>

            <v-container class="px-0" fluid>
              <v-radio-group
                v-model="model.TemperatureTransportCondition"
                readonly
              >
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
            >
            </text-field>
          </v-col>
          <v-col cols="12" md="6" lg="6">
            <text-field
              v-model="model.TotalAmount"
              label="Total Amount (in ml)"
              readonly
              property-name="TotalAmount"
            >
            </text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="12" lg="12">
            <text-field
              v-model="model.NumberOfInnerPackagingAndSize"
              readonly
              label="Number of inner packaging and size (if available):"
              property-name="NumberOfInnerPackagingAndSize"
            ></text-field>
          </v-col>
        </v-row>

        <div class="mb-5">
          <v-card-title>
            <v-spacer></v-spacer>
            <v-card-text>
              <h3>User Courier Details</h3>
              <v-row>
                <v-col>
                  <CourierBookingFormCourierUserTable>
                  </CourierBookingFormCourierUserTable>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <date-picker
                    :key="keyEstimateDateOfPickup"
                    v-model="model.EstimateDateOfPickup"
                    label="Estimate Date of Pickup"
                    readonly
                    property-name="EstimateDateOfPickup"
                  >
                  </date-picker>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <date-picker
                    v-model="model.DateOfPickup"
                    label="Date Of Pickup"
                    readonly
                    property-name="ShipmentReferenceNumber"
                  >
                  </date-picker>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <text-field
                    v-model="model.ShipmentReferenceNumber"
                    label="Shipment Reference Number"
                    readonly
                    property-name="ShipmentReferenceNumber"
                  >
                  </text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <date-picker
                    v-model="model.DateOfDelivery"
                    label="Date Of Delivery"
                    readonly
                    property-name="Delivery"
                  >
                  </date-picker>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="12" lg="12">
                  <text-field
                    v-model="model.TransportMode"
                    label="Transport Mode"
                    readonly
                    property-name="TransportMode"
                  >
                  </text-field>
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
  </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Model, Watch } from "vue-property-decorator";
import { CourierModule } from "../store";

import { CourierBookingForm } from "@/models/CourierBookingForm";

import { UserModule } from "../../users/store";
import TextFieldFloat from "@/components/TextFieldFloat.vue";
import TextField from "@/components/TextField.vue";
import DatePicker from "@/components/DatePicker.vue";
import Dropdown from "@/components/Dropdown.vue";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import CourierBookingFormCourierUserTable from "./CourierBookingFormCourierUserTable.vue";
import CourierBookingFormPickupUserTable from "./CourierBookingFormPickupUserTable.vue";
import BackButton from "@/components/BackButton.vue";
import CourierShippingInformationsTable from "./CourierShippingInformationsTable.vue";
import CourierMaterialsTable from "./CourierMaterialsTable.vue";

@Component({
  components: {
    TextField,
    DatePicker,
    TextFieldFloat,
    CourierBookingFormCourierUserTable,
    CourierBookingFormPickupUserTable,
    BackButton,
    CourierShippingInformationsTable,
    CourierMaterialsTable,
  },
})
export default class CourierBookingFormInfo extends Vue {
  private stringRules = [
    (s: string) => (s != null && s != undefined && s != "") || "required",
  ];

  private numberRules = [
    (n: any) =>
      (n != null && n != undefined && n.toString() != "") || "required",
  ];

  private propertiesErrors = new Map<string, Array<string>>();

  private mdiAccount = "mdi-account";

  // Props

  @Prop({ type: Boolean, default: false })
  readonly canEdit: boolean;

  @Prop({ type: Boolean, default: false })
  readonly canRead: boolean;

  @Prop({ type: String, default: "" })
  readonly title: string;

  $refs!: {
    form: any;
  };

  // Model

  get FromBioHub(): boolean {
    if (this.model != undefined) {
      return (
        this.model.WorklistFromBioHubItemId != null &&
        this.model.WorklistFromBioHubItemId != undefined &&
        this.model.WorklistFromBioHubItemId != ""
      );
    } else {
      return false;
    }
  }

  get ToBioHub(): boolean {
    if (this.model != undefined) {
      return (
        this.model.WorklistToBioHubItemId != null &&
        this.model.WorklistToBioHubItemId != undefined &&
        this.model.WorklistToBioHubItemId != ""
      );
    } else {
      return false;
    }
  }

  get pickupInstituteReference(): string {
    if (this.model != undefined) {
      if (this.model.ShipmentDirection == "From BioHub") {
        return "BioHub Facility";
      } else {
        return "The Laboratory";
      }
    } else {
      return "";
    }
  }

  get model() {
    return CourierModule.CourierBookingForm;
  }

  get deliveryInstituteReference(): string {
    if (this.model != undefined) {
      if (this.model.ShipmentDirection == "From BioHub") {
        return "The Laboratory";
      } else {
        return "BioHub Facility";
      }
    } else {
      return "";
    }
  }

  get pickupInstituteName(): string {
    if (this.model != undefined) {
      if (this.model.ShipmentDirection == "From BioHub") {
        return this.model.BioHubFacilityName;
      } else {
        return this.model.LaboratoryName;
      }
    } else {
      return "";
    }
  }

  get deliveryInstituteName(): string {
    if (this.model != undefined) {
      if (this.model.ShipmentDirection == "From BioHub") {
        return this.model.LaboratoryName;
      } else {
        return this.model.BioHubFacilityName;
      }
    } else {
      return "";
    }
  }

  get pickupInstituteAddress(): string {
    if (this.model != undefined) {
      if (this.model.ShipmentDirection == "From BioHub") {
        return this.model.BioHubFacilityAddress;
      } else {
        return this.model.LaboratoryAddress;
      }
    } else {
      return "";
    }
  }

  get deliveryInstituteAddress(): string {
    if (this.model != undefined) {
      if (this.model.ShipmentDirection == "From BioHub") {
        return this.model.LaboratoryAddress;
      } else {
        return this.model.BioHubFacilityAddress;
      }
    } else {
      return "";
    }
  }

  get pickupInstituteCountry(): string {
    if (this.model != undefined) {
      if (this.model.ShipmentDirection == "From BioHub") {
        return this.model.BioHubFacilityCountry;
      } else {
        return this.model.LaboratoryCountry;
      }
    } else {
      return "";
    }
  }

  get deliveryInstituteCountry(): string {
    if (this.model != undefined) {
      if (this.model.ShipmentDirection == "From BioHub") {
        return this.model.LaboratoryCountry;
      } else {
        return this.model.BioHubFacilityCountry;
      }
    } else {
      return "";
    }
  }

  get DeliveryUsers(): Array<string> {
    if (this.model != undefined) {
      if (this.model.ShipmentDirection == "From BioHub") {
        return this.bioHubFacilityUsers;
      } else {
        return this.LaboratoryUsers;
      }
    } else {
      return [];
    }
  }

  get bioHubFacilityUsers(): Array<string> {
    if (this.model == undefined) {
      return new Array<string>();
    }
    const bioHubFacilityUsers = this.model.BookingFormBioHubFacilityFocalPoints;
    if (bioHubFacilityUsers == undefined) {
      return new Array<string>();
    }
    let result = new Array<string>();
    bioHubFacilityUsers.forEach((elem) => {
      result.push(elem.UserName);
    });
    return result;
  }

  get LaboratoryUsers(): Array<string> {
    if (this.model == undefined) {
      return new Array<string>();
    }
    const laboratoryUsers = this.model.BookingFormLaboratoryFocalPoints;
    if (laboratoryUsers == undefined) {
      return new Array<string>();
    }
    let result = new Array<string>();
    laboratoryUsers.forEach((elem) => {
      result.push(elem.UserName);
    });
    return result;
  }

  get SubstanceCategoryName(): string {
    if (this.model != undefined) {
      return this.model.TransportCategoryName;
    } else {
      return "";
    }
  }

  get bookingFormId(): string {
    if (this.model != undefined) {
      return this.model.Id;
    } else {
      return "";
    }
  }
  onBack() {
    CourierModule.SET_ERROR(undefined);
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
