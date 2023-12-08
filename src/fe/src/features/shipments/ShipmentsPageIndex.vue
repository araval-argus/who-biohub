<template>
  <div v-if="!onlyPublic">
    <div v-if="canRead">
      <ShipmentsTable
        :laboratory-id="laboratoryId"
        :loading="loading"
        :shipments="outgoingShipments"
        title="Into BioHub (SMTA 1)"
      >
      </ShipmentsTable>
      <ShipmentsTable
        :laboratory-id="laboratoryId"
        :loading="loading"
        :shipments="incomingShipments"
        title="From BioHub (SMTA 2)"
      >
      </ShipmentsTable>
    </div>
  </div>
  <div v-else>
    <span v-if="error"> Error retrieving Shipments: {{ error }} </span>
    <tempalte v-else>
      <ShipmentsPublicTable
        :laboratory-id="laboratoryId"
        :loading="loading"
        :shipments="outgoingPublicShipments"
        title="Into BioHub (SMTA 1)"
      >
      </ShipmentsPublicTable>
      <ShipmentsPublicTable
        :laboratory-id="laboratoryId"
        :loading="loading"
        :shipments="incomingPublicSShipments"
        title="From BioHub (SMTA 2)"
      >
      </ShipmentsPublicTable>
    </tempalte>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import ShipmentsTable from "./components/ShipmentsTable.vue";
import ShipmentsPublicTable from "./components/ShipmentsPublicTable.vue";
import { ShipmentModule } from "./store";
import { BioHubFacilityModule } from "../biohubfacilities/store";
import { LaboratoryModule } from "../laboratories/store";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
import { hasPermission, getAreaFromRouteName } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { AreaType } from "@/models/enums/AreaType";
import { Shipment } from "@/models/Shipment";
import { ShipmentPublic } from "@/models/ShipmentPublic";
import { ShipmentDirection } from "@/models/enums/ShipmentDirection";

@Component({ components: { ShipmentsTable, ShipmentsPublicTable } })
export default class ShipmentsPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadBioHubFacility);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateBioHubFacility);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditBioHubFacility);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteBioHubFacility);
  }

  get laboratoryId(): string {
    return this.$route.params.laboratoryId == undefined
      ? ""
      : this.$route.params.laboratoryId;
  }

  get RouteName() {
    return this.$route.name ?? "";
  }

  get onlyPublic(): boolean {
    return getAreaFromRouteName(this.RouteName) == AreaType.Public;
  }

  get incomingShipments(): Array<Shipment> {
    if (this.onlyPublic) {
      return [];
    } else {
      let shipments = ShipmentModule.Shipments.filter((s) => {
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
  }

  get outgoingShipments(): Array<Shipment> {
    if (this.onlyPublic) {
      return [];
    } else {
      let shipments = ShipmentModule.Shipments.filter((s) => {
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
  }

  get incomingPublicShipments(): Array<ShipmentPublic> {
    return ShipmentModule.ShipmentsPublic.filter((s) => {
      return s.ShipmentDirection == ShipmentDirection.FromBioHub;
    });
  }

  get outgoingPublicShipments(): Array<ShipmentPublic> {
    return ShipmentModule.ShipmentsPublic.filter((s) => {
      return s.ShipmentDirection == ShipmentDirection.ToBioHub;
    });
  }

  async loadPageInfo() {
    await ShipmentModule.ListShipments();
    await BioHubFacilityModule.ListBioHubFacilities();
    await LaboratoryModule.ListLaboratories();
  }

  async loadPublicPageInfo() {
    await BioHubFacilityModule.ListBioHubFacilitiesPublic();
    await LaboratoryModule.ListLaboratoriesPublic();
    await ShipmentModule.ListShipmentsPublic();
  }

  async mounted() {
    try {
      if (this.onlyPublic == true) {
        await this.loadPublicPageInfo();
      } else {
        await this.loadPageInfo();
      }
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
