<template>
  <div>
    <ShipmentFormPublic> </ShipmentFormPublic>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import ShipmentFormPublic from "./components/ShipmentFormPublic.vue";
import { ShipmentModule } from "./store";
import { MaterialProductModule } from "../materialProducts/store";
import { BioHubFacilityModule } from "../biohubfacilities/store";
import { IsolationHostTypeModule } from "../isolationHostTypes/store";
import { LaboratoryModule } from "../laboratories/store";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";

@Component({ components: { ShipmentFormPublic } })
export default class ShipmentsPublicPageDetails extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get RouteName() {
    return this.$route.name ?? "";
  }

  async loadPageInfo() {
    await BioHubFacilityModule.ListBioHubFacilitiesPublic();
    await LaboratoryModule.ListLaboratoriesPublic();
    await IsolationHostTypeModule.ListIsolationHostTypesPublic();
    await MaterialProductModule.ListMaterialProductsPublic();
    await ShipmentModule.ReadShipmentPublic(this.$route.params.id);
  }

  async mounted() {
    AppModule.ShowLoading();
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }

  get error(): AppError | undefined {
    return ShipmentModule.Error;
  }
}
</script>
