<template>
  <div>
    <span v-if="error"> Error retrieving Shipments: {{ error }} </span>
    <tempalte v-else>
      <ShipmentsPublicTable
        :laboratory-id="laboratoryId"
        :loading="loading"
        :shipments="outgoingShipments"
        title="Into BioHub (SMTA 1)"
      >
      </ShipmentsPublicTable>
      <ShipmentsPublicTable
        :laboratory-id="laboratoryId"
        :loading="loading"
        :shipments="incomingShipments"
        title="From BioHub (SMTA 2)"
      >
      </ShipmentsPublicTable>
    </tempalte>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import ShipmentsPublicTable from "./components/ShipmentsPublicTable.vue";
import { ShipmentModule } from "./store";
import { BioHubFacilityModule } from "../biohubfacilities/store";
import { LaboratoryModule } from "../laboratories/store";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
import { AreaType } from "@/models/enums/AreaType";
import { ShipmentPublic } from "@/models/ShipmentPublic";
import { ShipmentDirection } from "@/models/enums/ShipmentDirection";

@Component({ components: { ShipmentsPublicTable } })
export default class ShipmentsPublicPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get laboratoryId(): string {
    return this.$route.params.laboratoryId == undefined
      ? ""
      : this.$route.params.laboratoryId;
  }

  get RouteName() {
    return this.$route.name ?? "";
  }

  get incomingShipments(): Array<ShipmentPublic> {
    let shipments = ShipmentModule.ShipmentsPublic.filter((s) => {
      return s.ShipmentDirection == ShipmentDirection.FromBioHub;
    });
    if (this.laboratoryId != "") {
      shipments = shipments.filter((s) => {
        return (
          s.LaboratoryId == this.laboratoryId ||
          s.BioHubFacilityId == this.laboratoryId
        );
      });
    }
    return shipments;
  }

  get outgoingShipments(): Array<ShipmentPublic> {
    let shipments = ShipmentModule.ShipmentsPublic.filter((s) => {
      return s.ShipmentDirection == ShipmentDirection.ToBioHub;
    });
    if (this.laboratoryId != "") {
      shipments = shipments.filter((s) => {
        return (
          s.LaboratoryId == this.laboratoryId ||
          s.BioHubFacilityId == this.laboratoryId
        );
      });
    }
    return shipments;
  }

  async loadPageInfo() {
    await BioHubFacilityModule.ListBioHubFacilitiesPublic();
    await LaboratoryModule.ListLaboratoriesPublic();
    await ShipmentModule.ListShipmentsPublic();
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    AppModule.HideLoading();
  }

  get error(): AppError | undefined {
    return ShipmentModule.Error;
  }
}
</script>
