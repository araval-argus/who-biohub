<template>
  <div v-if="canCreate">
    <h2>New Shipment Request</h2>
    <v-container class="px-0" fluid>
      <v-radio-group v-model="shipmentRequestSelection">
        <v-radio key="1" label="Send BMEPP into the BioHub" value="1"></v-radio>
        <v-radio
          key="2"
          label="Receive BMEPP from the BioHub"
          value="2"
        ></v-radio>
      </v-radio-group>
      <CardActionsSaveCancel
        save-name="Continue"
        @save="onSave"
        @cancel="onCancel"
      >
      </CardActionsSaveCancel>
      <FormPopup
        ref="createShipmentPopup"
        v-model="newPopupItems"
        title="Select BioHub Facility"
        @executeSave="onSave"
      >
      </FormPopup>
    </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import CardActionsSaveCancel from "../../components/CardActionsSaveCancel.vue";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
import { hasPermission } from "../../utils/helper";
import { getAreaFromRoleType } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { WorklistToBioHubItemModule } from "../worklistToBioHubItems/store";
import { WorklistFromBioHubItemModule } from "../worklistFromBioHubItems/store";
import { BioHubFacilityModule } from "../biohubfacilities/store";
import { FormPopupItem } from "@/models/FormPopupItem";
import { InputType } from "@/models/enums/InputType";
import { createFormPopupItem } from "../../utils/helper";
import FormPopup from "../../components/FormPopup.vue";
import { DropdownItem } from "@/models/DropdownItem";

@Component({
  components: { CardActionsSaveCancel, FormPopup },
})
export default class ShipmentRequestsPageForm extends Vue {
  private shipmentRequestSelection = "1";
  private openSaveOrDelete = false;

  private newPopupItems: Array<FormPopupItem> = [];

  $refs!: {
    createShipmentPopup: FormPopup;
  };

  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanAccessRequestIniziation);
  }

  get bioHubFacilities(): Array<DropdownItem> {
    return BioHubFacilityModule.BioHubFacilities.map((l) => {
      return {
        Value: l.Id,
        Text: l.Name,
      };
    });
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

  async createNewShipmentRequest(): Promise<void> {
    this.newPopupItems = new Array<FormPopupItem>(
      createFormPopupItem(
        InputType.Dropdown,
        "",
        "BioHubFacilityId",
        true,
        false,
        false,
        undefined,
        this.bioHubFacilities
      )
    );
    this.$refs.createShipmentPopup.showPopup();
  }

  async onSave(selection: Array<FormPopupItem>): Promise<void> {
    if (this.shipmentRequestSelection == "1") {
      WorklistToBioHubItemModule.CLEAR_WORKLISTTOBIOHUBITEM_CREATE();
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

  // async mounted() {
  //   await BioHubFacilityModule.ListBioHubFacilities();
  // }
}
</script>
