<template>
  <div v-if="canCreate">
    <h2>Create Past Shipment Request</h2>
    <v-container class="px-0" fluid>
      <v-radio-group v-model="shipmentRequestSelection">
        <v-radio key="1" label="Send BMEPP into the BioHub" value="1"></v-radio>
        <v-radio
          key="2"
          label="Receive BMEPP from the BioHub"
          value="2"
        ></v-radio>
      </v-radio-group>
      <date-picker
        v-model="assignedOperationDate"
        label="Assigned Operation Date"
        :readonly="false"
        property-name="AssignedOperationDate"
        :properties-errors="propertiesErrors"
        @input="update"
      >
      </date-picker>

      <CardActionsSaveCancel
        v-if="assignedOperationDate != null"
        save-name="Continue"
        @save="onSave"
        @cancel="onCancel"
      >
      </CardActionsSaveCancel>
    </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";

import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { getAreaFromRoleType } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { WorklistToBioHubItemModule } from "../worklistToBioHubItems/store";
import { WorklistFromBioHubItemModule } from "../worklistFromBioHubItems/store";
import FormPopup from "../../components/FormPopup.vue";
import { DropdownItem } from "@/models/DropdownItem";
import DatePicker from "../../components/DatePicker.vue";

@Component({
  components: { CardActionsSaveCancel, FormPopup, DatePicker },
})
export default class ShipmentPastRequestsPageForm extends Vue {
  private shipmentRequestSelection = "1";
  private openSaveOrDelete = false;
  private assignedOperationDate = null;

  $refs!: {
    createShipmentPopup: FormPopup;
  };

  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanAccessPastRequestIniziation);
  }

  updated() {
    AppModule.HideLoading();
  }

  private showLoading() {
    this.openSaveOrDelete = true;
    AppModule.ShowLoading();
  }

  private hideLoading() {
    this.openSaveOrDelete = false;

    AppModule.HideLoading();
  }

  get propertiesErrors(): any {
    if (this.shipmentRequestSelection == "1") {
      return WorklistToBioHubItemModule.ErrorMessage;
    } else {
      return WorklistFromBioHubItemModule.ErrorMessage;
    }
  }

  async onSave(): Promise<void> {
    if (this.shipmentRequestSelection == "1") {
      WorklistToBioHubItemModule.CLEAR_WORKLISTTOBIOHUBITEM_CREATE();
      WorklistToBioHubItemModule.SET_WORKLISTTOBIOHUBITEM_CREATE_ISPAST(true);
      WorklistToBioHubItemModule.SET_WORKLISTTOBIOHUBITEM_CREATE_ASSIGNED_OPERATION_DATE(
        this.assignedOperationDate
      );
      this.showLoading();
      await WorklistToBioHubItemModule.CreateWorklistToBioHubItem()
        .then((response) => {
          this.hideLoading();
          const area = getAreaFromRoleType();
          this.$router.push({
            name: area + "-shipment-requests",
          });
        })
        .catch((err) => {
          console.log(err);
        });
    } else if (this.shipmentRequestSelection == "2") {
      WorklistFromBioHubItemModule.CLEAR_WORKLISTFROMBIOHUBITEM_CREATE();
      WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_CREATE_ISPAST(
        true
      );
      WorklistFromBioHubItemModule.SET_WORKLISTFROMBIOHUBITEM_CREATE_ASSIGNED_OPERATION_DATE(
        this.assignedOperationDate
      );
      this.showLoading();
      await WorklistFromBioHubItemModule.CreateWorklistFromBioHubItem()
        .then((response) => {
          this.hideLoading();
          const area = getAreaFromRoleType();
          this.$router.push({
            name: area + "-shipment-requests",
          });
        })
        .catch((err) => {
          console.log(err);
        });
    }
  }

  onCancel(): void {
    const area = getAreaFromRoleType();
    this.$router.push({
      name: area + "-shipment-requests",
    });
  }
}
</script>
