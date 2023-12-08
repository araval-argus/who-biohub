<template>
  <div v-if="!onlyPublic">
    <div v-if="canRead">
      <v-container
        v-if="WorklistTimelines.length > 0"
        class="px-0 timeline-container"
        fluid
      >
        <template v-for="WorklistTimeline in WorklistTimelines">
          <WorklistTimelineComponent
            v-bind:key="WorklistTimeline"
            :timeline-title="WorklistTimeline.TimelineTitle"
            :worklist-timeline-events-days="WorklistTimeline.Events"
          >
          </WorklistTimelineComponent>
        </template>
      </v-container>
      <ShipmentForm v-model="model" :can-edit="CanEdit"> </ShipmentForm>
    </div>
  </div>
  <div v-else>
    <ShipmentFormPublic> </ShipmentFormPublic>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import ShipmentForm from "./components/ShipmentForm.vue";
import ShipmentFormPublic from "./components/ShipmentFormPublic.vue";
import { ShipmentModule } from "./store";
import { MaterialProductModule } from "../materialProducts/store";
import { BioHubFacilityModule } from "../biohubfacilities/store";
import { IsolationHostTypeModule } from "../isolationHostTypes/store";

import { LaboratoryModule } from "../laboratories/store";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
import { hasPermission, getAreaFromRouteName } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";
import { AreaType } from "@/models/enums/AreaType";
import { WorklistToBioHubItemModule } from "../worklistToBioHubItems/store";
import { WorklistFromBioHubItemModule } from "../worklistFromBioHubItems/store";
import WorklistTimelineComponent from "@/components/WorklistTimelineComponent.vue";
import { WorklistTimeline } from "@/models/WorklistTimeline";
import { Shipment } from "@/models/Shipment";
import { TransportModeModule } from "../transportModes/store";
import { TransportCategoryModule } from "../transportCategories/store";

@Component({
  components: {
    ShipmentForm,
    ShipmentFormPublic,
    WorklistTimelineComponent,
  },
})
export default class ShipmentsPageDetails extends Vue {
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

  get RouteName() {
    return this.$route.name ?? "";
  }

  get onlyPublic(): boolean {
    return getAreaFromRouteName(this.RouteName) == AreaType.Public;
  }

  get model(): Shipment | undefined {
    return ShipmentModule.Shipment;
  }

  get CanEdit(): boolean {
    return this.model?.CanEditReferenceNumber ?? false;
  }

  async loadPageInfo() {
    await BioHubFacilityModule.ListBioHubFacilities();
    await LaboratoryModule.ListLaboratories();
    await IsolationHostTypeModule.ListIsolationHostTypes();
    await MaterialProductModule.ListMaterialProducts();
    await TransportModeModule.ListTransportModes();
    await TransportCategoryModule.ListTransportCategories();
    await ShipmentModule.ReadShipment(this.$route.params.id);
  }

  async loadPublicPageInfo() {
    await BioHubFacilityModule.ListBioHubFacilitiesPublic();
    await LaboratoryModule.ListLaboratoriesPublic();
    await IsolationHostTypeModule.ListIsolationHostTypesPublic();
    await MaterialProductModule.ListMaterialProductsPublic();
    await ShipmentModule.ReadShipmentPublic(this.$route.params.id);
  }

  async mounted() {
    AppModule.ShowLoading();
    try {
      if (this.onlyPublic == true) {
        ShipmentModule.CLEAR_SHIPMENT_PUBLIC();
        await this.loadPublicPageInfo();
      } else {
        ShipmentModule.CLEAR_SHIPMENT();
        await this.loadPageInfo();
      }
    } finally {
      AppModule.HideLoading();
    }
  }

  get WorklistToBioHubItemId(): string {
    return ShipmentModule.WorklistToBioHubItemId;
  }

  get WorklistFromBioHubItemId(): string {
    return ShipmentModule.WorklistFromBioHubItemId;
  }

  get error(): AppError | undefined {
    return ShipmentModule.Error;
  }

  get WorklistTimelines(): Array<WorklistTimeline> {
    if (this.WorklistToBioHubItemId != "") {
      return WorklistToBioHubItemModule.WorklistTimelines;
    } else if (this.WorklistFromBioHubItemId != "") {
      return WorklistFromBioHubItemModule.WorklistTimelines;
    } else {
      return [];
    }
  }

  @Watch("WorklistToBioHubItemId")
  worklistToBioHubItemId() {
    if (this.WorklistToBioHubItemId != "") {
      WorklistToBioHubItemModule.ListWorklistToBioHubItemEvents(
        this.WorklistToBioHubItemId
      );
    }
  }

  @Watch("WorklistFromBioHubItemId")
  worklistFromBioHubItemId() {
    if (this.WorklistFromBioHubItemId != "") {
      WorklistFromBioHubItemModule.ListWorklistFromBioHubItemEvents(
        this.WorklistFromBioHubItemId
      );
    }
  }
}
</script>
